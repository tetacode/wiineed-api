using Data.StaticRepository;

namespace Data.Entity.Collection;

public class Product : MongoDbEntity
{
    public Product()
    {
        Name = new Locale();
        Description = new Locale();
        Allergens = new List<AllergenEnum>();
        Diets = new List<DietEnum>();
        Price = 0;
        Enabled = true;
    }

    public Locale Name { get; set; }
    public Locale Description { get; set; }
    public List<AllergenEnum> Allergens { get; set; }
    public List<DietEnum> Diets { get; set; }
    public decimal Price { get; set; }
    
    public Guid BusinessId { get; set; }
    public Boolean Enabled { get; set; }
}