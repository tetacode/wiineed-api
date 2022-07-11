using Data.Entity;

namespace Service.Model.DataInput;

public class CategoryCreateEdit
{
    public string Name { get; set; }
    public Media Image { get; set; }
    public List<Guid> Products { get; set; }
}