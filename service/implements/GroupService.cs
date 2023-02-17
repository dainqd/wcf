using AutoMapper;
using baseNetApi.context;
using baseNetApi.models;
using baseNetApi.service.interfaces;

namespace baseNetApi.service;

public class GroupService : IGroupService
{
    private MySQLDBContext _context;
    private readonly IMapper _mapper;
    
    public GroupService(
        MySQLDBContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    public IEnumerable<Groups> GetAll()
    {
        return _context.Groups;
    }

    public Groups GetById(int id)
    {
        return getGroups(id);
    }

    public void Update(int id, string name)
    {
        var groups = getGroups(id);
        if (name == null)
        {
            throw new KeyNotFoundException("Group name valid");
        }
        groups.group_name = name;
        groups.UpdatedAt = DateTimeOffset.Now.AddHours(7);
        _context.Groups.Update(groups);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var groups = getGroups(id);
        groups.DeletedAt = DateTimeOffset.Now.AddHours(7);
        _context.Groups.Remove(groups);
        _context.SaveChanges();
    }

    public void Create(string name)
    {
        if (name == null)
        {
            throw new KeyNotFoundException("Group name valid");
        }
        var group = new Groups();
        group.group_name = name;
        group.CreatedAt = DateTimeOffset.Now.AddHours(7);
        _context.Groups.Add(group);
        _context.SaveChanges();
    }
    //
    private Groups getGroups(int id)
    {
        var groups = _context.Groups.Find(id);
        if (groups == null)
        {
            throw new KeyNotFoundException("Group not found");
        }
        return groups;
    }
}