using Nest;

namespace N5_Challenge_API.Integration
{
    public interface IElasticSearchIntegration
    {
        public Task IndexDocumento<T>(T documento, string index) where T : class;
    }
}
