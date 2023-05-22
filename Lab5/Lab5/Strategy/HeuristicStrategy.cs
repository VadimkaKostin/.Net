using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5.Strategy
{
    /// <summary>
    /// Стратегія комп'ютера для гри "хрестики-нулики" із застосуванням 
    /// евристичного алгоритм з обмеженим пошуком.
    /// </summary>
    public class HeuristicStrategy : IStrategy
    {
        private int realPlayer = 1;
        private int computerPlayer = 2;

        public Tuple<int, int> ChooseNextTile(int[,] field)
        {
            Tuple<int, int> bestMove = null;
            int bestScore = int.MinValue;

            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (field[row, col] == 0)
                    {
                        int score = Evaluate(row, col, field);
                        if (score > bestScore)
                        {
                            bestScore = score;
                            bestMove = Tuple.Create(row, col);
                        }
                    }
                }
            }

            return bestMove;
        }

        /// <summary>
        /// Метод для оцінювання ходу комп'ютера.
        /// </summary>
        /// <param name="row">Рядок ходу комп'ютеру.</param>
        /// <param name="col">Ствопецю ходу комп'ютера.</param>
        /// <param name="field">Поле для оцінювання.</param>
        /// <returns>100 - якщо комп'ютер вийграє, 50 - якщо він блокує хід гравця, інакше - 0.</returns>
        private int Evaluate(int row, int col, int[,] field)
        {
            int score = 0;

            // Виграшні комбінації
            int[,] winPositions = new int[,]
            {
                { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 },
                { 0, 3, 6 }, { 1, 4, 7 }, { 2, 5, 8 }, 
                { 0, 4, 8 }, { 2, 4, 6 } 
            };

            field[row, col] = computerPlayer;
            if (CheckWin(computerPlayer, field))
            {
                score = 100;
            }
            else
            {
                field[row, col] = realPlayer;
                if (CheckWin(realPlayer, field))
                {
                    score = 50;
                }
            }

            field[row, col] = 0;
            return score;
        }

        /// <summary>
        /// Метод для перевірки чи є переможна комбінація для гравця на полі.
        /// </summary>
        /// <param name="player">Номер гравця(1 - реальний грок, 2 - комп'ютер).</param>
        /// <param name="field">Поле для перевірки.</param>
        /// <returns>True - якщо вийграшна комбінація існує, інакше - false.</returns>
        private bool CheckWin(int player, int[,] field)
        {
            // Виграшні комбінації
            int[,] winPositions = new int[,]
            {
                { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 }, 
                { 0, 3, 6 }, { 1, 4, 7 }, { 2, 5, 8 }, 
                { 0, 4, 8 }, { 2, 4, 6 } 
            };

            for (int i = 0; i < 8; i++)
            {
                int pos1 = winPositions[i, 0],
                    pos2 = winPositions[i, 1],
                    pos3 = winPositions[i, 2];

                if (field[pos1 / 3, pos1 % 3] == player &&
                    field[pos2 / 3, pos2 % 3] == player &&
                    field[pos3 / 3, pos3 % 3] == player)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
