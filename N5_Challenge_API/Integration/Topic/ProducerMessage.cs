using Confluent.Kafka;
using Elasticsearch.Net;
using N5_Challenge_API.Dto;
using N5_Challenge_API.Dto.Topic;
using N5_Challenge_API.Entitys;
using Nest;
using System.Text.Json;

namespace N5_Challenge_API.Integration.Topic
{
    public class ProducerMessage : IProducerMessage
    {
        private readonly ILogger<ElasticSearchIntegration> _logger;
        private readonly IConfiguration _configuration;
        private IEnumerable<KeyValuePair<string, string>> config;

        public ProducerMessage(ILogger<ElasticSearchIntegration> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public async Task Push(PermissionActionDto action) 
        {
            try
            {
                string methodName = "ProducerMessage.Push ";
                _logger.LogDebug(methodName + " Init");
                var config = new ProducerConfig
                {
                    BootstrapServers = _configuration["Kafka:BootstrapServers"],
                    //ClientId = ""
                };

                using (var producer = new ProducerBuilder<Null,string>(config).Build())
                {
                    string topicName = _configuration["Kafka:topic"];
                        producer.Produce(new TopicPartition(topicName,Partition.Any), new Message<Null, string> { Value = JsonSerializer.Serialize(action) },
                     (deliveryReport) =>
                     {
                         if (deliveryReport.Error.Code != ErrorCode.NoError)
                         {
                             _logger.LogError($"Failed to deliver message: {deliveryReport.Error.Reason}");
                         }
                         else
                         {
                             _logger.LogDebug($"Produced event to topic {topicName}");
                         }
                     });
                }
                _logger.LogDebug(methodName + " End");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                _logger.LogInformation(JsonSerializer.Serialize(action));
                throw;
            }
        }
    }
}
