namespace Passenger.Infrastructure.DTO
{
    public class JwtDto // do trzymania danych tokena
    {
        public string Token { get; set; }
        public long Expiry { get; set; }
    }
}