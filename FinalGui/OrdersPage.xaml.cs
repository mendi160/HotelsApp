using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for OrdersPage.xaml
    /// </summary>
    public partial class OrdersPage : Page
    {
        BlApi.IBl bl;
        BackgroundWorker mailSend;
        BO.Host host { set; get; }
        int hostID { set; get; }
        List<BO.GuestRequest> Requests { set; get; }
        List<BO.HostingUnit> availableHU { set; get; }
        BO.GuestRequest currentRequest { set; get; }
        BO.Client client { set; get; }
        BO.Order order { set; get; }
        ListView myListview { get; set; }
        public OrdersPage(BlApi.IBl bl, BO.Host host, ListView MyOrderListView)
        {
            InitializeComponent();
            this.bl = bl;
            this.host = host;
            Requests = bl.GetGetGuestRequestsExceptHostGr(host.Person.Id).ToList();
            this.host = host;
            hostID = this.host.Person.Id;
            MainListBox.ItemsSource = Requests;
            mailSend = new BackgroundWorker();
            myListview = MyOrderListView;
        }
        private void ViweGuestRequestButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            ViewGuestRequestWindow viewGuestRequestWindow = new ViewGuestRequestWindow((BO.GuestRequest)button.DataContext);

            viewGuestRequestWindow.ShowDialog();
        }
        private void CheckAvailable_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            currentRequest = button.DataContext as BO.GuestRequest;
            availableHU = bl.CheckForAvailableHostingUnit(currentRequest.EntryDate, bl.PassedDays(currentRequest.EntryDate, currentRequest.ReleaseDate)).ToList().FindAll(x => x.Owner == hostID);
            AvailableHUList.ItemsSource = availableHU;
            if (availableHU.Any())
            {
                AvailableHUTextBox.Background = Brushes.Green;
            }
            else
            {
                AvailableHUTextBox.Background = Brushes.Red;
            }
            AvailbeHUGrid.Visibility = Visibility.Visible;
        }

        private void AddOrderButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            client = bl.GetClient(currentRequest.ClientID);
            order = new BO.Order()
            {
                ClientFirstName = client.Details.FirstName,
                ClientLastName = client.Details.LastName,
                HostID = host.Person.Id,
                HostingUnit = btn.DataContext as BO.HostingUnit,
                GuestRequest = currentRequest,
                OrderDate = DateTime.Today,
                Status = BO.Order_Status.PENDING
            };
            try
            {
                order.Key = bl.AddOrder(order);
                MessageBox.Show("ההזמנה נוצרה בהצלחה");
                myListview.ItemsSource = bl.GetHost(hostID).Orders;
            }
            catch (DuplicateWaitObjectException exception) { MessageBox.Show(exception.Message); }
            catch (MissingMemberException exception) { MessageBox.Show(exception.Message); }
        }

        private void NumberFilterBtn_Click(object sender, RoutedEventArgs e)
        {

        }


        private void MenuItem_Checked_Area(object sender, RoutedEventArgs e)
        {


            var AreaMenuItem = (sender as MenuItem).Parent as MenuItem;
            foreach (MenuItem item in AreaMenuItem.Items)
            {
                if (item.Header != (sender as MenuItem).Header)
                {
                    item.IsChecked = false;
                }

            }
            if (((string)(sender as MenuItem).Header) == "הכל")
            {
                MainListBox.ItemsSource = Requests;
                return;
            }

            BO.Location selectedLocation = (BO.Location)MyDictionary.TranslatStringeToEnum((string)(sender as MenuItem).Header);
            MainListBox.ItemsSource = from Group in bl.GuestRequestGroupedBySpecificArea()
                                      where Group.Key == selectedLocation
                                      select Group into items
                                      from guestRequest in items
                                      where guestRequest.ClientID != hostID
                                      select guestRequest;
        }


        private void MenuItem_Checked_Number(object sender, RoutedEventArgs e)
        {
            var NumberMenuItem = (sender as MenuItem).Parent as MenuItem;
            foreach (MenuItem item in NumberMenuItem.Items)
            {
                if (item.Header != (sender as MenuItem).Header)
                {
                    item.IsChecked = false;
                }

            }
            string str = ((string)(sender as MenuItem).Header);
            if (str == "ללא הגבלה")
            {
                MainListBox.ItemsSource = Requests;
                return;
            }
            #region switch case
            int min;
            int max;
            switch (str)
            {
                case "2":
                    min = 2;
                    max = 2;
                    break;
                case "בין 2 ל 5":
                    min = 2;
                    max = 5;
                    break;
                case "בין 5 ל10":
                    min = 5;
                    max = 10;
                    break;
                case "גדול מ10":
                    min = 11;
                    max = 1000;
                    break;
                default:
                    min = 0;
                    max = 0;
                    break;
            }
            MainListBox.ItemsSource = from item in bl.GroupedByNumberOfGuests()
                                      where item.Key >= min && item.Key <= max
                                      select item
                    into items
                                      from item in items
                                      where item.ClientID != hostID
                                      select item;
            MainListBox.Items.Refresh();
            #endregion
        }


    }
}
