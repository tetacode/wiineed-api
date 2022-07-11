using Api.Controllers.BaseController;
using Api.Controllers.Dto;
using Api.Controllers.Dto.Mapper;
using Core.Service;
using Core.Service.Result;
using Data.Entity;
using Data.Entity.Collection;
using Data.StaticRepository;
using Microsoft.AspNetCore.Mvc;
using Service.Service.Abstract;

namespace Api.Controllers;

public class UserController : BaseApiController
{
    private readonly IUserService _userService;
    private readonly IStorageService _storageService;
    private readonly User _user;

    public UserController(IUserService userService, User user, IStorageService storageService)
    {
        _userService = userService;
        _user = user;
        _storageService = storageService;
    }

    [HttpGet]
    [Produces(typeof(DataResult<UserDto>))]
    public IActionResult Information()
    {
        return CreateResult(_user.DataResult().As<User, UserDto>());
    }

    [HttpPut]
    [Produces(typeof(DataResult<int>))]
    public IActionResult ChangeLanguage([FromBody] LanguageCodeEnum languageCode)
    {
        _userService.ChangeLanguage(_user.Id, languageCode);
        return CreateResult();
    }

    [HttpPost]
    [Produces(typeof(DataResult<Media>))]
    public IActionResult UploadFile(IFormFile file)
    {
        return CreateResult(_storageService.UploadMedia(file.OpenReadStream(), "user", file.FileName));
    }
}