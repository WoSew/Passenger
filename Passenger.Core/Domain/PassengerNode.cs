namespace Passenger.Core.Domain
{
    public class PassengerNode //value object
    {
        public Node Node { get; protected set; }
        public Passenger Passenger { get; protected set; }

        protected PassengerNode() //do poźniejszej serializacji
        {

        }

        protected PassengerNode(Node node, Passenger passenger)
        {
            Node = node;
            Passenger = passenger;
        }

        //builder
        public static PassengerNode Create(Node node, Passenger passenger)
            => new PassengerNode(node,passenger);
            
        
    }
}