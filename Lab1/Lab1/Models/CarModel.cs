using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Models
{
    public interface IModel : IIdentifible
    {
        int MarkId { get; set; }
        string Model { get; set; }
        VehicleBodyType BodyType { get; set; }
        string Maker { get; set; }
        int Year { get; set; }
    }
    public enum VehicleBodyType
    {
        Sedan,
        Hatchback,
        MUV,
        Coupe,
        Convertible,
        Wagon,
        Van,
        Jeep
    }
    public class CarModel : IModel
    {
        private static int _counter = 0;

        private int _id;
        private int _markId;
        private string _model;
        private VehicleBodyType _type;
        private string _maker;
        private int _year;

        public int Id { get { return _id; } }
        public int MarkId
        {
            get { return _markId; }
            set { _markId = value; }
        }
        public string Model
        {
            get { return _model; }
            set
            {
                if(string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("Model is required.");
                _model = value;
            }
        }
        public VehicleBodyType BodyType
        {
            get { return _type; }
            set { _type = value; }
        }
        public string Maker
        {
            get { return _maker; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("Vahicle maker is required.");
                _maker = value;
            }
        }
        public int Year
        {
            get { return _year; }
            set
            {
                if (value < 1950 || value > DateTime.Now.Year)
                    throw new ArgumentException("Year should be between 1950 and now.");
                _year = value;
            }
        }

        public CarModel(int markId, string model, VehicleBodyType type, string maker, int year)
        {
            _id = ++_counter;
            MarkId = markId;
            Model = model;
            BodyType = type;
            Maker = maker;
            Year = year;
        }

        public override string ToString()
        {
            return $"{Id} {MarkId} {Model} {BodyType} {Maker} {Year}";
        }
    }
}
