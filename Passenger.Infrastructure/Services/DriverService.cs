using System;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.DTO;

namespace Passenger.Infrastructure.Services
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _driverRepository;
        public DriverService(IDriverRepository driverRepository)
        {
            _driverRepository = driverRepository;
        }
        public DriverDto Get(Guid userId)
        {
            var driver = _driverRepository.Get(userId);

            return new DriverDto
            {
                Id = driver.Id,
                Vehicle = driver.Vehicle,
                Routes = driver.Routes,
                DailyRoute = driver.DailyRoutes
            };
        }
    }
}