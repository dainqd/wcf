using AutoMapper;
using baseNetApi.config;
using baseNetApi.models.products;
using baseNetApi.service.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace baseNetApi.Controllers.admin;

[Route("admin/api/contacts")]
[Authorize]
[ApiController]
public class AdminContactController : ControllerBase
{
    private IContactService _contactService;
    private IMapper _mapper;
    private readonly AppSettings _appSettings;
    
    public AdminContactController(
        IContactService contactService,
        IMapper mapper,
        IOptions<AppSettings> appSettings)
    {
        _contactService = contactService;
        _mapper = mapper;
    }
    
    [HttpGet("list")]
    public IActionResult GetAll()
    {
        var contacts = _contactService.GetAll();
        return Ok(contacts);
    }
    
    [HttpGet("detail/{id}")]
    public IActionResult GetById(int id)
    {
        var contact = _contactService.GetById(id);
        return Ok(contact);
    }
    
    [HttpPost]
    public IActionResult Create(CreateContactRequest model)
    {
        _contactService.Create(model);
        return Ok(new { message = "Contact created" });
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, UpdateContactRequest model)
    {
        _contactService.Update(id, model);
        return Ok(new { message = "Contact updated" });
    }
    
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _contactService.Delete(id);
        return Ok(new { message = "Contact deleted" });
    }
}