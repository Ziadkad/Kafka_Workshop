﻿using Confluent.Kafka;
using Producer.Broker.Producers;

namespace Producer.Broker;

public class RequestMiddleware : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IConsumer<Ignore, string> _consumer;
    private readonly ILogger<RequestMiddleware> _logger;
    private readonly string _middlewareTopic;
    private readonly string _taxService;
    public RequestMiddleware(IConfiguration configuration, IServiceProvider serviceProvider, ILogger<RequestMiddleware> logger)
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
        _middlewareTopic = configuration["Kafka:MiddlewareTopic"];
        _taxService = configuration["Kafka:TaxServiceTopic"];
    }
    
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Kafka consumer service starting.");
        _consumer.Subscribe(_middlewareTopic);
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
            var consumeResult = _consumer.Consume(TimeSpan.FromSeconds(1));
            if (consumeResult?.Message?.Value != null)
            {
                string message = consumeResult.Message.Value;
                _logger.LogInformation($"Kafka message consumed: {message}");
                switch (message)
                {
                    case var msg when msg == _taxService:
                        using (var scope = _serviceProvider.CreateScope())
                        {
                            ProductListProducer productListProducer = scope.ServiceProvider.GetRequiredService<ProductListProducer>();
                            await productListProducer.ProduceAsync(_taxService);
                        }
                        break;
                    
                    default:
                        _logger.LogInformation($"Received unknown message: {message}");
                        break;
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