using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DS;
using DO;
using System.Reflection;
/// <summary>
/// this file is the implementation of the IDAL 
/// this class is singelton
/// in every function we check first if its already exist
/// if its exist we throw exeption 
/// </summary>
namespace Dal
{
     sealed class DalObject : IDal
     {
        public event Action ConfigHandler;


        #region singelton

        static readonly DalObject instance = new DalObject();

       
        static DalObject() { }
          DalObject() { }
          public static DalObject Instance { get { return instance; } }
          #endregion
          #region Add to DataSource
          public void AddHost(Host host)//
          {
               bool exist = DataSource.hosts.Any(x => host.HostID == x.HostID);
               if (exist)
                    throw new DuplicateIdException("Host", host.HostID);
               Configuration.TotalHosts++;
               DataSource.hosts.Add(host.Clone());
          }//
          public void AddPerson(Person person)//
          {
               bool exist = DataSource.persons.Any(x => x.Id == person.Id);
               if (exist)
                    throw new DuplicateIdException("Person", person.Id);
               Configuration.TotalPersons++;
               DataSource.persons.Add(person.Clone());
          }
          public int AddGuesetRequest(GuestRequest request)// 
          {
               if (request.GuestRequestKey == 0)//new 
               {
                    request.GuestRequestKey = ++Configuration.GuestRequestKey;
                    Configuration.TotalGuestRequest++;
               }
               else
               {
                    bool GuestRequesExist = DataSource.guestRequests.Any(x => request.GuestRequestKey == x.GuestRequestKey);
                    if (GuestRequesExist)
                         throw new DuplicateIdException("GuestRequest", request.GuestRequestKey);
               }
               Configuration.TotalGuestRequest++;
               DataSource.guestRequests.Add(request.Clone());
               return request.GuestRequestKey;
          }//
          public int AddHostingUnit(HostingUnit hostingUnit)//
          {
               if (hostingUnit.Key == 0)
               {
                    hostingUnit.Key = Configuration.HostingUnitKey++;
               }
               else
               {
                    bool exist = DataSource.hostingUnits.Any(x => hostingUnit.Key == x.Key);
                    if (exist)
                         throw new DuplicateIdException("HostingUnit", hostingUnit.Key);

               }

               Configuration.TotalHostingUnit++;
               DataSource.hostingUnits.Add(hostingUnit.Clone());
               return hostingUnit.Key;
          }
          public int AddOrder(Order order)//
          {
               if (order.Key == 0)
               {
                    order.Key = Configuration.OrderKey++;
               }
               else
               {
                    bool exist = DataSource.orders.Any(x => order.Key == x.Key);
                    if (exist)
                         throw new DuplicateIdException("Order", order.Key);
               }


               Configuration.TotalOrders++;
               DataSource.orders.Add(order.Clone());
               return order.Key;
          }
          #endregion
          #region Delete from DataSource
          public void DeleteHost(Host host)//
          {
               int count = DataSource.hosts.RemoveAll(x => host.HostID == x.HostID);
               if (count == 0)
                    throw new MissingIdException("Host", host.HostID);
               host.Status = Status.INACTIVE;
               DataSource.hosts.Add(host.Clone());
          }
          public void DeleteHostingUnit(HostingUnit hostingUnit)//
          {
               int count = DataSource.hostingUnits.RemoveAll(x => hostingUnit.Key == x.Key);
               if (count == 0)
                    throw new MissingIdException("hostingUnit", hostingUnit.Key);
               hostingUnit.Status = Status.INACTIVE;
               DataSource.hostingUnits.Add(hostingUnit.Clone());
          }
          public void DeletePerson(Person person)//
          {
               int count = DataSource.persons.RemoveAll(x => person.Id == x.Id);
               if (count == 0)
                    throw new MissingIdException("Person", person.Id);
               Configuration.TotalPersons -= count;
          }
          #endregion
          #region Recive data from DataSource
          public int GetCommissionRate()
          {
               return Configuration.Commission;
          }
          public int GetCommissionFromOrder(Order order)
          {
               var temp = DataSource.orders.FirstOrDefault(x => x.Key == order.Key);
               return temp == null ? throw new MissingIdException("Order", order.Key) : temp.Commission;

          }
          public IEnumerable<BankBranch> GetBankBranchesList()
          {
               var list = from item in DataSource.bankBranches
                          select item.Clone();
               return list;
          }
          public IEnumerable<GuestRequest> GetGuestRequestsList()//
          {
               var temp = from item in DataSource.guestRequests
                          select item.Clone();
               return temp;
          }
          public IEnumerable<HostingUnit> GetHostingUnitsList(Func<HostingUnit, bool> predicate)//
          {
               return DataSource.hostingUnits.Where(predicate).Select(HU => HU.Clone());
          }
          public IEnumerable<Order> GetOrdersList(Func<Order, bool> predicate)//
          {
               return DataSource.orders.Where(predicate).Select(o => o.Clone());
          }
          public GuestRequest RecieveGuestRequest(int key)//
          {
               GuestRequest GR = DataSource.guestRequests.FirstOrDefault(x => x.GuestRequestKey == key);
               return GR == null ? throw new MissingIdException("GuestRequest", key) : GR.Clone();
          }
          public Host RecieveHost(int key)//
          {
               Host h = DataSource.hosts.FirstOrDefault(x => x.HostID == key);
               return h == null ? throw new MissingIdException("Host", key) : h.Clone();
          }

          public HostingUnit RecieveHostingUnit(int key)//
          {
               HostingUnit HU = DataSource.hostingUnits.FirstOrDefault(x => x.Key == key);
               return HU == null ? throw new MissingIdException("HostingUnit", key) : HU.Clone();
          }

          public Order RecieveOrder(int key)//
          {
               Order o = DataSource.orders.FirstOrDefault(x => x.Key == key);
               return o == null ? throw new MissingIdException("Order", key) : o.Clone();
          }

          public Person RecievePerson(int id)//
          {
               Person p = DataSource.persons.FirstOrDefault(x => x.Id == id);
               return p == null ? throw new MissingIdException("Order", id) : p.Clone();
          }
          public BankBranch RecieveBankBranch(int branchNumber)
          {
               BankBranch bb = DataSource.bankBranches.FirstOrDefault(x => x.BranchNumber == branchNumber);
               return bb == null ? throw new MissingIdException("BankBranch", branchNumber) : bb.Clone();
          }
          public IEnumerable<Host> GetHostsList()
          {
               return DataSource.hosts.Select(h => h.Clone());
          }
          #endregion
          #region Update DataSource

          public void UpdateGusetRequestStatus(GuestRequest request)//
          {
               UpdateGusetRequest(request);
          }
        public void UpdatePerson(Person person)
        {
            int count = DataSource.persons.RemoveAll(x => x.Id == person.Id);
            if (count == 0)
                throw new MissingIdException("Person", person.Id);
            DataSource.persons.Add(person.Clone());
        }
        public void UpdateGusetRequest(GuestRequest request)
          {

               int count = DataSource.guestRequests.RemoveAll(x => x.GuestRequestKey == request.GuestRequestKey);
               if (count == 0)
                    throw new MissingIdException("GuestRequest", request.GuestRequestKey);
               DataSource.guestRequests.Add(request.Clone());
          }

          public void UpdateHost(Host host)
          {
               int count = DataSource.hosts.RemoveAll(x => x.HostID == host.HostID);
               if (count == 0)
                    throw new MissingIdException("Host", host.HostID);
               DataSource.hosts.Add(host.Clone());
          }

          public void UpdateHostingUnit(HostingUnit hostingUnit)
          {
               int count = DataSource.hostingUnits.RemoveAll(x => x.Key == hostingUnit.Key);
               if (count == 0)
                    throw new MissingIdException("HostingUnit", hostingUnit.Key);
               DataSource.hostingUnits.Add(hostingUnit.Clone());
          }
          public void UpdateOrder(Order order)
          {
               int count = DataSource.orders.RemoveAll(x => x.Key == order.Key);
               if (count == 0)
                    throw new MissingIdException("Order", order.Key);
               DataSource.orders.Add(order.Clone());
          }

        public Dictionary<string, object> getConfig()
        {
            throw new NotImplementedException();
        }

        public void setConfig(string key, object value)
        {
            throw new NotImplementedException();
        }

        public void configHandler(Action action)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Person> GetPersons()
        {
            throw new NotImplementedException();
        }
        #endregion





    }
}
