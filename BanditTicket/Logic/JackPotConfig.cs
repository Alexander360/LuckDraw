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
        string userFilePath;
        string jackPotFilePath;
        public JackPotConfig()
        {
            jackPotFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", "Jackpot.xml");
            userFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", "user.xml");
        }
        public JackPotEntity loadJackPot()
        {
            try
            {
                var r = Deserialize<JackPotEntity>(File.ReadAllText(jackPotFilePath));

                return r;
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
                throw;
            }

        }

        public UserEntity loadUsers()
        {
           
            try
            {
                if (!File.Exists(userFilePath))
                {
                    return new UserEntity { users = new List<User>() };
                }
                var r = Deserialize<UserEntity>(File.ReadAllText(userFilePath));
                return r;
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
                throw;
            }
        }
        public void SaveUser(UserEntity jack)
        {
            try
            {
                if (File.Exists(userFilePath))
                {
                    string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "userlog");
                    if (!Directory.Exists(logPath))
                    {
                        Directory.CreateDirectory(logPath);
                    }
                    File.Move(userFilePath, Path.Combine(logPath, DateTime.Now.ToString("yyyyMMddHHmmss") + ".xml"));
                }
                File.WriteAllText(userFilePath, Serializa(jack));
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "抽奖失败，文件保存失败请联系管理员");
                throw;
            }

        }

        public void SaveJackPot(JackPotEntity jack)
        {
            try
            {
                if (File.Exists(jackPotFilePath))
                {
                    string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "potlog");
                    if (!Directory.Exists(logPath))
                    {
                        Directory.CreateDirectory(logPath);
                    }
                    File.Move(jackPotFilePath, Path.Combine(logPath, DateTime.Now.ToString("yyyyMMddHHmmss") + ".xml"));
                }
                File.WriteAllText(jackPotFilePath, Serializa(jack));
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "抽奖失败，文件保存失败请联系管理员");
                throw;
            }

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
