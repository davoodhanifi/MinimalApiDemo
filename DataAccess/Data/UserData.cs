
using DataAccess.DbAccess;
using DataAccess.Models;

namespace DataAccess.Data;

public class UserData
{
    private readonly ISqlDataAccess _dbAccess;

    public UserData(ISqlDataAccess dbAccess)
    {
        _dbAccess = dbAccess;
    }

    public async Task<IEnumerable<UserModel>> GetAll() => await _dbAccess.LoadData<UserModel, dynamic>("spUser_GetAll", new { });

    public async Task<UserModel> GetUser(int id)
    {
        var users = await _dbAccess.LoadData<UserModel, dynamic>("spUser_Get", new { Id = id });
        return users.FirstOrDefault();
    }

    public async Task Insert(UserModel user) => await _dbAccess.SaveData("spUser_Insert", new { FirstName = user.FirstName, LastName = user.LastName });

    public async Task Update(UserModel user) => await _dbAccess.SaveData("spUser_Update", user);

    public async Task Delete(int id) => await _dbAccess.SaveData("spUser_Delete", id);
}
