using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for LoginGuest.xaml
    /// </summary>
    public partial class Login : Page
    {
        BlApi.IBl bl;
        BO.Person person { set; get; }
        BackgroundWorker sendPassword;
        string password;
        public Login(BlApi.IBl bl)
        {
            InitializeComponent();
            this.bl = bl;
            person = new BO.Person();
            sendPassword = new BackgroundWorker();
            sendPassword.DoWork += sendPassword_DoWork;
            sendPassword.RunWorkerCompleted += sendPassword_RunWorkerCompleted;
        }
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                person = bl.GetPerson(int.Parse(LoginTextBox.Text));
            }
            catch(MissingMemberException exp)
            {
                MessageBox.Show("שם המשתמש או הסיסמה אינם נכונים", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (person.Id != int.Parse(LoginTextBox.Text) || person.Password != LoginPassword.Password)
            {
                MessageBox.Show("שם המשתמש או הסיסמה אינם נכונים", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            NavigationService.Navigate(new MainWindowPage(bl, person));

        }
        private void NewPersonButton_Click(object sender, RoutedEventArgs e)//
        {
            NavigationService.Navigate(new AddUserPage(bl));
        }

        private void LoginTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        private void forgotPassword_Click(object sender, RoutedEventArgs e)
        {
            LoginBorder.Visibility = Visibility.Collapsed;
            forgotPasswordGrid.Visibility = Visibility.Visible;

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            forgotPasswordGrid.Visibility = Visibility.Collapsed;
            LoginBorder.Visibility = Visibility.Visible;
        }

        private void SendPassword_Click(object sender, RoutedEventArgs e)
        {
            string mailAddress;
            try
            {
                person = bl.GetPerson(int.Parse(EmailAddressTB.Text));
                 mailAddress =person.MailAddress;
            }
            catch(MissingMemberException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            sendPassword.RunWorkerAsync(mailAddress);
        }
        private void sendPassword_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ((int)e.Result == 1)
            {
                person.Password = password;
                bl.UpdatePerson(person);
                MessageBox.Show(person.MailAddress+ "  :סיסמא חדשה נשלחה לכתובת");
                forgotPasswordGrid.Visibility = Visibility.Collapsed;
                LoginBorder.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("  :קרתה תקלה בניסיון השליחה לכתובת" + person.MailAddress);
            }

        }

        private void sendPassword_DoWork(object sender, DoWorkEventArgs e)
        {
            password = SendRandomPassword.CreateRandomPassword();
            try
            {
                SendRandomPassword.SendPasswordEmail((string)e.Argument, password);
                e.Result = 1;
            }
            catch
            {
                e.Result = 0;
            }

        }

        private void LoginTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (LoginPassword.Password.Length > 0 && LoginTextBox.Text.Length > 0)
                btnSubmit.IsEnabled = true;
            else
                btnSubmit.IsEnabled = false;
        }

        private void LoginPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (LoginPassword.Password.Length > 0 && LoginTextBox.Text.Length > 0)
                btnSubmit.IsEnabled = true;
            else
                btnSubmit.IsEnabled = false;
        }
    }
}

