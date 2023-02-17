using AutoMapper;
using baseNetApi.config;
using baseNetApi.models;
using baseNetApi.service.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace baseNetApi.Controllers;

[Route("api/groups")]
// [Authorize]
[ApiController]
public class GroupController : ControllerBase
{
    private IGroupService _groupService;
    private IMapper _mapper;
    private readonly AppSettings _appSettings;
    
    public GroupController(
        IGroupService groupService,
        IMapper mapper,
        IOptions<AppSettings> appSettings)
    {
        _groupService = groupService;
        _mapper = mapper;
    }
    
    //[Authorize]
    [HttpGet("list")]
    public IActionResult? GetAll()
    {
        var groups = _groupService.GetAll();
        return Ok(groups);
    }
    
    //[Authorize]
    [HttpGet("detail/{id}")]
    public IActionResult? GetById(int id)
    {
        var group = _groupService.GetById(id);
        return Ok(group);
    }
}