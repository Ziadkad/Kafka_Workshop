using TaxService.Entities;

namespace TaxService.Services.Interfaces;

public interface IProductService
{
    public Task<List<Product>> GetAllProducts();
}