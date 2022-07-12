using Core.Service.Result;
using Data.Entity;
using Service.Model.DataInput;

namespace Service.Service.Abstract;

public interface IBusinessService
{
    public void EditBusiness(BussinessEdit data);
    public DataResult<Guid> CreateMenu(MenuCreateEdit data);
    public void EditMenu(Guid id, MenuCreateEdit data);
    public DataListResult<Menu> GetMenuList();
    public DataResult<Menu> GetMenu(Guid id);
    public DataResult<Guid> CreateCategory(Guid menuId, CategoryCreateEdit data);
    public void EditCategory(Guid menuId, Guid categoryId, CategoryCreateEdit data);
    public DataResult<Category> GetCategory(Guid menuId, Guid categoryId);
    public DataListResult<Category> GetCategoryList(Guid menuId);
}