using Microsoft.AspNetCore.Mvc;
using Producer.Broker.Producers;

namespace Producer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly ProductListProducer _productListProducer;

    public ProductController(ProductListProducer productListProducer)
    {
        _productListProducer = productListProducer;
    }

    [HttpGet]
    public async Task<IActionResult> ProduceToTopic()
    {
        await _productListProducer.ProduceAsync("ProductListFotTaxServiceUsingController");
        return Ok();
    }
}