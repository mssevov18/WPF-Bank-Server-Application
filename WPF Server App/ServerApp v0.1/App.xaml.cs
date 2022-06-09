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
            string[] args = e.Args[0].Split("||");
            if (args[0] != string.Empty)
                Bank_DatabaseContext.ConnectionString = args[0];
            if (args[1] != string.Empty)
            {
                using (Bank_DatabaseContext dbContext = new Bank_DatabaseContext())
                {
#warning FirstOrDefault can return int.default -> May cause errors!
                    Bank bank = dbContext.Banks.Where(b => b.Name == args[1]).FirstOrDefault();
                    if (bank is not null)
                        Bank_DatabaseContext.BankId = bank.BankId;
                    else
                        Bank_DatabaseContext.BankId = 0;
                }
            }

            base.OnStartup(e);
        }
    }
}
