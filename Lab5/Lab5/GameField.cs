using Lab5.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    public class GameField
    {
        //Матриця що задає поле
        private int[,] _field = new int[3, 3] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };

        public int[,] Field { get { return _field; } }

        //Властивість, яка вказує який гравець зробив хід останнім
        public IPlayer CurrentPlayer { get; set; }

        /// <summary>
        /// Метод для заповнення гравцем клітинки поля певним символом. 
        /// </summary>
        /// <param name="coordinates">Координати поля.</param>
        /// <param name="charNumber">Номер символу(0 - порожній символ, 1 - X, 2 - O).</param>
        /// <param name="player">Гравець який робить хід.</param>
        /// <returns>True - якщо заповнення клітинки пройшло вдало, інакше - false.</returns>
        public bool FillTile(Tuple<int, int> coordinates, int charNumber, IPlayer player)
        {
            //Валідація індексів
            if((coordinates.Item1 > 2 || coordinates.Item1 < 0) || (coordinates.Item2 > 2 || coordinates.Item2 < 0))
                return false;

            //Якщо гравець намагається помітити вже зайняту клітинку, хід завершується невдало
            if (charNumber != 0 && (_field[coordinates.Item1, coordinates.Item2] == 1 || _field[coordinates.Item1, coordinates.Item2] == 2))
                return false;

            _field[coordinates.Item1, coordinates.Item2] = charNumber;

            CurrentPlayer = player;

            return true;
        }

        /// <summary>
        /// Метод для перевірки закінчення гри.
        /// </summary>
        /// <returns>True - якщо гра завершена, в іншому випадку - false.</returns>
        public bool IsGameOver()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (_field[i, j] == 0)
                        return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Метод що перевіряє чи є в грі переможець.
        /// </summary>
        /// <returns>True - якщо гра завершена перемогою одного з гравців, в іншому випадку - false.</returns>
        public bool IsWinnerPresent()
        {
            return _field[0, 0] == _field[0, 1] && _field[0, 1] == _field[0, 2] && _field[0, 0] != 0 ||
                   _field[1, 0] == _field[1, 1] && _field[0, 1] == _field[1, 2] && _field[1, 0] != 0 ||
                   _field[2, 0] == _field[2, 1] && _field[2, 1] == _field[2, 2] && _field[2, 0] != 0 ||
                   _field[0, 0] == _field[1, 0] && _field[1, 0] == _field[2, 0] && _field[0, 0] != 0 ||
                   _field[0, 1] == _field[1, 1] && _field[1, 1] == _field[2, 1] && _field[0, 1] != 0 ||
                   _field[0, 2] == _field[1, 2] && _field[1, 2] == _field[2, 2] && _field[0, 2] != 0 ||
                   _field[0, 0] == _field[1, 1] && _field[1, 1] == _field[2, 2] && _field[0, 0] != 0 ||
                   _field[0, 2] == _field[1, 1] && _field[1, 1] == _field[2, 0] && _field[0, 2] != 0;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            for(int i = 0; i < 3; i++)
            {
                sb.Append("-------------------\n");
                sb.Append("|     |     |     |\n|");
                for(int j = 0; j < 3; j++)
                {
                    char tile = ' ';

                    switch (_field[i,j])
                    {
                        case 1:
                            tile = 'X';
                            break;
                        case 2:
                            tile = 'O';
                            break;
                    }

                    sb.Append($"  {tile}  |");
                }
                sb.Append("\n|     |     |     |\n");
            }
            sb.Append("-------------------\n");

            return sb.ToString();
        }
    }
}
