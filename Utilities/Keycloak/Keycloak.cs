using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Utilities.Helpers;
namespace Utilities.Keycloak;

public class Keycloak
{
    private KeycloakConfig _config;
    private KeycloakAuthResponse? _authResponse;
    
    public Keycloak(KeycloakConfig config)
    {
        _config = config;
    }

    public KeycloakAuthResponse GetAuthResponse(bool refresh)
    {
        if (_authResponse == null || refresh)
        {
            _authResponse = Auth();
        }

        return _authResponse;
    }
    
    private AuthenticationHeaderValue GetAuthHeader(bool refresh)
    {
        return new AuthenticationHeaderValue("Bearer", GetAuthResponse(refresh).access_token);
    }

    public HttpResponseMessage KeycloakAuthMiddleware(HttpMethod method, string requestUri, Func<HttpClient, HttpRequestMessage, HttpResponseMessage> send)
    {
        var resp = HttpClientHelper.Send(method, requestUri, (client, message) =>
        {
            message.Headers.Authorization = GetAuthHeader(false);
            return send(client, message);
        });

        if (resp.StatusCode == HttpStatusCode.Unauthorized)
        {
            return HttpClientHelper.Send(method, requestUri, (client, message) =>
            {
                message.Headers.Authorization = GetAuthHeader(true);
                return send(client, message);
            });
        }

        return resp;
    }

    private KeycloakAuthResponse? Auth()
    {
        
        var authParams = new Dictionary<string, string>();

        if (_config.AuthType == KeycloakConfigEnum.ClientSecret)
        {
            authParams.Add("client_secret", _config.ClientSecret);
            authParams.Add("client_id", _config.ClientId);
            authParams.Add("grant_type", "client_credentials");
        }
        else if (_config.AuthType == KeycloakConfigEnum.UsernamePassword)
        {
            authParams.Add("username", _config.Username);
            authParams.Add("password", _config.Password);
            authParams.Add("client_id", _config.ClientId);
            authParams.Add("grant_type", "password");   
        }

        var response = HttpClientHelper.Send(HttpMethod.Post, _config.TokenEndpoint,
            (httpClient, message) =>
            {
                message.Content = new FormUrlEncodedContent(authParams);
                return httpClient.Send(message);
            });

        if (response.StatusCode == HttpStatusCode.OK)
        {
            return response.Content.ReadFromJsonAsync<KeycloakAuthResponse>().GetAwaiter().GetResult();
        }

        Console.WriteLine($"Keycloak:Auth:{response.Content.ReadAsStringAsync().GetAwaiter().GetResult()}");
        throw new Exception($"Keycloak:Auth:{response.Content.ReadAsStringAsync().GetAwaiter().GetResult()}");
    }
}