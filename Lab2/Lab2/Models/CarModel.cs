using Lab2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Models
{
    public class CarModel : IModel
    {
        private static int _counter = 1;

        public int Id { get; set; }
        public int MarkId { get; set; }
        public string Name { get; set; }
        public CarBodyType BodyType { get; set; }
        public string Maker { get; set; }
        public int Year { get; set; }

        public CarModel()
        {
            Id = _counter++;
        }
    }
}
