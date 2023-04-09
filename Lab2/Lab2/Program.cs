using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System;
using Lab2.Interfaces;
using Lab2.Models;
using Lab2.XML;
using Lab2.Generators;
using System.Linq;
using Lab2.ExtentionMethods;
using System.Xml.Serialization;
using System.Xml.XPath;
using System.Collections.ObjectModel;
using System.Collections;
using System.IO;

namespace Lab2
{
    public static class Program
    {
        private static Generator generator = new Generator();

        private static Random random = new Random();

        #region CONST VALUES
        const int PersonsCount = 100;
        const int MaxOwnersAmount = 2;
        #endregion

        #region TABLES
        public static List<IMark> Marks = new List<IMark>()
        {
            new Mark() { Name = "Toyota" },     //Id = 1
            new Mark() { Name = "BMW" },        //Id = 2
            new Mark() { Name = "Mercedes" }    //Id = 3
        };
        public static List<IModel> CarModels = new List<IModel>()
        {
            new CarModel() { MarkId =1, Name="Camry", BodyType = CarBodyType.Sedan, Maker = "Toyota Ukraine", Year = 2020 },    //id=1
            new CarModel() { MarkId =1, Name="Rav4", BodyType = CarBodyType.MUV, Maker = "Toyota Ukraine", Year = 2019 },       //id=2
            new CarModel() { MarkId =1, Name="Rav3", BodyType = CarBodyType.MUV, Maker = "Toyota Ukraine", Year = 2021 },       //id=3
            new CarModel() { MarkId =2, Name="GLA SUV", BodyType = CarBodyType.MUV, Maker = "BMW Ukraine", Year = 2017 },       //id=4
            new CarModel() { MarkId =2, Name="GLB SUV", BodyType = CarBodyType.MUV, Maker = "BMW Ukraine", Year = 2018 },       //id=5
            new CarModel() { MarkId =2, Name="GLC SUV", BodyType = CarBodyType.MUV, Maker = "BMW Ukraine", Year = 2018 },       //id=6
            new CarModel() { MarkId =3, Name="i4", BodyType = CarBodyType.Sedan, Maker = "Mercedes Ukraine", Year = 2019 },     //id=7
            new CarModel() { MarkId =3, Name="i4 M50", BodyType = CarBodyType.Sedan, Maker = "Mercedes Ukraine", Year = 2020 }, //id=8
            new CarModel() { MarkId =3, Name="iX", BodyType = CarBodyType.MUV, Maker = "Mercedes Ukraine", Year = 2021 },       //id=9
        };
        public static List<IVehicle> Vehicles = new List<IVehicle>();
        public static List<IAddress> Addresses = new List<IAddress>();
        public static List<IPerson> Persons = new List<IPerson>();
        public static List<IRecord> OwnersRecords = new List<IRecord>();
        public static List<IRecord> DriversRecords = new List<IRecord>();
        #endregion

        public static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.UTF8;

            InitializeAllTables();

            SerializeXML();

            CreateXML();

            Persons.Foreach(person => Console.WriteLine(string.Join(" ", person.Surname, person.Name, person.Patronymic)));

            //OuputXML();

            XDocument documentPerson = XDocument.Load("XML\\Persons.xml");
            XDocument documentAddress = XDocument.Load("XML\\Addresses.xml");
            XDocument documentOwnersRec = XDocument.Load("XML\\OwnersRecords.xml");
            XDocument documentDriversRec = XDocument.Load("XML\\DriversRecords.xml");
            XDocument documentVehicle = XDocument.Load("XML\\Vehicles.xml");
            XDocument documentModel = XDocument.Load("XML\\CarModels.xml");
            XDocument documentMark = XDocument.Load("XML\\Marks.xml");

            #region LINQ QUARIES
            //Запит для виведення усіх людей з прізвищем Ткаченко
            Console.WriteLine("\n1 Запит");
            var q1 = from person in documentPerson.Root.Elements("Person")
                     where person.Element("Surname").Value == "Ткаченко"
                     select person;

            q1.Foreach(person => Console.WriteLine(string.Join(" ", person.Element("Name").Value, person.Element("Surname").Value)));

            //Запит для виведення усіх людей з таблиці Person, які народились раніше 1990
            Console.WriteLine("\n2 Запит");

            var q2 = from person in documentPerson.Root.Elements("Person")
                     where Convert.ToDateTime(person.Element("DateOfBirth").Value).Year < 1990
                     select person;

            q2.Foreach(person =>
            Console.WriteLine(string.Join(" ", person.Element("Name").Value, person.Element("Surname").Value,
            person.Element("DateOfBirth").Value.Split(' ')[0])));

            //Запит, що виводить ім'я людини в алфавітному порядку
            Console.WriteLine("\n3 Запит");
            documentPerson.Root.Elements("Person")
                .Select(person => string.Join(" ", person.Element("Surname").Value,
                 person.Element("Name").Value, person.Element("Patronymic").Value))
                .OrderBy(name => name)
                .Foreach(name => Console.WriteLine(name));

            //Запит для підрахунку віку кожної людини
            Console.WriteLine("\n4 Запит");
            documentPerson.Root.Elements("Person")
                .Select(person =>
                new
                {
                    Name = string.Join(" ", person.Element("Surname").Value,
                    person.Element("Name").Value),
                    Age = GetAge(Convert.ToDateTime(person.Element("DateOfBirth").Value))
                })
                .Foreach(person => Console.WriteLine(person.Name + " " + person.Age));

            //Запит для підрахунку середнього віку усіх людей
            Console.WriteLine("\n5 Запит");
            double average = documentPerson.Root.Elements("Person")
                .Average(person => GetAge(Convert.ToDateTime(person.Element("DateOfBirth").Value)));

            Console.WriteLine(average);

            //Запит для виведення людей, вік яких менший за середній вік всіх людей
            Console.WriteLine("\n6 Запит");
            documentPerson.Root.Elements("Person")
                .Where(person => GetAge(Convert.ToDateTime(person.Element("DateOfBirth").Value)) < average)
                .Select(person =>
                new
                {
                    Name = string.Join(" ", person.Element("Surname").Value,
                    person.Element("Name").Value),
                    Age = GetAge(Convert.ToDateTime(person.Element("DateOfBirth").Value))
                })
                .Foreach(person => Console.WriteLine(person.Name + " " + person.Age));

            //Запит для виведення тих людей, які є власниками транспортних засобів
            Console.WriteLine("\n7 Запит");
            documentOwnersRec.Root.Elements("OwnersRecord")
                .Select(record => int.Parse(record.Element("PersonId").Value))
                .Distinct()
                .OrderBy(personId => personId)
                .Join(documentPerson.Root.Elements("Person"),
                personId => personId,
                person => int.Parse(person.Element("Id").Value),
                (personId, person) =>
                new
                {
                    Id = personId,
                    Name = person.Element("Surname").Value + " " +
                    person.Element("Name").Value
                })
                .Foreach(person => Console.WriteLine(person.Id + " " + person.Name));


            //Запит для виведення тих людей, які є водіями транспортних засобів
            Console.WriteLine("\n8 Запит");
            documentDriversRec.Root.Elements("DriversRecord")
                .Select(record => int.Parse(record.Element("PersonId").Value))
                .Distinct()
                .OrderBy(personId => personId)
                .Join(documentPerson.Root.Elements("Person"),
                personId => personId,
                person => int.Parse(person.Element("Id").Value),
                (personId, person) =>
                new
                {
                    Id = personId,
                    Name = person.Element("Surname").Value + " " +
                    person.Element("Name").Value
                })
                .Foreach(person => Console.WriteLine(person.Id + " " + person.Name));

            //Запит для виведення тих людей, які є і власниками і водіями транспортних засобів
            Console.WriteLine("\n9 Запит");
            documentOwnersRec.Root.Elements("OwnersRecord")
                .Select(record => int.Parse(record.Element("PersonId").Value))
                .Distinct()
                .Intersect(
                    documentDriversRec.Root.Elements("DriversRecord")
                    .Select(record => int.Parse(record.Element("PersonId").Value))
                    .Distinct()
                )
                .OrderBy(personId => personId)
                .Join(documentPerson.Root.Elements("Person"),
                personId => personId,
                person => int.Parse(person.Element("Id").Value),
                (personId, person) =>
                new
                {
                    Id = personId,
                    Name = person.Element("Surname").Value + " " +
                    person.Element("Name").Value
                })
                .Foreach(person => Console.WriteLine(person.Id + " " + person.Name));

            //Запит для підрахунку кількості транспортних засобів у володінні власників
            Console.WriteLine("\n10 Запит");
            documentOwnersRec.Root.Elements("OwnersRecord")
                .Select(record =>
                new
                {
                    PersonId = int.Parse(record.Element("PersonId").Value),
                    VehicleId = int.Parse(record.Element("VehicleId").Value),
                })
                .GroupBy(record => record.PersonId)
                .Join(documentPerson.Root.Elements("Person"),
                group => group.Key,
                person => int.Parse(person.Element("Id").Value),
                (group, person) =>
                new
                {
                    Name = person.Element("Surname").Value + " " +
                    person.Element("Name").Value,
                    VehicleAmount = group.Count()
                })
                .Foreach(person => Console.WriteLine(person.Name + " - " + person.VehicleAmount));

            //Запит для виведення VIN коду транспортних засобів у кожного власника
            Console.WriteLine("\n11 Запит");
            documentOwnersRec.Descendants("OwnersRecord")
                .GroupBy(record => record.Element("PersonId").Value)
                .Foreach(group =>
                {
                    XElement person = documentPerson.Descendants("Person")
                    .First(p => p.Element("Id").Value == group.Key);

                    Console.WriteLine(string.Join(" ", person.Element("Surname").Value,
                        person.Element("Name").Value, person.Element("Patronymic").Value));

                    documentOwnersRec.Descendants("OwnersRecord")
                    .Where(record => record.Element("PersonId").Value == group.Key)
                    .Foreach(record =>
                    {
                        XElement vehicleId = record.Element("VehicleId");

                        XElement vehicle = documentVehicle.Descendants("Vehicle")
                        .First(v => v.Element("Id").Value == vehicleId.Value);

                        Console.WriteLine("\t" + string.Join(" ", vehicle.Element("VINCode").Value));
                    });
                });

            //Запит для виведення всіх людей, що живуть на бульварі Лесі Українки
            Console.WriteLine("\n12 Запит");
            documentPerson.Descendants("Person")
                .Join(documentAddress.Descendants("Address"),
                person => int.Parse(person.Element("AddressId").Value),
                address => int.Parse(address.Element("Id").Value),
                (person, address) => new
                {
                    Name = string.Join(" ", person.Element("Surname").Value,
                    person.Element("Name").Value),
                    Address = address.Element("Street").Value + ", " +
                    address.Element("House").Value
                })
                .Where(person => person.Address.Contains("бул.Лесі Українки"))
                .Foreach(person => Console.WriteLine(person.Name + " - " + person.Address));

            //Запит для виведення усіх транспортних засобів з імнформацією про марку, модель та VINCode
            Console.WriteLine("\n13 Запит");
            var vehicles = documentVehicle.Descendants("Vehicle")
                .Select(vehicle =>
                new
                {
                    Id = int.Parse(vehicle.Element("Id").Value),
                    ModelId = int.Parse(vehicle.Element("ModelId").Value),
                    VINCode = vehicle.Element("VINCode").Value,
                });

            var models = documentModel.Descendants("CarModel")
                .Select(model =>
                new
                {
                    Id = int.Parse(model.Element("Id").Value),
                    Name = model.Element("Name").Value,
                    MarkId = int.Parse(model.Element("MarkId").Value)
                });

            var marks = documentMark.Descendants("Mark")
                .Select(mark =>
                new
                {
                    Id = int.Parse(mark.Element("Id").Value),
                    Name = mark.Element("Name").Value,
                });

            vehicles.Join(models,
                vehicle => vehicle.ModelId,
                model => model.Id,
                (vehicle, model) =>
                new
                {
                    Id = vehicle.Id,
                    MarkId = model.MarkId,
                    Model = model.Name,
                    VINCode = vehicle.VINCode
                })
                .Join(marks,
                vehicle => vehicle.MarkId,
                mark => mark.Id,
                (vehicle, mark) =>
                new
                {
                    Id = vehicle.Id,
                    Mark = mark.Name,
                    Model = vehicle.Model,
                    VINCode = vehicle.VINCode
                })
                .Foreach(vehicle =>
                Console.WriteLine(string.Join(" ", vehicle.Id, vehicle.Mark, vehicle.Model, vehicle.VINCode)));

            //Запит для виведення ієрархії XML документу від кореню до елемента ім'я людини
            Console.WriteLine("\n14 Запит");

            int count = 0;

            documentPerson.Descendants("Name").First().AncestorsAndSelf().Reverse()
                .Foreach(element => Console.WriteLine(Tabs(count++) + $"-{element.Name}"));

            //Запит для виведення повної ієрархії XML документу
            Console.WriteLine("\n15 Запит");
            documentPerson.Root.DescendantsAndSelf()
                               .Select(element => element.Name)
                               .Distinct()
                               .Foreach(element =>
                               {
                                   int tabs = documentPerson.Descendants(element).First().Ancestors().Count();
                                   Console.WriteLine(Tabs(tabs) + "-" + element);
                               });
            #endregion

            Console.ReadLine();
        }
        public static void InitializeAllTables()
        {
            InitializeVehicles();
            InitializeAddress();
            EnterPersons();
            InitializeOwnersRecords();
            InitializeDriversRecords();
        }
        public static void InitializeVehicles()
        {
            foreach(var model in CarModels)
            {
                for(int i = 0; i < 3; i++)
                {
                    IVehicle vehicle = (Vehicle)generator.Generate<Vehicle>();
                    vehicle.ModelId = model.Id;
                    Vehicles.Add(vehicle);
                }
            }
        }
        public static void InitializeAddress()
        {
            for(int i = 0; i < PersonsCount; i++)
            {
                IAddress address;

                do
                {
                    address = (Address)generator.Generate<Address>();
                    address.Id = i + 1;
                }
                while(Addresses.Any(a => a.Street == address.Street && a.House == address.House));
                
                Addresses.Add(address);
            }
        }
        public static void InitializePersons()
        {
            for(int i = 0; i < PersonsCount; i++)
            {
                IPerson person = (Person)generator.Generate<Person>();
                person.AddressId = i + 1;
                Persons.Add(person);
            }
        }
        public static void InitializeOwnersRecords()
        {
            foreach(var vehicle in Vehicles)
            {
                int ownersAmount = random.Next(1, MaxOwnersAmount + 1);

                for(int i = 0; i < ownersAmount; i++)
                {
                    int ownerId = random.Next(1, Persons.Count + 1);

                    IRecord record = new Record()
                    {
                        VehicleId = vehicle.Id,
                        PersonId = ownerId
                    };
                    if(!OwnersRecords.Contains(record))
                        OwnersRecords.Add(record);
                }
            }
        }
        public static void InitializeDriversRecords()
        {
            for(int i = 0; i < Persons.Count; i++)
            {
                int vehicleId = i % Vehicles.Count + 1;

                IRecord record = new Record()
                {
                    VehicleId = vehicleId,
                    PersonId = i+1
                };

                DriversRecords.Add(record);
            }
        }
        public static void EnterPersons()
        {
            Console.WriteLine("Enter list of 100 persons:");
            for (int i = 0; i < PersonsCount; i++)
            {
                IPerson person = (Person)generator.Generate<Person>();

                string[] fullName = Console.ReadLine().Split(' ');

                person.Surname = fullName[0];
                person.Name = fullName[1];
                person.Patronymic = fullName[2];

                person.AddressId = i + 1;
                Persons.Add(person);
            }
        }
        public static int GetAge(DateTime date)
        {
            DateTime now = DateTime.Now;

            int age = now.Year - date.Year;

            if (now.Month < date.Month)
                age--;
            else if (now.Month == date.Month && now.Day < date.Day)
                age--;

            return age;
        }
        public static string Tabs(int number)
        {
            StringBuilder sb = new StringBuilder();

            for(int i = 0; i < number; i++)
                sb.Append(' ');

            return sb.ToString();
        }
        public static void CreateXML()
        {
            XMLRedactor.Create(Marks, "Marks");
            XMLRedactor.Create(CarModels, "CarModels");
            XMLRedactor.Create(Vehicles, "Vehicles");
            XMLRedactor.Create(Addresses, "Addresses");
            XMLRedactor.Create(Persons, "Persons");
            XMLRedactor.Create(OwnersRecords, "OwnersRecords");
            XMLRedactor.Create(DriversRecords, "DriversRecords");
        }
        public static void SerializeXML()
        {
            //Persons

            //Serialize
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Person>));

            List<Person> persons = Persons.Select(person => (Person)person).ToList();

            using (TextWriter writer = File.CreateText($"XML\\Serialized\\Persons.xml"))
            {
                xmlSerializer.Serialize(writer, persons);
            }

            //Deserialize
            List<Person> newPersons;
            using (TextReader reader = File.OpenText($"XML\\Serialized\\Persons.xml"))
            {
                newPersons = (List<Person>)xmlSerializer.Deserialize(reader);
            }

            Console.WriteLine("\nDeserialized persons:");
            newPersons.Foreach(person => Console.WriteLine(string.Join(" ", person.Surname, person.Name,
                person.Patronymic)));
        }
        public static void OuputXML()
        {
            XMLRedactor.OutputWithLinq("Marks");
            XMLRedactor.OutputWithXmlDocument("CarModels");
            XMLRedactor.OutputWithLinq("Vehicles");
            XMLRedactor.OutputWithXmlDocument("Addresses");
            XMLRedactor.OutputWithLinq("Person");
            XMLRedactor.OutputWithXmlDocument("OwnersRecords");
            XMLRedactor.OutputWithLinq("DriversRecords");
        }
    }
}