using System.Runtime.InteropServices;
using linksnews2API.Models;
using Microsoft.Azure.Cosmos;

namespace linksnews2API.Services;
public class AccountService : IAccountService
{
    private readonly Container _container;
    private readonly string _accountId;
    public AccountService(
        CosmosClient cosmosClient,
        string databaseName,
        string containerName,
        string accountId
        )
    {
        _container = cosmosClient.GetContainer(databaseName, containerName);
        _accountId = accountId;
    }

    public async Task<List<Account>> GetAll()
    {
        var sql = "select * from Account as a";
        var query = _container.GetItemQueryIterator<Account>(new QueryDefinition(sql));

        List<Account> result = new List<Account>();
        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync();
            result.AddRange(response);
        }

        return result;
    }
    public async Task<Account> GetById([Optional] string id)
    {
        id = id == null ? _accountId : id;
        ItemResponse<Account> response = await _container.ReadItemAsync<Account>(id, new PartitionKey(id));

        return response.Resource;
    }

    public async Task<Account> Add(Account newAccount)
    {
        var item = await _container.CreateItemAsync<Account>(newAccount, new PartitionKey(newAccount.Id));
        return item;
    }

    public async Task<Account> Update(Account accountToUpdate)
    {
        var item = await _container.UpsertItemAsync<Account>(accountToUpdate, new PartitionKey(accountToUpdate.Id));
        return item;
    }

    public async Task Delete(string id)
    {
        await _container.DeleteItemAsync<Account>(id, new PartitionKey(id));
    }

    public async Task<bool> CheckLogin(string name, string password)
    {
        var sql = "select * from User as u";
        var query = _container.GetItemQueryIterator<Login>(new QueryDefinition(sql));

        List<Login> logins = new List<Login>();
        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync();
            logins.AddRange(response);
        }

        return logins.Find(x => x.Name == name && x.Password == password) != null;
    }


    //private

}