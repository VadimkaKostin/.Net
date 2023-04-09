using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Interfaces
{
    public interface IPerson : IIdentifiable, IFullName
    {
        string DriverRights { get; set; }
        DateTime DateOfBirth { get; set; }
        int AddressId { get; set; }
    }
}
