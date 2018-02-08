using System;
using System.Collections.Generic;
using Passenger.Core.Domain;

namespace Passenger.Infrastructure.DTO
{
    public class DriverDto
    {
        public Guid userId { get; set; }
        public string Name { get; set; }
        public VehicleDto Vehicle { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}