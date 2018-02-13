using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.DTO;
using Passenger.Infrastructure.Exceptions;
using Passenger.Infrastructure.Extensions;

namespace Passenger.Infrastructure.Services
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IUserRepository _userRepository;
        private readonly IVehicleProvider _vehicleProvider;
        private readonly IMapper _mapper;

        public DriverService(IDriverRepository driverRepository, IMapper mapper, IUserRepository userRepository, IVehicleProvider vehicleProvider)
        {
            _driverRepository = driverRepository;
            _userRepository = userRepository;
            _vehicleProvider = vehicleProvider;
            _mapper = mapper;
        }

        public async Task<DriverDetailsDto> GetAsync(Guid userId)
        {
            var driver = await _driverRepository.GetAsync(userId);

            return _mapper.Map<DriverDetailsDto>(driver);
        }

        public async Task<IEnumerable<DriverDto>> BrowseAsync()
        {
            var drivers = await _driverRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<DriverDto>>(drivers);
        }

        public async Task CreateAsync(Guid userId)
        {
            var user = await _userRepository.GerOrFailAsync(userId);
  
            var driver = await _driverRepository.GetAsync(userId);
            if(driver != null)
            {
                throw new ServiceException(Exceptions.ErrorCodes.DriverAlreadyExisty, $"Selected User with id number: '{user.Id}' already is a driver.");
            }

            driver = new Driver(user);
            await _driverRepository.AddAsync(driver);
        }

        public async Task DeleteAsync(Guid userId)
        {
            var driver = await _driverRepository.GerOrFailAsync(userId);
            await _driverRepository.DeleteAsync(driver);
        }

        public async Task SetVehicleAsync(Guid userId, string brand, string name)
        {
            var driver = await _driverRepository.GerOrFailAsync(userId);
          
            var vehicleDetails = await _vehicleProvider.GetAsync(brand, name);
            var vehicle = Vehicle.Create(vehicleDetails.Brand, vehicleDetails.Name, vehicleDetails.Seats);
            driver.SetVehicle(vehicle);
        }
    }
}