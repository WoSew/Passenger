using System;
using System.Collections.Generic;
using System.Linq;

namespace Passenger.Core.Domain
{
    public class Driver
    {
        private ISet<Route> _routes = new HashSet<Route>();
        private ISet<DailyRoute> _dailyRoutes = new HashSet<DailyRoute>();
        public Guid UserId { get; protected set; }
        public string Name { get; protected set; }
        public Vehicle Vehicle { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }
        public IEnumerable<Route> Routes
        {
            get { return _routes; }
            set { _routes = new HashSet<Route>(value); }
        }
        
        public IEnumerable<DailyRoute> DailyRoutes
        {
            get { return _dailyRoutes; }
            set { _dailyRoutes = new HashSet<DailyRoute>(value); }
        }

        protected Driver()
        {
        }
        public Driver(User user)
        {
            UserId = user.Id;
            Name = user.Username;
        }

        public void SetVehicle(Vehicle vehicle)
        {
            Vehicle = vehicle;
            UpdatedAt = DateTime.UtcNow;
        }

        public void AddRoute(string name, Node start, Node end, double distance)
        {
            var route = Routes.SingleOrDefault(x => x.Name == name);
            if(route != null)
            {
                throw new DomainException(ErrorCodes.NameAlreadyExist, $"Route with name: '{name}' already exist.");
            }
            if(distance < 0)
            {
                throw new DomainException(ErrorCodes.InvalidDistance, $"Route with name: '{name}' cannot have distance less than 0.");
            }


            _routes.Add(Route.Create(name, start, end, distance));
            UpdatedAt = DateTime.UtcNow;
        }

        public void DeleteRoute(string name)
        {
            var route = Routes.SingleOrDefault(x=> x.Name == name);
            if(route == null)
            {
                throw new DomainException(ErrorCodes.RouteNotFound, $"Route with name: '{name}' for driver '{Name}' does not exist.");
            }
            _routes.Remove(route);
            UpdatedAt = DateTime.UtcNow;
        }
    }
}