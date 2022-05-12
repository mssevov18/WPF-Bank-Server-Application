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

namespace WorkingWithMultipleWindows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Window1 window1;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void DoMath()
        {
            if (window1 == null)
                window1 = new Window1();

            ResultTextBox.Text = window1.GetCalled(int.Parse(ExpressionTextBox.Text), int.Parse(Expression2TextBox.Text)).ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Expression2TextBox.Text != "" && ExpressionTextBox.Text != "")
                DoMath();
        }

        private void ExpressionTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && Expression2TextBox.Text != "" && ExpressionTextBox.Text != "")
                DoMath();
        }
    }
}
