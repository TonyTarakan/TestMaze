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
            for (int r = 0; r < SizeX - 1; r++)
            {
                for (int c = 0; c < SizeY - 1; c++)
                {
                    if (MazeArray[r, c] == 0)
                    {
                        Rectangle rectangle = new Rectangle(r * pictureBox1.Width / SizeX, c * pictureBox1.Height / SizeY, pictureBox1.Width / SizeX, pictureBox1.Height / SizeY);
                        g.FillRectangle(blueBrush, rectangle);
                    }
                    if (MazeArray[r, c] == 3)
                    {
                        Rectangle rectangle = new Rectangle(r * pictureBox1.Width / SizeX + 1, c * pictureBox1.Height / SizeY + 1, pictureBox1.Width / SizeX - 2 , pictureBox1.Height / SizeY - 2);
                        g.FillRectangle(redBrush, rectangle);
                    }
                    if (MazeArray[r, c] == 2)
                    {
                        Rectangle rectangle = new Rectangle(r * pictureBox1.Width / SizeX, c * pictureBox1.Height / SizeY, pictureBox1.Width / SizeX, pictureBox1.Height / SizeY);
                        g.FillRectangle(whiteBrush, rectangle);
                    }
                }
            }
            pictureBox1.Image = bmp;
        }

        public int[,] generateMaze()//Новый лабиринт
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
                            if (MazeArray[i - 2, j] != 0) //если впереди стена
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
            return MazeArray;
        }
            
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public void button1_Click(object sender, EventArgs e)
        {
            //MazeGen maz = new MazeGen();
            //MazeArray = maz.generateMaze();
            MazeArray = generateMaze();
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
                RndmY = rnd.Next(Convert.ToInt32(SizeY * 0.4), Convert.ToInt32(SizeY * 0.6));

            } while (MazeArray[RndmX, RndmY] != 2);
            MazeArray[RndmX, RndmY] = 3;
            DrawPic();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

   /* 
    * class Constants
    {
        public const int SizeX = 50; //razmer labirinta
        public const int SizeY = 50;
    }

    public class MazeGen
    {
        int[,] MazeArray = new int[Constants.SizeX, Constants.SizeY];
        
    }*/
}

