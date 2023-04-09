using Lab2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Models
{
    public class Vehicle : IVehicle
    {
        private static int _counter = 1;

        public int Id { get; set; }
        public int ModelId { get; set; }
        public string VINCode { get; set; }
        public string Color { get; set; }
        public string Number { get; set; }
        public string State { get; set; }

        public Vehicle()
        {
            Id = _counter++;
        }
    }
}
