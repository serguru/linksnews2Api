using linksnews2API.Models;

namespace linksnews2API.Services;

public interface IUserService
{

    string? CurrentUser { get; set; }

    Task<bool> CheckLogin(string name, string password);
}