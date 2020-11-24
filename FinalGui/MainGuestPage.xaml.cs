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
using System.Xml.Linq;

namespace FinalGui
{
    /// <summary>
    /// Interaction logic for MainGuestPage.xaml
    /// </summary>
    public partial class MainGuestPage : Page
    {
        BlApi.IBl bl;
        public BO.Client Client { set; get; }
        public Dictionary<string, string> SubArea { set; get; }
        public MainGuestPage(BlApi.IBl bl, BO.Person person)
        {
            InitializeComponent();
            this.bl = bl;
            this.Client = bl.GetClient(person.Id);
            this.DataContext = Client;
        }
        private void AddGuestRequestButton_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new AddGuestRequestPage(bl, Client, MainListBox));
        }

        private void DeleteRequest_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mbr = MessageBox.Show("אתה בטוח שברצונך למחוק?", "הודעה", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No, MessageBoxOptions.RtlReading);
            if (mbr == MessageBoxResult.No)
                return;
            Button button = sender as Button;
            BO.GuestRequest guestRequest = (BO.GuestRequest)(button.DataContext);
            guestRequest.Status = BO.Request_Statut.CANCELLED;
            try
            {
                bl.UpdateGusetRequest(guestRequest);
                this.Client = bl.GetClient(Client.Details.Id);
                MainListBox.ItemsSource = Client.Requests;
                MainListBox.Items.Refresh();
            }
            catch (MissingMemberException exp)
            {
                MessageBox.Show(exp.Message);
            }
            catch (InvalidOperationException exc)
            {
                MessageBox.Show(exc.Message);
            }
           
        }

        private void UpdateRequest_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            NavigationService.Navigate(new UpdateRequestPage(bl, Client, MainListBox, ((BO.GuestRequest)button.DataContext)));
        }

        private void ViweGuestRequestButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            ViewGuestRequestWindow viewGuestRequestWindow = new ViewGuestRequestWindow((BO.GuestRequest)button.DataContext);

            viewGuestRequestWindow.ShowDialog();
        }
        private void AddToDictionary()
        {
            try
            {
                XElement cities = XElement.Load(@"CitiesList.xml");
            }
            catch
            {
                MessageBox.Show("קרתה תקלה בפתיחת הקובץ");
            }
        }
    }
}
