using Lab2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Models
{
    public class Record : IRecord
    {
        public int VehicleId { get; set; }
        public int PersonId { get; set; }

        public override bool Equals(object obj)
        {
            if(obj == null) 
                return false;

            if(obj.GetType() != typeof(Record)) 
                return false;

            Record other = (Record)obj;

            return this.VehicleId == other.VehicleId && this.PersonId == other.PersonId;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
