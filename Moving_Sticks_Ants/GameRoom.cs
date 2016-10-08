using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moving_Sticks_Ants
{
    class GameRoom
    {
        private Game[] newgame;//游戏数组对象
        private int antNum;//蚂蚁数量
        private double stickLen;//树枝长度
        private double[] totalTime;//蚂蚁爬完树枝花费时间数组对象
        private double maxTime;//最长时间
        private double minTime;//最短时间
        private int maxGame;//最长时间对应游戏id
        private int minGame;//最短时间对应游戏id
        private Ant[] ant;//蚂蚁数组对象

        /// <summary>
        /// 游戏数组对象GET\SET函数
        /// </summary>
        internal Game[] Newgame
        {
            get
            {
                return newgame;
            }

            set
            {
                newgame = value;
            }
        }

        /// <summary>
        /// 最长时间对应游戏idGET\SET函数
        /// </summary>
        public int MaxGame
        {
            get
            {
                return maxGame;
            }

            set
            {
                maxGame = value;
            }
        }

        /// <summary>
        /// 最短时间对应游戏idGET\SET函数
        /// </summary>
        public int MinGame
        {
            get
            {
                return minGame;
            }

            set
            {
                minGame = value;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ant"></param>
        /// <param name="stick_length"></param>
        public GameRoom(Ant[] ant,double stick_length)
        {
            this.ant = ant;
            stickLen = stick_length;
            antNum = ant.Count();
            Newgame = new Game[(int)Math.Pow(2,antNum)];
            totalTime = new double[(int)Math.Pow(antNum, 2)];
            maxTime = 0;
            minTime = double.MaxValue;
            MinGame = -1;
            MaxGame = -1;
        }

        /// <summary>
        /// 开始游戏
        /// </summary>
        public void startRoom()
        {           
            for (int i = 0; i < Math.Pow(2, antNum); i++)
            {
                /*构造速度*/
                string binary = Convert.ToString(i, 2);
                for (int j = 0; j < antNum; j++)
                {
                    if (binary.Count() <= j)
                    {
                        ant[j].Speed = Math.Abs(ant[j].Speed);
                    }
                    else if (binary[j] =='0')
                    {
                        ant[j].Speed = Math.Abs(ant[j].Speed);
                    }
                    else
                    {
                        ant[j].Speed = -1 * Math.Abs(ant[j].Speed);
                    }
                }

                /*初始化游戏*/
                Newgame[i] = new Game(ant, stickLen);
                
                /*游戏进行*/
                while (!Newgame[i].isOver())
                {
                    Newgame[i].begin();
                }

                /*如果该场游戏花费时间不是最短与最长，释放游戏(减少内存消耗)*/
                if (Newgame[i].TotalTime > minTime && Newgame[i].TotalTime < maxTime)
                {
                    Newgame[i] = null;
                }
                else
                {
                    /*更新最短时间对应游戏id*/
                    if (Newgame[i].TotalTime < minTime)
                    {
                        /*将原最短游戏释放(减少内存消耗)*/
                        if (MinGame!=-1&&MinGame != MaxGame)
                        {
                            Newgame[MinGame] = null;
                        }
                        MinGame = i;
                        minTime = Newgame[i].TotalTime;
                    }
                    /*更新最场时间对应游戏id*/
                    if (Newgame[i].TotalTime > maxTime)
                    {
                        /*将原最长游戏释放(减少内存消耗)*/
                        if (MaxGame != -1 && MinGame != MaxGame)
                        {
                            Newgame[MaxGame] = null;
                        }
                        MaxGame = i;
                        maxTime = Newgame[i].TotalTime;
                    }
                }
            }
        }
    }
}
