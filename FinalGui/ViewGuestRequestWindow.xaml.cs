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
using System.Windows.Shapes;

namespace FinalGui
{
    /// <summary>
    /// Interaction logic for ViewGuestRequestWindow.xaml
    /// </summary>
    public partial class ViewGuestRequestWindow : Window
    {
        BO.GuestRequest guest;
        public ViewGuestRequestWindow(BO.GuestRequest guest)
        {
            InitializeComponent();
            this.guest = guest;
            ViewRequest.DataContext = this.guest;
            var enumPreferences = Enum.GetValues(typeof(BO.Preferences));
            #region  convert enums for Update
            areaComboBox.ItemsSource = Enum.GetValues(typeof(BO.Location));
            childrensAttractionsComboBox.ItemsSource = enumPreferences;
            gardenComboBox.ItemsSource = enumPreferences;
            poolComboBox.ItemsSource = enumPreferences;
            jacuzziComboBox.ItemsSource = enumPreferences;
            typeComboBox.ItemsSource = Enum.GetValues(typeof(BO.Hosting_Type));
            StatusComboBox.ItemsSource = Enum.GetValues(typeof(BO.Request_Statut));
            #endregion
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
