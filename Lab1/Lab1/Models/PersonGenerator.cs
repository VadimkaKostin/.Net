using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Models
{
    public sealed class PersonGenerator
    {
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

        private string[] _fullName;
        private string _dateOfBirth;
        private string _driverRightsNumber;
        private string _street;
        private string _house;

        private void InitializeRandomParametrs()
        {
            _fullName = GetRandomName().Split(' ');
            _dateOfBirth = GetRandomDate();
            _driverRightsNumber = GetRandomDriverRightsNumber();
            _street = GetRandomStreet();
            _house = random.Next(31).ToString();
        }

        private Random random = new Random();
        public string GetRandomName()
        {
            return Names[random.Next(Names.Count)] + " " +
                Surnames[random.Next(Surnames.Count)] + " " +
                Patronymes[random.Next(Patronymes.Count)];
        }
        public string GetRandomDate()
        {
            int year = random.Next(1980, 2000);
            int month = random.Next(1, 12);
            int day = random.Next(1, 28);

            return year.ToString() + "-" + month.ToString() + "-" + day.ToString();
        }
        public string GetRandomDriverRightsNumber()
        {
            return "CXE" + random.Next(111111, 999999).ToString();
        }
        public string GetRandomStreet()
        {
            return Streets[random.Next(Streets.Count)];
        }
        public IPerson Generate()
        {
            this.InitializeRandomParametrs();

            IPerson person = new PersonBuilder()
                             .HasName(_fullName[0])
                             .HasSurname(_fullName[1])
                             .HasPatronymic(_fullName[2])
                             .HasDriverRights(_driverRightsNumber)
                             .WasBorn(_dateOfBirth)
                             .LivesIn("Київ")
                             .LivesAt(_street, _house)
                             .Build();

            return person;
        }
        public IDriver Generate(int vehicleId)
        {
            this.InitializeRandomParametrs();

            IDriver driver = (IDriver)new PersonBuilder()
                             .HasName(_fullName[0])
                             .HasSurname(_fullName[1])
                             .HasPatronymic(_fullName[2])
                             .HasDriverRights(_driverRightsNumber)
                             .WasBorn(_dateOfBirth)
                             .LivesIn("Київ")
                             .LivesAt(_street, _house)
                             .DrivesVehicle(vehicleId)
                             .Build();

            return driver;
        }
    }
}
