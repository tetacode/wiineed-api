using Core.Service.Result;
using Data.Entity;
using Service.Model.DataInput;

namespace Service.Service.Abstract;

public interface IBusinessService
{
    public void SetName(Locale name);
    public void SetDescription(Locale name);
    public void EditBusiness(BussinessEdit data);
    public DataResult<Guid> CreateMenu(MenuCreateEdit data);
    public void EditMenu(Guid id, MenuCreateEdit data);
    public DataListResult<Menu> GetMenuList();
    public DataResult<Menu> GetMenu(Guid id);
}