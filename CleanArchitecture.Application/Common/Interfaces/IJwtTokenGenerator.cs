﻿namespace CleanArchitecture.Application.Common.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(string userId, string email);
    }
}
