using AutoMapper;
using baseNetApi.config;
using baseNetApi.service.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace baseNetApi.Controllers.admin;

[Route("admin/api/groups")]
[Authorize]
[ApiController]
public class AdminGroupController : ControllerBase
{
    private IGroupService _groupService;
    private IMapper _mapper;
    private readonly AppSettings _appSettings;
    
    public AdminGroupController(
        IGroupService groupService,
        IMapper mapper,
        IOptions<AppSettings> appSettings)
    {
        _groupService = groupService;
        _mapper = mapper;
    }
    
    [HttpGet("list")]
    public IActionResult GetAll()
    {
        var groups = _groupService.GetAll();
        return Ok(groups);
    }
    
    [HttpGet("detail/{id}")]
    public IActionResult GetById(int id)
    {
        var group = _groupService.GetById(id);
        return Ok(group);
    }
    
    [HttpPost]
    public IActionResult Create(string name)
    {
        _groupService.Create(name);
        return Ok(new { message = "Group created" });
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, string name)
    {
        _groupService.Update(id, name);
        return Ok(new { message = "Group updated" });
    }
    
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _groupService.Delete(id);
        return Ok(new { message = "Group deleted" });
    }
}