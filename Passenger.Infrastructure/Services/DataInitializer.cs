using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Passenger.Infrastructure.Services
{
    public class DataInitializer : IDataInitializer
    {
        private readonly IUserService _userService;
        private readonly IDriverService _driverService;
        private readonly IDriverRouteService _driverRouteService;
        private readonly ILogger<DataInitializer> _logger;
        
        public DataInitializer(IUserService userService, IDriverService driverService,
            IDriverRouteService driverRouteService, ILogger<DataInitializer> logger)
        {
            _userService = userService;
            _driverService = driverService;
            _driverRouteService = driverRouteService;
            _logger = logger;
        }

        public async Task SeedAsync()
        {

            var users = await _userService.BrowseAsync();
            if(users.Any())
            {
                return;
            }

            _logger.LogTrace("Initializing data...");   

            var tasks = new List<Task>();

            for(int i=1; i<=10; i++)
            {
                var userId = Guid.NewGuid();
                var username = $"user{i}";

                _logger.LogTrace($"Adding user: '{username}'.");
                await _userService.RegisterAsync(userId, $"user{i}@test.com", username, "secret", "user");

                _logger.LogTrace($"Adding user: '{username}'.");
                await _driverService.CreateAsync(userId);

                _logger.LogTrace($"Setting Vehicle for: '{username}'.");
                await _driverService.SetVehicleAsync(userId, "Mazda", "3");
                _logger.LogTrace($"Created a new driver for: {username}.");

                _logger.LogTrace($"Adding route for: '{username}'.");
                await _driverRouteService.AddAsync(userId, "Route 1", 5, 5, 6, 6);
                await _driverRouteService.AddAsync(userId, "Route 2", 605, 221, 735, 000);
            }

            for(var i=1; i<=3; i++)
            {
                var userId = Guid.NewGuid();
                var username = $"admin{i}";

                 _logger.LogTrace($"Adding admin: '{username}'.");

                tasks.Add(_userService.RegisterAsync(userId, $"admin{i}@test.com", username, "secret", "admin"));
            }

            await Task.WhenAll(tasks);
            
            _logger.LogTrace("Data was initialized.");  
        }
    }
}