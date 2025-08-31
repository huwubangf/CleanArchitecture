namespace CleanArchitecture.Application.DTOs
{
    public record LoginRequest(string Email, string Password);

    public record AuthenticationResult(
        string UserId,
        string Email,
        string Token
    );
}
