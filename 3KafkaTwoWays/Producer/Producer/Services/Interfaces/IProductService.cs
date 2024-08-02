using Producer.Entities;

namespace Producer.Services.Interfaces;

public interface IProductService
{
    public List<Product> GetAllProducts();
}