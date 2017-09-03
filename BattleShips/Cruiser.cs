﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShips
{
    public class Cruiser :Ship
    {
        public Cruiser()
        {
            _hp = 3;
            vertical = false;
        }
        public override string ToString()
        {
            return "C";
        }
    }
}
