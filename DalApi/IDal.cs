//Yehonatan Eliyahu 311555387
//Mendi Shneorson 204290688
using DO;
using System;
using System.Collections.Generic;

namespace DalApi
{
    public interface IDal
    {
        void configHandler(Action action);
        IEnumerable<DO.Person> GetPersons();
        HostingUnit RecieveHostingUnit(int key);
        Dictionary<string, object> getConfig();
        void setConfig(string key, object value);
        event Action ConfigHandler;

         int AddHostingUnit(HostingUnit hostingUnit);
        void UpdateHostingUnit(HostingUnit hostingUnit);
        void DeleteHostingUnit(HostingUnit hostingUnit);
        Person RecievePerson(int key);
        void AddPerson(Person person);
        void DeletePerson(Person person);
        GuestRequest RecieveGuestRequest(int key);
        int AddGuesetRequest(GuestRequest request);
        void UpdateGusetRequest(GuestRequest request);
        void UpdateGusetRequestStatus(GuestRequest request);
        Host RecieveHost(int key);
        void AddHost(Host host);
        void DeleteHost(Host host);
        void UpdateHost(Host host);
        Order RecieveOrder(int key);
        int AddOrder(Order order);
        void UpdateOrder(Order order);
        IEnumerable<HostingUnit> GetHostingUnitsList(Func<HostingUnit, bool> predicate);
        IEnumerable<GuestRequest> GetGuestRequestsList();
        IEnumerable<Order> GetOrdersList(Func<Order, bool> predicate);
        IEnumerable<BankBranch> GetBankBranchesList();
        IEnumerable<Host> GetHostsList();
        int GetCommissionRate();
        int GetCommissionFromOrder(Order order);
        BankBranch RecieveBankBranch(int branchNumber);
        void UpdatePerson(Person person);
    }
}
