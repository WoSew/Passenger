using System;
using System.Collections.Generic;
using System.Linq;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;

namespace Passenger.Infrastructure.Repositories
{
    public class InMemoryDriverRepository : IDriverRepository
    {

        private static ISet<Driver> _drivers = new HashSet<Driver>();

        void IDriverRepository.Add(Driver driver)
        {
            _drivers.Add(driver);
        }

        public Driver Get(Guid userId)
            => _drivers.Single(x=> x.UserId == userId);

        public IEnumerable<Driver> GetAll()
            =>_drivers;
         void IDriverRepository.Remove(Guid userId)
        {
            var driver = Get(userId);
            _drivers.Remove(driver);
        }

        void IDriverRepository.Update(Driver driver)
        {
        }
    }


}
