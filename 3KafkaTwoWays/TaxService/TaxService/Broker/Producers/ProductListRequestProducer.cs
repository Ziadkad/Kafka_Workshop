using Confluent.Kafka;
using Newtonsoft.Json;

namespace TaxService.Broker.Producers;

public class ProductListRequestProducer
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IConfiguration _configuration;
    private readonly IProducer<Null, string> _producer;
    private readonly ILogger<ProductListRequestProducer> _logger;

    private readonly string _productMiddlewareTopic;
    private readonly string _productServiceRequestTopic;
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
        _productMiddlewareTopic = _configuration["Kafka:ProductMiddlewareTopic"];
        _productServiceRequestTopic = _configuration["Kafka:ProductServiceRequestTopic"];
    }

    public async Task ProduceAsync()
    {
        try
        {
            var kafkaMessage = new Message<Null, string> { Value =  _productServiceRequestTopic};
            _logger.LogInformation($"Producing message to Kafka topic {_productMiddlewareTopic}");

            await _producer.ProduceAsync(_productMiddlewareTopic, kafkaMessage);
            _logger.LogInformation($"Message produced successfully to topic {_productMiddlewareTopic}");
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"An error occurred while producing message to Kafka topic {_productMiddlewareTopic}");
        }
    }
}