using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BattleShips
{
    public class Submarine :Ship
    {
        public Submarine()
        {
            _hp = 1;
            vertical = true;
        }
        public override string ToString()
        {
            return "S";
        }
    }
}
