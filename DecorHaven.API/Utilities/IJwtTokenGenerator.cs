using DecorHaven.API.Models;

namespace DecorHaven.API.Utilities;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}

