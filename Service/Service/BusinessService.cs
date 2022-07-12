using System.Linq.Expressions;
using System.Reflection.Metadata;
using Core.Repository;
using Core.Service;
using Core.Service.Result;
using Data.Entity;
using Data.Entity.Collection;
using Data.Repository.Abstract;
using Data.StaticRepository;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Service.Model.DataInput;
using Service.Service.Abstract;

namespace Service.Service;

public class BusinessService : IBusinessService
{
    private readonly IBusinessRepository _businessRepository;
    private readonly User _user;
    private readonly Business _business;

    public BusinessService(IBusinessRepository businessRepository, User user, Business business)
    {
        _businessRepository = businessRepository;
        _user = user;
        _business = business;
    }

    public void EditBusiness(BussinessEdit data)
    {
        var business = _businessRepository
            .Query()
            .FirstOrDefault(x => x.Id == _user.BusinessId);

        business.Name = data.Name.ToLocale(_business.BusinessSettings.DefaultLanguageCode);
        business.Address = data.Address;
        business.Gallery = data.Gallery;
        business.Geolocation = data.Geolocation;
        business.Logo = data.Logo;
        business.LocationOptions = data.LocationOptions;
        business.SocialMedia = data.SocialMedia;
        business.WorkingHours = data.WorkingHours;

        _businessRepository.Update(business);
    }

    public DataResult<Guid> CreateMenu(MenuCreateEdit data)
    {
        var menu = new Menu();
        menu.Name = data.Name;
        menu.Enabled = data.Enabled;

        var update = Builders<Business>.Update.AddToSet(x => x.Menus, menu);

        _businessRepository
            .Collection()
            .UpdateOne(x => x.Id == _user.BusinessId, update);

        return menu.Id.DataResult();
    }

    public void EditMenu(Guid id, MenuCreateEdit data)
    {
        var update = Builders<Business>.Update
            .Set(x => x.Menus[-1].Name, data.Name)
            .Set(x => x.Menus[-1].Enabled, data.Enabled);

        var builder = Builders<Business>.Filter;
        var filter = builder.Eq(x => x.Id, _user.BusinessId)
                     & builder.ElemMatch(x => x.Menus, x => x.Id == id);

        _businessRepository
            .Collection()
            .UpdateOne(filter, update);
    }

    public DataListResult<Menu> GetMenuList()
    {
        var fieldsBuilder = Builders<Business>.Projection;
        var fields = fieldsBuilder
            .Exclude(x => x.Menus[-1].Categories);

        return _businessRepository
            .Collection()
            .Find(x => x.Id == _user.BusinessId)
            .Project(fields)
            .Project(x => x.Menus)
            .FirstOrDefault()
            .DataListResult();
    }

    public DataResult<Menu> GetMenu(Guid id)
    {
        var builder = Builders<Business>.Filter;
        var filter = builder.Eq(x => x.Id, _user.BusinessId)
                     & builder.ElemMatch(x => x.Menus, x => x.Id == id);
        
        
        var fieldsBuilder = Builders<Business>.Projection;
        var fields = fieldsBuilder
            .Exclude(ProjectionBuilder.Field<Business>(x => x.Menus[-1].Categories))
            .Exclude(ProjectionBuilder.Field<Business>(x => x.Menus[-1].Name));
        

        return _businessRepository
            .Collection()
            .Find(filter)
            .Project<Business>(fields)
            .FirstOrDefault()
            .Menus.FirstOrDefault()
            .DataResult();
    }

    public DataResult<Guid> CreateCategory(Guid menuId, CategoryCreateEdit data)
    {
        var category = new Category();
        category.Image = data.Image ?? new Media();
        category.Name = data.Name.ToLocale(_business.BusinessSettings.DefaultLanguageCode);
        category.Description = data.Description.ToLocale(_business.BusinessSettings.DefaultLanguageCode);
        category.Products = data.Products;

        var builder = Builders<Business>.Filter;
        var filter = builder.Eq(x => x.Id, _user.BusinessId)
                     & builder.ElemMatch(x => x.Menus, x => x.Id == menuId);

        var update = Builders<Business>.Update
            .AddToSet(x => x.Menus[-1].Categories, category);

        _businessRepository
            .Collection()
            .UpdateOne(filter, update);

        return category.Id.DataResult();
    }

    public void EditCategory(Guid menuId, Guid categoryId, CategoryCreateEdit data)
    {
        var builder = Builders<Business>.Filter;
        var filter = builder.Eq(x => x.Id, _user.BusinessId)
                     & builder.ElemMatch(x => x.Menus, x => x.Id == menuId)
                     & builder.ElemMatch(x => x.Menus, Builders<Menu>.Filter.ElemMatch(y => y.Categories, z => z.Id == categoryId));

        var update = Builders<Business>.Update
            .Set<Locale>(x => x.Menus[0].Categories[-1].Name, data.Name.ToLocale(_business.BusinessSettings.DefaultLanguageCode))
            .Set(x => x.Menus[0].Categories[-1].Image, data.Image)
            .Set(x => x.Menus[0].Categories[-1].Products, data.Products);

        _businessRepository
            .Collection()
            .UpdateOne(filter, update);
    }

    public DataResult<Category> GetCategory(Guid menuId, Guid categoryId)
    {
        var builder = Builders<Business>.Filter;
        var filter = builder.Eq(x => x.Id, _user.BusinessId)
                     & builder.ElemMatch(x => x.Menus, x => x.Id == menuId)
                     & builder.ElemMatch(x => x.Menus, Builders<Menu>.Filter.ElemMatch(y => y.Categories, z => z.Id == categoryId));

        var find = _businessRepository
            .Collection()
            .Find(filter);

        return find
            .Project(x => x.Menus.FirstOrDefault().Categories.FirstOrDefault())
            .FirstOrDefault()
            .DataResult();
    }

    public DataListResult<Category> GetCategoryList(Guid menuId)
    {
        var builder = Builders<Business>.Filter;
        var filter = builder.Eq(x => x.Id, _user.BusinessId)
                     & builder.ElemMatch(x => x.Menus, x => x.Id == menuId);

        return _businessRepository
            .Collection()
            .Find(filter)
            .Project(x => x.Menus.FirstOrDefault().Categories)
            .FirstOrDefault()
            .DataListResult();
    }
}