using AutoMapper;
using baseNetApi.config;
using baseNetApi.models.Auth;
using baseNetApi.service.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace baseNetApi.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private IAuthService _authService;
    private IMapper _mapper;
    private readonly AppSettings _appSettings;
    
    public AuthController(
        IAuthService authService,
        IMapper mapper,
        IOptions<AppSettings> appSettings)
    {
        _authService = authService;
        _mapper = mapper;
    }
    
    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult Authenticate(AuthenticateRequest model)
    {
        var response = _authService.Authenticate(model);
        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public IActionResult Register(RegisterRequest model)
    {
        _authService.Register(model);
        return Ok(new { message = "Registration successful" });
    }
}