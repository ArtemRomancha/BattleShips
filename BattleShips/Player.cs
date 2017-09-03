using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShips
{
    [Serializable]
    public class Player
    {
        Player _opponent;
        string _name;
        Grid _field;
        int[,] _fire;
        public Player(string name)
        {
            _name = name;
            _fire = new int[10, 10];
            _field = new Grid();
        }
        public void GetOpponent(Player Op)
        {
            _opponent = Op;
        }
        public string Name
        {
            get { return _name; }
        }
        public Grid GameField
        {
            get { return _field; }
        }
        public int[,] FireField
        {
            get { return _fire; }
        }

        public bool Defend(int x, int y)//Не пусто ли поле по которому стреляли
        {
            if (_field.Empty(x, y))
                return false;
            return true;
        }
        public bool Kill(int x, int y, ref int size, ref int Sx, ref int Sy, ref bool orient)
        {
            if (_field.Find(x,y).HP == 1) //Если у корабль убит, то удаляем его, возвращая его характеристики
            {
                size = _field.Find(x,y).SIZE;
                Sx = _field.Find(x,y).HeadX;
                Sy = _field.Find(x,y).HeadY;
                orient = _field.Find(x, y).vertorientation;
                _field.Attack(x, y);          
                return true;
            }
            else
            {
                _field.Attack(x, y);
                return false;
            }
        }
        public bool Fire(int x, int y)//Обстрел 
        {
            if (_fire[y, x] == 0) //Если по этому полю еще не стреляли
            {
                if (_opponent.Defend(x, y)) //Если оно не пусто
                {
                    _fire[y, x] = 2; //Отмечаем в матрице обстрела что тут корабль был
                    int size = 0, sx = 0, sy = 0;
                    bool vert = false;
                    if (_opponent.Kill(x, y, ref size, ref sx, ref sy, ref vert)) //Если он был убит то обводим его со всех сторон точками
                    {
                        for (int i = 0; i < size; i++)
                        {
                            if (vert)
                            {
                                if (sx > 0)
                                    _fire[sy + i, sx - 1] = 1;
                                if (sx < 9)
                                    _fire[sy + i, sx + 1] = 1;
                            }
                            else
                            {
                                if (sy > 0)
                                    _fire[sy - 1, sx + i] = 1;
                                if (sy < 9)
                                    _fire[sy + 1, sx + i] = 1;
                            }
                        }
                        if (!vert)
                        {
                            if (sx > 0)
                            {
                                if (sy > 0)
                                    _fire[sy - 1, sx - 1] = 1;
                                _fire[sy, sx - 1] = 1;
                                if (sy < 9)
                                    _fire[sy + 1, sx - 1] = 1;
                            }
                            if (sx + size < 9)
                            {
                                if (sy > 0)
                                    _fire[sy - 1, sx + size] = 1;
                                _fire[sy, sx + size] = 1;
                                if (sy < 9)
                                    _fire[sy + 1, sx + size] = 1;
                            }
                        }
                        else
                        {
                            if (sy > 0)
                            {
                                if (sx > 0)
                                    _fire[sy - 1, sx - 1] = 1;
                                _fire[sy - 1, sx] = 1;
                                if (sx < 9)
                                    _fire[sy - 1, sx + 1] = 1;
                            }
                            if (sy + size < 9)
                            {
                                if (sx > 0)
                                    _fire[sy + size, sx - 1] = 1;
                                _fire[sy + size, sx] = 1;
                                if (sx < 9)
                                    _fire[sy + size, sx + 1] = 1;
                            }
                        }
                    }
                    return true;
                }
                else
                {
                    _fire[y, x] = 1;
                }
                return true;
            }
            return false;
        }
    }
}
