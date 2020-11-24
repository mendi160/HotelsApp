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
    /// Interaction logic for AddHostingUnirPage.xaml
    /// </summary>
    public partial class AddHostingUnitPage : Page
    {
        BlApi.IBl bl;
        public BO.Host Host { set; get; }
        public BO.HostingUnit HostingUnit { set; get; }
        public ListBox LB { set; get; } 
        public AddHostingUnitPage(BlApi.IBl bl, BO.Host host, ListBox listBox)
        {
            InitializeComponent();
            Host = host;
            HostingUnit = new BO.HostingUnit();
            HostingUnit.Diary = new bool[12, 31];
            AddHUGrid.DataContext = HostingUnit;
            LB = listBox;
            this.bl = bl;
           // areaComboBox.ItemsSource = BO.MyDictionary.Locations.Select(x=>BO.MyDictionary.TranslateE(x));
            areaComboBox.ItemsSource = MyDictionary.Locations.Select(x => MyDictionary.TranslateEnumToString(x));
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            HostingUnit.Owner = Host.Person.Id;
           // HostingUnit.Area = (BO.Location)MyDictionary.TranslatStringeToEnum(areaComboBox.SelectedItem.ToString());
            try
            {
                HostingUnit.Key = bl.AddHostingUnit(HostingUnit);
                Host = bl.GetHost(Host.Person.Id);
            }
            catch (DuplicateWaitObjectException exp)
            {
                MessageBox.Show(exp.Message);
                return;
            }
            catch (MissingMemberException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            MessageBox.Show("היחידה הוספה בהצלחה");
            LB.ItemsSource = Host.HostingUnits;
            LB.Items.Refresh();
            NavigationService.GoBack();
        }

          private void CheckLink_Click(object sender, RoutedEventArgs e)
          {
              var textbox = (((sender as Button).Parent) as StackPanel).Children[1];//since its stack panel i know textblock that contains uri is [1]
               string link = ((TextBox)textbox).Text;
               if (link.Length==0)
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
     }
}
