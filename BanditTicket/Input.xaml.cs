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
using System.Windows.Shapes;

namespace BanditTicket
{
    /// <summary>
    /// Input.xaml 的交互逻辑
    /// </summary>
    public partial class Input : Window
    {
        inputReuslt currentResult;
        public Input(inputReuslt result)
        {
            InitializeComponent();
            currentResult = result;
            t1.Focus();
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            var bayNumber = t1.Text;
            var orderNumber = t2.Text;
            var str = t3.Text;
            double price = 0;
            if (bayNumber.Trim().Length != 4 || orderNumber.Trim().Length != 8)
            {
                MessageBox.Show("您的票号输入有误!");
                return;
            }
            else if (!double.TryParse(str, out price) || price < 99)
            {
                MessageBox.Show("单笔消费金额未满99 元不能抽奖!");
                return;
            }
            DialogResult = true;
            currentResult.bayNumber = bayNumber;
            currentResult.orderNumber = orderNumber;
            currentResult.price = price;
            Close();
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void t3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                button_Click(null, null);
            }
        }
    }
    public class inputReuslt
    {
        public string bayNumber { get; set; }
        public string orderNumber { get; set; }
        public double price { get; set; }
    }
}
