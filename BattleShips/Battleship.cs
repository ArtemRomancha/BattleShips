using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShips
{
    public class Battleship :Ship
    {
        public Battleship()
        {
            _hp = 4;
            vertical = false;
        }
        public override string ToString()
        {
            return "B";
        }
    }
}
