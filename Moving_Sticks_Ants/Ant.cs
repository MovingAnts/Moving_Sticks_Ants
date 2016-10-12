using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moving_Sticks_Ants
{
    class Ant
    {
        private int id;//蚂蚁id
        private double speed;//蚂蚁速度
        private double position;//蚂蚁位置

        /// <summary>
        /// 蚂蚁位置GET\SET函数
        /// </summary>
        public double Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;
            }
        }

        /// <summary>
        /// 蚂蚁速度GET\SET函数
        /// </summary>
        public double Speed
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
        /// <param name="i"></param>
        /// <param name="s"></param>
        /// <param name="p"></param>
        public Ant(int i, double s, double p)
        {
            id = i;
            Speed = s;
            Position = p;
        }

        /// <summary>
        /// 按照给定时间，结合自身速度、位置进行爬行操作，返回爬行后的位置
        /// </summary>
        /// <param name="time"></param>
        /// <returns>position</returns>
        public double creep(double time)
        {
            Position = Position + time * speed;
            return position;
        }

        /// <summary>
        /// 改变方向
        /// </summary>
        public void changeDirection()
        {
            speed = -1*speed;
        }

        /// <summary>
        /// 计算下一次碰撞时间
        /// </summary>
        /// <param name="ant"></param>
        /// <returns></returns>
        public double anticipateTime(Ant[] ant)
        {
            int targetAnt;
            if (speed > 0)
            {
                targetAnt = id + 1;
            }
            else if (speed == 0)
            {
                return double.MaxValue;
            }
            else
            {
                targetAnt = id - 1;
            }
            if (targetAnt == ant.Length||targetAnt==-1) return Double.MaxValue;
            double relativeDistance = position - ant[targetAnt].position;
            double relativeSpeed = speed - ant[targetAnt].speed;
            if (relativeSpeed == 0) return Double.MaxValue;
            if (relativeSpeed * relativeDistance > 0) return Double.MaxValue;
            else return -1*relativeDistance / relativeSpeed;
        }

        /// <summary>
        /// 计算无障碍时离开时间
        /// </summary>
        /// <param name="stickLength"></param>
        /// <returns></returns>
        public double endTime(double stickLength)
        {
            if (speed > 0)
            {
                return (stickLength-position)/ speed;
            }
            else if(speed ==0)
            {
                return double.MaxValue;
            }
            else
            {
                return position / speed * -1;
            }
        }
    }
}
