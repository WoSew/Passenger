using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Passenger.Infrastructure.Services
{
    public class DataInitializer : IDataInitializer
    {
        private readonly IUserService _userService;
        private readonly ILogger<DataInitializer> _logger;

        public DataInitializer(IUserService userService, ILogger<DataInitializer> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public async Task SeedAsync()
        {
            _logger.LogTrace("Initializing data...");   

            var tasks = new List<Task>();

            for(int i=1; i<=10; i++)
            {
                var userId = Guid.NewGuid();
                var username = $"user{i}";

                tasks.Add(_userService.RegisterAsync(userId, $"user{i}@test.com", username, "secret", "user"));

                _logger.LogTrace($"Adding user: '{username}'.");
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