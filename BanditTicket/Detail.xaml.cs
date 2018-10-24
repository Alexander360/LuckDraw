using BanditTicket.Logic;
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
    /// Detail.xaml 的交互逻辑
    /// </summary>
    public partial class Detail : Window
    {
        public Detail()
        {
            InitializeComponent();
            Loaded += Detail_Loaded;
        }

        private void Detail_Loaded(object sender, RoutedEventArgs e)
        {
            Logic.JackPotConfig config = new Logic.JackPotConfig();
            var user = config.loadUsers();
            var potEntity = config.loadJackPot();
            List<RandomModel> model = new List<RandomModel>();
            foreach (var item in potEntity.prize)
            {
                if (model.Any(m => m.id == item.id))
                {
                    thowEx("有重复的ID奖项");
                }
                model.Add(new RandomModel { id = item.id, name = item.name });
            }
            foreach (var item in potEntity.day)
            {
                foreach (var itemRow in item.items)
                {
                    try
                    {
                        itemRow.dt = DateTime.Parse(item.time + " " + getType(itemRow.OpenTime));
                    }
                    catch (Exception ex)
                    {
                        thowEx("日期识别错误" + item.time);
                    }
                    if (itemRow.dt < DateTime.Now)
                    {
                        var i1 = model.FirstOrDefault(m => m.id == itemRow.id);
                        if (i1 == null)
                        {
                            thowEx("未找到的奖品ID");
                        }
                        i1.number += itemRow.number;
                    }
                }
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("目前可以抽的剩余物品");
            foreach (var item in model)
            {
                sb.AppendLine(item.name + " -- 剩余--" + item.number);
            }
            sb.AppendLine();
            sb.AppendLine("目前人数统计");

            var key=user.users.GroupBy(m => DateTime.Parse(m.createTime).ToShortDateString()).ToList();
            foreach (var item in key)
            {
                sb.AppendLine(item.Key + " -- 剩余--" + item.ToList().Count);
            }
            detail.Text = sb.ToString();
        }
        private void thowEx(string message)
        {
            MessageBox.Show(this, message);
            throw new Exception();
        }

        private string getType(string type)

        {
            switch (type)
            {
                case "PM":
                    return "13:00:00";
                default:
                    return "00:00:00";
            }
        }
    }
}
