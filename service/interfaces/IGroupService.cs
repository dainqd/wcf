using baseNetApi.models;

namespace baseNetApi.service.interfaces;

public interface IGroupService
{
    IEnumerable<Groups> GetAll();
    Groups GetById(int id);
    void Update(int id, string name);
    void Delete(int id);
    void Create(string name);
}