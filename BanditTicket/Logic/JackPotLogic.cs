using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace BanditTicket.Logic
{
    public class JackPotLogic
    {
        Window ower;
        public JackPotLogic(Window ower2)
        {
            ower = ower2;
        }
        JackPotConfig config = new JackPotConfig();
        LiuxuRandom random = new LiuxuRandom();
        public RandomModel Next(string bayNumber, string orderNumber, double price)
        {
            if (bayNumber.Trim().Length != 4 || orderNumber.Trim().Length != 8)
            {
                thowEx("您的票号输入有误!");
            }
            else if (price < 99)
            {
                thowEx("单笔消费金额未满99 元不能抽奖!");
            }
            var users = config.loadUsers();
            if (users.users.Any(m => m.bayNumber == bayNumber && m.orderNumber == orderNumber))
            {
                thowEx("此票已开奖请勿重复抽奖!");
            }
            var result = GetRandowModel();
            users.users.Add(new User { bayNumber = bayNumber, model = result, orderNumber = orderNumber, price = price, createTime=DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") });
            config.SaveUser(users);
            return result;
        }
        public void isSengxia()
        {
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

            if (!model.Any(m => m.number > 0))
            {
                thowEx(DateTime.Now.Hour >= 13 ? "今天抽奖活动已经结束请等待第二天开奖 " : "等待下午开奖");
            }
        }
        private RandomModel GetRandowModel()
        {
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
            var resultPot = random.GetRandomModel(model);
            if (resultPot != null)
            {

                foreach (var item in potEntity.day)
                {
                    foreach (var itemRow in item.items)
                    {
                        if (itemRow.id == resultPot.id && itemRow.number > 0 && itemRow.dt < DateTime.Now)
                        {
                            resultPot.number--;
                            itemRow.number--;
                            config.SaveJackPot(potEntity);
                            return resultPot;
                        }
                    }
                }
            }
            else
            {
                thowEx(DateTime.Now.Hour >= 13 ? "今天抽奖活动已经结束请等待第二天开奖 " : "等待下午开奖");
            }
            return null;
        }
        private void thowEx(string message)
        {
            MessageBox.Show(ower, message);
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
