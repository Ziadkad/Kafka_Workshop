using TaxService.Entities;

namespace TaxService.Services.Interfaces;

public interface IAddTaxesToProducts
{
    public Product AddTaxesToProductPrice(Product product);
}