using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BanditTicket.Logic
{
    class LiuxuRandom
    {
        public RandomModel GetRandomModel(List<RandomModel> list)
        {
            var goList = list.Where(m => m.number > 0).OrderBy(m => m.id).ToList();

            if (goList.Any())
            {
                //把概率分成100（如果所有概率相加超过100） 报错
                var sumNumber = goList.Sum(m => m.number);
                //if (sumNumber > 100)
                //{
                //    //大于100自动缩小

                //    throw new ArgumentException("系统错误，请联系管理员！");
                //}
                //else if (sumNumber < 100)
                //{
                //    //小于100 自动增长
                //    var coff = 100.0 / sumNumber;
                //    //遍历所有概率自动增长
                //    list.ForEach(m => m.num = m.probability * 100 / sumProbability);
                //}
                var tRandom = new Random(Guid.NewGuid().GetHashCode());
                var result = tRandom.Next(0, sumNumber);
                int i = 0;
                foreach (var item in goList)
                {
                    if (i <= result && result < i + item.number)
                    {
                        return item;
                    }
                    i += item.number;
                }
            }
            return null;
        }
    }
}
