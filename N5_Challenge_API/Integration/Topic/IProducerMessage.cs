using N5_Challenge_API.Dto.Topic;
using Nest;

namespace N5_Challenge_API.Integration.Topic
{
    public interface IProducerMessage
    {
        public Task Push(PermissionActionDto action);
    }
}
