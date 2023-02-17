using AutoMapper;
using baseNetApi.config;
using baseNetApi.models;
using baseNetApi.service.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace baseNetApi.Controllers;

[Route("api/contacts")]
// [Authorize]
[ApiController]
public class ContactController : ControllerBase
{
    private IContactService _contactService;
    private IGroupService _groupService;
    private IMapper _mapper;
    private readonly AppSettings _appSettings;
    
    public ContactController(
        IContactService contactService,
        IGroupService groupService,
        IMapper mapper,
        IOptions<AppSettings> appSettings)
    {
        _contactService = contactService;
        _groupService = groupService;
        _mapper = mapper;
    }
    
    //[Authorize]
    [HttpGet("list")]
    public IActionResult? GetAll()
    {
        var contacts = _contactService.GetAllByStatus(ContactStatus.ACTIVE);
        return Ok(contacts);
    }
    
    //[Authorize]
    [HttpGet("detail/{id}")]
    public IActionResult? GetById(int id)
    {
        var contact = _contactService.GetByIdAndStatus(id);
        return Ok(contact);
    }
}