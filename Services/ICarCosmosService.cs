using linksnews2API.Models;

namespace linksnews2API.Services;

public interface ICarCosmosService
{
    Task<List<Car>> Get(string sqlCosmosQuery);
    Task<Car> AddAsync(Car newCar);
    Task<Car> Update(Car carToUpdate);
    Task Delete(string id, string make);
}