using Microsoft.AspNetCore.Http;
using OrderManagement.Application.DTO.Products;
using OrderManagement.Application.Interface;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Application.Services;

public class ProductTransactionServices
{
    private readonly IProductTransaction _transaction;
    private readonly IProductRepo _ingredients;
    private readonly IDataRepo _data;

    public ProductTransactionServices(IProductTransaction transaction, IDataRepo data, IProductRepo ingredients)
    {
        _transaction = transaction;
        _ingredients = ingredients;
        _data = data;
    }

    public async Task<List<InventoryTransactionDTO>> GetInventoryTransactionAsync()
    {
        var transactionList = await _transaction.GetInventoryTransactionsAsync() ??
            throw new KeyNotFoundException("Transaction not found");

        return transactionList.Select(s => new InventoryTransactionDTO
        {
            InventoryTransactionID = s.Id,
            IngredientId = s.IngredientId,
            Ingredients = s.Ingredients,
            ChargeAmount = s.ChargeAmount,
            CreatedAt = s.CreatedAt
        }).ToList();
    }

    public async Task<InventoryTransactionDTO> GetTransactionById(Guid transactionID)
    {
        var transaction = await _transaction.GetTransactionAsync(transactionID) ??
            throw new KeyNotFoundException($"Transaction Details with the ID: {transactionID} cannot be found");

        return new InventoryTransactionDTO
        {
            InventoryTransactionID = transaction.Id,
            IngredientId = transaction.IngredientId,
            Ingredients = transaction.Ingredients,
            ChargeAmount = transaction.ChargeAmount,
            CreatedAt = transaction.CreatedAt
        };
    }

    public async Task AddInventoryTransactions(AddInventoryTransaction addInventoryTransaction, Guid ingredientsID)
    {
        var transactionToAdd = new InventoryTransaction
        {
            IngredientId = ingredientsID,
            Ingredients = addInventoryTransaction.Ingredients,
            ChargeAmount = addInventoryTransaction.ChargeAmount,
            CreatedAt = DateTime.UtcNow
        };

        var checkIngredientsExists = await _ingredients.GetIngredientById(ingredientsID) ??
            throw new KeyNotFoundException($"Ingredients with this ID:{ingredientsID} cannot be found");

        if (checkIngredientsExists.QualityOnHand <= 0)
            throw new BadHttpRequestException("Ingredients is not enough to perform this transactions");

        await _transaction.AddInventoryTransaction(transactionToAdd);
        await _data.SaveChangesAsync();
    }
}