using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab1.Models
{
    public interface IPerson : IIdentifible
    {
        string DriverRightsNumber { get; set; }
        string Name { get; set; }
        string Surname { get; set; }
        string Patronymic { get; set; }
        string FullName { get; }
        DateTime DateOfBirth { get; set; }
        Address PersonAddress { get; set; }
    }
    public interface IDriver : IPerson
    {
        int VehicleId { get; set; }
    }

    public class Person : IPerson
    {
        protected static int _counter = 0;

        protected int _id;
        protected string _driverRightsNumber;
        protected string _name;
        protected string _surname;
        protected string _patronymic;
        protected DateTime _dateOfBirth;
        protected Address _personAddress;

        public int Id { get { return _id; } }
        public string DriverRightsNumber
        {
            get { return _driverRightsNumber; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("Driver rights number is required.");
                _driverRightsNumber = value;
            }
        }
        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("Person name is required.");
                _name = value;
            }
        }
        public string Surname
        {
            get { return _surname; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("Person surname is required.");
                _surname = value;
            }
        }
        public string Patronymic
        {
            get { return _patronymic; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException("Person name is required.");
                _patronymic = value;
            }
        }
        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("Person date of birth is required.");
                _dateOfBirth = value;
            }
        }
        public Address PersonAddress
        {
            get { return _personAddress; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("Person adress is required.");
                _personAddress = value;
            }
        }

        public string FullName
        {
            get { return Surname + " " + Name + " " + Patronymic; }
        }

        public Person(string driverRightsNumber, string name, string surname, string patronymic, DateTime dateOfBirth, Address personAddress)
        {
            _id = ++_counter;
            DriverRightsNumber = driverRightsNumber;
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
            DateOfBirth = dateOfBirth;
            PersonAddress = personAddress;
        }
        
        public override string ToString()
        {
            return $"{Id} {DriverRightsNumber} {Name} {Surname} {Patronymic} {DateOfBirth} {PersonAddress}";
        }
    }
    public sealed class Driver : Person, IDriver
    {
        private int _vehicleId;

        public int VehicleId
        {
            get { return _vehicleId; }
            set { _vehicleId = value; }
        }

        public Driver(string driverRightsNumber, string name, string surname, string patronymic, 
            DateTime dateOfBirth, Address personAddress, int vehicleId)
            : base(driverRightsNumber, name, surname, patronymic, dateOfBirth, personAddress)
        {
            VehicleId = vehicleId;
        }
        public Driver(Person person, int vehicleId)
            : base(person.DriverRightsNumber, person.Name, person.Surname, person.Patronymic,
                  person.DateOfBirth, person.PersonAddress)
        {
            VehicleId = vehicleId;
        }

        public override string ToString()
        {
            return base.ToString() + $" {VehicleId}";
        }
    }
}
