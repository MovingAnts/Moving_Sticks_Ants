using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Threading;

namespace Moving_Sticks_Ants
{
    class UI
    {
        public void draw(Ant[]ant,Game minGame,Game maxGame,Canvas ell,Storyboard storyboard,Label MinTime,Label MaxTime,Line Stick1,Line Stick2,double stickLength)
        {
            int antNum = ant.Count();
            Ellipse[][] ellipse;
            ellipse = new Ellipse[2][];
            Game[] newGame = new Game[2];
            newGame[0] = minGame;
            newGame[1] = maxGame;
            Stick1.X2 = stickLength + 10;
            Stick2.X2 = stickLength + 10;

            /*绘制每一只蚂蚁*/
            for (int j = 0; j < 2; j++)
            {
                ellipse[j] = new Ellipse[antNum];
                for (byte i = 0; i < antNum; i++)
                {
                    ellipse[j][i] = new Ellipse();
                    Canvas.SetLeft(ellipse[j][i], ant[i].Position);
                    Canvas.SetTop(ellipse[j][i], j * 100 + 50);
                    ellipse[j][i].Width = 20;
                    ellipse[j][i].Height = 20;
                    ellipse[j][i].Fill = new SolidColorBrush(Color.FromArgb(0xff, (Byte)((150 + i * 36) % 256), (Byte)((100 + i * 62) % 256)
                        , (Byte)((45 + i * 17) % 256)));
                    ell.Children.Add(ellipse[j][i]);
                }
            }
            int[] count = new int[2];

            /*用Storyboard绘制爬行过程*/
            for (int j = 0; j < 2; j++)
            {
                double time = 0;
                double[] last_ant = new double[antNum];
                for (int i = 0; i < antNum; i++)
                {
                    last_ant[i] = Canvas.GetLeft(ellipse[j][i]);
                }
                Queue<double[]> result=new Queue<double[]>();
                Queue<double> resultTime=new Queue<double>();
                while (newGame[j].Result.Count != 0)
                {
                    double[] ants = newGame[j].Result.Dequeue();
                    result.Enqueue(ants);
                    double speed = newGame[j].ResultTime.Dequeue() * 100;
                    resultTime.Enqueue(speed/100);
                    for (int i = 0; i < antNum; i++)
                    {
                        DoubleAnimation doubleAnimation = new DoubleAnimation();
                        doubleAnimation.From = last_ant[i];
                        doubleAnimation.To = ants[i];
                        last_ant[i] = ants[i];
                        doubleAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(speed));
                        doubleAnimation.BeginTime = TimeSpan.FromMilliseconds(time);
                        Storyboard.SetTarget(doubleAnimation, ellipse[j][i]);
                        Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("(Canvas.Left)"));
                        storyboard.Children.Add(doubleAnimation);
                    }
                    time = time + speed;
                }
                /*防止最短与最长是同一情况*/
                while (result.Count != 0)
                {
                    double[] ants = result.Dequeue();
                    newGame[j].Result.Enqueue(ants);
                    double speed =  resultTime .Dequeue();
                    newGame[j].ResultTime.Enqueue(speed);
                }

            }

            /*绘制文字*/
            string[] speed1 = new string[2];
            for (int j = 0; j < 2; j++)
            {
                for (int i = 0; i < antNum; i++)
                {
                    speed1[j] = speed1[j] + "(" + i + "," + newGame[j].Speed[i] + ")";
                }
            }
            MinTime.Content = Math.Round(newGame[0].TotalTime, 2) + " s    "+ speed1[0];
            MaxTime.Content = Math.Round(newGame[1].TotalTime, 2) + " s    "+ speed1[1];
            storyboard.Begin();
        }
    }
}
