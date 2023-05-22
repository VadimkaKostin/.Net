namespace Lab5.Strategy
{
    /// <summary>
    /// Стратегія комп'ютера для гри "хрестики-нулики" із застосуванням алгоритму Minimax.
    /// </summary>
    public class MinimaxStrategy : IStrategy
    {
        private int realPlayer = 1;
        private int computerPlayer = 2;

        public Tuple<int, int> ChooseNextTile(int[,] field)
        {
            int bestScore = int.MinValue;
            Tuple<int, int> bestMove = null;

            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (field[row, col] == 0)
                    {
                        field[row, col] = computerPlayer;
                        int score = Minimax(field, 0, false);
                        field[row, col] = 0;

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
        /// Метод для розразунку оцінки ходу.
        /// </summary>
        /// <param name="field">Поле для розразунку оцінки.</param>
        /// <returns>10 - якщо перемагає комп'ютер, -10 - якщо перемагає користувач, 0 - якщо нічия.</returns>
        private int Evaluate(int[,] field)
        {
            // Перевірка на рядки
            for (int row = 0; row < 3; row++)
            {
                if (field[row, 0] == field[row, 1] && field[row, 1] == field[row, 2])
                {
                    if (field[row, 0] == computerPlayer)
                    {
                        return 10;
                    }
                    else if (field[row, 0] == realPlayer)
                    {
                        return -10;
                    }
                }
            }

            // Перевірка на стовпці
            for (int col = 0; col < 3; col++)
            {
                if (field[0, col] == field[1, col] && field[1, col] == field[2, col])
                {
                    if (field[0, col] == computerPlayer)
                    {
                        return 10;
                    }
                    else if (field[0, col] == realPlayer)
                    {
                        return -10;
                    }
                }
            }

            // Перевірка на діагоналі
            if ((field[0, 0] == field[1, 1] && field[1, 1] == field[2, 2]) ||
                (field[0, 2] == field[1, 1] && field[1, 1] == field[2, 0]))
            {
                if (field[1, 1] == computerPlayer)
                {
                    return 10;
                }
                else if (field[1, 1] == realPlayer)
                {
                    return -10;
                }
            }

            return 0;
        }

        /// <summary>
        /// Метод який реалізує рекурсивний алгоритм Minimax, за допомогою якого можна зробити рекурсивне
        /// дослідження всіх сценаріїв розвитку подій та обрати найкращий.
        /// </summary>
        /// <param name="field">Поле для дослідження.</param>
        /// <param name="depth">Глибина занурення.</param>
        /// <param name="isMaximizing">Прапорець максимізації(На різних рівнях в нас можде бути 
        /// або максимізація ходу комп'ютера, або мінімізація ходу реального гравця.</param>
        /// <returns>Оцінка ходу.</returns>
        private int Minimax(int[,] field, int depth, bool isMaximizing)
        {
            int score = Evaluate(field);

            if (score == 10 || score == -10)
            {
                return score;
            }

            if (!IsStepsLeft(field))
            {
                return 0;
            }

            if (isMaximizing)
            {
                int bestScore = int.MinValue;

                for (int row = 0; row < 3; row++)
                {
                    for (int col = 0; col < 3; col++)
                    {
                        if (field[row, col] == 0)
                        {
                            field[row, col] = computerPlayer;
                            bestScore = Math.Max(bestScore, Minimax(field, depth + 1, !isMaximizing));
                            field[row, col] = 0;
                        }
                    }
                }

                return bestScore;
            }
            else
            {
                int bestScore = int.MaxValue;

                for (int row = 0; row < 3; row++)
                {
                    for (int col = 0; col < 3; col++)
                    {
                        if (field[row, col] == 0)
                        {
                            field[row, col] = realPlayer;
                            bestScore = Math.Min(bestScore, Minimax(field, depth + 1, !isMaximizing));
                            field[row, col] = 0;
                        }
                    }
                }

                return bestScore;
            }
        }

        /// <summary>
        /// Метод для перевірки чи незаповненні вже всі клітинки поля.
        /// </summary>
        /// <param name="field">Поле для перевірки.</param>
        /// <returns>True - якщо всі клітинки поля заповненні, інакше - false.</returns>
        private bool IsStepsLeft(int[,] field)
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (field[row, col] == 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
