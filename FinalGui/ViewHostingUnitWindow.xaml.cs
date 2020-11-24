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
    /// Interaction logic for ViewHostingUnitWindow.xaml
    /// </summary>
    public partial class ViewHostingUnitWindow : Window
    {
        BO.HostingUnit HostingUnit { get; set; }
        BlApi.IBl bl;
        public ViewHostingUnitWindow(BO.HostingUnit hostingUnit, BlApi.IBl bl)
          {

               InitializeComponent();
               HostingUnit = hostingUnit;
               try
               {
                    Image1.Source = new BitmapImage(new Uri(HostingUnit.ImageLink1));
                    Image2.Source = new BitmapImage(new Uri(HostingUnit.ImageLink2));
                    Image3.Source = new BitmapImage(new Uri(HostingUnit.ImageLink3));
               }
               catch (ArgumentNullException)
               {


               }
               catch (UriFormatException)
               { }
              
               ViewGrid.DataContext = HostingUnit;
               var areacollection = MyDictionary.Locations.Select(x => MyDictionary.TranslateEnumToString(x)).ToList();
               areacollection.Remove("הכל");
               areaComboBox.ItemsSource = areacollection;
               List<int> hip = new List<int>();
              



               HostingUnit = hostingUnit;
               this.bl = bl;
              
               //setBlackOutDates(hostingUnit);

          }

          private void ImageMouseEnter(object sender, MouseEventArgs e)
          {
               Image current = sender as Image;
               current.Width = 200;
               current.Height = 200;

          }
          private void ImageMouseLeave(object sender, MouseEventArgs e)
          {
               Image current = sender as Image;
               current.Width = 120;
               current.Height = 120;

          }
          private void CheckLink_Click(object sender, RoutedEventArgs e)
          {
               var textbox = (((sender as Button).Parent) as StackPanel).Children[1];//since its stack panel i know textblock that contains uri is [1]
               string link = ((TextBox)textbox).Text;
               if (link.Length == 0)
               {
                    return;
               }
               try
               {
                    ImageViewer.Source = new BitmapImage(new Uri(link));
               }
               catch (Exception tapuzina)
               {

                    MessageBox.Show(tapuzina.Message);
               }





          }

          private void UpdateButton_Click(object sender, RoutedEventArgs e)
          {
               bl.UpdateHostingUnit(HostingUnit);
               this.Close();
          }

        
      

          private void LinkTExtbox_GotMouseCapture(object sender, MouseEventArgs e)
          {
               var textbox = sender as TextBox;
               textbox.SelectAll();
          }



         
     }
}
