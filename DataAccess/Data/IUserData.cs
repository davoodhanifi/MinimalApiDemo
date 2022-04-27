using DataAccess.Models;

namespace DataAccess.Data;

public interface IUserData
{
    Task<IEnumerable<UserModel>> GetAll();

    Task<UserModel> GetUser(int id);

    Task Insert(UserModel user);

    Task Update(UserModel user);

    Task Delete(int id);
}