using Api.Controllers.BaseController;
using Core.Service;
using Core.Service.Result;
using Data.Entity;
using Microsoft.AspNetCore.Mvc;
using Service.Model.DataInput;
using Service.Service.Abstract;

namespace Api.Controllers;

public class BusinessController : BaseApiController
{
    private readonly IBusinessService _businessService;

    public BusinessController(IBusinessService businessService)
    {
        _businessService = businessService;
    }

    [HttpGet]
    [Produces(typeof(DataListResult<Menu>))]
    public IActionResult Menus()
    {
        return CreateResult(_businessService.GetMenuList());
    }

    [HttpGet("{menuId}")]
    [Produces(typeof(DataResult<Menu>))]
    public IActionResult Menus(Guid menuId)
    {
        return CreateResult(_businessService.GetMenu(menuId));
    }
    
    [HttpPost("create")]
    [Produces(typeof(DataResult<Guid>))]
    public IActionResult Menus([FromBody] MenuCreateEdit data)
    {
        return CreateResult(_businessService.CreateMenu(data));
    }
    
    [HttpPut("{menuId}/edit")]
    [Produces(typeof(DataResult<Guid>))]
    public IActionResult Menus(Guid menuId, [FromBody] MenuCreateEdit data)
    {
        _businessService.EditMenu(menuId, data);
        return CreateResult(menuId.DataResult());
    }
    
    [HttpGet("/api/Business/Menus/{menuId}/categories")]
    [Produces(typeof(DataListResult<Menu>))]
    public IActionResult Categories(Guid menuId)
    {
        return CreateResult(_businessService.GetCategoryList(menuId));
    }

    [HttpGet("/api/Business/Menus/{menuId}/categories/{categoryId}")]
    [Produces(typeof(DataResult<Menu>))]
    public IActionResult Categories(Guid menuId, Guid categoryId)
    {
        return CreateResult(_businessService.GetCategory(menuId, categoryId));
    }

    [HttpPost("/api/Business/Menus/{menuId}/categories/create")]
    [Produces(typeof(DataResult<Guid>))]
    public IActionResult Categories(Guid menuId, [FromBody] CategoryCreateEdit data)
    {
        return CreateResult(_businessService.CreateCategory(menuId, data));
    }

    [HttpPut("/api/Business/Menus/{menuId}/categories/{categoryId}/edit")]
    [Produces(typeof(DataResult<Guid>))]
    public IActionResult Categories(Guid menuId, Guid categoryId, [FromBody] CategoryCreateEdit data)
    {
        _businessService.EditCategory(menuId, categoryId, data);
        return CreateResult();
    }
}