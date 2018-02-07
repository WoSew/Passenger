using System;
using System.Text.RegularExpressions;

namespace Passenger.Core.Domain
{
    public class Node
    {
        private readonly Regex AddressRegex = new Regex("^(?![_.-])(?!.*[_.-]{2})[a-zA-Z0-9._.-]+(?<![_.-])$");
        public string Address { get; protected set; }
        public double Longitude { get; protected set; }
        public double Latitude { get; protected set; }
        public DateTime UpdateAt { get; protected set; }

        protected Node()
        {

        }
        protected Node(string address, double longitude, double latitude)
        {
            SetAddress(address);
            SetLongitude(longitude);
            SetLatitude(latitude);
        }
        public void SetAddress(string address)
        {
            if(!AddressRegex.IsMatch(address))
            {
                throw new Exception("Address is incorrect.");
            }
            Address = address;
            Update();
        }
        public void SetLongitude(double longitude)
        {
            if(double.IsNaN(longitude))
            {
                throw new Exception("Longitude must be a number.");
            }
            if(Longitude == longitude)
            {
                return;
            }
            Longitude = longitude;
            Update();
        }
        public void SetLatitude(double latitude)
        {
            if(double.IsNaN(latitude))
            {
                throw new Exception("Latitude must be a number.");
            }
            if(Latitude == latitude)
            {
                return;
            }
            Latitude = latitude;
            Update();
        }

        public static Node Create(string address, double longitude, double latitude)
            => new Node(address, longitude, latitude);
            
        public void Update()
        {
            UpdateAt = DateTime.UtcNow;
        }
    }
}