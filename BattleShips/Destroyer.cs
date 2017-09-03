using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShips
{
    public class Destroyer :Ship
    {
        public Destroyer()
        {
            _hp = 2;
            vertical = false;
        }
        public override string ToString()
        {
            return "D";
        }
    }
}
