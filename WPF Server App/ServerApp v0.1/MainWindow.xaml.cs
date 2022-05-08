using ServerApp_v0._1.Data.Models;
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

namespace ServerApp_v0._1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private System.Windows.Threading.DispatcherTimer TimeTimer;
        private System.Windows.Threading.DispatcherTimer DateTimer;
        private System.Windows.Threading.DispatcherTimer UpdateTimer;
        private bool isOn;
        private bool isStartStopButtonLocked;
        private int refreshRate;
        private List<Log> logs;

        public MainWindow()
        {
            InitializeComponent();
            LockStartStopButton();

            TimeTimer = new System.Windows.Threading.DispatcherTimer();
            TimeTimer.Tick += new EventHandler(TimeTimer_Tick);
            TimeTimer.Interval = new TimeSpan(0, 0, 1);
            TimeTimer_Tick(this, EventArgs.Empty);
            TimeTimer.Start();

            DateTimer = new System.Windows.Threading.DispatcherTimer();
            DateTimer.Tick += new EventHandler(DateTimer_Tick);
            DateTimer.Interval = new TimeSpan(0, 30, 0);
            DateTimer_Tick(this, EventArgs.Empty);
            DateTimer.Start();

            isOn = false;
            RefreshRateTextBox.Focus();
            logs = new List<Log>();
        }

        private void TimeTimer_Tick(object sender, EventArgs e)
        {
            TimeLabel.Content = DateTime.Now.ToString("HH:mm:ss");
        }

        private void DateTimer_Tick(object sender, EventArgs e)
        {
            DateLabel.Content = DateTime.Now.ToString("MM.dd.yyyy");
        }

        private void ChangeStartStopButton(bool state)
        {
            isOn = state;
            StartStopButton.Content = state == true ? "Stop" : "Start";
            StartStopButton.Background = state == true ? Brushes.Red : Brushes.Green;
            RefreshRateTextBox.IsReadOnly = state;

            // Stop the server
            // Connect to the server
            if (!isOn)
            {
                if (UpdateTimer != null)
                    UpdateTimer.Stop();
            }
            else
            {
                UpdateTimer = new System.Windows.Threading.DispatcherTimer();
                UpdateTimer.Tick += new EventHandler(UpdateLogViewBox);
                UpdateTimer.Interval = new TimeSpan(refreshRate);
                UpdateLogViewBox(this, EventArgs.Empty);
                UpdateTimer.Start();
            }
        }

        // When interval comes update log collection
        private void UpdateLogViewBox(object sender, EventArgs e)
        {
            using (Bank_DatabaseContext dbContext = new Bank_DatabaseContext())
            {
                // Make it add the new logs, instead of remaking logs...
                // Remaking logs makes it hard for the scroll and item selection to work
                // If left in this state it could cause issues
                logs = dbContext.Logs.ToList<Log>();

                //not gud
                //dbContext.Logs.Add(new Log("..", 0, $"Server {DateTime.Now}", 0, true));

                // TODO
                // Process the queries
                // TODO

            }
            LogListBox.ItemsSource = logs;
            LogListBox.DataContext = logs;
        }

        private void StartStopButton_Click(object sender, RoutedEventArgs e)
        {
            if (refreshRate == 0)
                throw new Exception("Refresh rate can't be 0");

            if (!isStartStopButtonLocked)
                ChangeStartStopButton(!isOn);
        }

        private void LockStartStopButton()
        {
            StartStopButton.Content = "Refresh Rate?";
            StartStopButton.Background = Brushes.Gray;
            isStartStopButtonLocked = true;
        }

        private void UnlockStartStopButton()
        {
            StartStopButton.Content = "Start";
            StartStopButton.Background = Brushes.Green;
            isStartStopButtonLocked = false;
        }

        private void RefreshRateTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (isOn)
                return;

            if (RefreshRateTextBox.Text == string.Empty || RefreshRateTextBox.Text == "")
                LockStartStopButton();
            else
                if (int.TryParse(RefreshRateTextBox.Text, out refreshRate))
                    UnlockStartStopButton();
        }

        private void NumericOnly(System.Object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = isOn ? false : IsTextNumeric(e.Text);
        }

        private static bool IsTextNumeric(string str)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("[^0-9]");
            return reg.IsMatch(str);

        }
    }
}
