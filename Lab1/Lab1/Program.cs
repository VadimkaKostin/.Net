using Lab1.Models;
using Lab1.ExtantionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1
{
    public class Program
    {
        public static Random random = new Random();

        public static VehicleGenerator vehicleGenerator = new VehicleGenerator();
        public static PersonGenerator personGenerator = new PersonGenerator();

        #region TABLES
        public static List<IMark> Marks = new List<IMark>()
        {
            new Mark("Toyota"),         //id=1
            new Mark("Mercedes-Benz"),  //id=2
            new Mark("BMW"),            //id=3
        };
        public static List<IModel> CarModels = new List<IModel>()
        {
            new CarModel(1, "Camry", VehicleBodyType.Sedan, "Toyota Ukraine", 2020),    //id=1
            new CarModel(1, "Rav4", VehicleBodyType.MUV, "Toyota Ukraine", 2019),       //id=2
            new CarModel(1, "Rav3", VehicleBodyType.MUV, "Toyota Ukraine", 2021),       //id=3
            new CarModel(2, "GLA SUV", VehicleBodyType.MUV, "Mercedes Ukraine", 2017),  //id=4
            new CarModel(2, "GLB SUV", VehicleBodyType.MUV, "Mercedes Ukraine", 2018),  //id=5
            new CarModel(2, "GLC SUV", VehicleBodyType.MUV, "Mercedes Ukraine", 2018),  //id=6
            new CarModel(3, "i4", VehicleBodyType.Sedan, "Mercedes Ukraine", 2019),     //id=7
            new CarModel(3, "i4 M50", VehicleBodyType.Sedan, "Mercedes Ukraine", 2020), //id=8
            new CarModel(3, "iX", VehicleBodyType.MUV, "Mercedes Ukraine", 2021),       //id=9
        };
        public static List<IPerson> Owners = new List<IPerson>();
        public static List<IVehicle> Vehicles = new List<IVehicle>();
        public static List<IDriver> Drivers = new List<IDriver>();
        #endregion

        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            InitializeAllTables();

            DisplayAllTables();

            #region LINQ QUERIES
            //Запит для фільрування водіїв по даті народження
            Console.WriteLine("1 Запит:");

            Drivers.Where(driver => driver.DateOfBirth.Year < 1990)
                   .Foreach(driver => Console.WriteLine("{0} {1}", driver.FullName, driver.DateOfBirth.ToShortDateString()));
            //==========================================================================

            //Запит для фільтрування транспортних засобів по даті випуску моделей
            Console.WriteLine("\n2 Запит:");

            var NewestVehicles = from vehicle in Vehicles
                                 join model in CarModels
                                 on vehicle.ModelId equals model.Id
                                 where model.Year >= 2020
                                 select vehicle;

            NewestVehicles.Join(CarModels,
                                vehicle => vehicle.ModelId,
                                model => model.Id,
                                (vehicle, model) =>
                                new
                                {
                                    vehicle.Id,
                                    model.Year
                                })
                          .Foreach(vehicle => Console.WriteLine("{0} {1}", vehicle.Id, vehicle.Year));
            //==========================================================================

            //Запит для фільтрування транспортних засобів по марці автомобіля
            Console.WriteLine("\n3 Запит:");
            var ToyotaVehicles = from vehicle in Vehicles
                                 join model in CarModels
                                 on vehicle.ModelId equals model.Id
                                 join mark in Marks
                                 on model.MarkId equals mark.Id
                                 where mark.Name == "Toyota"
                                 select vehicle;

            ToyotaVehicles.Join(CarModels,
                                vehicle => vehicle.ModelId,
                                model => model.Id,
                                (vehicle, model) =>
                                new
                                {
                                    vehicle.Id,
                                    model.Model,
                                    model.MarkId
                                })
                          .Join(Marks,
                                vehicle => vehicle.MarkId,
                                mark => mark.Id,
                                (vehicle, mark) =>
                                new
                                {
                                    Id = vehicle.Id,
                                    Mark = mark.Name,
                                    Model = vehicle.Model
                                })
                          .Foreach(vehicle => Console.WriteLine("{0} {1} {2}", vehicle.Id, vehicle.Mark, vehicle.Model));
            //==========================================================================

            //Запит сортування власників транспортних засобів за датою народження
            Console.WriteLine("\n4 Запит:");

            var ownersSorted = from owner in Owners
                               orderby owner.DateOfBirth
                               select new { Name = owner.Name, DateOfBirth = owner.DateOfBirth };

            ownersSorted.Foreach(owner =>
            Console.WriteLine($"{owner.Name} {owner.DateOfBirth.ToString().Split(' ')[0]}"));
            //===========================================================================

            //Запит сортування моделей авто по року випуску
            Console.WriteLine("\n5 Запит:");

            CarModels.OrderByDescending(model => model.Year)
                     .ThenBy(model => model.Model)
                     .Select(model => new { Name = model.Model, Year = model.Year })
                     .Foreach(model => Console.WriteLine($"{model.Name} {model.Year}"));
            //===========================================================================

            //Запиит об'єднання моделей та марок в одну таблицю
            Console.WriteLine("\n6 Запит:");
            var ModelsWithMarks = from model in CarModels
                                  join mark in Marks
                                  on model.MarkId equals mark.Id
                                  select new
                                  {
                                      Id = model.Id,
                                      Mark = mark.Name,
                                      Name = model.Model,
                                      BodyType = model.BodyType,
                                      Maker = model.Maker,
                                      Year = model.Year
                                  };

            ModelsWithMarks.Foreach(model =>
            Console.WriteLine($"{model.Id} {model.Mark} {model.Name} {model.BodyType} {model.Maker}" +
            $"{model.Year}"));
            //===========================================================================

            //Запит для об'єднання транспортних засобів із моделями
            Console.WriteLine("\n7 Запит:");
            var VehiclesWithModels = Vehicles.Join(ModelsWithMarks,
                                                   vehicle => vehicle.ModelId,
                                                   model => model.Id,
                                                   (vehicle, model) =>
                                                   new
                                                   {
                                                       Id = vehicle.Id,
                                                       Mark = model.Mark,
                                                       Model = model.Name,
                                                       BodyType = model.BodyType,
                                                       Maker = model.Maker,
                                                       Year = model.Year,
                                                       VINCode = vehicle.VINCode,
                                                       Color = vehicle.Color,
                                                       Number = vehicle.Number,
                                                       State = vehicle.State,
                                                       OwnerId = vehicle.OwnerId
                                                   });

            VehiclesWithModels.Foreach(vehicle =>
            Console.WriteLine($"{vehicle.Id} {vehicle.Mark} {vehicle.Model} {vehicle.BodyType} " +
            $"{vehicle.Maker} {vehicle.Year} {vehicle.VINCode} {vehicle.Color} {vehicle.Number} " +
            $"{vehicle.State} {vehicle.OwnerId}"));
            //===========================================================================

            //Запит для об'єднання трансопртних засобів і їх власників
            Console.WriteLine("\n8 Запит:");
            VehiclesWithModels.Join(Owners,
                                    vehicle => vehicle.OwnerId,
                                    owner => owner.Id,
                                    (vehicle, owner) =>
                                    new
                                    {
                                        Id = vehicle.Id,
                                        Mark = vehicle.Mark,
                                        Model = vehicle.Model,
                                        BodyType = vehicle.BodyType,
                                        Maker = vehicle.Maker,
                                        Year = vehicle.Year,
                                        VINCode = vehicle.VINCode,
                                        Color = vehicle.Color,
                                        Number = vehicle.Number,
                                        State = vehicle.State,
                                        Owner = owner.Name + " " + owner.Surname + " " + owner.Patronymic
                                    })
                             .Foreach(vehicle =>
                             Console.WriteLine($"{vehicle.Id} {vehicle.Mark} {vehicle.Model} {vehicle.BodyType} " +
                             $"{vehicle.Maker} {vehicle.Year} {vehicle.VINCode} {vehicle.Color} {vehicle.Number} " +
                             $"{vehicle.State} {vehicle.Owner}"));
            //===========================================================================

            //Запит для підрахунку кількості водіїв на кожний траспорт
            Console.WriteLine("\n9 Запит:");

            Drivers.GroupBy(driver => driver.VehicleId)
                   .Foreach(group =>
                   {
                       Console.WriteLine("Vehicle: {0} Drivers: {1}", group.Key, group.Count());
                   });
            //===========================================================================

            //Запит для виведення транспортів та їх водіїв
            Console.WriteLine("\n10 Запит:");

            var vehiclesDriversAmountQuerry = from driver in Drivers
                                              group driver by driver.VehicleId;

            vehiclesDriversAmountQuerry.Foreach(group =>
            {
                Console.Write("Vehicle: " + group.Key + " Drivers: ");
                group.Foreach(driver => Console.Write(driver.FullName + " | "));
                Console.WriteLine();
            });
            //===========================================================================

            //Запит для підрахунку кількості транспортних засобів у кожного власника
            Console.WriteLine("\n11 Запит:");

            VehiclesWithModels.GroupBy(vehicle => vehicle.OwnerId)
                .Join(Owners, group => group.Key, owner => owner.Id, (group, owner) =>
                new
                {
                    Id = group.Key,
                    Name = owner.FullName,
                    Amount = group.Count()
                })
                .OrderByDescending(owner => owner.Amount)
                .ThenBy(owner => owner.Name)
                .Foreach(owner => Console.WriteLine("Owner: {0} {1} Vehicles: {2}", owner.Id, owner.Name, owner.Amount));
            //===========================================================================

            //Запит для підрахунку кількості власників у кожного тарнспортного засобу
            Console.WriteLine("\n12 Запит:");

            VehiclesWithModels.GroupBy(vehicle => vehicle.Id)
                              .Select(group => new { Id = group.Key, Amount = group.Count() })
                              .Foreach(vehicle => Console.WriteLine("Vehicle: {0} Owners: {1}", vehicle.Id, vehicle.Amount));
            //===========================================================================

            //Запит для виведення перших 5 власників із найбільшою кількістю
            //транспортних засобів
            Console.WriteLine("\n13 Запит:");

            int counter = 1;

            VehiclesWithModels.GroupBy(vehicle => vehicle.OwnerId)
                .Select(group => new { Id = group.Key, Amount = group.Count() })
                .Join(Owners, group => group.Id, owner => owner.Id,
                (group, owner) => new
                {
                    Id = owner.Id,
                    Name = owner.FullName,
                    Amount = group.Amount
                })
                .OrderByDescending(owner => owner.Amount)
                .Take(5)
                .Foreach(owner =>
                {
                    Console.WriteLine(counter++ + ")" + owner.Name + " " + owner.Amount);
                });
            //===========================================================================

            //Запит для виведення другого за алфавітом водія
            Console.WriteLine("\n14 Запит:");
            var SecondDriver = Drivers.OrderBy(driver => driver.FullName)
                                .Take(2)
                                .Skip(1)
                                .First();

            Console.WriteLine(SecondDriver.FullName);
            //===========================================================================

            //Запит для виведення транспортних засобів, у яких більше 3 водіїв
            Console.WriteLine("\n15 Запит:");
            var VehiclesWithDrivers =
                from groupDriver in
                    (from driver in Drivers group driver by driver.VehicleId)
                where groupDriver.Count() > 3
                join groupVehicle in (from vehicle in VehiclesWithModels group vehicle by vehicle.Id)
                on groupDriver.Key equals groupVehicle.Key
                let Amount = groupDriver.Count()
                orderby Amount descending
                select new
                {
                    Id = groupVehicle.Key,
                    Amount
                };

            VehiclesWithDrivers.Foreach(vehicle =>
            {
                Console.WriteLine($"Vehicle:{vehicle.Id} Drivers: {vehicle.Amount}");
            });
            //===========================================================================

            //Запит для об'єднання колекцій автомобілів з 
            //фільтуванням по марці та року випуску моделі
            Console.WriteLine("\n16 Запит:");
            ToyotaVehicles.Union(NewestVehicles)
                          .OrderBy(vehicle => vehicle.Id)
                          .Foreach(vehicle => Console.WriteLine(vehicle.Id));
            //========================================================

            //Запит для об'єднання колекцій автомобілів з 
            //фільтуванням по марці та року випуску моделі
            //через конкатенацію та видалення дублікатів.
            //Результат має вийти той самий, що і в минулому запиті
            Console.WriteLine("\n17 Запит:");
            ToyotaVehicles.Concat(NewestVehicles)
                          .Distinct()
                          .OrderBy(vehicle => vehicle.Id)
                          .Foreach(vehicle => Console.WriteLine(vehicle.Id));
            //========================================================

            //Запит для перевірки чи є хочаб один водій який живе на певній вулиці
            Console.WriteLine("\n18 Запит:");
            bool result = Drivers.Any(driver =>
            driver.PersonAddress.Street == "вул.Велика Васильківська");

            Console.WriteLine(result);
            //========================================================

            //Запит для перевірки чи всі власники народились до 2000 року
            Console.WriteLine("\n19 Запит:");
            result = Owners.All(owner => owner.DateOfBirth.Year < 2000);

            Console.WriteLine(result);
            //========================================================

            //Запит для підрахунку середнього віку усіх водіїв
            Console.WriteLine("\n20 Запит:");
            double average = Drivers.Average(driver => GetAge(driver.DateOfBirth));

            Console.WriteLine(Math.Round(average, 2));
            #endregion

            Console.ReadLine();
        }

        #region INITIALIZE METHODS
        public static void InitializeOwners()
        {
            const int ownersCount = 12;

            for(int i=0; i<ownersCount; i++)
            {
                Person owner = (Person)personGenerator.Generate();
                Owners.Add(owner);
            }
        }
        public static void InitializeVehicles()
        {
            const double propabilityOfSecondOwner = 0.1;

            foreach(var model in CarModels)
            {
                for(int i = 0; i < 3; i++)
                {
                    int ownerId = random.Next(1, Owners.Count + 1);

                    Vehicle vehicle = vehicleGenerator.Generate(model.Id, ownerId);
                    Vehicles.Add(vehicle);

                    if(random.NextDouble() < propabilityOfSecondOwner)
                    {
                        while (ownerId == vehicle.OwnerId)
                            ownerId = random.Next(1, Owners.Count + 1);

                        Vehicle vehicleCopy = vehicle.Clone();

                        vehicleCopy.OwnerId = ownerId;

                        Vehicles.Add(vehicleCopy);
                    }
                }
            }

            EnsureAllOwnersHaveVehicles();
        }
        public static void EnsureAllOwnersHaveVehicles()
        {
            var ownersHavingVehiclesId = Vehicles.Select(vehicle => vehicle.OwnerId).ToList();

            foreach (var owner in Owners)
            {
                if (!ownersHavingVehiclesId.Contains(owner.Id))
                {
                    int modelId = random.Next(1, CarModels.Count + 1);
                    Vehicle vehicle = vehicleGenerator.Generate(modelId, owner.Id);
                    Vehicles.Add(vehicle);
                }
            }
        }
        public static void InicializeDrivers()
        {
            foreach(var vehicle in Vehicles)
            {
                int driversAmount = random.Next(1, 6);

                for(int i = 0; i < driversAmount; i++)
                {
                    Driver driver = (Driver)personGenerator.Generate(vehicle.Id);
                    Drivers.Add(driver);
                }
            }
        }
        public static void InitializeAllTables()
        {
            InitializeOwners();
            InitializeVehicles();
            InicializeDrivers();
        }
        #endregion

        public static void DisplayAllTables()
        {
            Console.WriteLine("Marks:");
            Marks.ForEach(mark => Console.WriteLine(mark.ToString()));
            Console.WriteLine();

            Console.WriteLine("Models");
            CarModels.ForEach(model => Console.WriteLine(model.ToString()));
            Console.WriteLine();

            Console.WriteLine("Owners:");
            Owners.ForEach(owner => Console.WriteLine(owner.ToString()));
            Console.WriteLine();

            Console.WriteLine("Vehicles:");
            Vehicles.ForEach(vehicle => Console.WriteLine(vehicle.ToString()));
            Console.WriteLine();

            Console.WriteLine("Drivers:");
            Drivers.ForEach(driver => Console.WriteLine(driver.ToString()));
            Console.WriteLine();
        }
        public static int GetAge(DateTime date)
        {
            DateTime now = DateTime.Now;

            int age = now.Year - date.Year;

            if (now.Month < date.Month)
                age--;
            else if(now.Month == date.Month && now.Day < date.Day)
                age--;

            return age;
        }
    }
}