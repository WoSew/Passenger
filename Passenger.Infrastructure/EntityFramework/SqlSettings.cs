namespace Passenger.Infrastructure.EntityFramework
{
    public class SqlSettings
    {
        public string ConnectionString { get; set; }
        public bool InMemory { get; set; }
    }
}