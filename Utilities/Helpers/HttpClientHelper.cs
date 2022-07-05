namespace Utilities.Helpers;

public class HttpClientHelper
{
    public static HttpResponseMessage Send(HttpMethod method, string requestUri, Func< HttpClient, HttpRequestMessage, HttpResponseMessage> send)
    {
        HttpClientHandler clientHandler = new HttpClientHandler();
        clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

        using (var client = new HttpClient(clientHandler))
        {
            HttpRequestMessage req = new HttpRequestMessage();
            req.Method = method;
            req.RequestUri = new Uri(requestUri);

            HttpResponseMessage resp = send(client, req);
            return resp;
        }
    }
}