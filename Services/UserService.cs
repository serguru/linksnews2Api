using System.Runtime.InteropServices;
using linksnews2API.Models;
using Microsoft.Azure.Cosmos;

namespace linksnews2API.Services;
public class UserService : IUserService
{
    private readonly Container _container;

    public UserService(
        CosmosClient cosmosClient,
        string databaseName,
        string containerName
        )
    {
        _container = cosmosClient.GetContainer(databaseName, containerName);
    }

    public async Task<bool> CheckLogin(string name, string password)
    {
        var sql = "select * from a";
        var query = _container.GetItemQueryIterator<Login>(new QueryDefinition(sql));

        List<Login> logins = new List<Login>();
        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync();
            logins.AddRange(response);
        }

        return logins.Find(x => x.Name == name && x.Password == password) != null;
    }

}