using Lab5.Commands;
using Lab5.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    public class Game
    {
        private RealPlayer _realPlayer;
        private ComputerPlayer _computerPlayer;

        private GameField _gameField;

        private RealPlayerStepCommand _commandReal;
        private ComputerPlayerStepCommand _commandComputer;

        public Game(RealPlayer realPlayer, ComputerPlayer computerPlayer)
        {
            _realPlayer = realPlayer;
            _computerPlayer = computerPlayer;

            _gameField = new GameField();
        }

        public async Task Run()
        {
            Console.WriteLine(this._gameField);

            while (true)
            {
                //Крок реального гравця
                _commandReal = new RealPlayerStepCommand(_realPlayer, _gameField);
                _commandReal.Execute();

                Console.Clear();

                Console.WriteLine(this._gameField);
                

                if (_gameField.IsWinnerPresent() || _gameField.IsGameOver()) break;

                //Крок комп'ютера
                Console.WriteLine("Хід комп'ютера...");

                await Task.Delay(2000);

                _commandComputer = new ComputerPlayerStepCommand(_computerPlayer, _gameField);
                _commandComputer.Execute();

                Console.Clear();

                Console.WriteLine(this._gameField);

                if (_gameField.IsWinnerPresent() || _gameField.IsGameOver()) break;

                //Хід назад
                Console.Write("\nЗробити хід назад(1/0): ");

                int choise = 0;

                try
                {
                    choise = int.Parse(Console.ReadLine());
                }
                catch (Exception) { }

                if (choise == 1)
                {
                    _commandReal.UnExecute();
                    _commandComputer.UnExecute();
                }

                Console.Clear();

                Console.WriteLine(this._gameField);
            } 

            this.ShowResult();
        }
        private void ShowResult()
        {
            if (_gameField.IsWinnerPresent())
            {
                if (_gameField.CurrentPlayer == _realPlayer)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nВи перемогли!");
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nКомп'ютер переміг!");
                    Console.BackgroundColor = ConsoleColor.Black;
                }

                return;
            }

            Console.WriteLine("Гра завершена, переможця немає!");
        }
    }
}
