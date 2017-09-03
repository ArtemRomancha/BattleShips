using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace BattleShips
{
    public partial class Form1 : Form
    {
        BinaryFormatter formatter = new BinaryFormatter();//Для сериализации        
        Graphics grBack; //Фон картинки
        Graphics grFront; //Передний план картинки
        int Size = 30; //Размер клетки игрового поля
        bool ButtonDown = false; //Кнопка не нажата
        Button confirm; //Кнопка применить необходима для подтверждения
        bool fire; //Был ли совершен выстрел
        bool gameOn = false; //Включен ли игровой режим (перестрелка)
        bool gameOver; //Игра закончилась когда игрок проиграл
        Timer Next = new Timer(); //Таймер между ходами игроков
        
        Player PlayerN, Player1, Player2; //ПлеерН - сосуд для ходящего сейчас игрока
        int temp; //Размер перетаскиваемого кораблика
        int posx = -1, posy = -1; //Позиция удаляемого кораблика
        bool vertorient; //Ориентация удаялемого кораблика

        public Form1()
        {
            InitializeComponent();
            
            Init(); //Инициализация графической части fr.Show();            

            confirm = new Button(); //Создание и расположение кнопки "принять" 
            confirm.Top = 130;
            confirm.Left = 354;
            confirm.Width = 25;
            confirm.Height = 140;
            confirm.Text = "Применить";
            this.Controls.Add(confirm);
            confirm.Click += Confirm;
            confirm.BringToFront(); //Переместить форму на передний план
            confirm.Visible = false; //Невидима форма

            Next.Interval = 1000; //Интервал между ходами игроков
            Next.Tick += GiveStep;

            NewGame();            
        }
        void NewGame()
        {
            Draw(); //Нарисовать фоновую клеточку 
            DrawTestField(0); //Нарисовать игровое 1 игровое поле где размещаться корабли будут
            DrawMemb(); //Рисуем кораблики справа (выбор кораблей для размещения)
            fire = false; //Выстрела еще не было
            gameOver = false; //Игра не проиграна 

            Player1 = new Player("Player1"); //Создаем игрока 1
            PlayerN = Player1; //Сейчас играет игрок 1

            ReDrawShips(Player1, true); //Отрисовывае кол-во кораблей для первого игрока
        }
        void Init()
        {
            Grid.Height = Size * 15 + 100; //Высота пикчер бокса 
            Grid.Width = Size * 30 + 50; //Ширина

            this.Height = Grid.Height - 100; //Высота окна
            this.Width = Size * 20 + 50; // Ширина окна

            Bitmap btmBack = new Bitmap(Grid.Width, Grid.Height); //Битмап дял отрисовки заднего плана
            Bitmap btmFront = new Bitmap(Grid.Width, Grid.Height); //Битмап для отрисовки переднего плана
           
            grBack = Graphics.FromImage(btmBack); //Привязка элемента графики к битмапам
            grFront = Graphics.FromImage(btmFront);

            MainMenu.BackgroundImage = btmBack; //Фон строкового меню тоже клетка
            Grid.Image = btmFront; //Передний план пикчербокса
            Grid.BackgroundImage = btmBack; //Задний план
            Grid.Refresh(); //Обновить картинку на пикчербоксе
        }
        void Draw() //Рисунок поля в клеточку   
        {
            Pen blackPen = new Pen(Color.DeepSkyBlue, (float)(0.1)); //Создаем кисть которой рисовать будем (Цвет, толщина)
            for (int i = 0; i * Size <= Grid.Width; i++)  //Вертикальные линии доски
            {
                grBack.DrawLine(blackPen, i * Size + 20, 0, i * Size + 20, Grid.Height - 50);
            }

            for (int f = 0; f * Size <= Grid.Height; f++) // Горизонтальные линии доски
            {
                grBack.DrawLine(blackPen, 0, f * Size + 20, Grid.Width, f * Size + 20);
            }

            Grid.Refresh(); // Обновляем рисунок в Пикчербоксе
        }     
        void DrawTestField(int x) //Рисунок игрового поля
        {
            string field = "ABCDEFGHIL";

            Font drawFont = new Font("Arial", 16); //Создаем шрифт которым будем писать условия
            SolidBrush drawBrush = new SolidBrush(Color.DarkViolet); //Создаем кисть 

            for (int i = 1; i <= 10; i++)
            {
                if (i != 10)
                    grBack.DrawString(i.ToString(), drawFont, drawBrush, 26 + x * Size, (i + 1) * Size - 5);
                else
                    grBack.DrawString(i.ToString(), drawFont, drawBrush, 18 + x * Size, (i + 1) * Size - 5);

                grBack.DrawString(field[i - 1].ToString(), drawFont, drawBrush, (i + 1 + x) * Size - 5, 26);
            }

            Pen VioletPen = new Pen(Color.DarkViolet, (float)(3));

            grBack.DrawLine(VioletPen, (x + 1) * Size + 20, 50, (x + 1) * Size + 20, 12 * Size - 10);//Вертикальные линии
            grBack.DrawLine(VioletPen, 49 + x * Size, Size + 20, (12 + x) * Size - 10 + 2, Size + 20);//Horizontal

            grBack.DrawLine(VioletPen, (11 + x) * Size + 20, 50, (11 + x) * Size + 20, 12 * Size - 10);//Вертикальные линии
            grBack.DrawLine(VioletPen, 49 + x*Size, 11 * Size + 20, (12+x) * Size - 10 + 2, 11 * Size + 20);//Horizontal

            Grid.Refresh();
        }
        void DrawMemb() //Рисуем корабли на свободном месте для размещения их на игровом поле
        {
            grBack.DrawRectangle(new Pen(Color.DarkViolet, (float)(5)), Size * 14 + 20, 50, 4 * Size, Size);
            grBack.DrawRectangle(new Pen(Color.DarkViolet, (float)(5)), Size * 14 + 20, 50 + 2 * Size, 3 * Size, Size);
            grBack.DrawRectangle(new Pen(Color.DarkViolet, (float)(5)), Size * 14 + 20, 50 + 4 * Size, 2 * Size, Size);
            grBack.DrawRectangle(new Pen(Color.DarkViolet, (float)(5)), Size * 14 + 20, 50 + 6 * Size, Size, Size);
        }
        void ReDrawShips(Player player, bool yes) //Отрисовать корабли игрока плеер, если ес==тру, то отрисовываем еще кол-во доступных кораблей
        {
            Font drawFont = new Font("Arial", 20); //Создаем шрифт которым будем писать условия
            SolidBrush drawBrush = new SolidBrush(Color.DarkViolet); //Создаем кисть 

            if (yes)
            {
                grFront.DrawString(player.GameField.Count(0).ToString(), drawFont, drawBrush, 410, 50);
                grFront.DrawString(player.GameField.Count(1).ToString(), drawFont, drawBrush, 410, 50 + 2 * Size);
                grFront.DrawString(player.GameField.Count(2).ToString(), drawFont, drawBrush, 410, 50 + 4 * Size);
                grFront.DrawString(player.GameField.Count(3).ToString(), drawFont, drawBrush, 410, 50 + 6 * Size);
            }
            
            foreach (Ship s in player.GameField.Squadron) //Отрисовываем все корабли 
                s.Draw(grFront, Size);

            Font NameFont = new Font("Monotype Corsiva", 35, FontStyle.Italic); //Создаем шрифт которым будем писать условия
            grFront.DrawString(player.Name, NameFont, drawBrush, 340, 350); //Пишем внизу имя игрока
        }
        void DrawFireField(Player Fighter, int x) //Отрисовываем поле выстрелов
        {   
            for (int i = 0; i < Fighter.FireField.GetLength(0); i++)
            {
                for (int f = 0; f < Fighter.FireField.GetLength(1); f++)
                {
                    if(Fighter.FireField[i,f]==1) //Если 1 то точеска
                        grFront.FillEllipse(new SolidBrush(Color.Red), 32 + (f + 13 - x) * Size, 32 + (i + 1) * Size, 7, 7);
                    if(Fighter.FireField[i,f]==2) //Если 2, то зачеркиваем клеточку
                    {
                        grFront.DrawLine(new Pen(Color.Red, (float)(3)), 20 + (f + 13 - x) * Size, 20 + (i + 1) * Size, 20 + (f + 14 - x) * Size, 20 + (i + 2) * Size);
                        grFront.DrawLine(new Pen(Color.Red, (float)(3)), 20 + (f + 13 - x) * Size, 20 + (i + 2) * Size, 20 + (f + 14 - x) * Size, 20 + (i + 1) * Size);
                    }
                }                
            }
            Grid.Refresh(); //Обновляем изображение
        }                
        private void Grid_MouseDown(object sender, MouseEventArgs e)//Нажата клавиша мышки
        {
            int X = Cursor.Position.X - this.DesktopLocation.X - Grid.Location.X - 8;  // Вычисляем позицию куда кликнули мышкой
            int Y = Cursor.Position.Y - this.DesktopLocation.Y - Grid.Location.Y - 30;
            if (e.Button == MouseButtons.Left) //Если левая кнопка
            {
                if (X > (Size * 14 + 20) && X < Size * 18 + 20 && Y > 50 && Y < 50 + Size) //И положение курсора над большим корабликом справа 
                {
                    if (PlayerN.GameField.Count(0) > 0) //Если доступен корабль для размежения
                    {
                        vertorient = false; //Запоминаем для отрисовки его положение 
                        temp = 4; //Размер
                        ButtonDown = true; //Говорим что кнопка зажата
                    }
                }
                if (X > (Size * 14 + 20) && X < Size * 17 + 20 && Y > 50 + 2 * Size && Y < 50 + 3 * Size) //Трехпалубник
                {
                    if (PlayerN.GameField.Count(1) > 0)
                    {
                        vertorient = false;
                        temp = 3;
                        ButtonDown = true;
                    }
                }
                if (X > (Size * 14 + 20) && X < Size * 16 + 20 && Y > 50 + 4 * Size && Y < 50 + 5 * Size) //Двухпалубник
                {
                    if (PlayerN.GameField.Count(2) > 0)
                    {
                        vertorient = false;
                        temp = 2;
                        ButtonDown = true;
                    }
                }
                if (X > (Size * 14 + 20) && X < Size * 15 + 20 && Y > 50 + 6 * Size && Y < 50 + 7 * Size) //Одинарный
                {
                    if (PlayerN.GameField.Count(3) > 0)
                    {
                        vertorient = false;
                        temp = 1;
                        ButtonDown = true;
                    }
                }

                int NY = (int)(Y - 20 - Size) / Size, NX = (int)(X - 20 - Size) / Size; //высчитываем в какой клетке (20 смещение клеток в пикчер боксе)
                if (NY >= 0 && NY < 10 && NX >= 0 && NX < 10) //Если в пределах игрового поля
                {
                    if (!PlayerN.GameField.Empty(NX, NY)) //Если клетка не пуста
                    {
                        ButtonDown = true;

                        temp = PlayerN.GameField.Find(NX, NY).SIZE; //Запоминаем размер корабля который находился в этой клетке
                        posx = PlayerN.GameField.Find(NX, NY).HeadX; //Координату Х его головы
                        posy = PlayerN.GameField.Find(NX, NY).HeadY; //Координату У его головы
                        vertorient = PlayerN.GameField.Find(NX, NY).vertorientation; //Ориентацию

                        PlayerN.GameField.RemoveShip(posx, posy, temp, vertorient, false); //Удаляем корабль из списка поставленных кораблей
                    }
                }
            }
        }
        private void Grid_MouseMove(object sender, MouseEventArgs e) //Отрисовываем перетаскивание кораблика
        {
            if (ButtonDown)
            {
                int X = Cursor.Position.X - this.DesktopLocation.X - Grid.Location.X - 8;  // Вычисляем позицию куда кликнули мышкой
                int Y = Cursor.Position.Y - this.DesktopLocation.Y - Grid.Location.Y - 30;

                grFront.Clear(Color.Empty); //Очищаем передний план
                if(!vertorient) //Если не вертикально кораблик расположен ,то отрисовываем его горизонтально
                    grFront.DrawRectangle(new Pen(Color.DarkViolet, (float)(5)), X, Y, temp * Size, Size);
                else
                    grFront.DrawRectangle(new Pen(Color.DarkViolet, (float)(5)), X, Y, Size, temp * Size);
                ReDrawShips(PlayerN, true); //Перерисовываем корабли расположенные на игровом поле
                Grid.Refresh(); //обновляем картинку
            }
        }
        private void Grid_MouseUp(object sender, MouseEventArgs e) //Если кнопку мыши отпустили
        {
            int X = Cursor.Position.X - this.DesktopLocation.X - Grid.Location.X - 8;  // Вычисляем позицию куда кликнули мышкой
            int Y = Cursor.Position.Y - this.DesktopLocation.Y - Grid.Location.Y - 30;

            int NY = (int)(Y - 20 - Size) / Size, NX = (int)(X - 20 - Size) / Size; //Определяем клетку где расположен курсор
            ButtonDown = false;
            if (NY >= 0 && NY < 10 && NX >= 0 && NX < 10) //Если в пределах игрового поля
            { 
                if (temp != 0) //Был запомнен корабль
                {
                    if (vertorient && NY <= 10 - temp || !vertorient && NX <= 10 - temp) //Если по размерам он подходит
                    {
                        if (posx == -1) //Если этот корабль добавлен из правой части то просто добавляем его 
                        {
                            PlayerN.GameField.AddShip(NX, NY, temp, false, true);
                            temp = 0;//Очищаем значени размера временного корабля
                        }
                        else
                        {
                            if (!PlayerN.GameField.AddShip(NX, NY, temp, vertorient, false)) //Если перетаскивали, то пробуем поставить его
                                PlayerN.GameField.AddShip(posx, posy, temp, vertorient, false); //Если он стать туда не может, то возвращаем его на исходное место
                            
                                temp = 0; //Обнуляем все переменные вспомогательные
                                posx = -1;
                                posy = -1;
                                vertorient = false;
                        }
                    }
                    else //Если не влазит полностью на игровое поле
                    {
                        if (posx != -1) //Пытались перетащить корабль, то возвращаем его на исходное место
                        {
                            PlayerN.GameField.AddShip(posx, posy, temp, vertorient, false);
                            temp = 0;
                            posx = -1;
                            posy = -1;
                            vertorient = false;
                        }
                    }
                }
            }
            else //если за пределами игрового поля
            {
                if (temp != 0) // и запомнен был корабль
                {
                    if (posx != -1) //и мы его пытались перетащить откуда-то
                    {
                        PlayerN.GameField.AddShip(posx, posy, temp, vertorient, false); // возвращаем на исходную позицию
                        temp = 0; //обнуляем временные переменные
                        posx = -1;
                        posy = -1;
                        vertorient = false;
                    }
                    else
                        temp = 0;
                }
            }

            if (PlayerN.GameField.Count(0) == 0 && PlayerN.GameField.Count(1) == 0 && PlayerN.GameField.Count(2) == 0 && PlayerN.GameField.Count(3) == 0) //Проверяем все ли доступные корабли были размещены на поле
                confirm.Visible = true; //если да, то подсвечиваем кнопку принять
            else
                confirm.Visible = false;

            grFront.Clear(Color.Empty); //Очищаем передний план
            ReDrawShips(PlayerN, true); //Перерисовываем все кораблики
            Grid.Refresh(); //обновляем рисунок
        }
        private void Grid_DoubleClick(object sender, EventArgs e) //удалить корабль
        {
            int X = Cursor.Position.X - this.DesktopLocation.X - Grid.Location.X - 8;  // Вычисляем позицию куда кликнули мышкой
            int Y = Cursor.Position.Y - this.DesktopLocation.Y - Grid.Location.Y - 30;

            int NY = (int)(Y - 20 - Size) / Size, NX = (int)(X - 20 - Size) / Size;

            if (NY >= 0 && NY < 10 && NX >= 0 && NX <= 10 - temp)
            {
                if (!PlayerN.GameField.Empty(NX, NY)) //Если ячейка не пуста
                {
                    PlayerN.GameField.RemoveShip(PlayerN.GameField.Find(NX, NY).HeadX, PlayerN.GameField.Find(NX, NY).HeadY, PlayerN.GameField.Find(NX, NY).SIZE, PlayerN.GameField.Find(NX, NY).vertorientation, true); //Удаляем кобарль из нее
                }
            }
            vertorient = false;
            grFront.Clear(Color.Empty);
            ReDrawShips(PlayerN, true); //перерисовываем 
        }
        private void Grid_MouseClick(object sender, MouseEventArgs e)//перевернуть фигурку
        {
            int X = Cursor.Position.X - this.DesktopLocation.X - Grid.Location.X - 8;  // Вычисляем позицию куда кликнули мышкой
            int Y = Cursor.Position.Y - this.DesktopLocation.Y - Grid.Location.Y - 30;

            int NY = (int)(Y - 20 - Size) / Size, NX = (int)(X - 20 - Size) / Size;
            if (e.Button == MouseButtons.Right) //Если правой клавишей кликнули
            {
                if (NY >= 0 && NY < 10 && NX >= 0 && NX <= 10 - temp)
                {
                    if (!PlayerN.GameField.Empty(NX, NY)) //если не пусто
                    {
                        posx = PlayerN.GameField.Find(NX, NY).HeadX; //запоминаем характеристики корабля
                        posy = PlayerN.GameField.Find(NX, NY).HeadY;
                        temp = PlayerN.GameField.Find(NX, NY).SIZE;
                        vertorient = PlayerN.GameField.Find(NX, NY).vertorientation;
                        PlayerN.GameField.RemoveShip(posx, posy, temp, vertorient, false); //удаляем его
                        if (!PlayerN.GameField.AddShip(posx, posy, temp, !vertorient, false)) //если нельзя разместитть перевернутым, то возвращаем на место
                            PlayerN.GameField.AddShip(posx, posy, temp, vertorient, false);
                        posx = -1;
                        temp = 0;
                        vertorient = false;
                    }
                    grFront.Clear(Color.Empty);
                    ReDrawShips(PlayerN, true);
                }
            }
        }
        private void Confirm(object sender, EventArgs e)//Принять
        {
            if (Player2 == null) //Если второй игрок еще не вступал в игру
            {
                Player2 = new Player("Player2"); //создаем его
                PlayerN = Player2; //Ходит второй игрок
                
                confirm.Visible = false;    //Прячем кнопку принять            
                Player1.GetOpponent(Player2); //Оппонент первого второй и наоборот
                Player2.GetOpponent(Player1);

                grFront.Clear(Color.Empty); //Очищаем план передний
                ReDrawShips(PlayerN, true); //Рисуем для второго
                Grid.Refresh();
            }
            else //Если второй разместил корабли
            {
                PlayerN = Player1; //Ходит сначала первый игрок
                confirm.Visible = false; //Прячем подтвердить
                GameModeON(); //Включаем режим перестрелки
                this.Width = Size * 24 + 50; //Увеличиваем размер окна под два поля
                grBack.Clear(Color.Empty); //Чистим полностью пикчербокс
                grFront.Clear(Color.Empty);
                Draw(); //Рисуем клеточку на задний фон
                DrawTestField(0); //Рисуем игровые поля для своих кораблей и для обстрела
                DrawTestField(12);
                ReDrawShips(Player1, false); //Отрисовываем корабли первого игрока
                Grid.Refresh();
            }
        }     
        private void NextStep(object sender, EventArgs e) //Следующий ход
        {
            confirm.Visible = false; //прячем кнопкуу принять
            fire = false; //Выстела еще не было
            grFront.Clear(Color.Empty); //чистим изображение 
            Grid.Refresh(); //отображаем чистое игровое поле
            
            if (PlayerN == Player1) //Меняем ход на след игрока
                PlayerN = Player2;
            else
                PlayerN = Player1;

            Next.Start();  //Пускаем таймер          
        }
        void GiveStep(object sender, EventArgs e)//По истечению таймера отрисовываем ноовому игроку все
        {
            Next.Stop(); //Останавливаем таймер

            if (PlayerN == Player1) //Рисуем поля обстрела противника
                DrawFireField(Player2, 12); 
            else
                DrawFireField(Player1, 12);            
            
            DrawFireField(PlayerN, 0); //Рисуем выстрелы самого игрока

            ReDrawShips(PlayerN, false); //Отрисовываем кораблики
            Grid.Refresh();
        }
        void GameModeON() //Режим перестрелки
        {
            Grid.MouseClick -= Grid_MouseClick; //Отключаем все события по размещению кораблей и добавляем для перестрелки
            Grid.DoubleClick -= Grid_DoubleClick;
            Grid.MouseDown -= Grid_MouseDown;
            Grid.MouseMove -= Grid_MouseMove;
            Grid.MouseUp -= Grid_MouseUp;
            confirm.Click -= Confirm;

            Grid.MouseClick+=Grid_MouseFireClick;
            confirm.Click += NextStep;
            gameOn = true;
        }
        void GameModeOff()//Режим размещения кораблей
        {
            Grid.MouseClick += Grid_MouseClick;
            Grid.DoubleClick += Grid_DoubleClick;
            Grid.MouseDown += Grid_MouseDown;
            Grid.MouseMove += Grid_MouseMove;
            Grid.MouseUp += Grid_MouseUp;
            confirm.Click += Confirm;

            Grid.MouseClick -= Grid_MouseFireClick;
            confirm.Click -= NextStep;
            gameOn = false;
        }
        
        private void Grid_MouseFireClick(object sender, MouseEventArgs e)
        {
            if (!fire) //если еще не стреляли
            {
                int X = Cursor.Position.X - this.DesktopLocation.X - Grid.Location.X - 8;  // Вычисляем позицию куда кликнули мышкой
                int Y = Cursor.Position.Y - this.DesktopLocation.Y - Grid.Location.Y - 30;
                                
                int NY = (int)(Y - 20 - Size) / Size, NX = (int)(X - 20 - Size) / Size - 12;

                if (NX >= 0 && NX <= 9 && NY >= 0 && NY <= 9) //в пределах ли игрового поля
                {
                    if (PlayerN.Fire(NX, NY)) 
                    {
                        fire = true;

                        if (PlayerN == Player1)//Проверяем проиграл ли кто-то
                        {
                            if (Player2.GameField.Defeat()) 
                            {
                                MessageBox.Show(Player2.Name + " Lose");
                                Grid.MouseClick -= Grid_MouseFireClick;
                                confirm.Click -= NextStep;
                                gameOver = true;
                            }
                            else
                                confirm.Visible = true; //Если нет, то отображаем кнопку для след хода
                        }
                        else
                            if (Player1.GameField.Defeat())
                            {
                                MessageBox.Show(Player1.Name + " Lose");
                                Grid.MouseClick -= Grid_MouseFireClick;
                                confirm.Click -= NextStep;
                                gameOver = true;
                            }
                            else
                                confirm.Visible = true;
                    }
                }
                DrawFireField(PlayerN, 0);   //Перерисовываем поле с новым выстрелом
            }            
        }

        private void начатьСначалаToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            grBack.Clear(Color.Empty);
            grFront.Clear(Color.Empty);            
            NewGame();
            GameModeOff();
            Player1 = new Player("Player1");
            Player2 = null;
            PlayerN = Player1;
            Grid.Refresh();
            this.Height = Grid.Height - 100;
            this.Width = Size * 20 + 50;
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!gameOver && gameOn && PlayerN == Player1 && !fire)
            {
                using (var fStream = new FileStream("./Player1.dat", FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    formatter.Serialize(fStream, Player1);
                }
                using (var fStream = new FileStream("./Player2.dat", FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    formatter.Serialize(fStream, Player2);
                }                
            }
            else
                MessageBox.Show("Невозможно сохранить!");
        }

        private void загрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists("./Player1.dat") && File.Exists("./Player2.dat") && File.Exists("./PlayerN.dat"))
            {
                using (var fStream = File.OpenRead("./Player1.dat"))
                {
                    Player1 = (Player)formatter.Deserialize(fStream);
                }
                using (var fStream = File.OpenRead("./Player2.dat"))
                {
                    Player2 = (Player)formatter.Deserialize(fStream);
                }
                
                PlayerN = Player1;
                confirm.Visible = false;
                fire = false;                
                GameModeON();
                this.Width = Size * 24 + 50;
                grBack.Clear(Color.Empty);
                grFront.Clear(Color.Empty);

                Player1.GetOpponent(Player2);
                Player2.GetOpponent(Player1);

                Draw();
                DrawTestField(0);
                DrawTestField(12);
                
                DrawFireField(Player1, 0);                
                DrawFireField(Player2, 12);                
                ReDrawShips(Player1, false);

                Grid.Refresh();
            }
            else
            {
                MessageBox.Show("Нет сохранений! Начните новую игру");
            }
        }        
    }
}
