using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BanditTicket.Logic
{
    public class UserEntity
    {
        public List<User> users { get; set; }

    }
    public class User
    {
        public string bayNumber { get; set; }
        public string orderNumber { get; set; }
        public double price { get; set; }
        public RandomModel model { get; set; }

        public string createTime { get; set; }
    }
}
