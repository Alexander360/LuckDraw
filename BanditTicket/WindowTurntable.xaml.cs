using BanditTicket.Logic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BanditTicket
{
    /// <summary>
    /// WindowTurntable.xaml 的交互逻辑
    /// </summary>
    public partial class WindowTurntable : Window
    {
        public WindowTurntable()
        {
            InitializeComponent();
#if !DEBUG
            Topmost = true;
#endif

        }

        private void Turntable_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Turntable_AwardProcess(RandomModel award, inputReuslt input)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("用户单号：" + input.orderNumber);
            sb.AppendLine("中了：" + award.name);
            MessageBox.Show(sb.ToString(), "恭喜", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "image", "back.jpg");
            if (File.Exists(path))
            {
                Uri uri = new Uri(path, UriKind.Absolute);
                BitmapImage bitmap = new BitmapImage(uri);
                back.Source = bitmap;
            }

        }
    }
}
