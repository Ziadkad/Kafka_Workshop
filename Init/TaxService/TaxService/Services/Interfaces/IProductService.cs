using Producer.Entities;

namespace Producer.Services.Interfaces;

public interface IProductService
{
    public Task<List<Product>> GetAllProducts();
}