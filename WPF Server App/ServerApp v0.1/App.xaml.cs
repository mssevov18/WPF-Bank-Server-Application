using Bank_Db_Class_Library;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ServerApp_v0._1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Bank_DatabaseContext.ConnectionString = e.Args[0];

            base.OnStartup(e);
        }
    }
}
