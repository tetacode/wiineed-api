using Data.Entity.Collection;

namespace Api.Controllers.Dto;

public class UserDto
{
    public Guid Key { get; set; }
    public string Name { get; set; }
    public string Lastname { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public UserSettingsDto UserSettings { get; set; }
    public BusinessDto Business { get; set; }
}