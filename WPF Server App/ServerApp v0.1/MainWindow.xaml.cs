using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
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

//using ServerApp_v0._1.Data.Models;
using Bank_Db_Class_Library;

///TODO_HIGH: Separate app into pages

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
        private ObservableCollection<Request> requests;

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
            requests = new ObservableCollection<Request>();
            RefreshRateTextBox.Focus();
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
                UpdateTimer.Tick += new EventHandler(UpdateRequestViewBox);
                UpdateTimer.Interval = new TimeSpan(refreshRate);
                UpdateRequestViewBox(this, EventArgs.Empty);
                UpdateTimer.Start();
            }
        }

        // When interval comes update log collection
        private void UpdateRequestViewBox(object sender, EventArgs e)
        {
            // Think of a way to pause the timer until the operation is done
            //UpdateTimer.Stop();
            using (Bank_DatabaseContext dbContext = new Bank_DatabaseContext())
            {
                try
                {
                    // Proccess all requests and add them to requests List

                    ///TODO_HIGH: Filter Request by timestamp!
                    foreach (Request request in dbContext.Requests)
                    {
                        try
                        {
                            if (request.IsSuccessful is not null)
                                continue;

                            Bank tempBank = null;
                            Person tempPerson = null;

                            switch (request.TableAffected)
                            {
                                case "Person":
                                    Person person = JsonSerializer.Deserialize<Person>(request.Arguments);

                                    // Check if person exists
                                    if (dbContext.People.Where(p => p.Egn == person.Egn).FirstOrDefault() == null)
                                        dbContext.People.Add(person);

                                    break;

                                case "Bank_Worker":
                                    BankWorker bankWorker = JsonSerializer.Deserialize<BankWorker>(request.Arguments);

                                    tempBank = dbContext.Banks.Where(b => b.BankId == bankWorker.BankId)
                                                              .FirstOrDefault();
                                    if (tempBank != null)
                                    {
                                        bankWorker.Bank = tempBank;
                                        ///TODO: Find if it is necessary to add bankworkers to bank's datasets etc.
                                        ///Is this necessary?
                                        //bankWorker.Bank.BankWorkers.Add(bankWorker);
                                    }

                                    tempPerson = dbContext.People.Where(p => p.Egn == bankWorker.PersonEgn)
                                                                 .FirstOrDefault();
                                    if (tempPerson != null)
                                    {
                                        bankWorker.PersonEgnNavigation = tempPerson;
                                        ///Is this necessary?
                                        //bankWorker.PersonEgnNavigation.BankWorkers.Add(bankWorker);
                                    }

                                    ///TODO_HIGH: Implement error catching
                                    //if (bankWorker.Bank == null || bankWorker.PersonEgnNavigation == null)

                                    // Check if worker exists
                                    ///TODO_HIGH: Think of better data to check against
                                    if (dbContext.BankWorkers.Where(bw => bw.Username == bankWorker.Username).FirstOrDefault() == null)
                                        dbContext.BankWorkers.Add(bankWorker);

                                    break;

                                case "Account":
#warning Needs testing!
                                    Account account = JsonSerializer.Deserialize<Account>(request.Arguments);

                                    if (!account.Email.Contains("@"))
                                        throw new Exception("Email does not conform to %@%");

                                    tempBank = dbContext.Banks.Where(ac => ac.BankId == account.BankId)
                                                              .FirstOrDefault();
                                    if (tempBank != null)
                                    {
                                        account.Bank = tempBank;
                                    }

                                    tempPerson = dbContext.People.Where(p => p.Egn == account.PersonEgn)
                                                                 .FirstOrDefault();
                                    if (tempPerson != null)
                                    {
                                        account.PersonEgnNavigation = tempPerson;
                                    }

                                    ///TODO_HIGH: Implement error catching

                                    // Check if worker exists
                                    ///TODO_HIGH: Think of better data to check against
                                    if (dbContext.Accounts.Where(ac => ac.AccountIban == account.AccountIban).FirstOrDefault() == null)
                                        dbContext.Accounts.Add(account);

                                    break;

                                case "Transaction":



                                    break;

                                /// Both Card and CardReader are left for later

                                case "Card":
                                    break;

                                case "Card_Reader":
                                    break;

                                default:
                                    break;
                            }

                            ///TODO_LOW: Add pagination to remove old logs


                            RequestListBox.Items.Add(request);
                            // Successfully handled the request
                            request.IsSuccessful = true;
                        }
                        catch (Exception)
                        {
                            request.IsSuccessful = false;
                            throw;
                        }
                    }
                    dbContext.SaveChanges();
                    // Why do I have to keep updating itemsource and datacontext????
                    //RequestListBox.ItemsSource = requests;
                    //RequestListBox.DataContext = requests;
                }
                //UpdateTimer.Start();
                catch (Exception exception)
                {
                    MessageBox.Show($"{exception.ToString()}\n\n{exception.Message}\n\n{exception.StackTrace}\n\n{exception.HelpLink}", $"{exception.GetType().ToString()}");
                }
            }
        }

        private void StartStopButton_Click(object sender, RoutedEventArgs e)
        {
            // Possible to click the button in the ms between the input and the input check
            if (RefreshRateTextBox.Text.Length > 5)
                return;
            if (refreshRate == 0)
            {
                MessageBox.Show("Refresh rate can't be 0");
                return;
            }

            if (!isStartStopButtonLocked)
                ChangeStartStopButton(!isOn);
        }

        private bool LockStartStopButton()
        {
            return ModifyStartStopButton("Refresh Rate?", Brushes.Gray, true);
        }
        private bool UnlockStartStopButton()
        {
            return ModifyStartStopButton("Start", Brushes.Green, false);
        }

        /// <summary>
        /// Moddifies the StartStop button
        /// </summary>
        /// <param name="content">Sets the button content.</param>
        /// <param name="brush">Sets the button background brush.</param>
        /// <param name="isLocked"><c>true</c> to lock the button.\n <c>false</c> to unlock the button</param>
        private bool ModifyStartStopButton(string content, Brush brush, bool isLocked)
        {
            StartStopButton.Content = content;
            StartStopButton.Background = brush;
            isStartStopButtonLocked = isLocked;
            return true;
        }

        private void RefreshRateTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (isOn)
                return;
            ///IDEA: ! Add a dropdown to choose between seconds(s), minutes(m), hours(h), days(d)?

            //// >A optimisation is possible -Dinko
            //// Lock the button -> No input
            //if (RefreshRateTextBox.Text == string.Empty || RefreshRateTextBox.Text == "")
            //    ModifyStartStopButton("Refresh Rate?", Brushes.Gray, true);
            //else
            //    if (int.TryParse(RefreshRateTextBox.Text, out refreshRate))
            //    {
            //        // Lock the button -> Too long
            //        if (RefreshRateTextBox.Text.Length > 5)
            //            ModifyStartStopButton("Too long!", Brushes.HotPink, true);

            //        // Unlock the button
            //        else
            //            UnlockStartStopButton();
            //    }

            bool empty = RefreshRateTextBox.Text == string.Empty || RefreshRateTextBox.Text == "" ?
                            LockStartStopButton() :
                            int.TryParse(RefreshRateTextBox.Text, out refreshRate) ?
                                RefreshRateTextBox.Text.Length > 5 ?
                                    ModifyStartStopButton("Too long!", Brushes.HotPink, true) :
                                    UnlockStartStopButton() :
                                false;

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

        private void RefreshRateTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                StartStopButton_Click(sender, e);
        }
    }
}
