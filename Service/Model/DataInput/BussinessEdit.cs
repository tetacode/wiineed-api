using Data.Entity;
using Data.StaticRepository;

namespace Service.Model.DataInput;

public class BussinessEdit
{
    public LocaleInput Name { get; set; }
    public Media Logo { get; set; }
    public List<Media> Gallery { get; set; }
    public Address Address { get; set; }
    public SocialMedia SocialMedia { get; set; }
    public Geolocation Geolocation { get; set; }
    public List<LocationOptionEnum> LocationOptions { get; set; }
    public List<WorkingHour> WorkingHours { get; set; }
}