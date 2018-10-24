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
using BanditTicket.Logic;
//new
using System.Windows.Media.Animation;
using System.Windows.Threading;
using System.IO;

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
            logic = new JackPotLogic(Application.Current.MainWindow);
            int angle = 5040;
            for (int i = 0; i < 8; i++)
            {
                angle += 45;
                _ListAngle.Add(angle);
            }
        }
        JackPotLogic logic;
        /// <summary>
        /// 保存八个角度
        /// </summary>
        List<int> _ListAngle = new List<int>();
        inputReuslt result = new inputReuslt();
        int _Index = 0;
        int _OldAngle = 0;
        RandomModel polt;
        private void btnStartTurntable_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                logic.isSengxia();
                var windo = new Input(result);
                windo.Owner = Application.Current.MainWindow;
                if (windo.ShowDialog() == true)
                {
                    polt = logic.Next(result.bayNumber, result.orderNumber, result.price);
                    btnStartTurntable.IsEnabled = false;
                    var t = polt.id - 2;
                    if (t<0)
                    {
                        t = 8 + t;
                    }
                    _Index = t;
                    Storyboard sb = (Storyboard)this.FindResource("zhuandong");
                    sb.Completed -= this.sb_Completed;
                    sb.Completed += new EventHandler(sb_Completed);
                    ((SplineDoubleKeyFrame)((DoubleAnimationUsingKeyFrames)sb.Children[0]).KeyFrames[0]).Value = _OldAngle;
                    ((SplineDoubleKeyFrame)((DoubleAnimationUsingKeyFrames)sb.Children[0]).KeyFrames[3]).Value = _ListAngle[_Index];
                    sb.Begin();
                }

            }
            catch (Exception eex)
            {
                return;
            }

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
                AwardProcess(polt, result);
            };
            dt.Start();
        }

        public delegate void AwardDelegate(RandomModel award, inputReuslt input);

        /// <summary>
        /// 返回转到的奖项信息
        /// </summary>
        public event AwardDelegate AwardProcess;

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "image", "img.png");
            if (File.Exists(path))
            {
                Uri uri = new Uri(path, UriKind.Absolute);
                BitmapImage bitmap = new BitmapImage(uri);
                back.Source = bitmap;
            }
        }
    }
}
