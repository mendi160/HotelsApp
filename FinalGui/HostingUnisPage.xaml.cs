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
    /// Interaction logic for HostingUnisPage.xaml
    /// </summary>
    public partial class HostingUnisPage : Page
    {

        BlApi.IBl bl;
        public BO.Host Host { set; get; }
        public int HostId { set; get; }
        public HostingUnisPage(BlApi.IBl bl, BO.Host host)
        {
            InitializeComponent();
            this.bl = bl;
            this.Host = host;
            HostId = Host.Person.Id;
            MainListBox.ItemsSource = Host.HostingUnits;  
        }

        private void DeleteHU_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mbr = MessageBox.Show("אתה בטוח שברצונך למחוק?", "הודעה", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No, MessageBoxOptions.RtlReading);
            if (mbr == MessageBoxResult.No)
                return;
            Button button = sender as Button;
            BO.HostingUnit HU = (BO.HostingUnit)(button.DataContext);
            HU.Status = BO.Status.INACTIVE;
            try
            {
                bl.UpdateHostingUnit(HU);
                this.Host = bl.GetHost(HostId);
                MainListBox.ItemsSource = Host.HostingUnits;
                MainListBox.Items.Refresh();
            }
            catch(MissingMemberException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateHU_Click(object sender, RoutedEventArgs e)
        {

        }

        private void viewDetails_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            ViewHostingUnitWindow newwindow = new ViewHostingUnitWindow((BO.HostingUnit)button.DataContext, bl);
            newwindow.ShowDialog();
               MainListBox.Items.Refresh();
        }
        private void AddHUButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddHostingUnitPage(bl, Host, MainListBox));
        }

          private void viewDetails_Click_1(object sender, RoutedEventArgs e)
          {
               HostingUnitDetails newWindow= new HostingUnitDetails( (((sender as Button).DataContext)as BO.HostingUnit) ,bl);
            NavigationService.Navigate(newWindow);
          }

         
     }
}
