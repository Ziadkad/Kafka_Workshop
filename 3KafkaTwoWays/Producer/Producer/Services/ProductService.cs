using Producer.Entities;
using Producer.Services.Interfaces;

namespace Producer.Services;

public class ProductService : IProductService
{
    public List<Product> GetAllProducts()
    {
        return Data.Data.Products;
    }
}