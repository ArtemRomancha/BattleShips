using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShips
{
    [Serializable]
    public class Deck
    {
        protected int _hp;
        protected int _size;
        public Deck(int health)
        {
            _hp = health;
            _size = health;
        }
        public int HP
        {
            get { return _hp; }
        }
        public int SIZE
        {
            get { return _size; }
        }
        public bool Alive()
        {
            if (_hp > 0)
                return true;
            else
                return false;
        }
        public void Attack()
        {
            _hp--;
        }
    }
}
