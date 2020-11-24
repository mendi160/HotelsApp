using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for AddUserPage.xaml
    /// </summary>
    public partial class AddUserPage : Page
    {
        BlApi.IBl bl;
        public AddUserPage(BlApi.IBl bl)
        {
            InitializeComponent();
            this.bl = bl;
            IdTypeComboBox.ItemsSource = MyDictionary.IDs.Select(x => MyDictionary.TranslateEnumToString(x));
        }

        private void TextBlock_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (!textBox.Text.All(char.IsLetter) || textBox.Text.Length == 0)
            {
                textBox.Background = Brushes.Red;
            }

        }

        private void GotFocus_ClearBG(object sender, RoutedEventArgs e)
        {
            (sender as Control).Background = Brushes.White;
        }

        private void IdTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IdTextBox.IsEnabled = true;
        }
        private void CheckIDNumber(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (IdTypeComboBox.SelectedIndex == 2)
            {
                if (!textBox.Text.All(char.IsDigit) || textBox.Text.Length != 7)
                {
                    textBox.Background = Brushes.Red;
                }

            }
            else if (!textBox.Text.All(char.IsDigit) || textBox.Text.Length != 9)
            {
                textBox.Background = Brushes.Red;
            }
        }

        private void MailAddressTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(MailAddressTextBox.Text, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            {
                MailAddressTextBox.Background = Brushes.Red;
            }
        }

        private void PasswordTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (PasswordTextBox.Password.Length < 4 || PasswordTextBox.Password.Length > 18)
            {
                PasswordTextBox.Background = Brushes.Red;
            }
            else
            {
                PasswordAgainTextBox.IsEnabled = true;
                PasswordTextBox.Background = Brushes.White;
            }
        }

        private void PasswordAgainTextBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (PasswordTextBox.Password != PasswordAgainTextBox.Password)
            {
                PasswordAgainTextBox.Background = Brushes.Red;
            }
            else
                PasswordAgainTextBox.Background = Brushes.LightGreen;
        }

        private void PhoneNumberTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (PhoneNumberTextBox.Text.Length != 10 || !PhoneNumberTextBox.Text.All(char.IsDigit))
            {
                PhoneNumberTextBox.Background = Brushes.Red;
            }
        }

        private void AddPersonButton_Click(object sender, RoutedEventArgs e)
        {


            foreach (var item in AddPerson.Children)
            {
                if (item is TextBox || item is PasswordBox)
                {
                    if ((item as Control).Background == Brushes.Red || (item as Control).Background == Brushes.WhiteSmoke)
                    {
                        MessageBox.Show("לא כל השדות מלאו כנדרש");
                        return;
                    }
                }
            }
            BO.Person person = new BO.Person();
            person.FirstName = this.FirstNameTextBox.Text;
            person.LastName = this.LastNameTextBox.Text;
            person.Id = int.Parse(this.IdTextBox.Text);
            person.MailAddress = this.MailAddressTextBox.Text;
            person.Password = this.PasswordTextBox.Password;
            person.PhoneNumber = int.Parse(this.PhoneNumberTextBox.Text);
            person.Status = BO.Status.ACTIVE;
            person.IdType = (BO.ID)MyDictionary.TranslatStringeToEnum(IdTypeComboBox.SelectedItem.ToString());
            try
            {
                bl.AddPerson(person);
            }
            catch (DuplicateWaitObjectException exp)
            {

                MessageBox.Show(exp.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            NavigationService.Navigate((new MainWindowPage(bl, person)));
        }
    }
}
