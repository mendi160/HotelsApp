using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for AddGuestRequestPage.xaml
    /// </summary>
    public partial class AddGuestRequestPage : Page
    {
        BlApi.IBl bl;
        public int clientID { set; get; }
        public BO.Client Client { set; get; }
        public BO.GuestRequest guest { get; set; }
        public ListBox listBox { set; get; }
        public AddGuestRequestPage(BlApi.IBl bl, BO.Client client, ListBox listBox)
        {
             //
            InitializeComponent();
            guest = new BO.GuestRequest();
            AddRequest.DataContext = guest;
            this.bl = bl;
            this.clientID = client.Details.Id;
            this.Client = client;
            this.listBox = listBox;
            guest.ClientID = clientID;
            guest.RegistrationDate = DateTime.Today;
            #region  convert enums for Add
            typeComboBox.ItemsSource = MyDictionary.HostingTypes.Select(x => MyDictionary.TranslateEnumToString(x));
            areaComboBox.ItemsSource = MyDictionary.Locations.Select(x => MyDictionary.TranslateEnumToString(x));
            childrensAttractionsComboBox.ItemsSource = MyDictionary.Preferences.Select(x => MyDictionary.TranslateEnumToString(x));
            gardenComboBox.ItemsSource = MyDictionary.Preferences.Select(x => MyDictionary.TranslateEnumToString(x));
            poolComboBox.ItemsSource = MyDictionary.Preferences.Select(x => MyDictionary.TranslateEnumToString(x));
            jacuzziComboBox.ItemsSource = MyDictionary.Preferences.Select(x => MyDictionary.TranslateEnumToString(x));
            #endregion
            XElement cities = XElement.Load(@"XML\CitiesList.xml");
            childrenComboBox.ItemsSource = Numbers.ArrayNumbers;
            adultsComboBox.ItemsSource = Numbers.ArrayNumbers;
            subAreaComboBox.ItemsSource = cities.Elements().Select(x => x.Value);
            subAreaComboBox.SelectedIndex = 0;
           
        }

        private void AddGuestRequestButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in AddRequest.Children)
            {
                if (item is ComboBox)
                {
                    if (((ComboBox)item).SelectedItem == null)
                    {
                        MessageBox.Show("לא מולאו כל השדות");
                        return;
                     }
                }
            }
            if (entryDateDatePicker.SelectedDate != null)
                guest.EntryDate = ((DateTime)entryDateDatePicker.SelectedDate);
            if (releaseDateDatePicker.SelectedDate != null)
                guest.ReleaseDate = ((DateTime)releaseDateDatePicker.SelectedDate);
            try
            {
                guest.GuestRequestKey = bl.AddGuestRequest(this.guest);
            }
            catch (DuplicateWaitObjectException exp)
            {
                MessageBox.Show(exp.Message);
                return;
            }
            catch (ArgumentOutOfRangeException exp)
            {
                MessageBox.Show(exp.Message);
                return;
            }
            catch
            {
                MessageBox.Show("שגיאה לא ידועה");
                return;
            }
            guest.GuestRequestKey = 0;//for next round
            MessageBox.Show("הבקשה התווספה בהצלחה");
            this.Client = bl.GetClient(clientID);
            this.listBox.ItemsSource = Client.Requests;
            this.listBox.Items.Refresh();
            NavigationService.GoBack();
        }

        private void entryDateDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            releaseDateDatePicker.SelectedDate = ((DateTime)(entryDateDatePicker.SelectedDate)).AddDays(1);
        }

    }
}
