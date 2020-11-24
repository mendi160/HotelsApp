using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace FinalGui
{
    class EnumToHebrew : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return MyDictionary.TranslateEnumToString((Enum)value);//return string
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return MyDictionary.TranslatStringeToEnum((string)value);//return enum
        }

    }
    class BoolToYeshEn : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                return "יש";
            }
            return "אין";

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    class ConfigToHebew : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return MyDictionary.ConfigTrenslate[(string)value];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public static class MyDictionary
    {
        static Dictionary<Enum, string> Hebrew_English;
        public static Dictionary<string, string> ConfigTrenslate;
        static public IEnumerable<BO.Location> Locations;
        static public IEnumerable<BO.Hosting_Type> HostingTypes;
        static public IEnumerable<BO.Order_Status> OrderStatus;
        static public IEnumerable<BO.ID> IDs;
        static public IEnumerable<BO.Request_Statut> RequestStatuts;
        static public IEnumerable<BO.Preferences> Preferences;
        static public IEnumerable<BO.Status> Statuses;
        static MyDictionary()
        {
            Locations = (IEnumerable<BO.Location>)(Enum.GetValues(typeof(BO.Location)));
            HostingTypes = (IEnumerable<BO.Hosting_Type>)(Enum.GetValues(typeof(BO.Hosting_Type)));
            IDs = (IEnumerable<BO.ID>)(Enum.GetValues(typeof(BO.ID)));
            RequestStatuts = (IEnumerable<BO.Request_Statut>)(Enum.GetValues(typeof(BO.Request_Statut)));
            Preferences = (IEnumerable<BO.Preferences>)(Enum.GetValues(typeof(BO.Preferences)));
            Statuses = (IEnumerable<BO.Status>)(Enum.GetValues(typeof(BO.Status)));
            OrderStatus = (IEnumerable<BO.Order_Status>)(Enum.GetValues(typeof(BO.Order_Status)));
            Hebrew_English = new Dictionary<Enum, string>();
            Hebrew_English.Add(BO.Location.NORTH, "צפון");
            Hebrew_English.Add(BO.Location.SOUTH, "דרום");
            Hebrew_English.Add(BO.Location.ALL, "הכל");
            Hebrew_English.Add(BO.Location.CENTER, "מרכז");
            Hebrew_English.Add(BO.Location.JERUSALEM, "ירושלים");
            Hebrew_English.Add(BO.Hosting_Type.APARTMENT, "דירה");
            Hebrew_English.Add(BO.Hosting_Type.HOTEL, "מלון");
            Hebrew_English.Add(BO.Hosting_Type.ZIMMER, "צימר");
            Hebrew_English.Add(BO.Hosting_Type.CAMPING, "אתר מחנאות");
            Hebrew_English.Add(BO.ID.ID, "ת.ז.");
            Hebrew_English.Add(BO.ID.PASSPORT, "דרכון");
            Hebrew_English.Add(BO.ID.DRIVER_LICENSE, "רשיון נהיגה");
            Hebrew_English.Add(BO.Status.ACTIVE, "פעיל");
            Hebrew_English.Add(BO.Status.INACTIVE, "לא פעיל");
            Hebrew_English.Add(BO.Order_Status.APPROVED, "אושרה");
            Hebrew_English.Add(BO.Order_Status.CLIENT_CLOSED, "לקוח סגור");
            Hebrew_English.Add(BO.Order_Status.EMAIL_SENT, "נשלח מייל");
            Hebrew_English.Add(BO.Order_Status.IGNORED_CLOSED, "נסגרה עקב התעלמות");
            Hebrew_English.Add(BO.Order_Status.IRRELEVANT, "לא רלוונטי");
            Hebrew_English.Add(BO.Order_Status.PENDING, "בהמתנה");
            Hebrew_English.Add(BO.Request_Statut.CANCELLED, "בוטלה");
            Hebrew_English.Add(BO.Request_Statut.EXPIRED, "פג תוקף");
            Hebrew_English.Add(BO.Request_Statut.OPEN, "פתוחה");
            Hebrew_English.Add(BO.Request_Statut.ORDERED, "הוזמנה");
            Hebrew_English.Add(BO.Preferences.YES, "כן");
            Hebrew_English.Add(BO.Preferences.NO, "לא");
            Hebrew_English.Add(BO.Preferences.MAYBE, "אולי");
            //------------------------------------------------
            ConfigTrenslate = new Dictionary<string, string>();
            ConfigTrenslate.Add("HostingUnitKey", "מספר סידורי יחידת אירוח");
            ConfigTrenslate.Add("GuestRequestKey", "מספר סידורי בקשות אירוח");
            ConfigTrenslate.Add("OrderKey", "מספר סידורי הזמנות");
            ConfigTrenslate.Add("LastUPdate", "זמן עדכון אחרון");
            ConfigTrenslate.Add("IgnoreTime", "זמן המתנה");
            ConfigTrenslate.Add("Commission", "עמלה ללילה");
            ConfigTrenslate.Add("Manager", "מנהל");
        }
        public static Enum TranslatStringeToEnum(string word)
        {

            return Hebrew_English.FirstOrDefault(x => x.Value == word).Key;
        }
        public static string TranslateEnumToString(Enum word)
        {

            return Hebrew_English[word];
        }
    }
    public static class Numbers
    {
        public static int[] ArrayNumbers = new int[] { 0,1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };
    }
    public static class SendRandomPassword
    {
        static Random random = new Random();
        public static string CreateRandomPassword(int length = 8)
        {

            string password = random.Next(0, 10).ToString();
            for (int i = 1; i < length; ++i)
                password += random.Next(0, 10).ToString();
            return password;
        }
        public static void SendPasswordEmail(string emailAddress, string newPassword)
        {
            MailMessage email = new MailMessage();
            email.To.Add(emailAddress);
            email.From = new MailAddress("NoReply@jct.ac.il");
            email.Subject = "סיסמא חדשה";
            email.IsBodyHtml = false;
            email.Body = newPassword + "         :סיסמתך החדשה הינה";
            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new System.Net.NetworkCredential("dotnet5780@gmail.com", "rede24@@");
            smtp.EnableSsl = true;
            smtp.Host = "smtp.gmail.com";
            try
            {
                smtp.Send(email);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
    }
}
