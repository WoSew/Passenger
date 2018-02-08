using System;
using Passenger.Infrastructure.DTO;

namespace Passenger.Infrastructure.Services
{
    public interface IJwtHandler
    {
         JwtDto CreateToken(Guid userID, string role);
    }
}