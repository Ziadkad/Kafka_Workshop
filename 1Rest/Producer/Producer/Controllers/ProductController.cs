using Microsoft.AspNetCore.Mvc;
using Producer.Entities;
using Producer.Services.Interfaces;

namespace Producer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public ActionResult<List<Product>> GetAllProductAfterTaxesController()
    {
        try
        {
            List<Product> products =  _productService.GetAllProducts();
            return Ok(products);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

    }
}