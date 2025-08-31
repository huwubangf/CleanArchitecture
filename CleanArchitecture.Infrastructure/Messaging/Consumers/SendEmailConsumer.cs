using CleanArchitecture.Domain.Events;
using MassTransit;

namespace CleanArchitecture.Infrastructure.Messaging.Consumers
{
    public class SendEmailConsumer : IConsumer<ProductCreatedEvent>
    {
        public async Task Consume(ConsumeContext<ProductCreatedEvent> context)
        {
            var product = context.Message;
            Console.WriteLine($"[Email] Gửi email xác nhận tới {product.Email} cho sản phẩm {product.Name}");
            await Task.CompletedTask;
        }
    }
}
