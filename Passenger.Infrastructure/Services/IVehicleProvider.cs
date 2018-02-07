using System.Collections.Generic;
using System.Threading.Tasks;
using Passenger.Core.Domain;
using Passenger.Infrastructure.DTO;

namespace Passenger.Infrastructure.Services
{
    public interface IVehicleProvider : IServices
    {
         Task <IEnumerable<VehicleDto>> BrowseAsync();
         Task<Vehicle> GetAsync(string brand, string name);
    }
}