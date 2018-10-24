using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BanditTicket.Logic
{
    public class JackPotEntity
    {
        public List<DayItem> day { get; set; }
        public List<prizeEntity> prize { get; set; }
    }
    public class DayItem
    {
        public string time { get; set; }
        public List<Item> items { get; set; }
    }
    public class Item
    {
        public int id { get; set; }
        public int number { get; set; }
        public string OpenTime { get; set; }
        [System.Xml.Serialization.XmlIgnore]
        [NonSerialized]
        public DateTime dt;
    }
    public class prizeEntity
    {
        public string name { get; set; }
        public int id { get; set; }
    }


}
