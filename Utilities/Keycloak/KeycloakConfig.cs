namespace Utilities.Keycloak;


public enum KeycloakConfigEnum
{
    ClientSecret,
    UsernamePassword
}
public class KeycloakConfig
{
    public KeycloakConfigEnum AuthType { get; set; } = KeycloakConfigEnum.ClientSecret;
    public string TokenEndpoint { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string ClientId { get; set; }
    
    public string ClientSecret { get; set; }
}