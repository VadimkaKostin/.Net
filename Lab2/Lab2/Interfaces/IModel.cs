using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Interfaces
{
    public interface IModel : IIdentifiable, IName
    {
        int MarkId { get; set; }
        CarBodyType BodyType { get; set; }
        string Maker { get; set; }
        int Year { get; set; }
    }
    public enum CarBodyType
    {
        Sedan,
        Hatchback,
        MUV,
        Coupe,
        Convertible,
        Wagon,
        Van,
        Jeep
    }
}
