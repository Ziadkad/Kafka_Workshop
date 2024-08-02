using System.Xml;
using TaxService.Entities;
using TaxService.Services.Interfaces;
using TaxService.StaticClasses;

namespace Producer.Services;

public class ProductService : IProductService
{
    public async Task<List<Product>> GetAllProducts()
    {
        if (!StaticProduct.Loading.Task.IsCompleted)
        {
            await StaticProduct.Loading.Task;
        }
        return StaticProduct.Products;
    }
    
}