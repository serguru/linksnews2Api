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

    // public async Task<Account> Add(Account newAccount)
    // {
    //     var item = await _container.CreateItemAsync<Account>(newAccount, new PartitionKey(newAccount.Id));
    //     return item;
    // }

    public async Task<Account> Update(Account accountToUpdate)
    {
        CheckIds(accountToUpdate);
        var item = await _container.UpsertItemAsync<Account>(accountToUpdate, new PartitionKey(accountToUpdate.Id));
        return item;
    }

    // public async Task Delete(string id)
    // {
    //     await _container.DeleteItemAsync<Account>(id, new PartitionKey(id));
    // }

    public void CheckIds(Account account)
    {
        if (account.Pages == null)
        {
            return;
        }
        account.Pages.ForEach((Page page) =>
        {
            if (page == null)
            {
                return;
            }

            if (page.Id == null || page.Id.Trim() == string.Empty)
            {
                page.Id = Guid.NewGuid().ToString();
            }

            if (page.Rows == null)
            {
                return;
            }

            page.Rows.ForEach((Row row) =>
            {
                if (row == null)
                {
                    return;
                }

                if (row.Id == null || row.Id.Trim() == string.Empty)
                {
                    row.Id = Guid.NewGuid().ToString();
                }

                if (row.Columns == null)
                {
                    return;
                }

                row.Columns.ForEach((Column column) =>
                {
                    if (column == null)
                    {
                        return;
                    }

                    if (column.Id == null || column.Id.Trim() == string.Empty)
                    {
                        column.Id = Guid.NewGuid().ToString();
                    }

                    if (column.Links == null)
                    {
                        return;
                    }


                    column.Links.ForEach((Link link) =>
                    {
                        if (link == null)
                        {
                            return;
                        }

                        if (link.Id == null || link.Id.Trim() == string.Empty)
                        {
                            link.Id = Guid.NewGuid().ToString();
                        }

                    });
                });
            });
        });
    }
}