using Api.Controllers.BaseController;
using Api.Controllers.Dto;
using Api.Controllers.Dto.Mapper;
using Core.Service;
using Core.Service.Result;
using Data.Entity;
using Microsoft.AspNetCore.Mvc;
using Service.Service.Abstract;

namespace Api.Controllers;

public class UserController : BaseApiController
{
    private readonly IUserService _userService;
    private readonly User _user;

    public UserController(IUserService userService, User user)
    {
        _userService = userService;
        _user = user;
    }

    [HttpGet]
    [Produces(typeof(DataResult<UserDto>))]
    public IActionResult User()
    {
        return CreateResult(_user.DataResult().As<User, UserDto>());
    }

    [HttpGet]
    [Produces(typeof(DataResult<UserDto>))]
    [Route("{userId}")]
    public IActionResult User(Guid userId)
    {
        return CreateResult(_userService.GetUser(userId).As<User, UserDto>());
    }
}