namespace Passenger.Infrastructure.DTO
{
    public class RouteDto
    {
        public string Name { get; set; }
        public NodeDto StartNode { get; set; }
        public NodeDto EndNode { get; set; }
        public double Distance { get; set;}
    }
}