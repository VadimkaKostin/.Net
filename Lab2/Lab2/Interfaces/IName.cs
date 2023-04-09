using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Interfaces
{
    public interface IName
    {
        string Name { get; set; }
    }
    public interface IFullName : IName
    {
        string Surname { get; set; }
        string Patronymic { get; set; }
    }
}
