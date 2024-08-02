using Microsoft.AspNetCore.Mvc;
using TaxService.Broker.Producers;
using TaxService.Entities;
using TaxService.Services.Interfaces;
using TaxService.StaticClasses;

namespace TaxService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductAfterTaxesController :ControllerBase
{
    private readonly IProductService _productService;
    private readonly IAddTaxesToProducts _addTaxesToProducts;
    private readonly ProductListRequestProducer _productListRequestProducer;

    public ProductAfterTaxesController(IProductService productService, IAddTaxesToProducts addTaxesToProducts, ProductListRequestProducer productListRequestProducer)
    {
        _productService = productService;
        _addTaxesToProducts = addTaxesToProducts;
        _productListRequestProducer = productListRequestProducer;
    }


    [HttpGet]
    public async Task<IActionResult> GetAllProductAfterTaxesController()
    {
        try
        {
            await _productListRequestProducer.ProduceAsync();
            List<Product> products = await _productService.GetAllProducts();
            List<Product> productsWithTaxes = products.Select(p => 
            {
                // Shallow Copy
                Product productCopy = new Product
                {
                    Id = p.Id,
                    ProductName = p.ProductName,
                    Price = p.Price,
                };
                return _addTaxesToProducts.AddTaxesToProductPrice(productCopy);
            }).ToList();
            StaticProduct.ResetLoading();
            return Ok(productsWithTaxes);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

    }
    
}