using System;
using System.Threading.Tasks;
using AutoMapper;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.Extensions;

namespace Passenger.Infrastructure.Services
{
    public class DriverRouteService : IDriverRouteService
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IRootMenager _rootMenager; 
        private readonly IMapper _mapper;

        public DriverRouteService(IDriverRepository driverRepository, IRootMenager rootMenager, IMapper mapper)
        {
            _driverRepository = driverRepository;
            _rootMenager = rootMenager;
            _mapper = mapper;
        }
        public async Task AddAsync(Guid userId, string name, double startLatitude, double startLongitude, double endLatitude, double endLongitude)
        {
            var driver = await _driverRepository.GerOrFailAsync(userId);

            var startAdress = await _rootMenager.GetAdressAsync(startLatitude, startLongitude);
            var endAdress = await _rootMenager.GetAdressAsync(endLatitude, endLongitude);

            var startNode = Node.Create(startAdress, startLatitude, startLongitude);
            var endNode = Node.Create(endAdress, endLatitude, endLongitude);

            var distance = _rootMenager.CalculateDistance(startLatitude, startLongitude, endLatitude, endLongitude);

            driver.AddRoute(name, startNode, endNode, distance);
            await _driverRepository.UpdateAsync(driver);
        }

        public async  Task DeleteAsync(Guid userId, string name)
        {
            var driver = await _driverRepository.GerOrFailAsync(userId);

            driver.DeleteRoute(name);
            await _driverRepository.UpdateAsync(driver);
            
        }
    }
}