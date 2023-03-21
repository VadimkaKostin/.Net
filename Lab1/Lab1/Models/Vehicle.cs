using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Models
{
    public interface IOwnable
    {
        int OwnerId { get; set; }
    }
    public interface IVehicle : IIdentifible, IOwnable
    {
        int ModelId { get; set; }
        string VINCode { get; set; }
        string Color { get; set; }
        string Number { get; set; }
        string State { get; set; }
    }
    
    public class Vehicle : IVehicle
    {
        private static int _counter = 0;

        private int _id;
        private int _modelId;
        private string _VINCode;
        private string _color;
        private string _number;
        private string _state;
        private int _ownerId;

        public int Id { get { return _id; } }
        public int ModelId 
        { 
            get { return _modelId;} 
            set { _modelId = value; }
        }
        public string VINCode
        {
            get { return _VINCode; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("VIN code is required.");
                _VINCode = value;
            }
        }
        public string Color
        {
            get { return _color; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("VIN code is required.");
                _color = value;
            }
        }
        public string Number
        {
            get { return _number; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("VIN code is required.");
                _number = value;
            }
        }
        public string State
        {
            get { return _state; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("VIN code is required.");
                _state = value;
            }
        }
        public int OwnerId
        {
            get { return _ownerId; }
            set
            {
                _ownerId = value;
            }
        }

        public Vehicle(int modelId, string VINCode, string color, string number, 
            string state, int ownerId)
        {
            _id = ++_counter;
            ModelId = modelId;
            this.VINCode = VINCode;
            Color = color;
            Number = number;
            State = state;
            OwnerId = ownerId;
        }

        private Vehicle(int id, int modelId, string VINCode, string color, string number, 
            string state, int ownerId)
        {
            ++_counter;
            _id = id;
            ModelId = modelId;
            this.VINCode = VINCode;
            Color = color;
            Number = number;
            State = state;
            OwnerId = ownerId;
        }

        public Vehicle Clone()
        {
            Vehicle vehicle = new Vehicle(Id, ModelId, VINCode, Color, Number, State, OwnerId);

            return vehicle;
        }

        public override string ToString()
        {
            return $"{Id} {ModelId} {VINCode} {Color} {Number} {State} {OwnerId}";
        }
    }
}
