namespace CleanArchitecture.Application.Authentication
{
    public record LoginRequest(string Email, string Password);

    public record AuthenticationResult(
        string UserId,
        string Email,
        string Token
    );

    public interface IAuthenticationService
    {
        Task<AuthenticationResult> LoginAsync(LoginRequest request);
    }
}
