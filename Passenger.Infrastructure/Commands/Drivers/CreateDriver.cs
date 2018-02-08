using System;

namespace Passenger.Infrastructure.Commands.Drivers
{
    public class CreateDriver : AuthenticatedCommandBase
    {
        public DriverVehicle Vehicle { get; set; }
        public class DriverVehicle
        {
            public string Brand { get; set; }
            public string Name { get; set; }
        }
    }
}