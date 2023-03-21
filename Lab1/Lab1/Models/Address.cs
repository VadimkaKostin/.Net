using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Models
{
    public class Address
    {
        private string _city;
        private string _street;
        private string _house;

        public string City
        { 
            get { return _city; } 
            set 
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Adress city is required.");
                _city = value; 
            } 
        }
        public string Street
        {
            get { return _street; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Adress city is required.");
                _street = value;
            }
        }
        public string House
        {
            get { return _house; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Adress city is required.");
                _house = value;
            }
        }

        public Address(string city, string street, string house)
        {
            this.City = city;
            this.Street = street;
            this.House = house;
        }

        public override string ToString()
        {
            return $"{this.City}, {this.Street} {this.House}";
        }
    }
}