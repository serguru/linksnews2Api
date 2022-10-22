using System.Runtime.InteropServices;
using linksnews2API.Models;
using Microsoft.Azure.Cosmos;

namespace linksnews2API.Services;
public class AccountService : IAccountService
{
    private readonly Container _container;

    public AccountService(
        CosmosClient cosmosClient,
        string databaseName,
        string containerName
        )
    {
        _container = cosmosClient.GetContainer(databaseName, containerName);
    }

    public async Task<List<Account>> GetAll()
    {
        var sql = "select * from a";
        var query = _container.GetItemQueryIterator<Account>(new QueryDefinition(sql));

        List<Account> result = new List<Account>();
        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync();
            result.AddRange(response);
        }

        return result;
    }
    public async Task<Account> GetById(string id)
    {
        ItemResponse<Account> response = await _container.ReadItemAsync<Account>(id, new PartitionKey(id));
        return response.Resource;
    }

    public async Task<Account> GetByName(string name)
    {
        if (name == null || name.Trim() == string.Empty)
        {
            return null;
        }

        var sql = $"select * from a where a.name = '{name}'";
        var query = _container.GetItemQueryIterator<Account>(new QueryDefinition(sql));

        List<Account> result = new List<Account>();
        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync();
            result.AddRange(response);
        }

        if (result.Count == 0) 
        {
            return null;
        }

        return result.First<Account>();
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

  

}