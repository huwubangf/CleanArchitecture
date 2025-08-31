using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Events;
using MediatR;

namespace CleanArchitecture.Application.Products.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IPublisher<ProductCreatedEvent> _publisher;
        public CreateProductCommandHandler(IApplicationDbContext applicationDbContext, IPublisher<ProductCreatedEvent> publisher)
        {
            _applicationDbContext = applicationDbContext;
            _publisher = publisher;
        }
        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = request.Name,
                Price = request.Price,
                Stock = request.Stock
            };
            await _applicationDbContext.Products.AddAsync(product, cancellationToken);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
            var productCreatedEvent = new ProductCreatedEvent
            {
                ProductId = product.Id,
                Email = "test@gamil.com",
                Name = request.Name
            };
            await _publisher.PublishAsync(productCreatedEvent);
            return product.Id;
        }
    }
}
