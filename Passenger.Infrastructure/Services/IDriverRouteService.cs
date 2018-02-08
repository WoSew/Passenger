using System;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Services
{
    public interface IDriverRouteService : IServices
    {
        Task AddAsync(Guid userId, string name, double startLatitude, double startLongitude, double endLatitude, double endLongitude);
        Task DeleteAsync(Guid userId, string name);
    }
}