using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml;
using System.Windows;
using System.Xml.Serialization;
using System.IO;

namespace BanditTicket.Logic
{
    class JackPotConfig
    {
        public void loadJackPot()
        {
            var r=Deserialize<JackPotEntity>(File.ReadAllText("Config/Jackpot.xml"));
            var temp = new JackPotEntity()
            {
                   jackpot1= new jackpot { prize= new List<prizeEntity> { new prizeEntity { id = 1, name = "ddd" } }}
                   , day=new List<DayItem> { new DayItem { items=new List<Item> { new Item { id=1, number=1, OpenTime=DateTime.Now, probability=20 } } , time=DateTime.Now} }
            };
            var s = Serializa<JackPotEntity>(temp);
        }
        /// <summary>
        /// XML序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="className"></param>
        /// <returns></returns>
        public static string Serializa<T>(T className)
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            MemoryStream stream = new MemoryStream();
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = new UTF8Encoding(false);
            settings.Indent = true;
            using (XmlWriter writer = XmlWriter.Create(stream, settings))
            {
                xs.Serialize(writer, className);
            }
            return Encoding.UTF8.GetString(stream.ToArray());
        }



        /// <summary>
        /// XML反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlString"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string xmlString)
        {
            StringReader stringReader = new StringReader(xmlString);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            T res = (T)xmlSerializer.Deserialize(stringReader);
            return res;
        }
    }
}
