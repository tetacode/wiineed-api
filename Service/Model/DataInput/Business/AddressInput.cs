using Data.Entity;

namespace Service.Model.DataInput.Business;

public class AddressInput
{
    public string AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string State { get; set; }
    public string City { get; set; }
    public string ZipCode { get; set; }
    public string Country { get; set; }
    public string? Email { get; set; }
    public Phone Phone { get; set; }
}