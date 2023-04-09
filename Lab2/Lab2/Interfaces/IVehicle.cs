using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Interfaces
{
    public interface IVehicle : IIdentifiable
    {
        int ModelId { get; set; }
        string VINCode { get; set; }
        string Color { get; set; }
        string Number { get; set; }
        string State { get; set; }
    }
}
