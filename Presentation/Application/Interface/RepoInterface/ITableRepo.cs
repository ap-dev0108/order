using OrderManagement.Domain.Entities;

namespace OrderManagement.Application.Interface;

public interface ITableRepo
{
    //Read Operation
    Task<List<RestaurantTable>> GetRestaurantTablesAsync();
    Task<RestaurantTable> GetRestaurantTableByIdAsync(Guid id);

    //Write Operation
    Task AddRestaurantTable(RestaurantTable restaurantTable);

    //Remove Operation
    Task RemoveTable(RestaurantTable restaurantTable);
}