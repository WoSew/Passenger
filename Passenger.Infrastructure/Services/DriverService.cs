using System;
using System.Threading.Tasks;
using AutoMapper;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.DTO;

namespace Passenger.Infrastructure.Services
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public DriverService(IDriverRepository driverRepository, IMapper mapper, IUserRepository userRepository)
        {
            _driverRepository = driverRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<DriverDto> GetAsync(Guid userId)
        {
            var driver = await _driverRepository.GetAsync(userId);

            return _mapper.Map<Driver, DriverDto>(driver);
        }
      
        public async Task CreateAsync(Guid userId, string vehicleBrand, string vehicleName, int vehicleSeats)
        {
            var user = await _userRepository.GetAsync(userId);
            if(user == null)
            {
                throw new Exception($"User with id number: '{userId}' does not exists. You can not create 'Driver'.");
            }

            var driver = await _driverRepository.GetAsync(user.Id);
            if(driver != null)
            {
                throw new Exception($"Selected User with id number: '{user.Id}' already is a driver.");
            }

            driver = new Driver(userId, vehicleBrand, vehicleName, vehicleSeats);
            await _driverRepository.AddAsync(driver);
        }
    }
}