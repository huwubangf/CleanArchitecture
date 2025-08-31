using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.DTOs;
using MediatR;

namespace CleanArchitecture.Application.Authentication
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthenticationResult>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public LoginCommandHandler(IJwtTokenGenerator jwtTokenGenerator)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<AuthenticationResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            if (request.Email != "test@example.com" || request.Password != "Password123")
                throw new UnauthorizedAccessException("Invalid credentials");

            var userId = Guid.NewGuid().ToString();

            var token = _jwtTokenGenerator.GenerateToken(request.Email,request.Password);

            return new AuthenticationResult(userId, request.Email, token);
        }
    }
}
