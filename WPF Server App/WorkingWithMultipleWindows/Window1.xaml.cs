using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;
using org.mariuszgromada.math.mxparser;

namespace WorkingWithMultipleWindows
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            this.Visibility = Visibility.Hidden;
            Closing += this.OnWindowClosing;
        }

        public int GetCalled(int num1, int num2)
        {
            this.Visibility = Visibility.Visible;
            ResultBox.Text = (num1 * num2).ToString();
            return num1 * num2;
        }

        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            ResultBox.Text = string.Empty;
            this.Visibility = Visibility.Hidden;
        }

    }
}
