using Lab2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Models
{
    public class Address : IAddress
    {
        private static int _counter = 1;

        public int Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int House { get; set; }

        public Address()
        {
            Id = _counter++;
        }
    }
}
