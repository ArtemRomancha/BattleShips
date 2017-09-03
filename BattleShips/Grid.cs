using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BattleShips
{
    [Serializable]
    public class Grid
    {
        Ship[,] field = new Ship[10, 10];
        List<Ship> squadron = new List<Ship>();
        int[] countShips = { 1 , 0, 1, 1 }; //Кол-во кораблей которые можно разместить на поле 1 четырех палубник, 2 трехпалубника и тд

        public int Count(int index)
        {
            return countShips[index];
        }
        public List<Ship> Squadron
        { get { return squadron; } }
        public bool Empty(int x, int y)
        {
            if (field[y, x] == null)
                return true;
            else
                return false;
        }
        public Ship Find(int x, int y)
        {
            return field[y, x];
        }
        public void Attack(int x, int y)
        {
            field[y, x].Attack();
            field[y, x] = null;            
        }
        public bool Defeat()//Есть ли хоть один живой корабль на поле
        {
            for (int i = 0; i < 10; i++)
                for (int f = 0; f < 10; f++)
                    if (field[i, f] != null)
                        return false;
            return true;
        }
        public void RemoveShip(int x, int y, int size, bool vertorient, bool add)
        {
            squadron.Remove(field[y, x]);
            for (int i = 0; i < size; i++)
            {
                if (vertorient)
                    field[y + i, x] = null;
                else
                    field[y, x + i] = null;
            }
            if(add)
                countShips[countShips.Length - size]++;
        }
        public bool AddShip(int x, int y, int size, bool vert, bool neu)//разместить корабль на поле
        {
            if (TryShip(x, y, size, vert))
            {
                squadron.Add(new Ship(size));
                squadron[squadron.Count - 1].HeadX = x;
                squadron[squadron.Count - 1].HeadY = y;
                squadron[squadron.Count - 1].vertorientation = vert;

                for (int i = 0; i < size; i++)
                {
                    if (!vert)
                        field[y, x + i] = squadron[squadron.Count - 1];
                    else
                        field[y + i, x] = squadron[squadron.Count - 1];
                }
                if(neu)
                    countShips[countShips.Length - size]--;
                return true;
            }
            return false;
        }        
        bool TryShip(int x, int y, int size, bool vert)//Проверка можно ли ставить корабль на поле
        {
            if (vert)
            {
                if (y + size > 10)
                    return false;
            }
            else
            {
                if (x + size > 10)
                    return false;
            }
            if (!vert)
            {
                for (int i = 0; i < size; i++)
                {
                    if (field[y, x + i] != null)
                        return false;
                    if (y > 0)
                        if (field[y - 1, x + i] != null)
                            return false;
                    if (y < 9)
                        if (field[y + 1, x + i] != null)
                            return false;
                }
                if (x > 0)
                {
                    if (field[y, x - 1] != null)
                        return false;
                    if (y > 0)
                        if (field[y - 1, x - 1] != null)
                            return false;
                    if (y < 9)
                        if (field[y + 1, x - 1] != null)
                            return false;
                }
                if (x + size < 10)
                {
                    if (field[y, x + size] != null)
                        return false;
                    if (y > 0)
                        if (field[y - 1, x + size] != null)
                            return false;
                    if (y < 9)
                        if (field[y + 1, x + size] != null)
                            return false;
                }
            }
            else
            {
                for (int i = 0; i < size; i++)
                {
                    if (field[y + i, x] != null)
                        return false;
                    if (x > 0)
                        if (field[y + i, x - 1] != null)
                            return false;
                    if (x < 9)
                        if (field[y + i, x + 1] != null)
                            return false;
                }
                if (y > 0)
                {
                    if (field[y - 1, x] != null)
                        return false;
                    if (x > 0)
                        if (field[y - 1, x - 1] != null)
                            return false;
                    if (x < 9)
                        if (field[y - 1, x + 1] != null)
                            return false;
                }
                if (y + size < 10)
                {
                    if (field[y + size, x] != null)
                        return false;
                    if (x > 0)
                        if (field[y + size, x - 1] != null)
                            return false;
                    if (x < 9)
                        if (field[y + size, x + 1] != null)
                            return false;
                }
            }
            return true;
        }        
    }
}
