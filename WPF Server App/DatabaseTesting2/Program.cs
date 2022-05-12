using DatabaseTesting2.Models;
using System;

namespace DatabaseTesting2
{
    class Program
    {
/*
Install-Package Microsoft.EntityFrameworkCore.Tools -Version 5.0.16
Install-Package Microsoft.EntityFrameworkCore.SqlServer -Version 5.0
Install-Package Microsoft.EntityFrameworkCore.SqlServer.Design

Scaffold-DbContext "Server=.\SQLExpress;Database=Bank_Database;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
*/
        static void Main(string[] args)
        {
            using (Bank_DatabaseContext dbContext = new Bank_DatabaseContext())
            {
                Console.WriteLine("============== GetAccountData ==============");
                foreach (var item in dbContext.GetAccountData)
                    Console.WriteLine($"{item.AccountFullName}({item.BankName}): {item.Balance}, {item.CardAmount}");

                Console.WriteLine();
                Console.WriteLine("============== GetBankWorkerData ==============");

                foreach (var item in dbContext.GetBankWorkerData)
                    Console.WriteLine($"{item.WorkerFullName}({item.BankName})");

                Console.WriteLine();
                Console.WriteLine("============== GetBranchData ==============");

                foreach (var item in dbContext.GetBranchData)
                    Console.WriteLine($"{item.BankName}: {item.BranchAddress}");
            }
        }
    }
}
