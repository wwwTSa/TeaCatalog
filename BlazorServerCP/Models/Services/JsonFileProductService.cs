using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BlazorServerCP.Models;
using Newtonsoft.Json;

public class JsonFileProductService
{
    private readonly string _filePath = "wwwroot/products.json";
    private readonly string _categoryFilePath = "wwwroot/categories.json";

    public List<Product> GetProducts()
    {
        if (!File.Exists(_filePath))
        {
            File.WriteAllText(_filePath, "[]");
        }

        var json = File.ReadAllText(_filePath);
        return json != null ? JsonConvert.DeserializeObject<List<Product>>(json) ?? new List<Product>() : new List<Product>();
    }

    public List<Category> GetCategories()
    {
        if (!File.Exists(_categoryFilePath))
        {
            File.WriteAllText(_categoryFilePath, "[]");
        }

        var json = File.ReadAllText(_categoryFilePath);
        return json != null ? JsonConvert.DeserializeObject<List<Category>>(json) ?? new List<Category>() : new List<Category>();
    }

    public void AddProduct(Product product)
    {
        var products = GetProducts();
        if (product.CategoryId == null)
        {
            product.CategoryId = 1; // Substitua 1 pelo valor padrão desejado
        }

        product.Category = GetCategoryById(product.CategoryId.Value);

        product.Id = products.Count + 1;
        products.Add(product);
        SaveProducts(products);
    }

    public void DeleteProduct(int productId)
    {
        var products = GetProducts();
        var productToRemove = products.FirstOrDefault(p => p.Id == productId);

        if (productToRemove != null)
        {
            products.Remove(productToRemove);
            SaveProducts(products);

            // Adicione mensagens de depuração
            Console.WriteLine($"Product with ID {productId} deleted successfully from in-memory list.");

            // Certifique-se de que a lista foi realmente atualizada
            Console.WriteLine($"Remaining products in in-memory list: {string.Join(", ", products.Select(p => p.Name))}");

            // Verifique se o arquivo está no caminho correto
            Console.WriteLine($"File path: {_filePath}");

            // Abra o arquivo e exiba seu conteúdo para verificar se foi atualizado corretamente
            Console.WriteLine($"File content after deletion:\n{File.ReadAllText(_filePath)}");
        }
        else
        {
            // Adicione mensagens de depuração se o produto não for encontrado
            Console.WriteLine($"Product with ID {productId} not found.");
        }
    }

    public void UpdateProduct(Product product)
    {
        var products = GetProducts();
        var existingProduct = products.FirstOrDefault(p => p.Id == product.Id);

        if (existingProduct != null)
        {
            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.CategoryId = product.CategoryId;
            existingProduct.Category = GetCategoryById(product.CategoryId ?? 0); // Alteração feita aqui

            existingProduct.ImageURL = product.ImageURL;
            existingProduct.Price = product.Price;

            SaveProducts(products);
        }
    }
    private void SaveProducts(List<Product> products)
    {
        var json = JsonConvert.SerializeObject(products, Formatting.Indented);
        File.WriteAllText(_filePath, json);
    }

    public void SaveCategories(List<Category> categories)
    {
        var json = JsonConvert.SerializeObject(categories, Formatting.Indented);
        File.WriteAllText(_categoryFilePath, json);
    }

    public Category GetCategoryById(int categoryId)
    {
        var categories = GetCategories();
        return categories.FirstOrDefault(c => c.Id == categoryId);
    }
}
