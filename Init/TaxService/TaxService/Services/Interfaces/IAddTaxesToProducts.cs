using Producer.Entities;

namespace Producer.Services.Interfaces;

public interface IAddTaxesToProducts
{
    public Product AddTaxesToProductPrice(Product product);
}