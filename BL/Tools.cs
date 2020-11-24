using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BL
{/// <summary>
/// this class is going to convert each element from do to bo and the other way
/// </summary>
    public class Conversions
    {
         // to  use our interface function  we need to get ibl object
        public static BlApi.IBl imp = BlApi.BlFactory.GetBL();

        #region Person
        public static BO.Person CastingToBOPerson(DO.Person person)
        {
            BO.Person bPerson = new BO.Person();
            bPerson.Id = person.Id;
            bPerson.IdType = (BO.ID)person.IdType;
            bPerson.Status = (BO.Status)person.Status;
            bPerson.Password = person.Password;
            bPerson.FirstName = person.FirstName;
            bPerson.LastName = person.LastName;
            bPerson.PhoneNumber = person.PhoneNumber;
            bPerson.MailAddress = person.MailAddress;
            bPerson.UserType = person.UserType;
            return bPerson;
        }
        public static DO.Person CastingToDOPerson(BO.Person bPerson)
        {
            DO.Person person = new DO.Person();
            person.Id = bPerson.Id;
            person.IdType = (DO.ID)bPerson.IdType;
            person.Status = (DO.Status)bPerson.Status;
            person.Password = bPerson.Password;
            person.FirstName = bPerson.FirstName;
            person.LastName = bPerson.LastName;
            person.PhoneNumber = bPerson.PhoneNumber;
            person.MailAddress = bPerson.MailAddress;
            person.UserType = bPerson.UserType;
            return person;
        }
        #endregion
        #region BankBranch
        public static BO.BankBranch CastingToBOBankBranch(DO.BankBranch bankBranch)
        {
            BO.BankBranch bBankBranch = new BankBranch
            {
                BankName = bankBranch.BankName,
                BankNumber = bankBranch.BankNumber,
                BranchAddress = bankBranch.BranchAddress,
                BranchCity = bankBranch.BranchCity,
                BranchNumber = bankBranch.BranchNumber
            };
            return bBankBranch;
        }
        public static DO.BankBranch CastingToDOBankBranch(BO.BankBranch bBankBranch)
        {
            DO.BankBranch BankBranch = new DO.BankBranch
            {
                BankName = bBankBranch.BankName,
                BankNumber = bBankBranch.BankNumber,
                BranchAddress = bBankBranch.BranchAddress,
                BranchCity = bBankBranch.BranchCity,
                BranchNumber = bBankBranch.BranchNumber
            };
            return BankBranch;
        }
        #endregion
        #region Host
        public static BO.Host CastingToBOHost(DO.Host host)
        {

            BO.Host bHost = new BO.Host();
            bHost.Person = new Person();
           bHost.Person =imp.GetPerson(host.HostID);
            bHost.BankBranch = imp.GetBankBranch(host.BranchNumber);
            bHost.BankAccuontNumber = host.BankAccuontNumber;
            bHost.CollectionClearance = host.CollectionClearance;
            bHost.Status = (BO.Status)host.Status;
            bHost.TotalCommission = host.TotalCommission;
            bHost.HostingUnits = imp.GetHostingUnitsOfHost(host.HostID);
            bHost.Orders = imp.GetOrdersOfHost(host.HostID);
            bHost.Website = host.Website;
            return bHost;
        }
        public static DO.Host CastingToDOHost(BO.Host host)
        {
            DO.Host dHost = new DO.Host()
            {
                Website = host.Website,
                BankAccuontNumber = host.BankAccuontNumber,
                Status = (DO.Status)host.Status,
                BankNumber = host.BankBranch.BankNumber,
                BranchNumber = host.BankBranch.BranchNumber,
                CollectionClearance = host.CollectionClearance,
                HostID = host.Person.Id,
                TotalCommission = host.TotalCommission
            };
            return dHost;
        }
        #endregion
        #region HostingUnit 
        public static DO.HostingUnit CastingToDOHostingUnit(BO.HostingUnit hostingUnit)
        {
            DO.HostingUnit HU = new DO.HostingUnit()
            {
                Diary = hostingUnit.Diary,
                HostingUnitName = hostingUnit.HostingUnitName,
                Key = hostingUnit.Key,
                Owner = hostingUnit.Owner,
                Status = (DO.Status)hostingUnit.Status,
                Area = (DO.Location)hostingUnit.Area,
                ImageLink1 =hostingUnit.ImageLink1,
                 ImageLink2 = hostingUnit.ImageLink2,
                 ImageLink3 = hostingUnit.ImageLink3,
                 Jacuzzi=hostingUnit.Jacuzzi,
                 Kitchen=hostingUnit.Kitchen,
                 SwimmingPool=hostingUnit.SwimmingPool,
                 TV=hostingUnit.TV,
                 WIFI=hostingUnit.WIFI,
                 PricePerNight =hostingUnit.PricePerNight
                 

            };
            return HU;
        }
        public static BO.HostingUnit CastingToBOHostingUnit(DO.HostingUnit hostingUnit)
        {
            BO.HostingUnit HU = new BO.HostingUnit()
            {
                Diary = hostingUnit.Diary,
                HostingUnitName = hostingUnit.HostingUnitName,
                Key = hostingUnit.Key,
                Owner = hostingUnit.Owner,
                Status = (BO.Status)hostingUnit.Status,
                Area = (BO.Location)hostingUnit.Area,
                ImageLink1 = hostingUnit.ImageLink1,
                 ImageLink2 = hostingUnit.ImageLink2,
                 ImageLink3 = hostingUnit.ImageLink3,
                 Jacuzzi = hostingUnit.Jacuzzi,
                 Kitchen = hostingUnit.Kitchen,
                 SwimmingPool = hostingUnit.SwimmingPool,
                 TV = hostingUnit.TV,
                 WIFI = hostingUnit.WIFI,
                  PricePerNight = hostingUnit.PricePerNight
            };
            return HU;
        }
        #endregion
        #region GuestRequest
        public static BO.GuestRequest CastingToBOGuestRequest(DO.GuestRequest guestRequest)
        {
            BO.GuestRequest BOguestRequest = new BO.GuestRequest()
            {
                Adults = guestRequest.Adults,
                Area = (BO.Location)guestRequest.Area,
                ChildrensAttractions = (BO.Preferences)guestRequest.ChildrensAttractions,
                SubArea = guestRequest.SubArea,
                Status = (BO.Request_Statut)guestRequest.Status,
                children = guestRequest.children,
                ClientID = guestRequest.ClientID,
                EntryDate = guestRequest.EntryDate,
                ReleaseDate = guestRequest.ReleaseDate,
                RegistrationDate = guestRequest.RegistrationDate,
                Garden = (BO.Preferences)guestRequest.Garden,
                GuestRequestKey = guestRequest.GuestRequestKey,
                Jacuzzi = (BO.Preferences)guestRequest.Jacuzzi,
                Pool = (BO.Preferences)guestRequest.Pool,
                HostingType = (BO.Hosting_Type)guestRequest.Type
            };
            return BOguestRequest;
        }
        public static DO.GuestRequest CastingToDOGuestRequest(BO.GuestRequest guestRequest)
        {
            DO.GuestRequest GR = new DO.GuestRequest()
            {
                Adults = guestRequest.Adults,
                Area = (DO.Location)guestRequest.Area,
                ChildrensAttractions = (DO.Preferences)guestRequest.ChildrensAttractions,
                SubArea = guestRequest.SubArea,
                Status = (DO.Request_Statut)guestRequest.Status,
                children = guestRequest.children,
                ClientID = guestRequest.ClientID,
                EntryDate = guestRequest.EntryDate,
                ReleaseDate = guestRequest.ReleaseDate,
                RegistrationDate = guestRequest.RegistrationDate,
                Garden = (DO.Preferences)guestRequest.Garden,
                GuestRequestKey = guestRequest.GuestRequestKey,
                Jacuzzi = (DO.Preferences)guestRequest.Jacuzzi,
                Pool = (DO.Preferences)guestRequest.Pool,
                Type = (DO.Hosting_Type)guestRequest.HostingType
            };
            return GR;
        }
        #endregion
        #region Order
        public static BO.Order CastingToBO_Order(DO.Order order)
        {
            BO.Order bOrder = new BO.Order()
            {
                CloseDate = order.CloseDate,
                OrderDate = order.OrderDate,
                GuestRequest = imp.GetGuestRequest(order.GuestRequestKey),
                SentDate = order.SentDate,
                Commission = order.Commission,
                HostingUnit = imp.GetHostingUnit(order.HostingUnitKey),
                ClientFirstName = imp.GetPerson(order.CliendID).FirstName,
                ClientLastName = imp.GetPerson(order.CliendID).LastName,
                Status = (BO.Order_Status)order.Status,
                HostID = order.HostID,
                Key = order.Key,
                TotalCost = order.TotalCost
            };
            return bOrder;
        }
        public static DO.Order CastingToDO_Order(BO.Order order)
        {
            DO.Order dOrder = new DO.Order()
            {
                CloseDate = order.CloseDate,
                OrderDate = order.OrderDate,
                GuestRequestKey = order.GuestRequest.GuestRequestKey,
                SentDate = order.SentDate,
                Commission = order.Commission,
                HostingUnitKey = order.HostingUnit.Key,
                CliendID = order.GuestRequest.ClientID,
                Status = (DO.Order_Status)order.Status,
                HostID = order.HostID,
                Key = order.Key,
                  TotalCost = order.TotalCost
            };
            return dOrder;
        }
        #endregion
    }
    static public class indexer
    {
        //static public bool GetDate(this DO.HostingUnit hostingUnit, DateTime dateTime)
        //{
        //    return hostingUnit.Diary[dateTime.Month - 1, dateTime.Day - 1];
        //}
        static public void SetDate(this DO.HostingUnit hostingUnit, DateTime dateTime, bool value)
        {
            hostingUnit.Diary[dateTime.Month - 1, dateTime.Day - 1] = value;
        }
        static public bool GetDate(this BO.HostingUnit hostingUnit, DateTime dateTime)
        {
            return hostingUnit.Diary[dateTime.Month - 1, dateTime.Day - 1];
        }
        static public void SetDate(this BO.HostingUnit hostingUnit, DateTime dateTime, bool value)
        {
            hostingUnit.Diary[dateTime.Month - 1, dateTime.Day - 1] = value;
        }
    }
}
