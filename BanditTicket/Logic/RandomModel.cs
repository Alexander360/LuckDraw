using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BanditTicket.Logic
{
    class RandomModel
    {
        /// <summary>
        /// 排序的id
        /// </summary>
        public int order { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int number { get; set; }
        /// <summary>
        /// 概率
        /// </summary>
        public int probability { get; set; }
        /// <summary>
        /// 中奖名称
        /// </summary>
        public string name { get; set; }
    }
}
