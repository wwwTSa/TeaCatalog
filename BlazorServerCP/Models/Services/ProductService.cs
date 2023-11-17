using BlazorServerCP.Models;
using System.Collections.Generic;

public class ProductService
{
    private readonly JsonFileProductService _jsonFileProductService;

    public ProductService(JsonFileProductService jsonFileProductService)
    {
        _jsonFileProductService = jsonFileProductService;
    }

    public List<Product> GetProducts()
    {
        return _jsonFileProductService.GetProducts();
    }

    public List<Category> GetCategories()
    {
        return _jsonFileProductService.GetCategories();
    }

    public void AddProduct(Product product)
    {
        _jsonFileProductService.AddProduct(product);
    }

    public void DeleteProduct(Product product)
    {
        _jsonFileProductService.DeleteProduct(product.Id);
    }

    public void UpdateProduct(Product product)
    {
        _jsonFileProductService.UpdateProduct(product);
    }
}