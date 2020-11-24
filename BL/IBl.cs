//Yehonatan Eliyahu 311555387
//Mendi Shneorson 204290688
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using BO;

namespace BlApi
{
    public interface IBl
    {
        IEnumerable<BO.Person> GetPersons();
        Client GetClient(int id);
        void configHandler(Action action);
        int AddHostingUnit(BO.HostingUnit hostingUnit);
        int AddGuestRequest(BO.GuestRequest guestRequest);
        int AddOrder(BO.Order order);
        void AddPerson(BO.Person person);
        void AddHost(BO.Host host);
        BO.Host GetHost(int key);
        void UpdateGusetRequest(BO.GuestRequest request);
        void UpdatePerson(BO.Person person);
        void UpdateHost(BO.Host host);
        void UpdateHostingUnit(BO.HostingUnit hostingUnit);
        void UpdateOrder(BO.Order order);
        bool CheckDate(BO.GuestRequest guestRequest);
        bool EmailPremissionCheck(BO.Host host);
        //check that we dont have overlap
        bool IsAvailableGuestRequest(BO.GuestRequest guestRequest, BO.HostingUnit hostingUnit);
        bool AbleToChangeOrderStatus(BO.Order order);
        int CalculateCommision(BO.Order order);
        void MarkDates(BO.Order order);
        void UpdateUserStatus(BO.Order order);
        bool DeleteableHostingUnit(BO.HostingUnit hostingUnit);
        void DeleteHost(BO.Host host);
        void DeleteHostingUnit(BO.HostingUnit hostingUnit);
        bool AbleToChangeCollectionClearance(BO.Host host);
        void SendEmail(BO.Order order);
        IEnumerable<BO.HostingUnit> CheckForAvailableHostingUnit(DateTime date, int days);
        int PassedDays(DateTime first, DateTime second = default(DateTime));
        IEnumerable<BO.Order> OrdersCreated(int days);
        IEnumerable<BO.GuestRequest> MatchingRequirment(Func<BO.GuestRequest, bool> predicate);
        int NumberOfInvitationsSent(BO.GuestRequest guestRequest);
        int NumberOfSentOrders(BO.HostingUnit hostingUnit);
        void CloseIrrelevantOrders(BO.Order order);
        #region  Grouping
        IEnumerable<IGrouping<BO.Location, BO.GuestRequest>> GuestRequestGroupedBySpecificArea();
        IEnumerable<IGrouping<int, BO.GuestRequest>> GroupedByNumberOfGuests();
        IEnumerable<IGrouping<int, BO.Host>> GroupedByNumberOfHostingUnit();
        IEnumerable<IGrouping<BO.Location, BO.HostingUnit>> HostingUnitGroupedBySpecificArea();
        #endregion
        void ExpiredThread();
        void CleanDiaryThread();
        bool IsOpenOrder(BO.Order order);
        BO.Person GetPerson(int id);
        BO.BankBranch GetBankBranch(int branchNumber);
        IEnumerable<BO.HostingUnit> GetHostingUnitsOfHost(int key);
        IEnumerable<BO.Order> GetOrdersOfHost(int key);
        IEnumerable<BO.GuestRequest> GetGuestRequests(int key);
        IEnumerable<BO.Order> GetOrders();
        IEnumerable<BO.GuestRequest> GetGetGuestRequestsExceptHostGr(int key);
        IEnumerable<BO.GuestRequest> GetGuestRequests();
        IEnumerable<BO.Host> GetHosts();
        BO.HostingUnit GetHostingUnit(int key);
        BO.GuestRequest GetGuestRequest(int key);
        IEnumerable<BO.HostingUnit> GetHostingUnits();
        BO.Order GetOrder(int key);
        Dictionary<string,int> GetConfig();
        IEnumerable<BO.Order> MailSentTime(IEnumerable<BO.Order> orders, int days);
        void updateConfig(string key ,object value);
    }
}
