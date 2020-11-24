using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for UpdateOrderPage.xaml
    /// </summary>
    public partial class UpdateOrderPage : Page
    {
        BlApi.IBl bl;
        BO.Order order { set; get; }
        ListView listViewOrder;
        BackgroundWorker email;
        public UpdateOrderPage(BlApi.IBl bl, BO.Order order, ListView listView, BackgroundWorker worker)
        {
            InitializeComponent();
            this.bl = bl;
            this.order = order;
            statusComboBox.ItemsSource = MyDictionary.OrderStatus.Select(x => MyDictionary.TranslateEnumToString(x));
            UpdateOrderGrid.DataContext = this.order;
            this.listViewOrder = listView;
            email = worker;
        }

        private void UpdateOrderButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (order.Status == BO.Order_Status.EMAIL_SENT)
                {
                    email.RunWorkerAsync();
                }
                else
                {
                    bl.UpdateOrder(order);
                }
                MessageBox.Show("ההזמנה עודכנה בהצלחה");

            }
            catch (MissingMemberException exp)
            {
                MessageBox.Show(exp.Message);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            try
            {
                listViewOrder.ItemsSource = bl.GetHost(order.HostID).Orders;
            }
            catch (MissingMemberException exp)
            {
                MessageBox.Show(exp.Message);
            }
            NavigationService.GoBack();
        }
    }
}
