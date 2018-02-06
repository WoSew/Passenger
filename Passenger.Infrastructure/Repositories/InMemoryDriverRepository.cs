using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;

namespace Passenger.Infrastructure.Repositories
{
    public class InMemoryDriverRepository : IDriverRepository
    {
        private readonly IUserRepository _userRepository;

        public InMemoryDriverRepository(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            //var guidUser1 = _userRepository.GetGuidAsync("user1@email.com");           
        }
         
        private static ISet<Driver> _drivers = new HashSet<Driver>
        {
           
            //new Driver(,"brand","name", 4)
            
        };

        public async Task<Driver> GetAsync(Guid userId)
            =>await Task.FromResult(_drivers.Single(x=> x.UserId == userId));

        public async Task<IEnumerable<Driver>> BrowseAsync()
            =>await Task.FromResult(_drivers);
            
        public async Task AddAsync(Driver driver)
        {
            _drivers.Add(driver);
            await Task.CompletedTask;
        }

        public async Task RemoveAsync(Guid userId)
        {
            var driver = await GetAsync(userId);
            _drivers.Remove(driver);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Driver driver)
        {
            await Task.CompletedTask;
        }
    }


}
