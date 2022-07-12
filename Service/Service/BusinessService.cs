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
using Service.Model.DataInput.Business;
using Service.Model.Output;
using Service.Service.Abstract;

namespace Service.Service;

public class BusinessService : IBusinessService
{
    private readonly IStorageService _storageService;
    
    private readonly IBusinessRepository _businessRepository;
    private readonly User _user;
    private readonly LanguageCodeEnum _defLangCode;

    private string GetDirectory()
    {
        return $"business/{_user.BusinessKey}";
    }

    public DataResult<Business> GetBusiness()
    {
        return _businessRepository
            .Collection()
            .Find(x => x.Key == _user.BusinessKey)
            .Project(x => new Business()
            {
                Description = x.Description,
                Name = x.Name
            })
            .FirstOrDefault()
            .DataResult();
    }
    
    public BusinessService(IBusinessRepository businessRepository, User user, IStorageService storageService)
    {
        _businessRepository = businessRepository;
        _user = user;
        _storageService = storageService;
        _defLangCode = user.Business.BusinessSettings.DefaultLanguageCode;
    }

    public DataResult<Address> GetBusinessAddress()
    {
        return _businessRepository
            .Collection()
            .Find(x => x.Key == _user.BusinessKey)
            .Project(x => x.Address)
            .FirstOrDefault()
            .DataResult();
    }

    public void EditBusinessAddress(AddressInput data)
    {
        var update = Builders<Business>.Update
            .Set(x => x.Address, new Address()
            {
                City = data.City,
                Country = data.Country,
                Email = data.Email,
                Phone = data.Phone,
                State = data.State,
                AddressLine1 = data.AddressLine1,
                AddressLine2 = data.AddressLine2,
                ZipCode = data.ZipCode
            });

        _businessRepository.Collection()
            .UpdateOne(x => x.Key == _user.BusinessKey, update);
    }

    public DataResult<Geolocation> GetBusinessGeolocation()
    {
        return _businessRepository
            .Collection()
            .Find(x => x.Key == _user.BusinessKey)
            .Project(x => x.Geolocation)
            .FirstOrDefault()
            .DataResult();
    }

    public void EditBusinessGeolocation(GeolocationInput data)
    {
        var update = Builders<Business>.Update
            .Set(x => x.Geolocation, new Geolocation()
            {
                Latitude = data.Latitude,
                Longitude = data.Longitude
            });
        
        _businessRepository.Collection()
            .UpdateOne(x => x.Key == _user.BusinessKey, update);
    }

    public DataListResult<LocationOptionEnum> GetBusinessLocationOptions()
    {
        return _businessRepository
            .Collection()
            .Find(x => x.Key == _user.BusinessKey)
            .Project(x => x.LocationOptions)
            .FirstOrDefault()
            .DataListResult();
    }

    public void EditBusinessLocationOptions(LocationOptionInput data)
    {
        var update = Builders<Business>.Update
            .Set(x => x.LocationOptions, data.LocationOptions);
        
        _businessRepository.Collection()
            .UpdateOne(x => x.Key == _user.BusinessKey, update);
    }

    public DataResult<Media> GetBusinessLogo()
    {
        return _businessRepository
            .Collection()
            .Find(x => x.Key == _user.BusinessKey)
            .Project(x => x.Logo)
            .FirstOrDefault()
            .DataResult();
    }

    public void EditBusinessLogo(FileUploadInput data)
    {
        var media = _storageService.UploadMedia(data.FileStream, GetDirectory(), data.FileName);
        
        var update = Builders<Business>.Update
            .Set(x => x.Logo, media.Data);
        
        _businessRepository.Collection()
            .UpdateOne(x => x.Key == _user.BusinessKey, update);
    }

    public DataListResult<WorkingHour> GetBusinessWorkingHours()
    {
        return _businessRepository
            .Collection()
            .Find(x => x.Key == _user.BusinessKey)
            .Project(x => x.WorkingHours)
            .FirstOrDefault()
            .DataListResult();
    }

    public void EditBusinessWorkingHours(WorkingHoursInput data)
    {
        var update = Builders<Business>.Update
            .Set(x => x.WorkingHours, data.WorkingHours.Select(x => new WorkingHour()
            {
                Day = x.Day,
                ClosingTime = TimeOnly.FromDateTime(x.ClosingTime),
                OpeningTime = TimeOnly.FromDateTime(x.OpeningTime),
            }).OrderBy(x => x.Day).ToList());
        
        _businessRepository.Collection()
            .UpdateOne(x => x.Key == _user.BusinessKey, update);
    }

    public DataResult<SocialMedia> GetBusinessSocialMedia()
    {
        return _businessRepository
            .Collection()
            .Find(x => x.Key == _user.BusinessKey)
            .Project(x => x.SocialMedia)
            .FirstOrDefault()
            .DataResult();
    }

    public void EditBusinessSocialMedia(SocialMediaInput data)
    {
        var update = Builders<Business>.Update
            .Set(x => x.SocialMedia, new SocialMedia()
            {
                Website = data.Website,
                Facebook = data.Facebook,
                Instagram = data.Instagram,
                Twitter = data.Twitter
            });
        
        _businessRepository.Collection()
            .UpdateOne(x => x.Key == _user.BusinessKey, update);
    }

    public DataListResult<Media> GetBusinessGallery()
    {
        return _businessRepository
            .Collection()
            .Find(x => x.Key == _user.BusinessKey)
            .Project(x => x.Gallery)
            .FirstOrDefault()
            .DataListResult();
    }

    public void UploadMediaBusinessGallery(FileUploadInput data)
    {
        var media = _storageService.UploadMedia(data.FileStream, GetDirectory(), data.FileName);
        var update = Builders<Business>.Update
            .AddToSet(x => x.Gallery, media.Data);
        
        _businessRepository.Collection()
            .UpdateOne(x => x.Key == _user.BusinessKey, update);
    }

    public void DeleteMediaFromBusinessGallery(Guid mediaKey)
    {
        var pull = Builders<Business>.Update
            .PullFilter(x => x.Gallery, b => b.Key == mediaKey);

        _businessRepository.Collection()
            .UpdateOne(x => x.Key == _user.BusinessKey, pull);
    }

    public void EditBusiness(BussinessEdit data)
    {
        var update = Builders<Business>.Update
            .Set(x => x.Name, data.Name.ToLocale(_defLangCode))
            .Set(x => x.Description, data.Description.ToLocale(_defLangCode));

        _businessRepository
            .Collection()
            .UpdateOne(x => x.Key == _user.BusinessKey, update);
    }

    public DataResult<Guid> CreateMenu(MenuCreateEdit data)
    {
        var menu = new Menu();
        menu.Name = data.Name;
        menu.Enabled = data.Enabled;

        var update = Builders<Business>.Update.AddToSet(x => x.Menus, menu);

        _businessRepository
            .Collection()
            .UpdateOne(x => x.Key == _user.BusinessKey, update);

        return menu.Key.DataResult();
    }

    public void EditMenu(Guid key, MenuCreateEdit data)
    {
        var update = Builders<Business>.Update
            .Set(x => x.Menus[-1].Name, data.Name)
            .Set(x => x.Menus[-1].Enabled, data.Enabled);

        var builder = Builders<Business>.Filter;
        var filter = builder.Eq(x => x.Key, _user.BusinessKey)
                     & builder.ElemMatch(x => x.Menus, x => x.Key == key);

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
            .Find(x => x.Key == _user.BusinessKey)
            .Project(fields)
            .Project(x => x.Menus)
            .FirstOrDefault()
            .DataListResult();
    }

    public DataResult<Menu> GetMenu(Guid key)
    {
        var builder = Builders<Business>.Filter;
        var filter = builder.Eq(x => x.Key, _user.BusinessKey)
                     & builder.ElemMatch(x => x.Menus, x => x.Key == key);
        
        
        var fieldsBuilder = Builders<Business>.Projection;
        var fields = fieldsBuilder
            .Exclude(Fielder.Field<Business>(x => x.Menus[-1].Categories))
            .Exclude(Fielder.Field<Business>(x => x.Menus[-1].Name));
        

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
        category.Name = data.Name.ToLocale(_defLangCode);
        category.Description = data.Description.ToLocale(_defLangCode);
        category.Products = data.Products;

        var builder = Builders<Business>.Filter;
        var filter = builder.Eq(x => x.Key, _user.BusinessKey)
                     & builder.ElemMatch(x => x.Menus, x => x.Key == menuId);

        var update = Builders<Business>.Update
            .AddToSet(x => x.Menus[-1].Categories, category);

        _businessRepository
            .Collection()
            .UpdateOne(filter, update);

        return category.Key.DataResult();
    }

    public void EditCategory(Guid menuId, Guid categoryId, CategoryCreateEdit data)
    {
        var builder = Builders<Business>.Filter;
        var filter = builder.Eq(x => x.Key, _user.BusinessKey)
                     & builder.ElemMatch(x => x.Menus, x => x.Key == menuId)
                     & builder.ElemMatch(x => x.Menus, Builders<Menu>.Filter.ElemMatch(y => y.Categories, z => z.Key == categoryId));

        var update = Builders<Business>.Update
            .Set<Locale>(x => x.Menus[0].Categories[-1].Name, data.Name.ToLocale(_defLangCode))
            .Set(x => x.Menus[0].Categories[-1].Image, data.Image)
            .Set(x => x.Menus[0].Categories[-1].Products, data.Products);

        _businessRepository
            .Collection()
            .UpdateOne(filter, update);
    }

    public DataResult<Category> GetCategory(Guid menuId, Guid categoryId)
    {
        var builder = Builders<Business>.Filter;
        var filter = builder.Eq(x => x.Key, _user.BusinessKey)
                     & builder.ElemMatch(x => x.Menus, x => x.Key == menuId)
                     & builder.ElemMatch(x => x.Menus, Builders<Menu>.Filter.ElemMatch(y => y.Categories, z => z.Key == categoryId));

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
        var filter = builder.Eq(x => x.Key, _user.BusinessKey)
                     & builder.ElemMatch(x => x.Menus, x => x.Key == menuId);

        return _businessRepository
            .Collection()
            .Find(filter)
            .Project(x => x.Menus.FirstOrDefault().Categories)
            .FirstOrDefault()
            .DataListResult();
    }
}