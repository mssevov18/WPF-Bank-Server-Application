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

namespace DatabaseTesting4
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
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
