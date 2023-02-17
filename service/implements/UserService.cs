using AutoMapper;
using baseNetApi.context;
using baseNetApi.models;
using baseNetApi.models.user;
using baseNetApi.service.interfaces;

using baseNetApi.config;

namespace baseNetApi.service.user;

public class UserService : IUserService
{
    
    private MySQLDBContext _context;
    private readonly IMapper _mapper;
    
    public UserService(
        MySQLDBContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public IEnumerable<User> GetAll()
    {
        return _context.User;
    }

    public IEnumerable<User> GetAllByStatus(UserStatus status)
    {
        status = UserStatus.ACTIVE;
        return _context.User.Where(v => v.status == status).ToList();
    }

    public User GetById(int id)
    {
        return getUser(id);
    }

    public User GetByIdAndStatus(int id)
    {
        return getUserByIdAndStatus(id);
    }

    public void ChangPass(int id, ChangePasswordRequest model)
    {
        var user = getUser(id);
        
        if(model.password.Length<6 || model.oldPassword.Length<6)
            throw new AppException("Password invalid!");

        if (model.password != model.confirmPassword)
            throw new AppException("Password or Password Confirm incorrect!");

        
        if (!BCrypt.Net.BCrypt.Verify(model.oldPassword, user.password))
            throw new AppException("Old password is incorrect!");
        
        // hash password if it was entered
        if (!string.IsNullOrEmpty(model.password))
            user.password = BCrypt.Net.BCrypt.HashPassword(model.password);

        user.UpdatedAt = DateTimeOffset.Now.AddHours(7);
        user.UpdatedBy = id;

        // copy model to user and save
        _context.User.Update(user);
        _context.SaveChanges();
    }

    public void UpdateInfo(int id, UpdateRequest model)
    {
        var user = getUser(id);
        
        if(model.username == null)
            throw new AppException("Username invalid!");
        user.UpdatedAt = DateTimeOffset.Now.AddHours(7);
        user.UpdatedBy = id;
        // copy model to user and save
        _mapper.Map(model, user);
        _context.User.Update(user);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var user = getUser(id);
        user.status = UserStatus.DELETED;
        user.DeletedAt = DateTimeOffset.Now.AddHours(7);
        _context.User.Update(user);
        _context.SaveChanges();
    }

    public void Create(CreateRequest model)
    {
        // validate
        if (_context.User.Any(x => x.username == model.username))
            throw new AppException("User with the username '" + model.username + "' already exists");

        if (model.username == null || model.password == null)
            throw new AppException("Username or Password invalid!");

        if(model.password.Length<6)
            throw new AppException("Password invalid!");
        
        if (model.password != model.confirmPassword)
            throw new AppException("Password or Password Confirm incorrect!");

        // map model to new user object
        var user = _mapper.Map<User>(model);

        // hash password
        user.password = BCrypt.Net.BCrypt.HashPassword(model.password);

        user.CreatedAt = DateTimeOffset.Now.AddHours(7);
        // save user
        _context.User.Add(user);
        _context.SaveChanges();
    }
    
    private User getUser(int id)
    {
        var user = _context.User.Find(id);
        if (user == null) 
            throw new KeyNotFoundException("User not found");
        return user;
    }
    
    private User getUserByIdAndStatus(int id)
    {
        var user = _context.User.Find(id);
        if (user == null) 
            throw new KeyNotFoundException("User not found");
        if (user.status != UserStatus.ACTIVE)
            throw new KeyNotFoundException("User not found");
        return user;
    }
}