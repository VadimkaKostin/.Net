using Lab5.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5.Commands
{
    public class ComputerPlayerStepCommand : ICommand
    {
        private ComputerPlayer _computerPlayer;
        private GameField _gameField;

        private Tuple<int, int> _coordinates;

        public ComputerPlayerStepCommand(ComputerPlayer computerPlayer, GameField gameField)
        {
            _computerPlayer = computerPlayer;
            _gameField = gameField;
        }

        public void Execute()
        {
            _coordinates = this._computerPlayer.MakeStep(this._gameField);

            this._gameField.FillTile(_coordinates, 2, _computerPlayer);
        }

        public void UnExecute()
        {
            this._gameField.FillTile(_coordinates, 0, _computerPlayer);
        }
    }
}
