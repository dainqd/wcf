using AutoMapper;
using baseNetApi.config;
using baseNetApi.models;
using baseNetApi.models.user;
using baseNetApi.service.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace baseNetApi.Controllers;

[Route("api/user")]
// [Authorize]
[ApiController]
public class UserController : ControllerBase
{
    private IUserService _userService;
    private IMapper _mapper;
    private readonly AppSettings _appSettings;
    
    public UserController(
        IUserService userService,
        IMapper mapper,
        IOptions<AppSettings> appSettings)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [Authorize]
    [HttpGet("list")]
    public IActionResult GetAll()
    {
        var users = _userService.GetAllByStatus(UserStatus.ACTIVE);
        return Ok(users);
    }

    [Authorize]
    [HttpGet("detail/{id}")]
    public IActionResult GetById(int id)
    {
        var user = _userService.GetByIdAndStatus(id);
        return Ok(user);
    }
    
    
    [Authorize]
    [HttpPut("change-pass/{id}")]
    public IActionResult ChangPass(int id, ChangePasswordRequest model)
    {
        _userService.ChangPass(id, model);
        return Ok(new { message = "Change Password Success!" });
    }

    [Authorize]
    [HttpPut("update-info/{id}")]
    public IActionResult Update(int id, UpdateRequest model)
    {
        _userService.UpdateInfo(id, model);
        return Ok(new { message = "User updated" });
    }
}