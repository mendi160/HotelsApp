using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DalApi;
using DO;

namespace Dal
{
    sealed class DalXML : IDal
    {

        XML_Tool XML = new XML_Tool();
        static readonly DalXML instance = new DalXML();
        DalXML()
        {

            
        }

       
       

        static DalXML()
        {
           
        }
        public void configHandler (Action action)
        {
            ConfigHandler += action;
        }

        Dictionary<string, object> config;
        public Dictionary<string, object> Config
        {

            get { return config; }
            set
            {
                config = value;                
               
            }

        }

        public static DalXML Instance { get { return instance; } }

        public event Action ConfigHandler;

        public HostingUnit RecieveHostingUnit(int key)
        {
            HostingUnit hostingUnit = XML.RecieveHostingUnit(key);

            return hostingUnit == null ? throw new MissingMemberException($"hostinUnit {key}  dosen't Exist") : hostingUnit;
        }
        public Dictionary<string, object> getConfig()
        {
            return XML.LoadConfig();
        }

        public void setConfig(string key, object value)
        {
            Config = XML.LoadConfig();
            (Config[key] as ConfigData).Value = value;
            XML.SaveConfig(Config);
            if (ConfigHandler != null)
            {
                ConfigHandler();
            }
        }

        public int AddHostingUnit(HostingUnit hostingUnit)//TODO
        {
            Config = XML.LoadConfig();
            if (XML.RecieveHostingUnit(hostingUnit.Key) != null)
                throw new DuplicateWaitObjectException($"Hosting Unit {hostingUnit.Key} Already Exist");
            // Configuration.TotalPersons++;
            hostingUnit.Key = int.Parse((string)(Config["HostingUnitKey"] as ConfigData).Value);
            XML.AddHostingUnit(hostingUnit);
            setConfig("HostingUnitKey", (hostingUnit.Key + 1));
            return hostingUnit.Key;
        }

        public void UpdateHostingUnit(HostingUnit hostingUnit)
        {
            if (XML.RecieveHostingUnit(hostingUnit.Key) == null)
                throw new MissingMemberException($"Hosting Unit {hostingUnit.Key} dosen't Exist");
            // Configuration.TotalPersons++;
            // hostingUnit.Key= Configuration
            XML.UpdateHostingUnit(hostingUnit);
        }

        public void DeleteHostingUnit(HostingUnit hostingUnit)
        {
            if (XML.RecieveHostingUnit(hostingUnit.Key) == null)
                throw new MissingMemberException($"Hosting unit  {hostingUnit.Key} dosen't Exist");
            // Configuration.TotalPersons--;
            XML.UpdateHostingUnit(hostingUnit);
        }

        public Person RecievePerson(int key)
        {
            DO.Person person = XML.RecievePerson(key);
            return person == null ? throw new MissingMemberException($"person {key}  dosen't Exist") : person;
        }

        public void AddPerson(Person person)
        {

            if (XML.RecievePerson(person.Id) != null)
                throw new DuplicateWaitObjectException($"Person {person.Id} Already Exist");
           // setConfig("TotalPersons", (person.Id + 1));
            XML.AddPerson(person);
        }

        public void DeletePerson(Person person)
        {
            if (XML.RecievePerson(person.Id) == null)
                throw new MissingMemberException($"Person  {person.Id}   dosen't Exist");
           
            XML.RemovePerson(person.Id);
        }
        public IEnumerable<DO.Person> GetPersons()
        {
            return XML.GetPersons();
        }
        public GuestRequest RecieveGuestRequest(int key)
        {
            DO.GuestRequest guestRequest = XML.RecieveGuestRequest(key);
            return guestRequest == null ? throw new MissingMemberException($"Guest request  {key}   dosen't Exist")
                : guestRequest;
        }

        public int AddGuesetRequest(GuestRequest request)//TODO
        {
            Config = XML.LoadConfig();
            if (XML.RecieveGuestRequest(request.GuestRequestKey) != null)
                throw new DuplicateWaitObjectException($"guestrequest {request.GuestRequestKey} Already Exist");
           
            request.GuestRequestKey =int.Parse((string)(Config["GuestRequestKey"] as ConfigData).Value);
            XML.AddGuestRequest(request);
            setConfig("GuestRequestKey", (request.GuestRequestKey + 1));
            return request.GuestRequestKey;
        }

        public void UpdateGusetRequest(GuestRequest request)
        {
            if (XML.RecieveGuestRequest(request.GuestRequestKey) == null)
                throw new MissingMemberException($"Guest request  {request.GuestRequestKey}  dosen't Exist");
           
            XML.UpdateGuestRequest(request);
        }

        public void UpdateGusetRequestStatus(GuestRequest request)
        {
            if (XML.RecieveGuestRequest(request.GuestRequestKey) == null)
                throw new MissingMemberException($"GuestRequest {request.GuestRequestKey} dosen't Exist");
            // Configuration.TotalPersons++;
            // hostingUnit.Key= Configuration
            XML.UpdateGuestRequest(request);
        }

        public Host RecieveHost(int key)
        {
            DO.Host host = XML.RecieveHost(key);
            return host == null ? throw new MissingMemberException($"host {key} dosen't exist") : host;
        }

        public void AddHost(Host host)
        {

            if (XML.RecieveHost(host.HostID) != null)
                throw new DuplicateWaitObjectException($"Person {host.HostID} Already Exist");
            // Configuration.TotalPersons++;
            XML.AddHost(host);
        }

        public void DeleteHost(Host host)
        {
            if (XML.RecieveHost(host.HostID) == null)
                throw new MissingMemberException($"Host  {host.HostID} dosen't Exist");
            // Configuration.TotalPersons--;
            XML.RemoveHost(host.HostID);//TODO
        }

        public void UpdateHost(Host host)
        {
            if (XML.RecieveHost(host.HostID) == null)
                throw new MissingMemberException($"Host {host.HostID} dosen't Exist");
            // Configuration.TotalPersons++;
            // hostingUnit.Key= Configuration
            XML.UpdateHost(host);
        }

        public Order RecieveOrder(int key)
        {
            DO.Order order = XML.RecieveOrder(key);
            return order == null ? throw new MissingMemberException($"Order {key} dosen't Exist") : order;
        }

        public int AddOrder(Order order)
        {
            Config = XML.LoadConfig();
            if (XML.RecieveOrder(order.Key) != null)
                throw new DuplicateWaitObjectException($"Person {order.Key} Already Exist");
            // Configuration.TotalPersons++;
            order.Key = int.Parse((string)(Config["OrderKey"] as ConfigData).Value);
            XML.AddOrder(order);
            setConfig("OrderKey", (order.Key + 1).ToString());
            return order.Key;

        }

        public void UpdateOrder(Order order)
        {
            if (XML.RecieveOrder(order.Key) == null)
                throw new MissingMemberException($"Order {order.Key} dosen't Exist");
            // Configuration.TotalPersons++;
            // hostingUnit.Key= Configuration
            XML.UpdateOrder(order);
        }

        public IEnumerable<HostingUnit> GetHostingUnitsList(Func<HostingUnit, bool> predicate)
        {
            HostingUnitXML hostingUnit = new HostingUnitXML();
            return XML.GetHostingunits().Select(x => x.ConvertToDO()).Where(predicate);
        }

        public IEnumerable<GuestRequest> GetGuestRequestsList()
        {
            try
            {
                return XML.GetGuestRequestList();
            }
            catch (KeyNotFoundException e)
            {

                throw e;
            }
            catch (Exception)
            {
                throw new Exception("אירעה שגיאה אחושילינג");
            }

        }

        public IEnumerable<Order> GetOrdersList(Func<Order, bool> predicate)
        {
            Order order = new Order();
            return XML.GetOrdersList().Where(predicate);
        }
        public IEnumerable<BankBranch> GetBankBranchesList()
        {

            BankBranch bank = new BankBranch();
            return XML.GetBankBranches();
        }

        public IEnumerable<Host> GetHostsList()
        {
            Host host = new Host();
            return XML.GetAllHosts();
        }

        public int GetCommissionRate()
        {
            Config = XML.LoadConfig();
            return int.Parse((string)(Config["Commission"] as ConfigData).Value);
        }

        public int GetCommissionFromOrder(Order order)
        {
            var orderr = XML.RecieveOrder(order.Key);
            return orderr != null ? orderr.Commission : -1;

        }

        public BankBranch RecieveBankBranch(int branchNumber)
        {
            BankBranch bankBranch = XML.RecieveBankBranch(branchNumber);
            return bankBranch == null ? throw new MissingMemberException($"Bank Branch {branchNumber} dosen't Exist") : bankBranch;
        }

        public void UpdatePerson(Person person)
        {
            if (XML.RecievePerson(person.Id) == null)
                throw new MissingMemberException($"person {person.Id} dosen't exist");
            XML.UpdatePerson(person);
        }

       //public Dictionary<string, object> getConfig()
       // {
       //     throw new NotImplementedException();
       // }
    }
}
