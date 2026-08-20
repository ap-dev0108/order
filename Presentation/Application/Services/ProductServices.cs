using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OrderManagement.Application.DTO.Products;
using OrderManagement.Application.Interface;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Application.Services;

public class ProductServices
{
    private readonly IProductRepo _productRepo;
    private readonly IDataRepo _dataRepo;

    public ProductServices(IProductRepo productRepo, IDataRepo dataRepo)
    {
        _productRepo = productRepo;
        _dataRepo = dataRepo;
    }

    public async Task<List<ProductDTO>> GetProductsAsync()
    {
        var ingredientsList = await _productRepo.GetIngredientsAsync() ??
            throw new KeyNotFoundException("Ingredients list not found");

        return ingredientsList.Select(i => new ProductDTO
        {
            Id = i.Id,
            Title = i.Title,
            QualityOnHand = i.QualityOnHand,
            ReorderThreshold = i.ReorderThreshold,
            Units = i.Units,
            UpdatedAt = i.UpdatedAt
        }).ToList();
    }

    public async Task<ProductDTO> GetProductsById(Guid id)
    {
        var productList = await _productRepo.GetIngredientById(id) ??
            throw new KeyNotFoundException($"Ingredient with ID: {id} cannot be found");

        return new ProductDTO
        {
            Id = productList.Id,
            Title = productList.Title,
            QualityOnHand = productList.QualityOnHand,
            ReorderThreshold = productList.ReorderThreshold,
            Units = productList.Units,
            UpdatedAt = productList.UpdatedAt
        };
    }

    public async Task AddIngredients(AddProductsDTO addProductsDTO)
    {
        var newProductData = new Ingredient
        {
            Title = addProductsDTO.Title,
            QualityOnHand = addProductsDTO.QualityOnHand,
            ReorderThreshold = addProductsDTO.ReorderThreshold,
            Units = addProductsDTO.Units,
            UpdatedAt = addProductsDTO.UpdatedAt
        };

        await _productRepo.AddIngredients(newProductData);
        await _dataRepo.SaveChangesAsync();
    }

    public async Task EditIngredients(EditProductDTO editProductDTO, Guid id)
    {
        var product = await _productRepo.GetIngredientById(id) ??
            throw new KeyNotFoundException("Product with ID cannot be found");

        product.Title = editProductDTO.Title;
        product.QualityOnHand = editProductDTO.QualityOnHand;
        product.ReorderThreshold = editProductDTO.ReorderThreshold;
        product.Units = editProductDTO.Units;
        product.UpdatedAt = editProductDTO.LastUpdated;

        await _dataRepo.SaveChangesAsync();
    }

    public async Task RemoveIngredients(Guid id)
    {
        var productToRemove = await _productRepo.GetIngredientById(id) ??
            throw new KeyNotFoundException($"Product with ID: {id} cannot be found");

        await _productRepo.RemoveIngredients(productToRemove);
        await _dataRepo.SaveChangesAsync();
    }
}