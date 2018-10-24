using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BanditTicket.Logic;
//new
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace BanditTicket
{
    /// <summary>
    /// Turntable.xaml 的交互逻辑
    /// </summary>
    public partial class Turntable : UserControl
    {
        public Turntable()
        {
            InitializeComponent();

            int angle = 5040;
            for (int i = 0; i < 8; i++)
            {
                angle += 45;
                _ListAngle.Add(angle);
            }
        }

        /// <summary>
        /// 保存八个角度
        /// </summary>
        List<int> _ListAngle = new List<int>();
        /// <summary>
        /// 产生随机数
        /// </summary>
        Random _Random = new Random();
        int _Index = 0;
        int _OldAngle = 0;
        private void btnStartTurntable_Click(object sender, RoutedEventArgs e)
        {
            btnStartTurntable.IsEnabled = false;

            var ran = new LiuxuRandom().GetRandomModel(null);

            _Index = _Random.Next(0, 8);
            Storyboard sb = (Storyboard)this.FindResource("zhuandong");
            sb.Completed -= this.sb_Completed;
            sb.Completed += new EventHandler(sb_Completed);
            ((SplineDoubleKeyFrame)((DoubleAnimationUsingKeyFrames)sb.Children[0]).KeyFrames[0]).Value = _OldAngle;
            ((SplineDoubleKeyFrame)((DoubleAnimationUsingKeyFrames)sb.Children[0]).KeyFrames[3]).Value = _ListAngle[_Index];
            sb.Begin();
        }

        void sb_Completed(object sender, EventArgs e)
        {
            DispatcherTimer dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromSeconds(0.3);
            dt.Tick += delegate 
            {
                dt.Stop();
                _OldAngle = (_ListAngle[_Index] % 360);
                btnStartTurntable.IsEnabled = true;
                AwardProcess(GetAward(_ListAngle[_Index]));
            };
            dt.Start();
        }

        public delegate void AwardDelegate(Award award);

        /// <summary>
        /// 返回转到的奖项信息
        /// </summary>
        public event AwardDelegate AwardProcess;

        private Award GetAward(int angle)
        {

            Award result = Award.谢谢参与;
            switch (angle)
            {
                case 5085:
                    result = Award.一等奖;
                    break;
                case 5130:
                    //result = "谢谢参与";
                    break;
                case 5175:
                    result = Award.二等奖;
                    break;
                case 5220:
                    //result = "谢谢参与";
                    break;
                case 5265:
                    result = Award.三等奖;
                    break;
                case 5310:
                    //result = "谢谢参与";
                    break;
                case 5355:
                    result = Award.幸运奖;
                    break;
                case 5400:
                    //result = "谢谢参与";
                    break;
                default:
                    break;
            }

            return result;
        }
    }

    public enum Award
    {
        一等奖,
        二等奖,
        三等奖,
        幸运奖,
        谢谢参与
    }
}
