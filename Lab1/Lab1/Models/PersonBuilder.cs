using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Models
{
    public sealed class PersonBuilder
    {
        #region PERSON PARAMETERS
        private string _driverReightsNumber;
        private string _name;
        private string _surname;
        private string _patronynic;
        private DateTime _dateOfBirth;
        private string _city;
        private string _street;
        private string _house;
        private int? _vehicleId;
        #endregion

        #region VALIDATION FLAGS
        private bool _isDriverRightsNumberSupplied;
        private bool _isNameSupplied;
        private bool _isSurnameSupplied;
        private bool _isPatronymicSupplied;
        private bool _isDateOfBirthSupplied;
        private bool _isCitySupplied;
        private bool _isStreetSupplied;
        #endregion

        #region BUILDING METHODS
        //Defines person`s name.
        public PersonBuilder HasName(string name)
        {
            if(string.IsNullOrEmpty(name))
                throw new ArgumentNullException("Name cannot be null or empty.");

            this._name = name;
            this._isNameSupplied = true;

            return this;
        }
        //Defines person`s surname.
        public PersonBuilder HasSurname(string surname)
        {
            if (string.IsNullOrEmpty(surname))
                throw new ArgumentNullException("Surname cannot be null or empty.");

            this._surname = surname;
            this._isSurnameSupplied = true;

            return this;
        }
        //Defines person`s patronymic.
        public PersonBuilder HasPatronymic(string patronymic)
        {
            if (string.IsNullOrEmpty(patronymic))
                throw new ArgumentNullException("Patronymic cannot be null or empty.");

            this._patronynic = patronymic;
            this._isPatronymicSupplied = true;

            return this;
        }
        //Defines person`s driver rights number.
        public PersonBuilder HasDriverRights(string number)
        {
            if (string.IsNullOrEmpty(number))
                throw new ArgumentNullException("Driver rights number cannot be null or empty.");

            this._driverReightsNumber = number;
            this._isDriverRightsNumberSupplied = true;

            return this;
        }
        //Defines person`s date of birth.
        public PersonBuilder WasBorn(string date)
        {
            this._dateOfBirth = Convert.ToDateTime(date);
            this._isDateOfBirthSupplied = true;

            return this;
        }
        //Defines in which city person lives.
        public PersonBuilder LivesIn(string city)
        {
            if (string.IsNullOrEmpty(city))
                throw new ArgumentNullException("City cannot bi null or empty.");

            this._city = city;
            this._isCitySupplied = true;

            return this;
        }
        //Defines at what street person lives.
        public PersonBuilder LivesAt(string street, string house)
        {
            if (string.IsNullOrEmpty(street))
                throw new ArgumentNullException("Street cannot be null or empty.");
            if (string.IsNullOrEmpty(house))
                throw new ArgumentNullException("House cannot be null or empty.");

            this._street = street;
            this._house = house;
            this._isStreetSupplied = true;

            return this;
        }

        public PersonBuilder DrivesVehicle(int vehicleId)
        {
            if (vehicleId < 0) 
                throw new ArgumentOutOfRangeException("Vehicle id must have positive value.");

            this._vehicleId = vehicleId;

            return this;
        }
        #endregion

        //Method which builds new object of person.
        public IPerson Build()
        {
            //Validation for supply
            if (!this._isDriverRightsNumberSupplied)
                throw new ArgumentException("Driver rights number is not supplied.");
            if (!this._isNameSupplied)
                throw new ArgumentException("Name is not supplied.");
            if (!this._isSurnameSupplied)
                throw new ArgumentException("Surname is not supplied.");
            if (!this._isPatronymicSupplied)
                throw new ArgumentException("Patronymic is not supplied.");
            if (!this._isDateOfBirthSupplied)
                throw new ArgumentException("Date of birth is not supplied.");
            if (!this._isCitySupplied)
                throw new ArgumentException("City is not supplied.");
            if (!this._isStreetSupplied)
                throw new ArgumentException("Street is not supplied.");

            //if validation success

            //if vehicle id is not supplied returns Person, else returns Driver
            return _vehicleId == null ?
            new Person(_driverReightsNumber, _name, _surname,
                       _patronynic, _dateOfBirth,
                       new Address(_city, _street, _house)) :
            new Driver(_driverReightsNumber, _name, _surname,
                       _patronynic, _dateOfBirth,
                       new Address(_city, _street, _house), _vehicleId.Value);
        }
    }
}
