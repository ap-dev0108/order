namespace OrderManagement.Application.DTO.Products;

public class EditProductDTO
{
    public AddProductsDTO addProductsDTO {get; set;}
    public DateTime LastUpdated {get; set;} = DateTime.UtcNow;
    public string Notes {get; set;} = string.Empty;
}