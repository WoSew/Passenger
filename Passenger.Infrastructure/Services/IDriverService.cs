using System;
using System.Threading.Tasks;
using Passenger.Infrastructure.DTO;

namespace Passenger.Infrastructure.Services
{
    public interface IDriverService : IServices
    {
       Task<DriverDto> GetAsync(Guid userId);

       Task CreateAsync(Guid userId, string vehicleBrand, string vehicleName, int vehicleSeats);
    }
}