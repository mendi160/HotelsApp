using System;
using System.Collections.Generic;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BlApi.IBl bl = BlApi.BlFactory.GetBL();
        public MainWindow()
        {
            try
            {
                InitializeComponent();
                bl.ExpiredThread();
                bl.CleanDiaryThread();
                myframe.Navigate(new Login(bl));
                this.Closing += MainWindow_Closing;
              
            }
            catch (Exception)
            {

                MessageBox.Show("Error");
            } 
           

        }
          private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
          {
               Closing closing = new Closing();
               closing.Show();
               BackgroundWorker backgroundWorker = new BackgroundWorker();
               backgroundWorker.DoWork += BackgroundWorker_DoWork;
              backgroundWorker.RunWorkerAsync();
          }

        
          private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
          {
               Thread.Sleep(2500);
               Environment.Exit(Environment.ExitCode);
               
          }
     }
}
