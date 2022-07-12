using Api.Controllers.BaseController;
using Api.Controllers.Dto;
using Api.Controllers.Dto.Mapper;
using Core.Service;
using Core.Service.Result;
using Data.Entity;
using Data.Entity.Collection;
using Data.StaticRepository;
using Microsoft.AspNetCore.Mvc;
using Service.Model.DataInput;
using Service.Model.DataInput.Business;
using Service.Service.Abstract;

namespace Api.Controllers;

public class BusinessController : BaseApiController
{
    private readonly IBusinessService _businessService;

    public BusinessController(IBusinessService businessService)
    {
        _businessService = businessService;
    }

    [HttpGet("/api/business")]
    [Produces(typeof(DataResult<BusinessDto>))]
    public IActionResult GetBusiness()
    {
        return CreateResult(_businessService.GetBusiness().As<Business, BusinessDto>());
    }

    [HttpPut("/api/business")]
    [Produces(typeof(DataResult<int>))]
    public IActionResult EditBusiness(BussinessEdit data)
    {
        _businessService.EditBusiness(data);
        return CreateResult();
    }
    
    [HttpGet("/api/business/address")]
    [Produces(typeof(DataResult<AddressDto>))]
    public IActionResult GetBusinessAddress()
    {
        return CreateResult(_businessService.GetBusinessAddress().As<Address, AddressDto>());
    }
    
    [HttpPut("/api/business/address")]
    [Produces(typeof(DataResult<int>))]
    public IActionResult EditBusinessAddress(AddressInput data)
    {
        _businessService.EditBusinessAddress(data);
        return CreateResult();
    }
    
    [HttpGet("/api/business/geolocation")]
    [Produces(typeof(DataResult<GeolocationDto>))]
    public IActionResult GetBusinessGeolocation()
    {
        return CreateResult(_businessService.GetBusinessGeolocation().As<Geolocation, GeolocationDto>());
    }
    
    [HttpPut("/api/business/geolocation")]
    [Produces(typeof(DataResult<int>))]
    public IActionResult EditBusinessGeolocation(GeolocationInput data)
    {
        _businessService.EditBusinessGeolocation(data);
        return CreateResult();
    }
    
    [HttpGet("/api/business/options")]
    [Produces(typeof(DataListResult<LocationOptionEnum>))]
    public IActionResult GetBusinessLocationOptions()
    {
        return CreateResult(_businessService.GetBusinessLocationOptions());
    }
    
    [HttpPut("/api/business/options")]
    [Produces(typeof(DataResult<int>))]
    public IActionResult EditBusinessLocationOptions(LocationOptionInput data)
    {
        _businessService.EditBusinessLocationOptions(data);
        return CreateResult();
    }
    
    [HttpGet("/api/business/logo")]
    [Produces(typeof(DataResult<MediaDto>))]
    public IActionResult GetBusinessLogo()
    {
        return CreateResult(_businessService.GetBusinessLogo().As<Media, MediaDto>());
    }
    
    [HttpPut("/api/business/logo")]
    [Produces(typeof(DataResult<int>))]
    public IActionResult EditBusinessLogo(IFormFile file)
    {
        _businessService.EditBusinessLogo(new FileUploadInput()
        {
            FileName = file.Name,
            FileStream = file.OpenReadStream()
        });
        return CreateResult();
    }
    
    [HttpGet("/api/business/hours")]
    [Produces(typeof(DataListResult<WorkingHourDto>))]
    public IActionResult GetBusinessWorkingHours()
    {
        return CreateResult(_businessService.GetBusinessWorkingHours().As<WorkingHour, WorkingHourDto>());
    }
    
    [HttpPut("/api/business/hours")]
    [Produces(typeof(DataResult<int>))]
    public IActionResult EditBusinessWorkingHours(WorkingHoursInput data)
    {
        _businessService.EditBusinessWorkingHours(data);
        return CreateResult();
    }
    
    [HttpGet("/api/business/social")]
    [Produces(typeof(DataResult<SocialMediaDto>))]
    public IActionResult GetBusinessSocialMedia()
    {
        return CreateResult(_businessService.GetBusinessSocialMedia().As<SocialMedia, SocialMediaDto>());
    }
    
    [HttpPut("/api/business/social")]
    [Produces(typeof(DataResult<int>))]
    public IActionResult EditBusinessSocialMedia(SocialMediaInput data)
    {
        _businessService.EditBusinessSocialMedia(data);
        return CreateResult();
    }
    
    [HttpGet("/api/business/gallery")]
    [Produces(typeof(DataListResult<MediaDto>))]
    public IActionResult GetBusinessGallery()
    {
        return CreateResult(_businessService.GetBusinessGallery().As<Media, MediaDto>());
    }
    
    [HttpPut("/api/business/gallery")]
    [Produces(typeof(DataResult<int>))]
    public IActionResult UploadMediaBusinessGallery(IFormFile file)
    {
        _businessService.UploadMediaBusinessGallery(new FileUploadInput()
        {
            FileName = file.Name,
            FileStream = file.OpenReadStream()
        });
        return CreateResult();
    }
    
    [HttpDelete("/api/business/gallery/{mediaKey}")]
    [Produces(typeof(DataResult<int>))]
    public IActionResult DeleteMediaFromBusinessGallery(Guid mediaKey)
    {
        _businessService.DeleteMediaFromBusinessGallery(mediaKey);
        return CreateResult();
    }

    [HttpGet]
    [Produces(typeof(DataListResult<MenuDto>))]
    public IActionResult Menus()
    {
        return CreateResult(_businessService.GetMenuList().As<Menu, MenuDto>());
    }

    [HttpGet("{menuId}")]
    [Produces(typeof(DataResult<MenuDto>))]
    public IActionResult Menus(Guid menuId)
    {
        return CreateResult(_businessService.GetMenu(menuId).As<Menu, MenuDto>());
    }
    
    [HttpPost("create")]
    [Produces(typeof(DataResult<Guid>))]
    public IActionResult Menus([FromBody] MenuCreateEdit data)
    {
        return CreateResult(_businessService.CreateMenu(data));
    }
    
    [HttpPut("{menuId}/edit")]
    [Produces(typeof(DataResult<int>))]
    public IActionResult Menus(Guid menuId, [FromBody] MenuCreateEdit data)
    {
        _businessService.EditMenu(menuId, data);
        return CreateResult();
    }
    
    [HttpGet("/api/Business/Menus/{menuId}/categories")]
    [Produces(typeof(DataListResult<CategoryDto>))]
    public IActionResult Categories(Guid menuId)
    {
        return CreateResult(_businessService.GetCategoryList(menuId).As<Category, CategoryDto>());
    }

    [HttpGet("/api/Business/Menus/{menuId}/categories/{categoryId}")]
    [Produces(typeof(DataResult<CategoryDto>))]
    public IActionResult Categories(Guid menuId, Guid categoryId)
    {
        return CreateResult(_businessService.GetCategory(menuId, categoryId).As<Category, CategoryDto>());
    }

    [HttpPost("/api/Business/Menus/{menuId}/categories/create")]
    [Produces(typeof(DataResult<Guid>))]
    public IActionResult Categories(Guid menuId, [FromBody] CategoryCreateEdit data)
    {
        return CreateResult(_businessService.CreateCategory(menuId, data));
    }

    [HttpPut("/api/Business/Menus/{menuId}/categories/{categoryId}/edit")]
    [Produces(typeof(DataResult<int>))]
    public IActionResult Categories(Guid menuId, Guid categoryId, [FromBody] CategoryCreateEdit data)
    {
        _businessService.EditCategory(menuId, categoryId, data);
        return CreateResult();
    }
}