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
    /// Interaction logic for MainOrdrerPage.xaml
    /// </summary>
    public partial class MainOrdrerPage : Page
    {
        BlApi.IBl bl;
        BackgroundWorker mailSend;
        BO.Host host { set; get; }
        int hostID { set; get; }
        BO.Order order { set; get; }
        Button btn { set; get; }
       
        public MainOrdrerPage(BlApi.IBl bl, BO.Host host) 
        {
            InitializeComponent();
            this.host = host;
            this.bl = bl;
            myListview.ItemsSource = this.host.Orders;
            mailSend = new BackgroundWorker();
            hostID = this.host.Person.Id;
            mailSend.DoWork += MailSend_DoWork;
            mailSend.RunWorkerCompleted += MailSend_RunWorkerCompleted;
        }

        private void AddOrderButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new OrdersPage(bl, host, myListview));
        }


        private void sendEmailButton_Click(object sender, RoutedEventArgs e)
        {
            #region EmailPremission
            if (!bl.EmailPremissionCheck(host))
            {
                MessageBoxResult mbr = MessageBox.Show(@"אינך יכול לשלוח הזמנות כיוון שלא אישרת את הרשאת החיוב




                                                             האם תרצה לשנות את הרשאת החיוב?"

                           , "שגיאה", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes, MessageBoxOptions.RtlReading);
                if (mbr != MessageBoxResult.Yes)
                    return;
                else
                {
                    host.CollectionClearance = true;

                    try
                    {
                        bl.UpdateHost(host);
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show(exp.Message);
                        return;
                    }
                }
            }
            #endregion
            btn = sender as Button;
            order = (BO.Order)btn.DataContext;
            MaterialDesignThemes.Wpf.ButtonProgressAssist.SetIsIndicatorVisible(btn, true);//TODO
          
               try
               {
                mailSend.RunWorkerAsync();
               }
               catch (Exception exception)
               {

                    MessageBox.Show(exception.Message);
                    MaterialDesignThemes.Wpf.ButtonProgressAssist.SetIsIndicatorVisible(btn, false);
               }

          }

        private void MailSend_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

           MaterialDesignThemes.Wpf.ButtonProgressAssist.SetIsIndicatorVisible(btn, false);
            if ((int)e.Result == 1)
            {
                MessageBox.Show("ההזמנה נשלחה בהצלחה");
                order.SentDate = DateTime.Today;
                bl.UpdateOrder(order);
                myListview.ItemsSource = bl.GetHost(hostID).Orders;
            }
            else
            {
                MessageBox.Show("קרתה תקלה בשליחת ההזמנה");
                order.Status = BO.Order_Status.PENDING;
                bl.UpdateOrder(order);
            }
        }

        private void MailSend_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                order.Status = BO.Order_Status.EMAIL_SENT;
                bl.UpdateOrder(order);
                e.Result = 1;
            }
            catch
            {
                e.Result = 0;
            }
        }
        private void UpdateOrderButton_Click(object sender, RoutedEventArgs e)
        {
            btn = sender as Button;
            order = btn.DataContext as BO.Order;
            NavigationService.Navigate(new UpdateOrderPage(bl, order, myListview, mailSend));
        }
    }
}
