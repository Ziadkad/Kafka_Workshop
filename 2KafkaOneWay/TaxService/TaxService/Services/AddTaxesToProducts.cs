using TaxService.Entities;
using TaxService.Services.Interfaces;

namespace TaxService.Services;

public class AddTaxesToProducts : IAddTaxesToProducts
{
    private decimal  TAX_AMOUNT_IN_POURCENTAGE = 0.2m;
    public Product AddTaxesToProductPrice(Product product)
    {
        product.Price += product.Price + product.Price * TAX_AMOUNT_IN_POURCENTAGE;
        return product;
    }
}