using Core.Service.Result;
using Data.Entity;
using Data.Entity.Collection;
using Data.StaticRepository;
using Service.Model.DataInput;
using Service.Model.DataInput.Business;
using Service.Model.Output;

namespace Service.Service.Abstract;

public interface IBusinessService
{

    public DataResult<Business> GetBusiness();
    public void EditBusiness(BussinessEdit data);
    public DataResult<Address> GetBusinessAddress();
    public void EditBusinessAddress(AddressInput data);
    public DataResult<Geolocation> GetBusinessGeolocation();
    public void EditBusinessGeolocation(GeolocationInput data);
    public DataListResult<LocationOptionEnum> GetBusinessLocationOptions();
    public void EditBusinessLocationOptions(LocationOptionInput data);
    public DataResult<Media> GetBusinessLogo();
    public void EditBusinessLogo(FileUploadInput data);
    public DataListResult<WorkingHour> GetBusinessWorkingHours();
    public void EditBusinessWorkingHours(WorkingHoursInput data);
    public DataResult<SocialMedia> GetBusinessSocialMedia();
    public void EditBusinessSocialMedia(SocialMediaInput data);
    public DataListResult<Media> GetBusinessGallery();
    public void UploadMediaBusinessGallery(FileUploadInput data);
    public void DeleteMediaFromBusinessGallery(Guid mediaKey);
    public DataResult<Guid> CreateMenu(MenuCreateEdit data);
    public void EditMenu(Guid key, MenuCreateEdit data);
    public DataListResult<Menu> GetMenuList();
    public DataResult<Menu> GetMenu(Guid key);
    public DataResult<Guid> CreateCategory(Guid menuId, CategoryCreateEdit data);
    public void EditCategory(Guid menuId, Guid categoryId, CategoryCreateEdit data);
    public DataResult<Category> GetCategory(Guid menuId, Guid categoryId);
    public DataListResult<Category> GetCategoryList(Guid menuId);
}