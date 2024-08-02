using Confluent.Kafka;
using Newtonsoft.Json;

namespace TaxService.Broker.Producers;

public class ProductListRequestProducer
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IConfiguration _configuration;
    private readonly IProducer<Null, string> _producer;
    private readonly ILogger<ProductListRequestProducer> _logger;

    private const string TOPIC_FOR_REQUEST = "ProductServiceMiddleware";
    private const string TOPIC_FOR_PRODUCT_REQUEST = "ProductListForTaxService";
    public ProductListRequestProducer(IConfiguration configuration, IServiceProvider serviceProvider,ILogger<ProductListRequestProducer> logger)
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

    public async Task ProduceAsync()
    {
        try
        {
            var kafkaMessage = new Message<Null, string> { Value =  TOPIC_FOR_PRODUCT_REQUEST};
            _logger.LogInformation($"Producing message to Kafka topic {TOPIC_FOR_REQUEST}");

            await _producer.ProduceAsync(TOPIC_FOR_REQUEST, kafkaMessage);
            _logger.LogInformation($"Message produced successfully to topic {TOPIC_FOR_REQUEST}");
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"An error occurred while producing message to Kafka topic {TOPIC_FOR_REQUEST}");
        }
    }
}