using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace FinalGui
{
    /// <summary>
    /// Interaction logic for Manager.xaml
    /// </summary>
    public partial class Manager : Page
    {
        public Dictionary<string, int> Config { get; set; }
        public BlApi.IBl bl { get; set; }
        List<BO.Host> hosts;
        List<BO.GuestRequest> guestRequests;
        List<BO.Person> people;
        List<BO.HostingUnit> hostingUnits;
        List<BO.Order> orders;
        public Action ConfigAction;
        public Manager(BlApi.IBl bl)
        {

            InitializeComponent();
            this.bl = bl;
            Config = bl.GetConfig();
            ConfigList.ItemsSource = Config;
            cfgComboBox.Items.Add("עמלה ללילה");
            cfgComboBox.Items.Add("זמן המתנה");
            information();
            ConfigAction = new Action(information);
            bl.configHandler(ConfigAction);
        }
       
        private void ConfigList_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {

        }
        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            ConfigList.Visibility = Visibility.Collapsed;
            editGrid.Visibility = Visibility.Visible;
            dataGridInfo.Visibility = Visibility.Collapsed;
        }

        private void UpdateBTN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.updateConfig(MyDictionary.ConfigTrenslate.Where(x => x.Value == cfgComboBox.SelectedItem.ToString()).FirstOrDefault().Key, cfgTextBox.Text);
                MessageBox.Show("הנתון עודכן בהצלחה");
                ConfigList.ItemsSource = bl.GetConfig();
            }
            catch
            {
                MessageBox.Show("היתה תקלה בעדכון");
            }
            ConfigList.Visibility = Visibility.Visible;
            editGrid.Visibility = Visibility.Collapsed;
            dataGridInfo.Visibility = Visibility.Collapsed;
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            ConfigList.Visibility = Visibility.Visible;
            editGrid.Visibility = Visibility.Collapsed;
        }

        private void cfgTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        private void hostBtn_Click(object sender, RoutedEventArgs e)
        {
            editGrid.Visibility = Visibility.Collapsed;
            dataGridInfo.Visibility = Visibility.Visible;
            dataGridInfo.ItemsSource = hosts;
        }
        private void information()
        {
            var temp = from order in bl.GetOrders()
                       where order.Status == BO.Order_Status.APPROVED &&
                       order.CloseDate.Month == DateTime.Today.Month
                       select order;
            Transactions.Text = temp.Count().ToString();
            var activeHost = from Host in bl.GetHosts()
                             where Host.Status == BO.Status.ACTIVE
                             select Host;
            ActiveHost.Text = activeHost.Count().ToString();
            try
            {
                hosts = bl.GetHosts().ToList();
                guestRequests = bl.GetGuestRequests().ToList();
                hostingUnits = bl.GetHostingUnits().ToList();
                people=bl.GetPersons().ToList();
                orders = bl.GetOrders().ToList();
            }
            catch
            {

            }

        }

        private void GRbtn_Click(object sender, RoutedEventArgs e)
        {
            editGrid.Visibility = Visibility.Collapsed;
            dataGridInfo.Visibility = Visibility.Visible;
            dataGridInfo.ItemsSource = guestRequests;
        }

        private void hostingunitBtn_Click(object sender, RoutedEventArgs e)
        {
            dataGridInfo.Visibility = Visibility.Visible;
            dataGridInfo.ItemsSource = hostingUnits;
        }

        private void persontBtn_Click(object sender, RoutedEventArgs e)
        {
            dataGridInfo.Visibility = Visibility.Visible;
            dataGridInfo.ItemsSource = people;
        }

        private void orderBtn_Click(object sender, RoutedEventArgs e)
        {
            dataGridInfo.Visibility = Visibility.Visible;
            dataGridInfo.ItemsSource = orders;
        }
    }
}
