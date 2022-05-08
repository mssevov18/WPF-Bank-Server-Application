using DatabaseTesting3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DatabaseTesting3
{
/*
Install-Package Microsoft.EntityFrameworkCore.Tools -Version 5.0.16
Install-Package Microsoft.EntityFrameworkCore.SqlServer -Version 5.0
Install-Package Microsoft.EntityFrameworkCore.SqlServer.Design

Scaffold-DbContext "Server=.\SQLExpress;Database=Bank_Database;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
*/

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<GetAccountDatum> AccountDataList;
        private List<GetBankWorkerDatum> BankWorkerDataList;
        private List<GetBranchDatum> BranchDataList;

        public MainWindow()
        {
            InitializeComponent();
            using (Bank_DatabaseContext dbContext = new Bank_DatabaseContext())
            {
                AccountDataList = dbContext.GetAccountData.ToList();
                BankWorkerDataList = dbContext.GetBankWorkerData.ToList();
                BranchDataList = dbContext.GetBranchData.ToList();
            }
            DataListBox.DataContext = null;
            DataTitleBlock.Text = "Empty";
        }

        private void RefreshViews()
        {
            using (Bank_DatabaseContext dbContext = new Bank_DatabaseContext())
            {
                AccountDataList = dbContext.GetAccountData.ToList();
                BankWorkerDataList = dbContext.GetBankWorkerData.ToList();
                BranchDataList = dbContext.GetBranchData.ToList();
            }
        }

        private void Empty_Click(object sender, RoutedEventArgs e)
        {
            DataListBox.DataContext = null;
            DataTitleBlock.Text = "Empty";
        }

        private void GetAccountDataButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshViews();
            DataListBox.DataContext = AccountDataList;
            DataTitleBlock.Text = "Account Data";
        }

        private void GetBankWorkerData_Click(object sender, RoutedEventArgs e)
        {
            RefreshViews();
            DataListBox.DataContext = BankWorkerDataList;
            DataTitleBlock.Text = "Bank Worker Data";
        }

        private void GetBranchData_Click(object sender, RoutedEventArgs e)
        {
            RefreshViews();
            DataListBox.DataContext = BranchDataList;
            DataTitleBlock.Text = "Bank Branch Data";
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
