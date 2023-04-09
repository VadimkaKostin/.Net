using Lab2.Interfaces;
using Lab2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Generators
{
    /// <summary>
    /// Клас для генерації об'єктів IVehicle, IPerson та IAddress.
    /// </summary>
    public sealed class Generator
    {
        private Random random = new Random();

        #region VEHICLES PROPERTIES
        public List<string> Colors = new List<string>()
        {
            "червоний", "жовтий", "зелений",
            "золотистий", "срібний", "блакитний"
        };
        private string GetRandomNumber()
        {
            string number = random.Next(1111, 9999).ToString();

            return "AA " + number + " AK";
        }
        private string GetRandomVINCode()
        {
            return Guid.NewGuid().ToString();
        }
        private string GetRandomColor()
        {
            return Colors[random.Next(Colors.Count)];
        }
        #endregion

        #region PERSONS PROPERTIES
        public List<string> Names = new List<string>()
        {
            "Вадим", "Марк", "Максім", "Дмитро", "Сергій", "Валерій", "Олексій",
            "Григорій", "Борис", "Владислав", "Богдан", "Гліб", "Петро", "Федір",
            "Володимир", "Анатолійович", "Олександр", "Ілля", "Кирило", "Данило", "Євген"
        };
        public List<string> Surnames = new List<string>()
        {
            "Шевченко", "Василенко", "Овчаренко", "Борисенко", "Ткаченко",
            "Федоренко", "Терещенко", "Яковенко", "Тимошенко", "Коваленко",
            "Бондаренко", "Проценко", "Пархоменко", "Єременко", "Кириленко",
            "Романченко", "Симоненко", "Савенко", "Іваненко", "Лукашенко",
            "Петренко", "Павленко", "Іващенко", "Скляренко", "Кравченко",
            "Кулішенко", "Захарченко", "Пономаренко", "Кузьменко", "Ноженко",
            "Малюченко", "Худенко", "Ярошенко", "Яценко", "Солодченко", "Порошенко",
            "Синенко", "Федорченко", "Гейченко", "Гуліченко", "Марченко", "Мартиненко"
        };
        public List<string> Patronymes = new List<string>()
        {
            "Вадимович", "Маркович", "Максимович", "Дмитрович", "Сергійович", "Валерійович",
            "Олексійович", "Григорійович", "Борисович", "Владиславович", "Богданович",
            "Глібович", "Петрович", "Федорович", "Володимирович", "Анатолійович",
            "Олександрович", "Ілліч", "Кирилович", "Данилович", "Євгенович"
        };

        private string GetRandomName()
        {
            return Names[random.Next(Names.Count)] + " " +
                Surnames[random.Next(Surnames.Count)] + " " +
                Patronymes[random.Next(Patronymes.Count)];
        }
        private DateTime GetRandomDate()
        {
            int year = random.Next(1980, 2000);
            int month = random.Next(1, 12);
            int day = random.Next(1, 28);

            return Convert.ToDateTime($"{year}-{month}-{day}");
        }
        private string GetRandomDriverRightsNumber()
        {
            return "CXE" + random.Next(111111, 999999).ToString();
        }
        #endregion

        #region ADDRESS PROPERTIES
        public List<string> Streets = new List<string>()
        {
            "просп.Берестейський",
            "бул.Тараса Шевченка",
            "бул.Лесі Українки",
            "вул.Велика Васильківська",
            "вул.Антоновича",
            "вул.Саксаганського",
            "вул.Жилянського",
            "вул.Святошинська",
            "просп.Голосіївьский",
            "вул.Хрещатик",
            "вул.Старонаводницька",
            "вул.Дніпровська небережна",
            "вул.Миколи Бажана",
            "вул.Броварська",
        };
        private string GetRandomStreet()
        {
            return Streets[random.Next(Streets.Count)];
        }
        #endregion

        /// <summary>
        /// Метод для генерації об'єктів IVehicle, IPerson та IAddress.
        /// </summary>
        /// <typeparam name="T">Інтерфейс, об'єкт якого треба згенерувати.</typeparam>
        /// <returns>Повертає згенерований об'єкт обраного типу.</returns>
        public IIdentifiable Generate<T>() where T : IIdentifiable, new()
        {
            if(typeof(T).GetInterfaces().Contains(typeof(IVehicle)))
            {
                return GenerateVehicle();
            }
            else if(typeof(T).GetInterfaces().Contains(typeof(IPerson)))
            {
                return GeneratePerson();
            }
            else if(typeof(T).GetInterfaces().Contains(typeof(IAddress)))
            {
                return GenerateAddress();
            }
            else
            {
                throw new ArgumentException();
            }
        }

        private IVehicle GenerateVehicle()
        {
            string VINCode = GetRandomVINCode();
            string number = GetRandomNumber();
            string color = GetRandomColor();

            return new Vehicle()
            {
                VINCode = VINCode,
                Color = color,
                Number = number,
                State = "задовільний"
            };
        }
        private IPerson GeneratePerson()
        {
            string[] fullName = this.GetRandomName().Split(' ');

            string name = fullName[0];
            string surname = fullName[1];
            string patronymic = fullName[2];

            string driverRights = this.GetRandomDriverRightsNumber();
            DateTime dateOfBirth = this.GetRandomDate();

            return new Person()
            {
                Name = name,
                Surname = surname,
                Patronymic = patronymic,
                DriverRights = driverRights,
                DateOfBirth = dateOfBirth
            };
        }
        private IAddress GenerateAddress()
        {
            string street = this.GetRandomStreet();
            int house = random.Next(1, 31);

            return new Address() { City = "Київ", Street = street, House = house };
        }
    }
}
