using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5.Strategy
{
    public interface IStrategy
    {
        /// <summary>
        /// Метод для визначення клітинки поля для наступного ходу.
        /// </summary>
        /// <param name="field">Поле для визначення наступного ходу.</param>
        /// <returns>Кортеж із координатами клітинки наступного ходу.</returns>
        Tuple<int, int> ChooseNextTile(int[,] field);
    }
}
