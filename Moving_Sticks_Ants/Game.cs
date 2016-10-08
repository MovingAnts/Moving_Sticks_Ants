using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Moving_Sticks_Ants
{
    class Game
    {
        private int antNum;//蚂蚁数量
        Ant[] ant;//蚂蚁数组对象
        Queue<double[]> result;//游戏结果，记录每一个步骤
        Queue<double> resultTime;//游戏结果中每一个步骤对应时间
        double[] speed;//蚂蚁原始速度
        double totalTime;//总花费时间
        double stickLength;//数值长度

        /// <summary>
        /// 游戏结果，记录每一个步骤GET\SET函数
        /// </summary>
        internal Queue<double[]> Result
        {
            get
            {
                return result;
            }

            set
            {
                result = value;
            }
        }

        /// <summary>
        /// 蚂蚁数组对象GET\SET函数
        /// </summary>
        internal Ant[] Ant
        {
            get
            {
                return ant;
            }

            set
            {
                ant = value;
            }
        }

        /// <summary>
        /// 游戏结果中每一个步骤对应时间GET\SET函数
        /// </summary>
        public Queue<double> ResultTime
        {
            get
            {
                return resultTime;
            }

            set
            {
                resultTime = value;
            }
        }

        /// <summary>
        /// 总花费时间GET\SET函数
        /// </summary>
        public double TotalTime
        {
            get
            {
                return totalTime;
            }

            set
            {
                totalTime = value;
            }
        }

        /// <summary>
        /// 蚂蚁原始速度GET\SET函数
        /// </summary>
        public double[] Speed
        {
            get
            {
                return speed;
            }

            set
            {
                speed = value;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="an"></param>
        /// <param name="sl"></param>
        public Game(Ant[] an,double sl)
        {
            antNum = an.Count();
            stickLength = sl;
            Ant = new Ant[antNum];
            Speed = new double[antNum];
            for(int i = 0; i < antNum; i++)
            {
                Ant[i] = new Ant(i, an[i].Speed, an[i].Position);
                Speed[i] = an[i].Speed;
            }
            
            Stick stick = new Stick(stickLength);
            Result = new Queue<double[]>();
            ResultTime = new Queue<double>();
            TotalTime = 0;
        }

        /// <summary>
        /// 判断该场游戏是否结束
        /// </summary>
        /// <returns></returns>
        public bool isOver()
        {
            bool over = true;
            for(int i = 0; i < antNum; i++)
            {
                if (ant[i].Speed != 0)
                {
                    over = false;
                }
            }
            return over;
        }

        /// <summary>
        /// 开始游戏
        /// </summary>
        public void begin()
        {

            double[] minus_time = new double[antNum];
            double[] minus_end_time = new double[antNum];
            double[] position = new double[antNum];
            double minus = double.MaxValue;
            Queue<int> crash_ant = new Queue<int>();

            /*计算每一只蚂蚁第一次碰撞发生时间，并记录最短时间*/
            for (int i = 0; i < antNum; i++)
            {
                
                minus_time[i] = Ant[i].anticipateTime(Ant);
                if (minus_time[i] < minus)
                {
                    minus = minus_time[i];
                }
            }
            /*计算每一只蚂蚁无障碍时结束爬行发生时间，与最短碰撞时间比对得出最短时间*/
            for (int i = 0; i < antNum; i++)
            {
                minus_end_time[i] = ant[i].endTime(stickLength);
                if (minus_end_time[i] < minus)
                {
                    minus = minus_end_time[i];
                }
            }
            /*获取发生碰撞的蚂蚁*/
            for (int i = 0; i < antNum; i++)
            {
                if (minus_time[i] == minus && ant[i].Position > 0 && ant[i].Position < stickLength)
                {
                    crash_ant.Enqueue(i);
                }
            }
            /*每一只蚂蚁按照最短时间爬行，并将出界蚂蚁速度更新为0*/
            for (int i = 0; i < antNum; i++)
            {
                position[i] = Ant[i].creep(minus);
                if (Math.Abs(position[i])<0.001)
                {
                    ant[i].Speed = 0;
                }
                else if(Math.Abs(position[i]- stickLength)<0.001)
                {
                    ant[i].Speed = 0;
                }
            }
            /*将碰撞蚂蚁方向反向*/
            while (crash_ant.Count != 0)
            {
                int i = crash_ant.Dequeue();
                ant[i].changeDirection();
            }
            /*记录该次步骤*/
            Result.Enqueue(position);
            /*记录该次步骤花费时间*/
            ResultTime.Enqueue(minus);
            /*更新总花费时间*/
            totalTime = totalTime + minus;
        }
    }
}
