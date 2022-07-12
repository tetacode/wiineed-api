using Data.Entity;
using Data.StaticRepository;

namespace Service.Model.DataInput;

public class BussinessEdit
{
    public LocaleInput Name { get; set; }
    public LocaleInput Description { get; set; }
}