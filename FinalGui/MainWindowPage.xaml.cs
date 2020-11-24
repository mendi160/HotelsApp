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
    /// Interaction logic for MainWindowPage.xaml
    /// </summary>
    public partial class MainWindowPage : Page
    {
        BlApi.IBl bl;
        //  Frame myframe;
        public BO.Person CurrentPerson { set; get; }
        public BO.Host currentHost { set; get; }
        Manager manager;
        public MainWindowPage(BlApi.IBl bl, BO.Person Person)
        {
            InitializeComponent();
            this.bl = bl;
            this.CurrentPerson = Person;
            this.DataContext = CurrentPerson;
            ButtonUser.Content = CurrentPerson.FirstName + " " + CurrentPerson.LastName;
            ManagerButton.IsEnabled = isManager();
            createManager();
        }

        private void GuestButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainGuestPage(bl, CurrentPerson));
        }

        private void HostButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                currentHost = bl.GetHost(CurrentPerson.Id);
                NavigationService.Navigate(new MainHostPage(bl, currentHost));
            }
            catch (MissingMemberException ex)
            {

                MessageBox.Show("עלייך להשלים פרטים אם הינך מעוניין להמשיך כמארח", "הודעה", MessageBoxButton.OK, MessageBoxImage.Information);
                currentHost = new BO.Host();
                currentHost.Person = CurrentPerson;
                NavigationService.Navigate(new NewHostPage(bl, currentHost));
            }

        }

        private void ManagerButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(manager);
        }
        private void createManager()
        {
            manager = new Manager(bl);
        }
        private bool isManager()
        {
            Dictionary<string, int> temp = bl.GetConfig();
            return temp["Manager"] == CurrentPerson.Id;
        }
        private void ButtonUser_Click(object sender, RoutedEventArgs e)
        {
            Details.Visibility = Visibility.Visible;
        }

        private void ButtonUser_DeleteClick(object sender, RoutedEventArgs e)
        {
            Details.Visibility = Visibility.Collapsed;
        }

        private void EditDetails_Click(object sender, RoutedEventArgs e)
        {
            EditDetails.Visibility = Visibility.Collapsed;
            UpdateDetails.Visibility = Visibility.Visible;
        }

        private void UpdateDetails_Click(object sender, RoutedEventArgs e)
        {
            EditDetails.Visibility = Visibility.Visible;
            UpdateDetails.Visibility = Visibility.Collapsed;
        }
    }

}
