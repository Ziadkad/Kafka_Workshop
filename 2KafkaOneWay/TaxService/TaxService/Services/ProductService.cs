using TaxService.Entities;
using TaxService.Services.Interfaces;

namespace Producer.Services;

public class ProductService : IProductService
{
    public async Task<List<Product>> GetAllProducts()
    {
        return new List<Product>();
    }
}