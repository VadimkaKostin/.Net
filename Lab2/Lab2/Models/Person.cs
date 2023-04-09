using Lab2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Models
{
    public class Person : IPerson
    {
        private static int _counter = 1;

        public int Id { get; set; }
        public string DriverRights { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int AddressId { get; set; }

        public Person()
        {
            Id = _counter++;
        }
    }
}
