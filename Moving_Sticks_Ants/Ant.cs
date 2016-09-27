using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moving_Sticks_Ants
{
    class Ant
    {
        private int direction;//-1 左 1右
        private double speed;
        private double position;
        private int status;//0爬出去了 1未爬出

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

        public int Direction
        {
            get
            {
                return direction;
            }

            set
            {
                direction = value;
            }
        }

        Ant(int d,double s,double p)
        {
            Direction = d;
            Speed = s;
            Position = p;
        }

        public void creep(double time)
        {
            Position = Position + time * speed * direction;
        }
    }
}
