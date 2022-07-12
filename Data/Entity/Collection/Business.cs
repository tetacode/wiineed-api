using Data.StaticRepository;

namespace Data.Entity.Collection;


public class Business : MongoDbEntity
{
    public Business()
    {
        Enabled = true;
        Name = new Locale();
        Description = new Locale();
        Address = new Address();
        SocialMedia = new SocialMedia();
        Logo = new Media();
        BusinessSettings = new BusinessSettings();
        Menus = new List<Menu>();
        Gallery = new List<Media>();
        WorkingHours = new List<WorkingHour>();
        Geolocation = new Geolocation();
        LocationOptions = new List<LocationOptionEnum>();
    }
    
    public Boolean Enabled { get; set; }
    public Locale Name { get; set; }
    public Locale Description { get; set; }
    public Address Address { get; set; }
    public SocialMedia SocialMedia { get; set; }
    public Media Logo { get; set; }
    public BusinessSettings BusinessSettings { get; set; }
    public List<Menu> Menus { get; set; }
    public List<Media> Gallery { get; set; }
    
    public Geolocation Geolocation { get; set; }
    
    public List<LocationOptionEnum> LocationOptions { get; set; }

    public List<WorkingHour> WorkingHours { get; set; }

}