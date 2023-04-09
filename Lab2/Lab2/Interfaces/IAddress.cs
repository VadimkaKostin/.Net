﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Interfaces
{
    public interface IAddress : IIdentifiable
    {
        string City { get; set; }
        string Street { get; set; }
        int House { get; set; }
    }
}
