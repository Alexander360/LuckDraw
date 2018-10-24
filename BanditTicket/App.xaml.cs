using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace BanditTicket
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            //  new Logic.JackPotConfig().loadJackPot()
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (BanditTicket.Properties.Settings.Default.admin == "1")
            {
                StartupUri = new Uri("Detail.xaml", UriKind.Relative);
            }
            else
            {
                StartupUri = new Uri("WindowTurntable.xaml", UriKind.Relative);

            }
        }
    }
}
