using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BanditTicket.Logic
{
    public class JackPotEntity
    {
        public List<DayItem> day { get; set; }
        public jackpot jackpot1 { get; set; }
    }
    public class DayItem
    {
        public DateTime time { get; set; }
        public List<Item> items { get; set; }
    }
    public class Item
    {
        public int id { get; set; }
        public int number { get; set; }
        public int probability { get; set; }
        public DateTime OpenTime { get; set; }
    }
    public class jackpot
    {
        public List<prizeEntity> prize { get; set; }
    }
    public class prizeEntity
    {
        public string name { get; set; }
        public int id { get; set; }
    }


}
