using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    //public static class MyDictionary
    //{
    //    static Dictionary<Enum, string> Hebrew_English;
    //    static public IEnumerable<BO.Location> Locations;
    //    static public IEnumerable<BO.Hosting_Type> HostingTypes;
    //    static public IEnumerable<BO.Order_Status> OrderStatus;
    //    static public IEnumerable<BO.ID> IDs;
    //    static public IEnumerable<BO.Request_Statut> RequestStatuts;
    //    static public IEnumerable<BO.Preferences> Preferences;
    //    static public IEnumerable<BO.Status> Statuses;
    //    static MyDictionary()
    //    {
    //        Locations = (IEnumerable<BO.Location>)(Enum.GetValues(typeof(BO.Location)));
    //        HostingTypes = (IEnumerable<BO.Hosting_Type>)(Enum.GetValues(typeof(BO.Hosting_Type)));
    //        IDs = (IEnumerable<BO.ID>)(Enum.GetValues(typeof(BO.ID)));
    //        RequestStatuts = (IEnumerable<BO.Request_Statut>)(Enum.GetValues(typeof(BO.Request_Statut)));
    //        Preferences= (IEnumerable<BO.Preferences>)(Enum.GetValues(typeof(BO.Preferences)));
    //        Statuses= (IEnumerable<BO.Status>)(Enum.GetValues(typeof(BO.Status)));
    //        Hebrew_English = new Dictionary<Enum, string>();
    //        Hebrew_English.Add(BO.Location.NORTH, "צפון");
    //        Hebrew_English.Add(BO.Location.SOUTH, "דרום");
    //        Hebrew_English.Add(BO.Location.ALL, "הכל");
    //        Hebrew_English.Add(BO.Location.CENTER, "מרכז");
    //        Hebrew_English.Add(BO.Location.JERUSALEM, "ירושלים");
    //        Hebrew_English.Add(BO.Hosting_Type.APARTMENT, "דירה");
    //        Hebrew_English.Add(BO.Hosting_Type.HOTEL, "מלון");
    //        Hebrew_English.Add(BO.Hosting_Type.ZIMMER, "צימר");
    //        Hebrew_English.Add(BO.Hosting_Type.CAMPING, "אתר מחנאות");
    //        Hebrew_English.Add(BO.ID.ID, "ת.ז.");
    //        Hebrew_English.Add(BO.ID.PASSPORT, "דרכון");
    //        Hebrew_English.Add(BO.ID.DRIVER_LICENSE, "רשיון נהיגה");
    //        Hebrew_English.Add(BO.Status.ACTIVE, "פעיל");
    //        Hebrew_English.Add(BO.Status.INACTIVE, "לא פעיל");
    //        Hebrew_English.Add(BO.Order_Status.APPROVED, "אושרה");
    //        Hebrew_English.Add(BO.Order_Status.CLIENT_CLOSED, "לקוח סגור");
    //        Hebrew_English.Add(BO.Order_Status.EMAIL_SENT, "נשלח מייל");
    //        Hebrew_English.Add(BO.Order_Status.IGNORED_CLOSED, "נסגרה עקב התעלמות");
    //        Hebrew_English.Add(BO.Order_Status.IRRELEVANT, "לא רלוונטי");
    //        Hebrew_English.Add(BO.Order_Status.PENDING, "בהמתנה");
    //        Hebrew_English.Add(BO.Request_Statut.CANCELLED, "בוטלה");
    //        Hebrew_English.Add(BO.Request_Statut.EXPIRED, "פג תוקף");
    //        Hebrew_English.Add(BO.Request_Statut.OPEN, "פתוחה");
    //        Hebrew_English.Add(BO.Request_Statut.ORDERED, "הוזמנה");
    //        Hebrew_English.Add(BO.Preferences.YES, "כן");
    //        Hebrew_English.Add(BO.Preferences.NO, "לא");
    //        Hebrew_English.Add(BO.Preferences.MAYBE, "אולי");
    //    }
    //    public static Enum Translate(string word)
    //    {
    //        return Hebrew_English.FirstOrDefault(x => x.Value == word).Key;
    //    }
    //    public static string TranslateE(Enum word)
    //    {

    //        return Hebrew_English[word];
    //    }
    //}
}
