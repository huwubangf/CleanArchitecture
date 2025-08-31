using CleanArchitecture.Application.Common.Interfaces;
using MassTransit;

namespace CleanArchitecture.Infrastructure.Messaging.Publishers
{
    public class RabbitMqPublisher<T> : IPublisher<T>
    where T : class
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public RabbitMqPublisher(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task PublishAsync(T @event)
        {
            await _publishEndpoint.Publish(@event);
        }
    }
}
