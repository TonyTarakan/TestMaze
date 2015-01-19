using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace WindowsFormsApplication1
{
    
    public partial class Form1 : Form
    {
        public const int SizeX = 50; //razmer labirinta
        public const int SizeY = 50;
        int[,] MazeArray = new int[SizeX, SizeY];
        int[,] Neighbours = new int[3, 3];
        int PosX;
        int PosY;

        public Form1()
        {
            InitializeComponent();
        }

        public void DrawPic()//Рисовалка
        {
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(bmp);
            SolidBrush blueBrush = new SolidBrush(Color.Blue);
            SolidBrush whiteBrush = new SolidBrush(Color.White);
            SolidBrush yellowBrush = new SolidBrush(Color.Yellow);
            SolidBrush redBrush = new SolidBrush(Color.Red);
            SolidBrush greenBrush = new SolidBrush(Color.Green);
            for (int r = 0; r < SizeX - 1; r++)
            {
                for (int c = 0; c < SizeY - 1; c++)
                {
                    if (MazeArray[r, c] == 0) // стенки
                    {
                        Rectangle rectangle = new Rectangle(r * pictureBox1.Width / SizeX, c * pictureBox1.Height / SizeY, pictureBox1.Width / SizeX, pictureBox1.Height / SizeY);
                        g.FillRectangle(blueBrush, rectangle);
                    }
                    if (MazeArray[r, c] == 3) // один раз прошли
                    {
                        Rectangle rectangle = new Rectangle(r * pictureBox1.Width / SizeX + 1, c * pictureBox1.Height / SizeY + 1, pictureBox1.Width / SizeX - 2 , pictureBox1.Height / SizeY - 2);
                        g.FillRectangle(redBrush, rectangle);
                    }
                   /* if (MazeArray[r, c] == 4) // дважды прошли
                    {
                        Rectangle rectangle = new Rectangle(r * pictureBox1.Width / SizeX + 1, c * pictureBox1.Height / SizeY + 1, pictureBox1.Width / SizeX - 2, pictureBox1.Height / SizeY - 2);
                        g.FillRectangle(greenBrush, rectangle);
                    }*/
                    if (MazeArray[r, c] == 2) // тропинки
                    {
                        Rectangle rectangle = new Rectangle(r * pictureBox1.Width / SizeX, c * pictureBox1.Height / SizeY, pictureBox1.Width / SizeX, pictureBox1.Height / SizeY);
                        g.FillRectangle(whiteBrush, rectangle);
                    }
                }
            }
            pictureBox1.Image = bmp;
        }

        public int[,] GenerateMaze()//Прорезаем лабиринт по алгоритму DepthFirst
        {
            // bazoviy massiv
            //
            int i;
            int j;
            //int[,] MazeArray = new int[Constants.SizeX, Constants.SizeY]; 
            for (i = 0; i < SizeX; i++)
            {
                for (j = 0; j < SizeY; j++)
                {
                    MazeArray[i, j] = 0; // 0-neobrab yacheyka
                }
            }

            //точка отправления
            Random rand = new Random();
            int StartPosX = rand.Next(0, SizeX - 1);
            int StartPosY = rand.Next(0, SizeY - 1);
            StartPosX = StartPosX - (StartPosX % 2); //делаем четным
            StartPosY = StartPosY - (StartPosY % 2);
            i = StartPosX;
            j = StartPosY;

            MazeArray[StartPosX, StartPosY] = 1; // 1 - пройдена один раз


            for (int lol = 0; lol < 10000000; lol++)
            {
                switch (rand.Next(4))
                {
                    case 0:
                        if (i > 0)
                        {
                            if (MazeArray[i - 2, j] != 0) //если впереди проточено
                            {
                                //if (MazeArray[i - 1, j] == 1) //а вдруг мы тут уже проходили один раз
                                if (MazeArray[i - 1, j] != 0)
                                {
                                    i--; //продвигаемся по натоптанному
                                    MazeArray[i, j] = 2; //отмечаем возвращение
                                    i--; //на 2
                                    MazeArray[i, j] = 2; //отмечаем возвращение
                                }
                                break; //меняем направление
                            }
                            //ежели нет, топчем вперед
                            i--;
                            MazeArray[i, j] = 1;
                            i--;
                            MazeArray[i, j] = 1;
                        }
                        break;
                    case 1:
                        if (i < SizeX - 2)
                        {
                            if (MazeArray[i + 2, j] != 0) //если впереди проходили
                            {
                                if (MazeArray[i + 1, j] != 0) //а вдруг мы тут уже проходили один раз
                                {
                                    i++; //продвигаемся по натоптанному
                                    MazeArray[i, j] = 2; //отмечаем возвращение
                                    i++; //на 2
                                    MazeArray[i, j] = 2; //отмечаем возвращение
                                }
                                break; //меняем направление
                            }
                            //ежели нет, топчем вперед
                            i++;
                            MazeArray[i, j] = 1;
                            i++;
                            MazeArray[i, j] = 1;
                        }
                        break;
                    case 2:
                        if (j > 0)
                        {
                            if (MazeArray[i, j - 2] != 0) //если впереди стена
                            {
                                if (MazeArray[i, j - 1] != 0) //а вдруг мы тут уже проходили один раз
                                {
                                    j--; //продвигаемся по натоптанному
                                    MazeArray[i, j] = 2; //отмечаем возвращение
                                    j--; //на 2
                                    MazeArray[i, j] = 2; //отмечаем возвращение
                                }
                                break; //меняем направление
                            }
                            //ежели нет, топчем вперед
                            j--;
                            MazeArray[i, j] = 1;
                            j--;
                            MazeArray[i, j] = 1;
                        }
                        break;
                    case 3:
                        if (j < SizeY - 2)
                        {
                            if (MazeArray[i, j + 2] != 0) //если впереди стена
                            {
                                if (MazeArray[i, j + 1] != 0) //а вдруг мы тут уже проходили один раз
                                {
                                    j++; //продвигаемся по натоптанному
                                    MazeArray[i, j] = 2; //отмечаем возвращение
                                    j++; //на 2
                                    MazeArray[i, j] = 2; //отмечаем возвращение
                                }
                                break; //меняем направление
                            }
                            //ежели нет, топчем вперед
                            j++;
                            MazeArray[i, j] = 1;
                            j++;
                            MazeArray[i, j] = 1;
                        }
                        break;
                }
            }
           /* for (i = 0; i < SizeX; i++)
            {
                for (j = 0; j < SizeY; j++)
                {
                    if (MazeArray[i, j] == 0) MazeArray[i, j] = 2; // инвертируем(гарантирован выход из лабиринта + быстрее потом сравнивать с 0)
                    else MazeArray[i, j] = 0;
                }
            }*/
            return MazeArray;
        }

        public void Watcher(int X, int Y)//осматриваемся
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Neighbours[i, j] = 0;
                }
            }
            Neighbours[1, 2] = MazeArray[X, Y++];
            Neighbours[1, 0] = MazeArray[X, Y--];
            Neighbours[0, 1] = MazeArray[X++, Y];
            Neighbours[2, 1] = MazeArray[X--, Y];
        }

        public void Driver(int Dir)//перемещаемся
        {
            switch(Dir)
            {
                case 8:
                    PosX--;
                    break;
                case 2:
                    PosX++;
                    break;
                case 4:
                    PosY--;
                    break;
                case 6:
                    PosY++;
                    break;
            }
        }
            
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MazeArray = GenerateMaze();
            DrawPic();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            int RndmX;
            int RndmY;
            do
            {
                RndmX = rnd.Next(Convert.ToInt32(SizeX * 0.4), Convert.ToInt32(SizeX * 0.6));
,                RndmY = rnd.Next(Convert.ToInt32(SizeY * 0.4), Convert.ToInt32(SizeY * 0.6));
                //RndmX = RndmX - (RndmX % 2); //делаем четным, чтоб искать полегче
                //RndmX = RndmX - (RndmX % 2);
            } while (MazeArray[RndmX, RndmY] == 0);
            MazeArray[RndmX, RndmY] = 3;
            PosX = RndmX;
            PosY = RndmY;
            DrawPic();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button4_Click(object sender, EventArgs e)//Командир ведет нас вдоль правой стенки
        {
            //      8
            //
            //  4   +   6
            //
            //      2

            int Direction = 8; //
            while (PosX > 0 && PosY > 0 && PosX < (SizeX - 1) && PosY < (SizeY - 1))
            //for (int lol = 0; lol < 1000; lol++)
            {
                Watcher(PosX, PosY);
                switch (Direction)
                {
                    case 8:
                        if (Neighbours[0, 1] != 0)//если впереди нет стены
                        {
                            if (Neighbours[1, 2] != 0)//а может, можем свернуть?
                            {
                                Direction = 6;
                                break;
                            }
                            Driver(Direction); //поворачиваем
                            MazeArray[PosX, PosY] = 3; //отмечаем путь
                            DrawPic();
                        }
                        break;
                    case 6:
                        if (Neighbours[1, 2] != 0)
                        {
                            if (Neighbours[2, 1] != 0)
                            {
                                Direction = 2;
                                break;
                            }
                            Driver(Direction); //поворачиваем
                            MazeArray[PosX, PosY] = 3; //отмечаем путь
                            DrawPic();
                        }
                        break;
                    case 2:
                        if (Neighbours[2, 1] != 0)
                        {
                            if (Neighbours[1, 0] != 0)
                            {
                                Direction = 4;
                                break;
                            }
                            Driver(Direction); //поворачиваем
                            MazeArray[PosX, PosY] = 3; //отмечаем путь
                            DrawPic();
                        }
                        break;
                    case 4:
                        if (Neighbours[1, 0] != 0)
                        {
                            if (Neighbours[0, 1] != 0)
                            {
                                Direction = 8;
                                break;
                            }
                            Driver(Direction); //поворачиваем
                            MazeArray[PosX, PosY] = 3; //отмечаем путь
                            DrawPic();
                        }
                        break;

                }
                
                /*if (Neighbours[0, 1] != 0)//если впереди нет стены
                {
                    if (Neighbours[1, 2] != 0)//а может, можем свернуть?
                    {
                        Driver(Direction); //поворачиваем
                        MazeArray[PosX, PosY] = 3; //отмечаем путь
                        DrawPic();
                    }

                    Driver(Direction); //гоним
                    MazeArray[PosX, PosY] = 3; //отмечаем путь
                    DrawPic();
                }
                else if (Neighbours[1, 2] != 0)
                {
                    if (Neighbours[2, 1] != 0)
                    {
                        Driver(2); //поворачиваем
                        MazeArray[PosX, PosY] = 3; //отмечаем путь
                        DrawPic();
                    }
                    Driver(6); //гоним
                    MazeArray[PosX, PosY] = 3; //отмечаем путь
                    DrawPic();
                }
                else if (Neighbours[2, 1] != 0)
                {
                    if (Neighbours[1, 0] != 0)
                    {
                        Driver(4); //поворачиваем
                        MazeArray[PosX, PosY] = 3; //отмечаем путь
                        DrawPic();
                    }
                    Driver(2); //гоним
                    MazeArray[PosX, PosY] = 3; //отмечаем путь
                    DrawPic();
                }
                else if (Neighbours[1, 0] != 0)
                {
                    if (Neighbours[0, 1] != 0)
                    {
                        Driver(8); //поворачиваем
                        MazeArray[PosX, PosY] = 3; //отмечаем путь
                        DrawPic();
                    }
                    Driver(4); //гоним
                    MazeArray[PosX, PosY] = 3; //отмечаем путь
                    DrawPic();
                }*/

                //0 - тропинки, 2 - стены, 3 - один раз прошли, 4 - дважды прошли
                //switch (rand.Next(4))
                //{
                   // case 0:
                //по правилу правой руки

                     /*   if (Neighbours[0, 2] != 0)//если впереди нет новой дорожки
                        {
                            if (MazeArray[1, 2] == 3) //а вдруг мы тут уже проходили один раз
                            {
                                Driver(8); //продвигаемся по натоптанному
                                MazeArray[PosX, PosY] = 4; //отмечаем возвращение
                                DrawPic();
                                Driver(8); //на 2
                                MazeArray[PosX, PosY] = 4; //отмечаем возвращение
                                DrawPic();
                            }
                            break; //меняем направление
                        }
                        //ежели нет, топчем вперед
                        Driver(8);
                        MazeArray[PosX, PosY] = 3;
                        DrawPic();
                        Driver(8);
                        MazeArray[PosX, PosY] = 3;
                        DrawPic();
                        //break;
                   // case 1:
                        /*if (Neighbours[4, 2] != 0)//если впереди нет новой дорожки
                        {
                            if (MazeArray[3, 2] == 3) //а вдруг мы тут уже проходили один раз
                            //if (MazeArray[i - 1, j] != 0)
                            {
                                Driver(2); //продвигаемся по натоптанному
                                MazeArray[PosX, PosY] = 4; //отмечаем возвращение
                                DrawPic();
                                Driver(2); //на 2
                                MazeArray[PosX, PosY] = 4; //отмечаем возвращение
                                DrawPic();
                            }
                            break; //меняем направление
                        }
                        //ежели нет, топчем вперед
                        Driver(2);
                        MazeArray[PosX, PosY] = 3;
                        DrawPic();
                        Driver(2);
                        MazeArray[PosX, PosY] = 3;
                        DrawPic();
                    //    break;
                    //case 2:
                        if (Neighbours[2, 0] != 0)//если впереди нет новой дорожки
                        {
                            if (MazeArray[2, 1] == 3) //а вдруг мы тут уже проходили один раз
                            //if (MazeArray[i - 1, j] != 0)
                            {
                                Driver(4); //продвигаемся по натоптанному
                                MazeArray[PosX, PosY] = 4; //отмечаем возвращение
                                DrawPic();
                                Driver(4); //на 2
                                MazeArray[PosX, PosY] = 4; //отмечаем возвращение
                                DrawPic();
                            }
                            break; //меняем направление
                        }
                        //ежели нет, топчем вперед
                        Driver(4);
                        MazeArray[PosX, PosY] = 3;
                        DrawPic();
                        Driver(4);
                        MazeArray[PosX, PosY] = 3;
                        DrawPic();
                        break;
                   // case 3:
                        if (Neighbours[2, 4] != 0)//если впереди нет новой дорожки
                        {
                            if (MazeArray[2, 3] == 3) //а вдруг мы тут уже проходили один раз
                            //if (MazeArray[i - 1, j] != 0)
                            {
                                Driver(6); //продвигаемся по натоптанному
                                MazeArray[PosX, PosY] = 4; //отмечаем возвращение
                                DrawPic();
                                Driver(6); //на 2
                                MazeArray[PosX, PosY] = 4; //отмечаем возвращение
                                DrawPic();
                            }
                            break; //меняем направление
                        }
                        //ежели нет, топчем вперед
                        Driver(6);
                        MazeArray[PosX, PosY] = 3;
                        DrawPic();
                        Driver(6);
                        MazeArray[PosX, PosY] = 3;
                        DrawPic();
                        break;*/
                //}
            }

        }
    }

}

