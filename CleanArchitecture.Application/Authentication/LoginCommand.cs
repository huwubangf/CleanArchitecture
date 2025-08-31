using CleanArchitecture.Application.DTOs;
using MediatR;

namespace CleanArchitecture.Application.Authentication
{
    public record LoginCommand(string Email, string Password) : IRequest<AuthenticationResult>;
}
