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
}