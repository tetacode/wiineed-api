using Core.Service;
using Core.Service.Result;
using Data.Entity;
using Data.Entity.Collection;
using Data.Repository.Abstract;
using MongoDB.Driver;
using Service.Model.DataInput;
using Service.Service.Abstract;

namespace Service.Service;

public class BusinessService : IBusinessService
{
    private readonly IBusinessRepository _businessRepository;
    private readonly User _user;

    public BusinessService(IBusinessRepository businessRepository, User user)
    {
        _businessRepository = businessRepository;
        _user = user;
    }
    
    public void SetName(Locale name)
    {
        var update = Builders<Business>.Update.Set(x => x.Name, name);
        var res = _businessRepository
            .Collection()
            .UpdateOne(x => x.Id == _user.BusinessId, update);
    }
    
    public void SetDescription(Locale name)
    {
        var update = Builders<Business>.Update.Set(x => x.Description, name);
        var res = _businessRepository
            .Collection()
            .UpdateOne(x => x.Id == _user.BusinessId, update);
    }

    public void EditBusiness(BussinessEdit data)
    {
        var business = _businessRepository
            .Query()
            .FirstOrDefault(x => x.Id == _user.BusinessId);

        business.Address = data.Address;
        business.Gallery = data.Gallery;
        business.Geolocation = data.Geolocation;
        business.Logo = data.Logo;
        business.BusinessSettings = data.BusinessSettings;
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
        return _businessRepository
            .Collection()
            .Find(x => x.Id == _user.BusinessId)
            .Project(x => x.Menus)
            .FirstOrDefault()
            .DataListResult();
    }

    public DataResult<Menu> GetMenu(Guid id)
    {
        var builder = Builders<Business>.Filter;
        var filter = builder.Eq(x => x.Id, _user.BusinessId)
                     & builder.ElemMatch(x => x.Menus, x => x.Id == id);

        return _businessRepository
            .Collection()
            .Find(filter)
            .Project(x => x.Menus.FirstOrDefault())
            .FirstOrDefault()
            .DataResult();
    }
}