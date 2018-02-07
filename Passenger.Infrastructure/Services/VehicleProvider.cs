using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Passenger.Core.Domain;
using Passenger.Infrastructure.DTO;

namespace Passenger.Infrastructure.Services
{
    public class VehicleProvider : IVehicleProvider
    {
        private readonly IMemoryCache _cache;
        private readonly static string CacheKey = "vehicles";

        private static readonly IDictionary<string, IEnumerable<VehicleDetails>> availableVehicles = new Dictionary<string, IEnumerable<VehicleDetails>>
        {
            ["Audi"] = new List<VehicleDetails>
            {
                new VehicleDetails("A4", 5),
                new VehicleDetails("Q7", 5),
                new VehicleDetails("RS8", 5)
            },
            ["Mercedes"] = new List<VehicleDetails>
            {
                new VehicleDetails("CLA 200", 5),
                new VehicleDetails("C 180", 8)
            },
            ["Mazda"] = new List<VehicleDetails>
            {
                new VehicleDetails("3", 5),
                new VehicleDetails("6", 5)
            },
            ["Ford"] = new List<VehicleDetails>
            {
                new VehicleDetails("Focus", 5),
                new VehicleDetails("Mustang", 5)
            },
            ["Skoda"] = new List<VehicleDetails>
            {
                new VehicleDetails("Fabia", 5),
                new VehicleDetails("Rapid", 5)
            },
            ["Volkswagen"] = new List<VehicleDetails>
            {
                new VehicleDetails("Passat", 5)
            },
        };

        public VehicleProvider(IMemoryCache cache)
        {
            _cache = cache;
        }

        public async Task<IEnumerable<VehicleDto>> BrowseAsync()
        {
            var vehicle = _cache.Get<IEnumerable<VehicleDto>>(CacheKey);
            
            if(vehicle == null || !vehicle.Any()) // null || kolekcja pusta
            {
                vehicle = await GetAllAsync();
                _cache.Set(CacheKey, vehicle);
            }
            return vehicle;
        }

        public async Task<VehicleDto> GetAsync(string brand, string name)
        {
            if(!availableVehicles.ContainsKey(brand))
            {
                throw new Exception($"Vehicle brand : '{brand}' is not avaliable.");
            }

            var vehicles = availableVehicles[brand];
            var vehicle = vehicles.SingleOrDefault(x => x.Name == name);
            
            if(vehicle == null)
            {
                throw new Exception($"Vehicle: '{name}' for brand: '{brand}' is not avaliable.");
            }

            return await Task.FromResult(new VehicleDto
            {
                Brand = brand,
                Name = vehicle.Name,
                Seats = vehicle.Seats
            });
        }

        private async Task<IEnumerable<VehicleDto>> GetAllAsync() 
            => await Task.FromResult(availableVehicles.GroupBy(x => x.Key)
                .SelectMany(g => g.SelectMany(v => v.Value.Select(x => new VehicleDto
                {
                    Brand = v.Key,
                    Name = x.Name,
                    Seats = x.Seats
                }))));    

        private class VehicleDetails
        {
            public string Name { get; }
            public int Seats { get; }
            public VehicleDetails(string name, int seats)
            {
                Name = name;
                Seats = seats;
            }
        }

    }
}