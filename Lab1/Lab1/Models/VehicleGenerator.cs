using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Models
{
    public sealed class VehicleGenerator
    {
        public List<string> Colors = new List<string>()
        {
            "червоний", "жовтий", "зелений",
            "золотистий", "срібний", "блакитний"
        };
        private Random random = new Random();

        public string GetRandomNumber()
        {
            string number = random.Next(1111, 9999).ToString();

            return "AA " + number + " AK"; 
        }
        public string GetRandomVINCode()
        {
            return Guid.NewGuid().ToString();
        }
        public string GetRandomColor()
        {
            return Colors[random.Next(Colors.Count)];
        }
        public Vehicle Generate(int modelId, int ownerId)
        {
            string VINCode = GetRandomVINCode();
            string number = GetRandomNumber();
            string color = GetRandomColor();

            return new Vehicle(modelId, VINCode, color, number, "задовільний", ownerId);
        }
    }
}
