using Confluent.Kafka;
using Newtonsoft.Json;
using TaxService.Entities;
using TaxService.Services.Interfaces;
using TaxService.StaticClasses;

namespace TaxService.Broker.Consumers;

public class ProductListConsumer : BackgroundService
{
       
    private readonly IServiceProvider _serviceProvider;
    private readonly IConsumer<Ignore, string> _consumer;
    private readonly ILogger<ProductListConsumer> _logger;
    private const string TOPIC = "ProductListFotTaxServiceUsingController";
    public ProductListConsumer(IConfiguration configuration, IServiceProvider serviceProvider, ILogger<ProductListConsumer> logger)
    {
        ConsumerConfig consumerConfig = new ConsumerConfig
        {
            BootstrapServers = configuration["Kafka:BootstrapServers"],
            GroupId = configuration["Kafka:GroupId"],
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
        _consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build();
        _serviceProvider = serviceProvider;
        _logger = logger;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Kafka consumer service starting.");
        _consumer.Subscribe(TOPIC);
        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // _logger.LogInformation("Waiting to process Kafka message...");
                await ProcessKafkaMessage(stoppingToken);
                await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
            }
        }
        catch (OperationCanceledException)
        {
            _logger.LogError("OperationCanceledException");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while executing the Kafka consumer service.");
        }
        finally
        {
            _consumer.Close();
            _consumer.Dispose();
        }
    }
    private async Task ProcessKafkaMessage(CancellationToken stoppingToken)
    {
        try
        {
            // _logger.LogInformation("Consuming Kafka message...");
            var consumeResult = _consumer.Consume(TimeSpan.FromSeconds(1));
            if (consumeResult?.Message?.Value != null)
            {
                string message = consumeResult.Message.Value;
                _logger.LogInformation($"Kafka message consumed: {message}");
                List<Product> products = JsonConvert.DeserializeObject<List<Product>>(message);
                if (products is not null)
                {
                    StaticProduct.SetAndResetList(products);
                }
            }
            else
            {
                _logger.LogInformation("No Kafka message received in the specified time frame.");
            }
        }
        catch (ConsumeException ex)
        {
            _logger.LogError(ex, "Error consuming Kafka message");
        }
    }
}