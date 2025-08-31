using CleanArchitecture.Application.Authentication;

namespace CleanArchitecture.Infrastructure.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public AuthenticationService(JwtTokenGenerator jwtTokenGenerator)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public Task<AuthenticationResult> LoginAsync(LoginRequest request)
        {
            if (request.Email != "test@example.com" || request.Password != "Password123")
                throw new UnauthorizedAccessException("Invalid credentials");

            var userId = Guid.NewGuid().ToString(); // mock userId
            var token = _jwtTokenGenerator.GenerateToken(userId, request.Email);

            return Task.FromResult(new AuthenticationResult(userId, request.Email, token));
        }
    }
}
