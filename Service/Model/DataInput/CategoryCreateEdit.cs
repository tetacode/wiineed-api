using Data.Entity;

namespace Service.Model.DataInput;

public class CategoryCreateEdit
{
    public LocaleInput Name { get; set; }
    public LocaleInput Description { get; set; }
    public Media? Image { get; set; }
    public List<Guid> Products { get; set; }
}