using System.Threading.Tasks;

namespace Passenger.Infrastructure.Services
{
    public interface IDataInitializer : IServices
    {
         Task SeedAsync();
    }
}