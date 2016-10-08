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
        int antNum = 10;//蚂蚁数量 取值范围(1,12)
        double stickLen = 950;//树枝长度 取值范围(150,950)
        Ant[] ant;//蚂蚁数组对象
        GameRoom gameRoom;//游戏场对象
        public MainWindow()
        {
            InitializeComponent();
            ant = new Ant[antNum];
            /*设置蚂蚁速度*/
            ant[0] = new Ant(0, 5, 30);
            ant[1] = new Ant(1, 5, 80);
            ant[2] = new Ant(2, 5, 110);
            ant[3] = new Ant(3, 5, 160);
            ant[4] = new Ant(4,5, 250);
            ant[5] = new Ant(5, 11, 550);
            ant[6] = new Ant(6, 7, 570);
            ant[7] = new Ant(7, 8, 750);
            ant[8] = new Ant(8, 9, 850);
            ant[9] = new Ant(9, 10, 950);
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
