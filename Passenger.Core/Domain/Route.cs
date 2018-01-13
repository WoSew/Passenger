using System;

namespace Passenger.Core.Domain
{
    public class Route
    {
        public Guid Id { get; protected set; }
        public Node StartNode { get; protected set; }
        public Node EndNode { get; protected set; }

        protected Route(Node startNode, Node endNode)
        {
            Id = Guid.NewGuid();
            StartNode = startNode;
            EndNode = endNode;
            
        }

        public static Route Create(Node startNode, Node endNode)
            => new Route(startNode, endNode);
        
    }
}