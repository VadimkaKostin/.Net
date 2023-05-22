using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5.Commands
{
    public interface ICommand
    {
        void Execute();
        void UnExecute();
    }
}
