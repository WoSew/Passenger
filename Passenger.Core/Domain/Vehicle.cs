using System;

namespace Passenger.Core.Domain
{
    public class Vehicle // valueObject - obiekt który tworzymy tylko raz i pozniej go nie zmieniamny. Immutable
    {
        public string Brand { get; protected set; }
        public string Name { get; protected set; }
        public int Seats { get; protected set; }

        protected Vehicle()
        {

        }

        protected Vehicle(string brand, string name, int seats)
        {
            SetBrand(brand);
            SetName(name);
            SetSeats(seats);
        }

        private void SetBrand(string brand)
        {
            if(string.IsNullOrWhiteSpace(brand))
            {
                throw new DomainException(ErrorCodes.InvalidName, "Brand is incorrect.");
            }
            if(Brand == brand)
            {
                return;
            }
            Brand = brand;
        }

        private void SetName(string name)
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                throw new DomainException(ErrorCodes.InvalidName, "Name is incorrect.");
            }
            if(Name == name)
            {
                return;
            }
            Name = name;
        }

        private void SetSeats(int seats)
        {
            if(seats < 2)
            {
                throw new DomainException(ErrorCodes.InvalidNumberOfSeats, "Quantity of seats is incorrect. The number of seats must by greater than of equal to 2 if You want to be able to use the application.");
            }
            if(Seats == seats)
            {
                return;
            }
            Seats = seats;
        }

        public static Vehicle Create(string brand, string name, int seats)
            => new Vehicle(brand,name,seats);
    }
}