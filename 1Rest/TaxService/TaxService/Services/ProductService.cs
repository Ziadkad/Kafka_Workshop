using Newtonsoft.Json;
using Producer.Entities;
using Producer.Services.Interfaces;

namespace Producer.Services;

public class ProductService : IProductService
{
    private readonly IConfiguration _configuration;

    public ProductService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<List<Product>> GetAllProducts()
    {
        using (HttpClient httpClient = new HttpClient())
        {
            string apiUrl = _configuration.GetValue<string>("EndPoint")+"/api/Product";
            var response = await httpClient.GetAsync(apiUrl);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responseContent = await response.Content.ReadAsStringAsync();
            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(responseContent);
            return products;
        }
    }
}