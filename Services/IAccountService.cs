using linksnews2API.Models;

namespace linksnews2API.Services;

public interface IAccountService
{
    Task<List<Account>> GetAll();
    Task<Account> GetById(string id);
    Task<Account> GetByName(string name);
    //    Task<Account> Add(Account newAccount);
    Task<Account> Update(Account accountToUpdate);
    // Task Delete(string id);
}