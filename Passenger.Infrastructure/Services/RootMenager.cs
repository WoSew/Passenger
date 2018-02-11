using System;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Services
{
    public class RootMenager : IRootMenager // przykladowa implementacja, normalnie powinno pobierac adres np z google maps
    {
        private static readonly Random Random = new Random();

        public async Task<string> GetAdressAsync(double latitude, double longitude)
            => await Task.FromResult($"Sample address {Random.Next(100)}");

        public double CalculateDistance(double startLatitude, double startLongitude, double endtLatitude, double endtLongitude)
            => Random.Next(500,10000);

    }
}