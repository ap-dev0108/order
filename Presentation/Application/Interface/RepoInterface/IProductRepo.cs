using OrderManagement.Domain.Entities;

namespace OrderManagement.Application.Interface;

public interface IProductRepo
{
    //Read Operation
    Task<List<Ingredient>> GetIngredientsAsync();
    Task<Ingredient> GetIngredientById(Guid id);

    //Write Operation
    Task AddIngredients(Ingredient ingredient);

    //Remove Operation
    Task RemoveIngredients(Ingredient ingredient);
}