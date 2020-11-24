using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public enum UserType { GUEST, HOST, ADMIN }
    public enum ID
    {
        ID, PASSPORT, DRIVER_LICENSE
    }
    public enum Status
    {
        ACTIVE, INACTIVE
    }
    public enum Hosting_Type
    {
        ZIMMER, APARTMENT, HOTEL, CAMPING
    };
    public enum Location
    {
        ALL, NORTH, SOUTH, CENTER, JERUSALEM
    };
    public enum Order_Status
    {
        PENDING, APPROVED, EMAIL_SENT, IGNORED_CLOSED, CLIENT_CLOSED, IRRELEVANT
    };
    public enum Request_Statut
    {
        OPEN, EXPIRED, CANCELLED, ORDERED
    };
    public enum Preferences
    {
        MAYBE, YES, NO
    };
    //static public class StringStatus
    //{
    //    static public string[] Preferences = new string[] { "אולי", "כן", "לא" };
    //    static public string[] Request_Statut = new string[] { "פתוח", "פג תוקף", "בוטלה","הוזמנה" };
    //    static public string[] Order_Status = new string[] { "PENDING", "APPROVED", "EMAIL_SENT", "IGNORED_CLOSED", "CLIENT_CLOSED", "IRRELEVANT" };
    //    static public string[] Location = new string[] { "כללי", "YES", "NO" };
    //    static public string[] Hosting_Type = new string[] { "MAYBE", "YES", "NO" };
    //    static public string[] Status = new string[] { "MAYBE", "YES", "NO" };
    //    static public string[] ID = new string[] { "MAYBE", "YES", "NO" };

    //}
}
