using AutoMapper;
using baseNetApi.config;
using baseNetApi.context;
using baseNetApi.models;
using baseNetApi.models.products;
using baseNetApi.service.interfaces;

namespace baseNetApi.service;

public class ContactService : IContactService
{
    private MySQLDBContext _context;
    private readonly IMapper _mapper;
    
    public ContactService(
        MySQLDBContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public IEnumerable<Contacts> GetAll()
    {
        return _context.Contacts;
    }

    public IEnumerable<Contacts> GetAllByStatus(ContactStatus status)
    {
        status = ContactStatus.ACTIVE;
        return _context.Contacts.Where(v => v.status == status).ToList();
    }

    public Contacts GetById(int id)
    {
        return getContacts(id);
    }

    public ContactResponse GetByIdAndStatus(int id)
    {
        return getContactsByIdAndStatus(id);
    }

    public void Update(int id, UpdateContactRequest model)
    {
        var contacts = getContacts(id);
        
        if(model.name == null)
            throw new AppException("Name invalid!");
        contacts.UpdatedAt = DateTimeOffset.Now.AddHours(7);
        var groups = _context.Groups.Find(model.group_id);
        if (groups == null)
        {
            throw new KeyNotFoundException("Group not found");
        }
        _mapper.Map(model, contacts);
        _context.Contacts.Update(contacts);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var contacts = getContacts(id);
        contacts.status = ContactStatus.DELETED;
        contacts.DeletedAt = DateTimeOffset.Now.AddHours(7);
        _context.Contacts.Update(contacts);
        _context.SaveChanges();
    }

    public void Create(CreateContactRequest model)
    {
        var contacts = _mapper.Map<Contacts>(model);

        if (model.name == null)
        {
            throw new AppException("Name invalid!");
        }
        var groups = _context.Groups.Find(model.group_id);
        if (groups == null)
        {
            throw new KeyNotFoundException("Group not found");
        }
        contacts.CreatedAt = DateTimeOffset.Now.AddHours(7);

        _context.Contacts.Add(contacts);
        _context.SaveChanges();
    }
    
    private Contacts getContacts(int id)
    {
        var contacts = _context.Contacts.Find(id);
        if (contacts == null) 
            throw new KeyNotFoundException("Contact not found");
        return contacts;
    }
    
    private ContactResponse getContactsByIdAndStatus(int id)
    {
        var contacts = _context.Contacts.Find(id);
        if (contacts == null) 
            throw new KeyNotFoundException("Contacts not found");
        if (contacts.status != ContactStatus.ACTIVE)
            throw new KeyNotFoundException("Contacts not found");
        var group = _context.Contacts.Find(contacts.group_id);
        if (group == null)
        {
            throw new KeyNotFoundException("Contacts not found"); 
        }
        // chịu khó hard đoạn này vì không map được!
        var response = new ContactResponse();
        response.id = contacts.id;
        response.name = contacts.name;
        response.number = contacts.number;
        response.description = contacts.description;
        response.avt = contacts.avt;
        response.status = contacts.status.ToString();
        response.contact_name = group.name;
        return response;
    }
}