using System;
using System.Collections.Generic;

namespace Passenger.Core.Domain
{
    public class Driver
    {
        public Guid UserId { get; protected set; }
        public Vehicle Vehicle { get; protected set; }
        public IEnumerable<Route> Routes { get; protected set; }
        public IEnumerable<DailyRoute> DailyRoutes { get; protected set; }

        public DateTime UpdatedAt { get; protected set; }

        protected Driver()
        {
        }
        public Driver(Guid userId, string vehicleBrand, string vehicleName, int vehicleSeats)
        {
            UserId = userId;
            Vehicle.Create(vehicleBrand,vehicleName,vehicleSeats);
        }
    }
}