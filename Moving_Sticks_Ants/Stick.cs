using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moving_Sticks_Ants
{
    class Stick
    {
        private double length;//树竿长度
        private int ant_num;//树竿上蚂蚁的数目

        public double Length
        {
            get
            {
                return length;
            }

            set
            {
                length = value;
            }
        }

        public int Ant_num
        {
            get
            {
                return ant_num;
            }

            set
            {
                ant_num = value;
            }
        }

        public Stick(double l)
        {
            length = l;
        }
    }
}
