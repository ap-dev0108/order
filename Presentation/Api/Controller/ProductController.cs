using Microsoft.AspNetCore.Mvc;
using OrderManagement.Application.DTO;
using OrderManagement.Application.DTO.Products;
using OrderManagement.Application.Services;

namespace OrderManagement.Api.Controller;

public class ProductController : ControllerBase
{
    private readonly ProductServices _product;

    public ProductController(ProductServices product)
    {
        _product = product;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetProductsList()
    {
        var productList = await _product.GetProductsAsync();

        return Ok(new Response<List<ProductDTO>>
        {
            Success = true,
            Message = "Product List fetched",
            Data = productList
        });
    }

    [HttpGet("byId")]
    public async Task<IActionResult> GetProductsById(Guid id)
    {
        var productToFetch = await _product.GetProductsById(id);

        return Ok(new Response<ProductDTO>
        {
            Success = true,
            Message = "Product by ID fetched",
            Data = productToFetch
        });
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddProducts([FromBody] AddProductsDTO addProductsDTO)
    {
        await _product.AddIngredients(addProductsDTO);

        return Ok(new Response<AddProductsDTO>
        {
            Success = true,
            Message = "Products have been added",
            Data = addProductsDTO
        });
    }

    [HttpPut("edit")]
    public async Task<IActionResult> EditProducts([FromBody] EditProductDTO editProductDTO, Guid productID)
    {
        await _product.EditIngredients(editProductDTO, productID);

        return Ok(new Response<EditProductDTO>
        {
            Success = true,
            Message = "Product edited",
            Data = editProductDTO
        });
    }

    [HttpDelete("remove")]
    public async Task<IActionResult> RemoveProducts(Guid id)
    {
        var productToRemove = await _product.GetProductsById(id);
        await _product.RemoveIngredients(id);

        return Ok(new Response<ProductDTO>
        {
            Success = true,
            Message = "Product Removed",
            Data = productToRemove
        });
    }
}