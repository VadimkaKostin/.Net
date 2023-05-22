using Lab5.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5.Commands
{
    public class RealPlayerStepCommand : ICommand
    {
        private RealPlayer _realPlayer;
        private GameField _gameField;

        private Tuple<int, int> _coordinates;

        public RealPlayerStepCommand(RealPlayer realPlayer, GameField gameField)
        {
            _realPlayer = realPlayer;
            _gameField = gameField;
        }

        public void Execute()
        {
            do
            {
                _coordinates = this._realPlayer.MakeStep(this._gameField);
            }
            while (!this._gameField.FillTile(_coordinates, 1, _realPlayer));
        }

        public void UnExecute()
        {
            this._gameField.FillTile(_coordinates, 0, _realPlayer);
        }
    }
}
