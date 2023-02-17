using baseNetApi.models;
using baseNetApi.models.user;

namespace baseNetApi.service.interfaces;

public interface IUserService
{
    IEnumerable<User> GetAll();
    IEnumerable<User> GetAllByStatus(UserStatus status);
    User GetById(int id);
    User GetByIdAndStatus(int id);
    void ChangPass(int id, ChangePasswordRequest model);
    void UpdateInfo(int id, UpdateRequest model);
    void Delete(int id);
    void Create(CreateRequest model);
}