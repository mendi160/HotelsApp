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

namespace FinalGui
{
    /// <summary>
    /// Interaction logic for MainHostPage.xaml
    /// </summary>
    public partial class MainHostPage : Page
    {
        BlApi.IBl bl;
        public BO.Host Host { set; get; }
        public int HostId { set; get; }

        public MainHostPage(BlApi.IBl bl, BO.Host host)
        {
            InitializeComponent();
            this.bl = bl;
            this.Host = host;
            UpdateHostGrid.DataContext = Host;
            HostId = Host.Person.Id;
            try
            {
                this.Host.HostingUnits = bl.GetHostingUnitsOfHost(HostId);
                this.Host.Orders = bl.GetOrdersOfHost(HostId);
            }
            catch
            {
                this.Host.HostingUnits =  new List<BO.HostingUnit>();
                this.Host.Orders = new List<BO.Order>(); 
            }
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new MainOrdrerPage(bl, Host));
        }

        private void HostingUnitButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new HostingUnisPage(bl, Host));
        }

        private void AddHostButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.UpdateHost(Host);
                MessageBox.Show("הפרטים עודכנו בהצלחה");

            }
            catch(MissingMemberException exp)
            {
                MessageBox.Show(exp.Message);
            }
            Host = bl.GetHost(HostId);
            UpdateHostGrid.DataContext = Host;
        }
    }
}
