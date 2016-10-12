using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace Moving_Sticks_Ants
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        int antNum = 5;//蚂蚁数量 取值范围(1,12)
        double stickLen = 300;//树枝长度 取值范围(150,950)
        Ant[] ant;//蚂蚁数组对象
        GameRoom gameRoom;//游戏场对象
        public MainWindow()
        {
            InitializeComponent();
            ant = new Ant[antNum];
            /*设置蚂蚁速度，位置信息需要按照编号递增,即低序号蚂蚁必须在高序号蚂蚁的左侧0.0*/
            ant[0] = new Ant(0, 5, 30);
            ant[1] = new Ant(1, 5, 80);
            ant[2] = new Ant(2, 5, 110);
            ant[3] = new Ant(3, 5, 160);
            ant[4] = new Ant(4,5, 250);
            //ant[5] = new Ant(5, 121, 250);
            //ant[6] = new Ant(6, 7, 570);
            //ant[7] = new Ant(7, 8, 850);
            //ant[8] = new Ant(8, 9, 850);
            //ant[9] = new Ant(9, 10, 851);
            //ant[10] = new Ant(0, 5, 890);
            //ant[11] = new Ant(1, 5,900);
            //ant[12] = new Ant(2, 5, 111);
            //ant[13] = new Ant(3, 5, 161);
            //ant[14] = new Ant(4, 5, 251);
            //ant[15] = new Ant(5, 111, 551);
            //ant[16] = new Ant(6, 7, 571);
            //ant[17] = new Ant(7, 8, 751);
            //ant[18] = new Ant(8, 9, 851);
            //ant[19] = new Ant(9, 10, 951);
            /*初始化游戏场*/
            gameRoom = new GameRoom(ant, stickLen);
            /*游戏开始*/
            gameRoom.startRoom();
        }

        /// <summary>
        /// START按钮触发事件，开始进行结果绘图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UI ui = new UI();
            ui.draw(ant, gameRoom.Newgame[gameRoom.MinGame], gameRoom.Newgame[gameRoom.MaxGame], ell, storyboard, MinTime, MaxTime,Stick1,Stick2,stickLen);
        }
    }
    
}
