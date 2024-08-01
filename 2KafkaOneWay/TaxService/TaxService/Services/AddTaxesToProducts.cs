using Producer.Entities;
using Producer.Services.Interfaces;

namespace Producer.Services;

public class AddTaxesToProducts : IAddTaxesToProducts
{
    private decimal  TAX_AMOUNT_IN_POURCENTAGE = 0.2m;
    public Product AddTaxesToProductPrice(Product product)
    {
        product.Price += product.Price + product.Price * TAX_AMOUNT_IN_POURCENTAGE;
        return product;
    }
}