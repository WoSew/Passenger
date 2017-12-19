using System;

namespace Passenger.Core.Domain
{
    public class Vehicle
    {
        public string Brand { get; protected set; }
        public string Name { get; protected set; }
        public int Seats { get; protected set; }

        public Vehicle(string brand, string name, int seats)
        {
            SetBrand(brand);
            SetName(name);
            SetSeats(seats);
        }

        public void SetBrand(string brand)
        {
            if(string.IsNullOrWhiteSpace(brand))
            {
                throw new Exception("Brand is incorrect.");
            }
            if(Brand == brand)
            {
                return;
            }
            Brand = brand;
        }

        public void SetName(string name)
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                throw new Exception("Name is incorrect.");
            }
            if(Name == name)
            {
                return;
            }
            Name = name;
        }

        public void SetSeats(int seats)
        {
            if(seats < 2)
            {
                throw new Exception("Quantity of seats is incorrect. The number of seats must by greater than of equal to 2 if You want to be able to use the application.");
            }
            if(Seats == seats)
            {
                return;
            }
            Seats = seats;
        }
    }
}