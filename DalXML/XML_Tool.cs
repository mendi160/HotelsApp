using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using DalApi;
using DO;

namespace Dal
{
    class XML_Tool
    {
        Dictionary<Type, string> pathDictionary;
        XElement PersonsRoot;
        XElement HostRoot;
        XElement GuestRequestRoot;
        XElement OrderRoot;
        XElement HostingUnitRoot;
        XElement ConfigRoot;
        XElement BankRoot;
        XElement BankBranchRoot;
        const string PersonPath = @"XML/Person.xml";
        const string HostPath = @"XML/Host.xml";
        const string GuestRequestPath = @"XML/GuestRequest.xml";
        const string OrderPath = @"XML/Order.xml";
        const string HostingUnitPath = @"XML/HostingUnit.xml";
        const string ConfigurationPath = @"XML/Configuration.xml";
        const string BankPath = @"XML/Bank.xml";
        const string BankBranchPath = @"XML/BankBranch.xml";
          const string Cities = @"XML/CitiesList.xml";

          public XML_Tool()
        {
            InitializeDictionary();
            try
            {
                LoadData();
            }
            catch
            {

                throw new Exception("שגיאה בטעינת הנתונים מקבצי XML ");
            }
            Thread UpdateBank = new Thread(UpdateBankBranch);
               Thread cities = new Thread(() =>
               {
                    using (System.Net.WebClient wc = new System.Net.WebClient())
                    {
                         try
                         {
                              string xmlServerPath =
                             @"https://data.gov.il/dataset/3fc54b81-25b3-4ac7-87db-248c3e1602de/resource/a68209f0-8b97-47b1-a242-690fba685c48/download/yeshuvim_20190401.xml";
                              wc.DownloadFile(xmlServerPath, Cities);
                         }
                         catch (Exception)
                         {
                              Thread.Sleep(2000);
                              string xmlServerPath = @"https://data.gov.il/dataset/3fc54b81-25b3-4ac7-87db-248c3e1602de/resource/a68209f0-8b97-47b1-a242-690fba685c48/download/yeshuvim_20190401.xml";
                              wc.DownloadFile(xmlServerPath, Cities);

                         }
                    }

                    XElement Newciti = XElement.Load(Cities);
                    Newciti = new XElement("Cities", from item in Newciti.Elements()
                                                     select new XElement("city", item.Element("שם_ישוב").Value));
                    Newciti.AddFirst(new XElement("city", "אופציונאלי"));
                    Newciti.Save(Cities);
                                                        
               }
               );
               cities.Start();

            UpdateBank.Start();//downloading the banks files    
        }
        void LoadData()
        {
            try
            {
                PersonsRoot = XElement.Load(PersonPath);
            }
            catch (Exception)
            {
                PersonsRoot = new XElement("Persons");
                PersonsRoot.Save(PersonPath);
            }
            try
            {
                BankBranchRoot = XElement.Load(BankBranchPath);
            }
            catch (Exception)
            {
                BankBranchRoot = new XElement("BankBranches");
                BankBranchRoot.Save(BankBranchPath);
            }
            try
            {
                GuestRequestRoot = XElement.Load(GuestRequestPath);
            }
            catch (Exception)
            {
                GuestRequestRoot = new XElement("GuestRequests");
                GuestRequestRoot.Save(GuestRequestPath);
            }
            try
            {
                HostRoot = XElement.Load(HostPath);
            }
            catch (Exception)
            {
                HostRoot = new XElement("Hosts");
                HostRoot.Save(HostPath);
            }
            try
            {
                OrderRoot = XElement.Load(OrderPath);
            }
            catch (Exception)
            {
                OrderRoot = new XElement("Orders");
                OrderRoot.Save(OrderPath);
            }
            try
            {
                HostingUnitRoot = XElement.Load(HostingUnitPath);
            }
            catch (Exception)
            {
                HostingUnitRoot = new XElement("HostingUnits");
                HostingUnitRoot.Save(HostingUnitPath);
            }
            try
            {


                ConfigRoot = XElement.Load(ConfigurationPath);


            }
            catch (Exception)
            {
                ConfigRoot= new XElement("ConfigRoot");
                Dictionary<string, ConfigData> cfg = new Dictionary<string, ConfigData>();
                cfg.Add("LastUpdate", new ConfigData() { Value = DateTime.Today, Access = ConfigDataAccess.READABLE });
                cfg.Add("HostingUnitKey", new ConfigData() { Value = 10000000, Access = ConfigDataAccess.READABLE });
                cfg.Add("OrderKey", new ConfigData() { Value = 20000000, Access = ConfigDataAccess.READABLE });
                cfg.Add("GuestRequestKey", new ConfigData() { Value = 30000000, Access = ConfigDataAccess.READABLE });
                cfg.Add("Commission", new ConfigData() { Value = 10, Access = ConfigDataAccess.WRITEABLE });
                cfg.Add("IgnoreTime", new ConfigData() { Value = 10, Access = ConfigDataAccess.READABLE });
                cfg.Add("Manager", new ConfigData() { Value = 123123122, Access = ConfigDataAccess.READABLE });

                ConfigRoot.Add(from item in cfg
                               select new XElement("cfg", new XElement("Name", item.Key), new XElement("Value", (item.Value as ConfigData).Value), new XElement("Access", (item.Value as ConfigData).Access)));

                ConfigRoot.Save(ConfigurationPath);
            }
            try
            {
                BankRoot = XElement.Load(BankPath);
            }
            catch (Exception)
            {
                BankRoot = new XElement("Banks");
                BankRoot.Save(BankPath);
            }

        }
        #region person
        public void AddPerson(Person person)
        {
            XElement personID = new XElement("Id", person.Id);
            XElement password = new XElement("Password", person.Password);
            XElement personEmail = new XElement("MailAddress", person.MailAddress);
            XElement firstName = new XElement("FirstName", person.FirstName);
            XElement lastName = new XElement("LastName", person.LastName);
            XElement phoneNumber = new XElement("PhoneNumber", person.PhoneNumber);
            XElement status = new XElement("Status", person.Status);
            XElement idType = new XElement("IdType", person.IdType);
            XElement per = new XElement("Person", personID, password, personEmail, firstName, lastName, phoneNumber, status, idType);
            PersonsRoot.Add(per);
            PersonsRoot.Save(PersonPath);
        }
        public void UpdatePerson(Person person)
        {
            RemovePerson(person.Id);
            AddPerson(person);
        }
        public void RemovePerson(int key)
        {
            PersonsRoot = XElement.Load(PersonPath);
            XElement Temp;
            try
            {
                Temp = (from person in PersonsRoot.Elements()
                        where int.Parse(person.Element("Id").Value) == key
                        select person).FirstOrDefault();
            }
            catch (Exception)
            {
                throw new Exception("הייתה תקלה  במחיקת ישות מסוג אדם");

            }
            Temp.Remove();
            PersonsRoot.Save(PersonPath);
        }
        public DO.Person RecievePerson(int personId)
        {
            PersonsRoot = XElement.Load(PersonPath);
            DO.Person person = new Person();
            try
            {
                person = (from p in PersonsRoot.Elements()
                          where int.Parse(p.Element("Id").Value) == personId

                          select new Person()
                          {
                              Id = int.Parse(p.Element("Id").Value),
                              Password = p.Element("Password").Value,
                              MailAddress = p.Element("MailAddress").Value,
                              FirstName = p.Element("FirstName").Value,
                              LastName = p.Element("LastName").Value,
                              IdType = (DO.ID)Enum.Parse(typeof(DO.ID), p.Element("IdType").Value),
                              PhoneNumber = int.Parse(p.Element("PhoneNumber").Value),
                              Status = (DO.Status)Enum.Parse(typeof(DO.Status), p.Element("Status").Value)
                          }).FirstOrDefault();

            }
            catch
            {
                person = null;
            }

            return person;
        }
        #endregion
        #region host
        public void AddHost(Host host)
        {
            XElement BankAccntNum = new XElement("BankAccuontNumber", host.BankAccuontNumber);
            XElement BranchNumber = new XElement("BranchNumber", host.BranchNumber);
            XElement Website = new XElement("Website", host.Website);
            XElement BankNumber = new XElement("BankNumber", host.BankNumber);
            XElement HostId = new XElement("HostID", host.HostID);
            XElement Collection = new XElement("CollectionClearance", host.CollectionClearance);
            XElement Status = new XElement("Status", host.Status);
            XElement TotalComission = new XElement("TotalCommission", host.TotalCommission);
            HostRoot.Add(new XElement("Host", HostId, Status, BankNumber, BankAccntNum, BranchNumber, Website, Collection, TotalComission));
            HostRoot.Save(HostPath);

        }
        public void RemoveHost(int id)
        {
            HostRoot = XElement.Load(HostPath);
            XElement Temp;
            try
            {

                Temp = (from host in HostRoot.Elements()
                        where int.Parse(host.Element("HostID").Value) == id
                        select host).FirstOrDefault();
            }
            catch (Exception)
            {
                throw new Exception("הייתה תקלה  במחיקת ישות מסוג מארח");

            }
            Temp.Remove();
            HostRoot.Save(HostPath);
        }
        public void UpdateHost(Host host)
        {
            RemoveHost(host.HostID);
            AddHost(host);
        }
        public Host RecieveHost(int hostId)
        {
            HostRoot = XElement.Load(HostPath);
            try
            {
                return (from host in HostRoot.Elements()
                        where int.Parse(host.Element("HostID").Value) == hostId
                        select new Host()
                        {
                            BankAccuontNumber = int.Parse(host.Element("BankAccuontNumber").Value),
                            BankNumber = int.Parse(host.Element("BankNumber").Value),
                            BranchNumber = int.Parse(host.Element("BranchNumber").Value),
                            CollectionClearance = bool.Parse(host.Element("CollectionClearance").Value),
                            HostID = int.Parse(host.Element("HostID").Value),
                            Status = (Status)Enum.Parse(typeof(Status), host.Element("Status").Value),
                            TotalCommission = int.Parse(host.Element("TotalCommission").Value),
                            Website = host.Element("Website").Value
                        }).FirstOrDefault();

            }
            catch (ArgumentNullException)
            {

                throw new Exception($"המארח{hostId} לא נמצא ");
            }
            catch
            {
                throw new Exception($"שגיאה בטעינת מארח");
            }
        }
        public List<Host> GetAllHosts()
        {
            HostRoot = XElement.Load(HostPath);
            List<Host> hosts;
            try
            {
                hosts = (from host in HostRoot.Elements()
                                 select new Host()
                                 {
                                     BankAccuontNumber = int.Parse(host.Element("BankAccuontNumber").Value),
                                     BankNumber = int.Parse(host.Element("BankNumber").Value),
                                     BranchNumber = int.Parse(host.Element("BranchNumber").Value),
                                     CollectionClearance = bool.Parse(host.Element("CollectionClearance").Value),
                                     HostID = int.Parse(host.Element("HostID").Value),
                                     Status = (Status)Enum.Parse(typeof(Status), host.Element("Status").Value),
                                     TotalCommission = int.Parse(host.Element("TotalCommission").Value),
                                     Website = host.Element("Website").Value,
                                 }).ToList();
            }
            catch (Exception)
            {

                throw new KeyNotFoundException("לא ניתן לפתוח את הרשימה (HostList)");
            }
            return hosts;
        }
        public List<Person> GetPersons()
        {
            PersonsRoot = XElement.Load(PersonPath);
            List<Person> person;
            try
            {
                person = (from p in PersonsRoot.Elements()
                          select new Person()
                          {
                              Id = int.Parse(p.Element("Id").Value),
                              Password = p.Element("Password").Value,
                              MailAddress = p.Element("MailAddress").Value,
                              FirstName = p.Element("FirstName").Value,
                              LastName = p.Element("LastName").Value,
                              IdType = (DO.ID)Enum.Parse(typeof(DO.ID), p.Element("IdType").Value),
                              PhoneNumber = int.Parse(p.Element("PhoneNumber").Value),
                              Status = (DO.Status)Enum.Parse(typeof(DO.Status), p.Element("Status").Value)
                          }).ToList();
            }
            catch
            {
                throw new KeyNotFoundException("לא ניתן לפתוח את הרשימה (PersonList)");
            }
            return person;
        }
        #endregion
        #region guest request
        public void AddGuestRequest(GuestRequest guestRequest)
        {
            XElement GuestRequestKey = new XElement("GuestRequestKey", guestRequest.GuestRequestKey);
            XElement type = new XElement("Type", guestRequest.Type);
            XElement Area = new XElement("Area", guestRequest.Area);
            XElement SubArea = new XElement("SubArea", guestRequest.SubArea);
            XElement Status = new XElement("Status", guestRequest.Status);
            XElement Jacuzzi = new XElement("Jacuzzi", guestRequest.Jacuzzi);
            XElement Pool = new XElement("Pool", guestRequest.Pool);
            XElement RegistrationDate = new XElement("RegistrationDate", guestRequest.RegistrationDate);
            XElement EntryDate = new XElement("EntryDate", guestRequest.EntryDate);
            XElement ReleaseDate = new XElement("ReleaseDate", guestRequest.ReleaseDate);
            XElement Adults = new XElement("Adults", guestRequest.Adults);
            XElement children = new XElement("children", guestRequest.children);
            XElement ChildrensAttractions = new XElement("ChildrensAttractions", guestRequest.ChildrensAttractions);
            XElement ClientID = new XElement("ClientID", guestRequest.ClientID);
            XElement Garden = new XElement("Garden", guestRequest.Garden);
            XElement GR = new XElement("GuestRequest", GuestRequestKey, type, Area, SubArea, Status, Jacuzzi, Pool, RegistrationDate, EntryDate, ReleaseDate, Adults, children, ChildrensAttractions, ClientID, Garden);
            GuestRequestRoot.Add(GR);
            GuestRequestRoot.Save(GuestRequestPath);
        }
        public void RemoveGuestRequest(int key)
        {
            GuestRequestRoot = XElement.Load(GuestRequestPath);
            XElement Temp;
            try
            {

                Temp = (from guest in GuestRequestRoot.Elements()
                        where int.Parse(guest.Element("GuestRequestKey").Value) == key
                        select guest).FirstOrDefault();
            }
            catch (Exception)
            {
                throw new Exception("הייתה תקלה  במחיקת ישות מסוג דרישת לקוח");

            }
            Temp.Remove();
            GuestRequestRoot.Save(GuestRequestPath);
        }
        public void UpdateGuestRequest(GuestRequest guestRequest)
        {
            RemoveGuestRequest(guestRequest.GuestRequestKey);
            AddGuestRequest(guestRequest);
        }
        public GuestRequest RecieveGuestRequest(int key)
        {
            GuestRequestRoot = XElement.Load(GuestRequestPath);
            try
            {
                return (from item in GuestRequestRoot.Elements()
                        where int.Parse(item.Element("GuestRequestKey").Value) == key
                        select new GuestRequest()
                        {
                            Adults = int.Parse(item.Element("Adults").Value),
                            Area = (Location)Enum.Parse(typeof(Location), item.Element("Area").Value),
                            ChildrensAttractions = (Preferences)Enum.Parse(typeof(Preferences), item.Element("ChildrensAttractions").Value),
                            SubArea = item.Element("SubArea").Value,
                            children = int.Parse(item.Element("children").Value),
                            ClientID = int.Parse(item.Element("ClientID").Value),
                            EntryDate = DateTime.Parse(item.Element("EntryDate").Value),
                            Garden = (Preferences)Enum.Parse(typeof(Preferences), item.Element("Garden").Value),
                            GuestRequestKey = int.Parse(item.Element("GuestRequestKey").Value),
                            Jacuzzi = (Preferences)Enum.Parse(typeof(Preferences), item.Element("Jacuzzi").Value),
                            Pool = (Preferences)Enum.Parse(typeof(Preferences), item.Element("Pool").Value),
                            RegistrationDate = DateTime.Parse(item.Element("RegistrationDate").Value),
                            ReleaseDate = DateTime.Parse(item.Element("ReleaseDate").Value),
                            Status = (Request_Statut)Enum.Parse(typeof(Request_Statut), item.Element("Status").Value),
                            Type = (Hosting_Type)Enum.Parse(typeof(Hosting_Type), item.Element("Type").Value),


                        }).FirstOrDefault();
            }
            catch (Exception)
            {

                throw new Exception($"הייתה שגיאה בטעינת בקשה מספר {key}");
            }
        }

        #endregion
        #region order
        public void AddOrder(Order order)
        {
            XElement Key = new XElement("Key", order.Key);
            XElement HostingUnitKey = new XElement("HostingUnitKey", order.HostingUnitKey);
            XElement Status = new XElement("Status", order.Status);
            XElement OrderDate = new XElement("OrderDate", order.OrderDate);
            XElement SentDate = new XElement("SentDate", order.SentDate);
            XElement CloseDate = new XElement("CloseDate", order.CloseDate);
            XElement HostID = new XElement("HostID", order.HostID);
            XElement CliendID = new XElement("CliendID", order.CliendID);
            XElement Commission = new XElement("Commission", order.Commission);
            XElement GuestRequestKey = new XElement("GuestRequestKey", order.GuestRequestKey);
            XElement TotalCost = new XElement("TotalCost", order.TotalCost);
            XElement o = new XElement("Order", Key, Commission, HostingUnitKey, Status, OrderDate, SentDate, CloseDate, HostID, CliendID, GuestRequestKey, TotalCost);
            OrderRoot.Add(o);
            OrderRoot.Save(OrderPath);
        }
        public void RemoveOrder(int key)
        {

            OrderRoot = XElement.Load(OrderPath);
            XElement Temp;
            try
            {

                Temp = (from order in OrderRoot.Elements()
                        where int.Parse(order.Element("Key").Value) == key
                        select order).FirstOrDefault();
            }
            catch (Exception)
            {
                throw new Exception("הייתה תקלה  במחיקת ישות מסוג הזמנה");

            }
            Temp.Remove();
            OrderRoot.Save(OrderPath);
        }
        public void UpdateOrder(Order order)
        {
            RemoveOrder(order.Key);
            AddOrder(order);
        }
        public Order RecieveOrder(int key)
        {
            OrderRoot = XElement.Load(OrderPath);
            DO.Order order = new Order();
            try
            {
                order = (from o in OrderRoot.Elements()
                         where int.Parse(o.Element("Key").Value) == key
                         select new Order()
                         {
                             Key = int.Parse(o.Element("Key").Value),
                             CliendID = int.Parse(o.Element("CliendID").Value),
                             HostID = int.Parse(o.Element("HostID").Value),
                             HostingUnitKey = int.Parse(o.Element("HostingUnitKey").Value),
                             GuestRequestKey = int.Parse(o.Element("GuestRequestKey").Value),
                             Commission = int.Parse(o.Element("Commission").Value),
                             TotalCost = int.Parse(o.Element("TotalCost").Value),
                             OrderDate = DateTime.Parse(o.Element("OrderDate").Value),
                             SentDate = DateTime.Parse(o.Element("SentDate").Value),
                             CloseDate = DateTime.Parse(o.Element("CloseDate").Value),
                             Status = (DO.Order_Status)Enum.Parse(typeof(DO.Order_Status), o.Element("Status").Value),
                         }).FirstOrDefault();
            }
            catch
            {
                order = null;
            }

            return order;
        }
        public List<Order> GetOrdersList()
        {
            OrderRoot = XElement.Load(OrderPath);
            List<Order> orders;

            try
            {
                orders = (from ord in OrderRoot.Elements()
                          select new Order()
                          {
                              Key = int.Parse(ord.Element("Key").Value),
                              CliendID = int.Parse(ord.Element("CliendID").Value),
                              HostID = int.Parse(ord.Element("HostID").Value),
                              HostingUnitKey = int.Parse(ord.Element("HostingUnitKey").Value),
                              GuestRequestKey = int.Parse(ord.Element("GuestRequestKey").Value),
                              Commission = int.Parse(ord.Element("Commission").Value),
                              TotalCost = int.Parse(ord.Element("TotalCost").Value),
                              OrderDate = DateTime.Parse(ord.Element("OrderDate").Value),
                              SentDate = DateTime.Parse(ord.Element("SentDate").Value),
                              CloseDate = DateTime.Parse(ord.Element("CloseDate").Value),
                              Status = (DO.Order_Status)Enum.Parse(typeof(DO.Order_Status), ord.Element("Status").Value),
                          }).ToList();

            }
            catch (Exception)
            {

                throw;
            }

            return orders;


        }

        #endregion
        #region bank
        void UpdateBankBranch()
        {
            BankBranchRoot = XElement.Load(BankBranchPath);
            try
            {
                DateTime LastUpdate = DateTime.Parse(BankBranchRoot.Element("LastUpdate").Value);
                if (!((DateTime.Today - LastUpdate).Days > 7))
                {
                    return;
                }
            }
            catch { }
            using (System.Net.WebClient wc = new System.Net.WebClient())
            {
                try
                {
                    string xmlServerPath =
                   @"http://www.jct.ac.il/~coshri/atm.xml";
                    wc.DownloadFile(xmlServerPath, BankPath);
                }
                catch (Exception)
                {
                    string xmlServerPath = @"http://www.jct.ac.il/~coshri/atm.xml";
                    wc.DownloadFile(xmlServerPath, BankPath);
                        
                }
                    BankRoot = XElement.Load(BankPath);
                    BankBranchRoot = new XElement("BankBranches", from bank in BankRoot.Elements()
                                                              select new
                                                              XElement("BankBranch",
                                                              new XElement("BankName", bank.Element("שם_בנק").Value)
                                                              ,
                                                              new XElement("BankNumber", bank.Element("קוד_בנק").Value)
                                                              , new XElement("BranchAddress", bank.Element("כתובת_ה-ATM").Value),
                                                              new XElement("BranchCity", bank.Element("ישוב").Value),
                                                               new XElement("BranchNumber", bank.Element("קוד_סניף").Value))


                                                             );
                BankBranchRoot.Add(new XElement("LastUpdate", DateTime.Today));
                BankBranchRoot.Save(BankBranchPath);

            }
        }

        public BankBranch RecieveBankBranch(int branchNumber)
        {
            BankBranch bbranch = new BankBranch();
            bbranch = (from bank in BankBranchRoot.Elements()
                       where int.Parse(bank.Element("BranchNumber").Value) == branchNumber
                       select new BankBranch()
                       {
                           BankName = bank.Element("BankName").Value,
                           BranchAddress = bank.Element("BranchAddress").Value,
                           BankNumber = int.Parse(bank.Element("BankNumber").Value),
                           BranchCity = bank.Element("BranchCity").Value,
                           BranchNumber = int.Parse(bank.Element("BranchNumber").Value),

                       }).FirstOrDefault();

            return bbranch == null ? null : bbranch;
        }
        public void DeleteBankBranch(int branchNumber)
        {
            List<BankBranch> bankBranches = LoadFromXML<List<BankBranch>>(BankBranchPath);
            bankBranches.RemoveAll(x => x.BranchNumber == branchNumber);
            SaveToXML<List<BankBranch>>(bankBranches, BankBranchPath);


        }
        public void AddBankBranch(BankBranch bankBranch)
        {
            List<BankBranch> bankBranches = LoadFromXML<List<BankBranch>>(BankBranchPath);
            bankBranches.Add(bankBranch);
            SaveToXML<List<BankBranch>>(bankBranches, BankBranchPath);

        }
        public List<BankBranch> GetBankBranches()
        {
            BankBranchRoot = XElement.Load(BankPath);
            List<BankBranch> banks;

            try
            {
                banks = (from bank in BankBranchRoot.Elements()
                         select new BankBranch()
                         {
                             BankName = bank.Element("BankName").Value,
                             BranchAddress = bank.Element("BranchAddress").Value,
                             BankNumber = int.Parse(bank.Element("BankNumber").Value),
                             BranchCity = bank.Element("BranchCity").Value,
                             BranchNumber = int.Parse(bank.Element("BranchNumber").Value),

                         }).ToList();

            }
            catch (Exception)
            {

                throw;
            }
            return banks;
        }

        public Dictionary<int, string> GetBanksNumbers()
        {

            return (from p in BankRoot.Elements()
                    select new { code = int.Parse(p.Element("קוד_בנק").Value), name = p.Element("שם_בנק").Value }
                     into g
                    group g by g.code).ToDictionary(h => h.Key, h => h.ElementAt(0).name);

        }
        public Dictionary<int, string> GetBanksBranches()
        {
            return (from p in BankRoot.Elements()
                    select new { number = int.Parse(p.Element("קוד_סניף").Value), address = p.Element("כתובת_ה-ATM").Value, City = p.Element("ישוב").Value }
                       into g
                    group g by g.number).ToDictionary(h => h.Key, h => h.ElementAt(0).address + "@" + h.ElementAt(0).City);


        }
        #endregion
        #region Hostingunit
        public void AddHostingUnit(HostingUnit hosting)
        {
            HostingUnitXML hostingUnit = hosting.ConvertToHostingUnitXML();
            XElement area = new XElement("Area", hostingUnit.Area);
            XElement diary = new XElement("Diary");
            foreach (var item in hostingUnit.Diary_XML)
            {
                diary.Add(new XElement("bool", item));
            }
            XElement hostingunitname = new XElement("HostingUnitName", hostingUnit.HostingUnitName);
            XElement link = new XElement("ImageLink1", hostingUnit.ImageLink1);
            XElement link2 = new XElement("ImageLink2", hostingUnit.ImageLink2);
            XElement link3 = new XElement("ImageLink3", hostingUnit.ImageLink3);
            XElement jacuzzi = new XElement("Jacuzzi", hostingUnit.Jacuzzi);
            XElement key = new XElement("Key", hostingUnit.Key);
            XElement kitchen = new XElement("Kitchen", hostingUnit.Kitchen);
            XElement wifi = new XElement("WIFI", hostingUnit.WIFI);
            XElement pricepernight = new XElement("PricePerNight", hostingUnit.PricePerNight);
            XElement owner = new XElement("Owner", hostingUnit.Owner);
            XElement status = new XElement("Status", hostingUnit.Status);
            XElement swimmingpool = new XElement("SwimmingPool", hostingUnit.SwimmingPool);
            XElement tv = new XElement("TV", hostingUnit.TV);
            XElement HU = new XElement("HostingUnit", key, hostingunitname, area, link, link2, link3, jacuzzi, diary, kitchen, wifi, pricepernight, owner, status, swimmingpool, tv);
            HostingUnitRoot.Add(HU);
            HostingUnitRoot.Save(HostingUnitPath);

        }
        public void RemoveHostingUnit(int key)
        {
            HostingUnitRoot = XElement.Load(HostingUnitPath);
            XElement Temp;
            try
            {

                Temp = (from HU in HostingUnitRoot.Elements()
                        where int.Parse(HU.Element("Key").Value) == key
                        select HU).FirstOrDefault();
            }
            catch (Exception)
            {
                throw new Exception("הייתה תקלה  במחיקת ישות מסוג יחידת אירוח");

            }
            Temp.Remove();
            HostingUnitRoot.Save(HostingUnitPath);
        }
        public HostingUnit RecieveHostingUnit(int key)
        {
            HostingUnitRoot = XElement.Load(HostingUnitPath);
            HostingUnitXML hostingUnitXML = new HostingUnitXML();
            try
            {
                hostingUnitXML = (from HU in HostingUnitRoot.Elements()
                                  where int.Parse(HU.Element("Key").Value) == key
                                  select new HostingUnitXML()
                                  {
                                      Key = int.Parse(HU.Element("Key").Value),
                                      Owner = int.Parse(HU.Element("Owner").Value),
                                      PricePerNight = int.Parse(HU.Element("PricePerNight").Value),
                                      HostingUnitName = HU.Element("HostingUnitName").Value,
                                      Area = (DO.Location)Enum.Parse(typeof(DO.Location), HU.Element("Area").Value),
                                      Diary_XML = (from boolean in HU.Element("Diary").Elements()
                                                   select bool.Parse(boolean.Value)).ToArray(),
                                      Status = (DO.Status)Enum.Parse(typeof(DO.Status), HU.Element("Status").Value),
                                      WIFI = bool.Parse(HU.Element("WIFI").Value),
                                      SwimmingPool = bool.Parse(HU.Element("SwimmingPool").Value),
                                      Kitchen = bool.Parse(HU.Element("Kitchen").Value),
                                      TV = bool.Parse(HU.Element("TV").Value),
                                      Jacuzzi = bool.Parse(HU.Element("Jacuzzi").Value),
                                      ImageLink1 = HU.Element("ImageLink1").Value,
                                      ImageLink2 = HU.Element("ImageLink2").Value,
                                      ImageLink3 = HU.Element("ImageLink3").Value
                                  }).FirstOrDefault();
            }
            catch
            {
                return null;
            }
            return hostingUnitXML == null ? null : hostingUnitXML.ConvertToDO();
        }
        public void UpdateHostingUnit(HostingUnit hostingUnit)
        {
            RemoveHostingUnit(hostingUnit.Key);
            AddHostingUnit(hostingUnit);
        }
        public List<HostingUnitXML> GetHostingunits()
        {
            HostingUnitRoot = XElement.Load(HostingUnitPath);
            List<HostingUnitXML> hostingunits;

            try
            {
                hostingunits = (from HU in HostingUnitRoot.Elements()
                                select new HostingUnitXML()
                                {
                                    Key = int.Parse(HU.Element("Key").Value),
                                    Owner = int.Parse(HU.Element("Owner").Value),
                                    PricePerNight = int.Parse(HU.Element("PricePerNight").Value),
                                    HostingUnitName = HU.Element("HostingUnitName").Value,
                                    Area = (DO.Location)Enum.Parse(typeof(DO.Location), HU.Element("Area").Value),
                                    Diary_XML = (from boolean in HU.Element("Diary").Elements()
                                                 select bool.Parse(boolean.Value)).ToArray(),
                                    Status = (DO.Status)Enum.Parse(typeof(DO.Status), HU.Element("Status").Value),
                                    WIFI = bool.Parse(HU.Element("WIFI").Value),
                                    SwimmingPool = bool.Parse(HU.Element("SwimmingPool").Value),
                                    Kitchen = bool.Parse(HU.Element("Kitchen").Value),
                                    TV = bool.Parse(HU.Element("TV").Value),
                                    Jacuzzi = bool.Parse(HU.Element("Jacuzzi").Value),
                                    ImageLink1 = HU.Element("ImageLink1").Value,
                                    ImageLink2 = HU.Element("ImageLink2").Value,
                                    ImageLink3 = HU.Element("ImageLink3").Value
                                }).ToList();

            }
            catch (Exception)
            {

                throw;
            }

            return hostingunits;


        }
        #endregion
        public static void SaveToXML<T>(T source, string path)
        {
            FileStream file;
            try
            {
                file = new FileStream(path, FileMode.Open);
            }
            catch
            {
                file = new FileStream(path, FileMode.Create);
            }
            XmlSerializer xmlSerializer = new XmlSerializer(source.GetType());
            xmlSerializer.Serialize(file, source);
            file.Close();
        }
        public static T LoadFromXML<T>(string path) where T : new()
        {
            FileStream file;
            try
            {
                file = new FileStream(path, FileMode.Open);
            }
            catch
            {
                throw new Exception($"היתה שגיאה בטעינת הקובץ של: {path}");
            }
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            T result = new T();
            try
            {
                result = (T)xmlSerializer.Deserialize(file);
            }
            catch (Exception)
            {
                //hell yeah
            }
            file.Close();
            return result;
        }
        public Dictionary<string, object> LoadConfig()
        {
            return (from item in ConfigRoot.Elements()
                    select (item)).ToDictionary(x => x.Element("Name").Value, x =>(object) new ConfigData { Value = x.Element("Value").Value, Access =(ConfigDataAccess) Enum.Parse(typeof(ConfigDataAccess),x.Element("Access").Value )});
                                              //{Key=item.Element("name").Value , Value = new ConfigData() { Value = item.Element("Key").Value, Access = (ConfigDataAccess)Enum.Parse(typeof(ConfigDataAccess), item.Element("Access").Value) } }).ToDictionary(x=>x.Key,x=>(object)x.Value);
        }
        public void SaveConfig(Dictionary<string, object> cfg)
        {
            ConfigRoot=new XElement("Config",(from item in cfg
                           select new XElement("cfg", new XElement("Name", item.Key), new XElement("Value", (item.Value as ConfigData).Value), new XElement("Access", (item.Value  as ConfigData).Access))));
            ConfigRoot.Save(ConfigurationPath);
        }

        public List<T> GetList<T>(T t)
        {
            string path = pathDictionary[typeof(T)];
            return (List<T>)LoadFromXML<List<T>>(path);
        }
        public List<GuestRequest> GetGuestRequestList()
        {
            List<GuestRequest> guestRequests;
            GuestRequestRoot = XElement.Load(GuestRequestPath);
            try
            {
                guestRequests = (from item in GuestRequestRoot.Elements()
                                 select new GuestRequest()
                                 {
                                     Adults = int.Parse(item.Element("Adults").Value),
                                     Area = (Location)Enum.Parse(typeof(Location), item.Element("Area").Value),
                                     ChildrensAttractions = (Preferences)Enum.Parse(typeof(Preferences), item.Element("ChildrensAttractions").Value),
                                     SubArea = item.Element("SubArea").Value,
                                     children = int.Parse(item.Element("children").Value),
                                     ClientID = int.Parse(item.Element("ClientID").Value),
                                     EntryDate = DateTime.Parse(item.Element("EntryDate").Value),
                                     Garden = (Preferences)Enum.Parse(typeof(Preferences), item.Element("Garden").Value),
                                     GuestRequestKey = int.Parse(item.Element("GuestRequestKey").Value),
                                     Jacuzzi = (Preferences)Enum.Parse(typeof(Preferences), item.Element("Jacuzzi").Value),
                                     Pool = (Preferences)Enum.Parse(typeof(Preferences), item.Element("Pool").Value),
                                     RegistrationDate = DateTime.Parse(item.Element("RegistrationDate").Value),
                                     ReleaseDate = DateTime.Parse(item.Element("ReleaseDate").Value),
                                     Status = (Request_Statut)Enum.Parse(typeof(Request_Statut), item.Element("Status").Value),
                                     Type = (Hosting_Type)Enum.Parse(typeof(Hosting_Type), item.Element("Type").Value),
                                 }).ToList();


            }
            catch (Exception)
            {

                throw new KeyNotFoundException("לא ניתן לפתוח את הרשימה (GuestRequestList)");
            }

            return guestRequests;
        }
        void InitializeDictionary()
        {
            pathDictionary = new Dictionary<Type, string>();
            pathDictionary.Add(typeof(Person), PersonPath);
            pathDictionary.Add(typeof(Host), HostPath);
            pathDictionary.Add(typeof(HostingUnitXML), HostingUnitPath);
            pathDictionary.Add(typeof(BankBranch), BankBranchPath);
            pathDictionary.Add(typeof(GuestRequest), GuestRequestPath);
            pathDictionary.Add(typeof(Order), OrderPath);
        }

    }

}

