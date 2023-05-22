using Lab5.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5.Players
{
    public class ComputerPlayer : IPlayer
    {
        public IStrategy ComputerStrategy { get; set; }

        public Tuple<int, int> MakeStep(GameField field)
        {
            return ComputerStrategy.ChooseNextTile(field.Field);
        }
    }
}
