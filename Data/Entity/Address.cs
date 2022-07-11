namespace Data.Entity;

public class Address
{
    public Address()
    {
        Phone = new Phone();
    }

    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string State { get; set; }
    public string City { get; set; }
    public string ZipCode { get; set; }
    public string Country { get; set; }
    public string Email { get; set; }
    public string Website { get; set; }
    public Phone Phone { get; set; }
}