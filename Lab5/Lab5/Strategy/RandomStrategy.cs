using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5.Strategy
{
    /// <summary>
    /// Стратегія комп'ютера для гри "хрестики-нулики" із застосуванням методу обирання рандомної вільної клітинки.
    /// </summary>
    public class RandomStrategy : IStrategy
    {
        private readonly Random _random = new Random();
        public Tuple<int, int> ChooseNextTile(int[,] field)
        {
            int row, col;

            do
            {
                row = _random.Next(0, 3);
                col = _random.Next(0, 3);
            } while (field[row, col] != 0);

            return Tuple.Create(row, col);
        }
    }
}
