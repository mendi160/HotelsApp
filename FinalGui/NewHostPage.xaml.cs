using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
using System.Xml.Serialization;

namespace FinalGui
{
     /// <summary>
     /// Interaction logic for NewHostPage.xaml
     /// </summary>
     public partial class NewHostPage : Page
     {
          BlApi.IBl bl;
          public BO.Host Host { set; get; }
          public Dictionary<int, string> BankNumberDictionary { set; get; }
          public Dictionary<int, string> BankAddressDictionary { set; get; }
          public NewHostPage(BlApi.IBl bl, BO.Host host)
          {
               InitializeComponent();
               this.bl = bl;
               this.Host = host;
               this.DataContext = Host;
               NewHostGrid.DataContext = Host;
               CreateXMLBankFiles();
               bankNameComboBox.ItemsSource = BankNumberDictionary.Values;
               host.BankBranch = new BO.BankBranch();




          }

          private void AddHostButton_Click(object sender, RoutedEventArgs e)

          {
               Host.BankBranch = bl.GetBankBranch(int.Parse(branchNumberTextBox.Text)); 
               bl.AddHost(Host);

               NavigationService.Navigate(new MainHostPage(bl, Host));
          }
          void CreateXMLBankFiles()
          {
               XElement xml = XElement.Load("XML/Bank.xml");

               BankNumberDictionary = (from p in xml.Elements()
                                       select new { code = int.Parse(p.Element("קוד_בנק").Value), name = p.Element("שם_בנק").Value }
                           into g
                                       group g by g.code).ToDictionary(h => h.Key, h => h.ElementAt(0).name);
               BankAddressDictionary = (from p in xml.Elements()
                                        select new { number = int.Parse(p.Element("קוד_סניף").Value), address = p.Element("כתובת_ה-ATM").Value, City = p.Element("ישוב").Value }
                          into g
                                        group g by g.number).ToDictionary(h => h.Key, h => h.ElementAt(0).address + "@" + h.ElementAt(0).City);


          }

          private void bankNameComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
          {
               ComboBox comboBox = sender as ComboBox;
               string name = comboBox.SelectedItem as string;
               bankNumberTextBox.Text = BankNumberDictionary.FirstOrDefault(x => x.Value == name).Key.ToString();

          }

          private void branchNumberTextBox_TextChanged(object sender, TextChangedEventArgs e)
          {

               TextBox textBox = sender as TextBox;
               string address;
               if (textBox.Text.Any(char.IsLetter) || textBox.Text.Length == 0 || BankAddressDictionary.TryGetValue(int.Parse(textBox.Text), out address) == false)
               {
                    return;
               }

               branchCityTextBox.Text = address.Substring(address.IndexOf('@') + 1);
               branchAddressTextBox.Text = address.Substring(0, address.LastIndexOf('@'));
          }
     }
}