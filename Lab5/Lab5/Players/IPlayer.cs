using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5.Players
{
    public interface IPlayer
    {
        Tuple<int, int> MakeStep(GameField field);
    }
}
