using Confluent.Kafka;
using Newtonsoft.Json;
using Producer.Entities;
using Producer.Services.Interfaces;

namespace Producer.Broker.Producers;

public class ProductListProducer
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IConfiguration _configuration;
    private readonly IProducer<Null, string> _producer;
    private readonly ILogger<ProductListProducer> _logger;
    public ProductListProducer(IConfiguration configuration, IServiceProvider serviceProvider,ILogger<ProductListProducer> logger)
    {
        _serviceProvider = serviceProvider;
        _configuration = configuration;
        _logger = logger;
        var producerconfig = new ProducerConfig
        {
            BootstrapServers = _configuration["Kafka:BootstrapServers"]
        };
        _producer = new ProducerBuilder<Null, string>(producerconfig).Build();
    }
    public async Task ProduceAsync(string topic)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            IProductService productService = scope.ServiceProvider.GetRequiredService<IProductService>();
            try
            {
                _logger.LogInformation("Fetching all products from the product service.");
                List<Product> products = productService.GetAllProducts();
                
                string jsonProducts = JsonConvert.SerializeObject(products);
                _logger.LogInformation("Serialized products to JSON.");

                var kafkaMessage = new Message<Null, string> { Value = jsonProducts };
                _logger.LogInformation("Producing message to Kafka topic {Topic}", topic);

                await _producer.ProduceAsync(topic, kafkaMessage);
                _logger.LogInformation("Message produced successfully to topic {Topic}", topic);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while producing message to Kafka topic {Topic}", topic);
            }
            
        }
    }
}