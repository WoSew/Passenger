using System.Threading.Tasks;

namespace Passenger.Infrastructure.Services
{
    public interface IRootMenager : IServices
    {
         Task<string> GetAdressAsync(double latitude, double longitude);

         double CalculateDistance(double startLatitude, double startLongitude, double endtLatitude, double endtLongitude);
         
    }
}