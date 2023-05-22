using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5.Players
{
    public class RealPlayer : IPlayer
    {
        public Tuple<int, int> MakeStep(GameField field)
        {
            int row = -1, col = -1;

            while (true)
            {
                Console.Write("\nУведіть координати клітинки для ходу: ");

                try
                {
                    string? str = Console.ReadLine();
                    string[] args = str.Split(' ');
                    row = int.Parse(args[0]);
                    col = int.Parse(args[1]);
                }
                catch(Exception)
                {
                    Console.WriteLine("Неправильний формат координат");
                    continue;
                }

                break;
            }

            return Tuple.Create(--row, --col);
        }
    }
}
