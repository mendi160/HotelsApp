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
    /// Interaction logic for Host.xaml
    /// </summary>
    public partial class Host : Page
    {
        BlApi.IBl bl;
        List<BO.Host> Hosts;
        public Host(BlApi.IBl bl)
        {
            InitializeComponent();
            this.bl = bl;
            try
            {
                Hosts = bl.GetHosts().ToList();
                HsotsList.ItemsSource = Hosts;
            }
            catch
            {

            }
        }
        private void DeleteHost_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            BO.Host host = (BO.Host)(button.DataContext);
            if (host.Status == BO.Status.INACTIVE)
            {
                MessageBox.Show("המארח כבר נמחק מהמערכת");
                return;
            }
            MessageBoxResult mbr = MessageBox.Show("אתה בטוח שברצונך למחוק?", "הודעה", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No, MessageBoxOptions.RtlReading);
            if (mbr == MessageBoxResult.No)
                return;
            try
            {
              host.Status = BO.Status.INACTIVE;
                bl.UpdateHost(host);
                Hosts = bl.GetHosts().ToList();
                HsotsList.ItemsSource = Hosts;
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
    }
}
