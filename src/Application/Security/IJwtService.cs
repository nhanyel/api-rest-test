using System;

namespace Application.Security
{
    public interface IJwtService
    {
        string GenerateToken(Guid userId);
    }
}