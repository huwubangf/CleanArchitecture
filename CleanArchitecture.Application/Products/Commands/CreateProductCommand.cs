using MediatR;

namespace CleanArchitecture.Application.Products.Commands
{
    public record CreateProductCommand(string Name, decimal Price,int Stock) : IRequest<int>;
}
