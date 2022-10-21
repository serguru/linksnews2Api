using linksnews2API.Models;

namespace linksnews2API.Services;

public interface IAccountService
{
    Task<List<Account>> GetAll();
    Task<Account> GetById([System.Runtime.InteropServices.Optional] string id);
    Task<Account> Add(Account newAccount);
    Task<Account> Update(Account accountToUpdate);
    Task Delete(string id);
}