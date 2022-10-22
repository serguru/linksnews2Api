using linksnews2API.Models;

namespace linksnews2API.Services;

public interface IUserService
{
    Task<bool> CheckLogin(string name, string password);
}