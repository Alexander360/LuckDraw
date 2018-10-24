using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BanditTicket.Logic
{
    public class JackPotEntity
    {
        public List<DayItem> day { get; set; }

    }
    public class DayItem
    {
        public DateTime time { get; set; }
        public List<Item> items { get; set; }
    }
    public class Item
    {

    }

}
