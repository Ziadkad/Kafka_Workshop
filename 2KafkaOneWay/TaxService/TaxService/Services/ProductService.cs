using TaxService.Entities;
using TaxService.Services.Interfaces;
using TaxService.StaticClasses;

namespace Producer.Services;

public class ProductService : IProductService
{
    public async Task<List<Product>> GetAllProducts()
    {
        return StaticProduct.Products;
    }

    
}