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
    /// Interaction logic for UpdateRequestPage.xaml
    /// </summary>
    public partial class UpdateRequestPage : Page
    {
        
        BlApi.IBl bl;
        public int clientID { set; get; }
        public BO.Client Client { set; get; }
        public BO.GuestRequest guest { get; set; }
        public ListBox listBox { set; get; }
        public UpdateRequestPage(BlApi.IBl bl, BO.Client client, ListBox listBox, BO.GuestRequest guestRequest)
        {
            InitializeComponent();
            guest = guestRequest;
            this.bl = bl;
            this.clientID = client.Details.Id;
            this.Client = client;
            this.listBox = listBox;
              
            var enumPreferences = Enum.GetValues(typeof(BO.Preferences));
            #region  convert enums for Update
            typeComboBox.ItemsSource = MyDictionary.HostingTypes.Select(x => MyDictionary.TranslateEnumToString(x));
            areaComboBox.ItemsSource = MyDictionary.Locations.Select(x => MyDictionary.TranslateEnumToString(x));
            childrensAttractionsComboBox.ItemsSource = MyDictionary.Preferences.Select(x => MyDictionary.TranslateEnumToString(x));
            gardenComboBox.ItemsSource = MyDictionary.Preferences.Select(x => MyDictionary.TranslateEnumToString(x));
            poolComboBox.ItemsSource = MyDictionary.Preferences.Select(x => MyDictionary.TranslateEnumToString(x));
            jacuzziComboBox.ItemsSource = MyDictionary.Preferences.Select(x => MyDictionary.TranslateEnumToString(x));
            #endregion
            UpdateRequest.DataContext = guest;
            adultsComoboBox.ItemsSource = Numbers.ArrayNumbers;
            childrenComboBox.ItemsSource = Numbers.ArrayNumbers;
            subAreaComboBox.ItemsSource = (XElement.Load(@"XML/CitiesList.xml").Elements().Select(x => x.Value));
        }

        private void UpdateGuestRequestButton_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                bl.UpdateGusetRequest(this.guest);
            }
            catch (MissingMemberException exp)
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
            MessageBox.Show("הבקשה עודכנה בהצלחה");
            this.Client = bl.GetClient(clientID);
            this.listBox.ItemsSource = Client.Requests;
            this.listBox.Items.Refresh();
            NavigationService.GoBack();
        }

    }
}
