using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BattleShips
{
    [Serializable]
    public class Ship:Deck
    {
        public int HeadX, HeadY;        
        public bool vertorientation = false;
        public Ship(int hp):base(hp)
        { }       
        
        public void Draw(Graphics grFront, int Size)
        {
            if (!vertorientation)
            {
                grFront.DrawRectangle(new Pen(Color.DarkViolet, (float)(5)), 20 + (HeadX + 1) * Size, 20 + (HeadY + 1) * Size, _size * Size, Size);
            }
            else
                grFront.DrawRectangle(new Pen(Color.DarkViolet, (float)(5)), 20 + (HeadX + 1) * Size, 20 + (HeadY + 1) * Size, Size, _size * Size);
        }        
        public override string ToString()
        {
            return "s";
        }
    }
}
