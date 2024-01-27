using Elasticsearch.Net;
using N5_Challenge_API.Dto;
using N5_Challenge_API.Entitys;
using Nest;
using System.Text.Json;

namespace N5_Challenge_API.Integration
{
    public class ElasticSearchIntegration : IElasticSearchIntegration
    {
        private readonly ILogger<ElasticSearchIntegration> _logger;
        private readonly IConfiguration _configuration;
        public ElasticSearchIntegration(ILogger<ElasticSearchIntegration> logger, IConfiguration configuration)
        {
            this._logger = logger;
            _configuration = configuration;
        }
        public async Task IndexDocumento<T>(T documento, string index) where T : class
        {
            try
            {
                string methodName = "ElasticSearchIntegration.IndexDocumento ";

                _logger.LogDebug(methodName + "Init");
                string cx = _configuration.GetConnectionString("es");
                var settings = new ConnectionSettings(new Uri(cx))
                    .DefaultIndex(index);

                var client = new ElasticClient(settings);

                _logger.LogDebug(methodName + "Sending..");
                var indexResponse = await client.IndexDocumentAsync(documento);
                _logger.LogDebug(methodName + "Reciving..");
                if (!indexResponse.IsValid)
                {
                    throw new Exception("Error to sending data to elasticsearch");
                }
                _logger.LogDebug(methodName + " End");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                _logger.LogInformation(JsonSerializer.Serialize(documento));
                throw;
            }
        }
    }
}
