using Microsoft.AspNetCore.Mvc;
using TaxService.Entities;
using TaxService.Services.Interfaces;

namespace TaxService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductAfterTaxesController :ControllerBase
{
    private readonly IProductService _productService;
    private readonly IAddTaxesToProducts _addTaxesToProducts;

    public ProductAfterTaxesController(IProductService productService, IAddTaxesToProducts addTaxesToProducts)
    {
        _productService = productService;
        _addTaxesToProducts = addTaxesToProducts;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProductAfterTaxesController()
    {
        try
        {
            List<Product> products = await _productService.GetAllProducts();
            products = products.Select<Product, Product>(p => _addTaxesToProducts.AddTaxesToProductPrice(p)).ToList();
            return Ok(products);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

    }
    
}