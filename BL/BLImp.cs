using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using DO;
using BO;
using System.ComponentModel;
using System.Net.Mail;
using System.IO;
using System.Threading;
using System.Reflection;

namespace BL
{
    class BLImp : IBl
    {
        readonly DalApi.IDal dal = DalApi.DalFactory.GetDal();

        Dictionary<string, int> Config;

        #region Add to DS
        //all adding functions check if its exist or no
        //so we dont check here
        public void AddHost(BO.Host host)
        {
            dal.AddHost(Conversions.CastingToDOHost(host));

        }
        public void AddPerson(BO.Person person)
        {
            dal.AddPerson(Conversions.CastingToDOPerson(person));
        }
        public int AddHostingUnit(BO.HostingUnit hostingUnit)//
        {
            return dal.AddHostingUnit(Conversions.CastingToDOHostingUnit(hostingUnit));
        }
        public int AddGuestRequest(BO.GuestRequest guestRequest)//
        {

            if (CheckDate(guestRequest))
            {
                return dal.AddGuesetRequest(Conversions.CastingToDOGuestRequest(guestRequest));
            }
            return 0;
        }
        public Dictionary<string, int> GetConfig()
        {
            loadCfg();
            return Config;
        }

        public int AddOrder(BO.Order order)//
        {
            return dal.AddOrder(Conversions.CastingToDO_Order(order));
        }
        #endregion
        #region Delete from DS
        public void DeleteHostingUnit(BO.HostingUnit hostingUnit)//
        {
            if (DeleteableHostingUnit(hostingUnit))
                dal.DeleteHostingUnit(Conversions.CastingToDOHostingUnit(hostingUnit));
            else
                throw new InvalidOperationException("It is impossible to delete a hosting unit that has open orders");
        }
        public void DeleteHost(BO.Host host)
        {
            
               dal.DeleteHost(Conversions.CastingToDOHost(host));
        }
        #endregion
        #region Update DS
        public void UpdatePerson(BO.Person person)
        {
            dal.UpdatePerson(Conversions.CastingToDOPerson(person));
        }
        public void UpdateGusetRequest(BO.GuestRequest request)
        {

            if (NumberOfInvitationsSent(request) == 0)
            {
                if (CheckDate(request))
                    dal.UpdateGusetRequest(Conversions.CastingToDOGuestRequest(request));

            }
            else
                throw new InvalidOperationException("You can't change a guest request that has open invitations for it");
        }
        public void UpdateHost(BO.Host host)
        {
            if (host.CollectionClearance == false)
                if (!AbleToChangeCollectionClearance(host))
                    throw new InvalidOperationException("You can't change a collection clearance Status to false because you have open invitations for you");
            dal.UpdateHost(Conversions.CastingToDOHost(host));

        }
        public void UpdateHostingUnit(BO.HostingUnit hostingUnit)
        {
            dal.UpdateHostingUnit(Conversions.CastingToDOHostingUnit(hostingUnit));
        }

        public void UpdateOrder(BO.Order order)
        {
            DO.Order dalOrder = new DO.Order();
            try
            {
                dalOrder = dal.RecieveOrder(order.Key);
            }
            catch (MissingMemberException e)
            {
                throw e;
            }
            if (!AbleToChangeOrderStatus(order))
                throw new InvalidOperationException("You can't change a order status after other placed/ alradey irrelevant");
            else
            {
                //check if order placed now
                if (IsOpenOrder(Conversions.CastingToBO_Order(dalOrder)))
                {
                    if (order.Status == BO.Order_Status.EMAIL_SENT && dalOrder.Status != DO.Order_Status.EMAIL_SENT)
                    {
                        SendEmail(order);
                    }
                    if (order.Status == BO.Order_Status.APPROVED)
                    {
                        PlaceOrder(order);
                        order.CloseDate = DateTime.Now;
                    }
                }
            }
            dal.UpdateOrder(Conversions.CastingToDO_Order(order));

        }
        #endregion
        #region get from DS
        public BO.Host GetHost(int key)
        {
            return Conversions.CastingToBOHost(dal.RecieveHost(key));
        }
        public IEnumerable<BO.Host> GetHosts()
        {
            return dal.GetHostsList().Select(x => Conversions.CastingToBOHost(x));
        }
        public IEnumerable<BO.Person> GetPersons()
        {   
            return dal.GetPersons().Select(x => Conversions.CastingToBOPerson(x));
        }
        public BO.Person GetPerson(int id)
        {
            return Conversions.CastingToBOPerson(dal.RecievePerson(id));
        }
        public BO.BankBranch GetBankBranch(int branchNumber)
        {
            return Conversions.CastingToBOBankBranch(dal.RecieveBankBranch(branchNumber));
        }
        public BO.Order GetOrder(int id)
        {
            return Conversions.CastingToBO_Order(dal.RecieveOrder(id));
        }
        public IEnumerable<BO.HostingUnit> GetHostingUnits()
        {
            return dal.GetHostingUnitsList(x => true).Where(x => x.Status == DO.Status.ACTIVE).Select(x => Conversions.CastingToBOHostingUnit(x));
        }
        public IEnumerable<BO.HostingUnit> GetHostingUnitsOfHost(int key)
        {
            return dal.GetHostingUnitsList(x => x.Owner == key).Where(x => x.Status == DO.Status.ACTIVE).Select(x => Conversions.CastingToBOHostingUnit(x));
        }
        public IEnumerable<BO.Order> GetOrdersOfHost(int key)
        {
            return dal.GetOrdersList(x => x.HostID == key).Select(x => Conversions.CastingToBO_Order(x));
        }
        public IEnumerable<BO.GuestRequest> GetGuestRequests(int key)
        {
            return dal.GetGuestRequestsList().Where(x => x.ClientID == key && x.Status != DO.Request_Statut.CANCELLED).Select(x => Conversions.CastingToBOGuestRequest(x));
        }
        public IEnumerable<BO.GuestRequest> GetGuestRequests()
        {
            return dal.GetGuestRequestsList().Where(x => x.Status != DO.Request_Statut.CANCELLED).Select(x => Conversions.CastingToBOGuestRequest(x));
        }
        public IEnumerable<BO.GuestRequest> GetGetGuestRequestsExceptHostGr(int key)
        {
            return dal.GetGuestRequestsList().Where(x => x.ClientID != key && x.Status != DO.Request_Statut.CANCELLED).Select(x => Conversions.CastingToBOGuestRequest(x));
        }
        public BO.HostingUnit GetHostingUnit(int key)
        {
            return Conversions.CastingToBOHostingUnit(dal.RecieveHostingUnit(key));
        }
        public BO.GuestRequest GetGuestRequest(int key)
        {
            return Conversions.CastingToBOGuestRequest(dal.RecieveGuestRequest(key));
        }
        public IEnumerable<BO.Order> GetOrders()
        {
            return dal.GetOrdersList(x => true).Select(x => Conversions.CastingToBO_Order(x));
        }
        #endregion
        #region Grouping
        public IEnumerable<IGrouping<int, BO.GuestRequest>> GroupedByNumberOfGuests()
        {
            var groups = from GuestRequest in dal.GetGuestRequestsList()
                         select Conversions.CastingToBOGuestRequest(GuestRequest)
                         into items
                         group items by (items.Adults + items.children);
            return groups;
        }
        public IEnumerable<IGrouping<int, BO.Host>> GroupedByNumberOfHostingUnit()
        {
            var hosts = from host in dal.GetHostsList()
                        select Conversions.CastingToBOHost(host)
                        into bHost
                        orderby bHost.HostingUnits.Count()
                        group bHost by bHost.HostingUnits.Count();
            return hosts;
        }
        public IEnumerable<IGrouping<BO.Location, BO.GuestRequest>> GuestRequestGroupedBySpecificArea()
        {
            var guestRequests = from guestRequest in dal.GetGuestRequestsList()
                                select Conversions.CastingToBOGuestRequest(guestRequest)
                        into bGuestRequest
                                group bGuestRequest by bGuestRequest.Area;
            return guestRequests;
        }
        public IEnumerable<IGrouping<BO.Location, BO.HostingUnit>> HostingUnitGroupedBySpecificArea()
        {
            var hostingUnits = from hostingUnit in dal.GetHostingUnitsList(x => true)
                               select Conversions.CastingToBOHostingUnit(hostingUnit)
                         into bHostingUnit
                               group bHostingUnit by bHostingUnit.Area;
            return hostingUnits;
        }
        #endregion
        #region Threads
        public void ExpiredThread()
        {


            new Thread(() =>
            {
                while (true)
                {
                    if (DateTime.Now.Hour == 0) //12AM
                    {
                        loadCfg();
                        var cfg = Config["IgnoreTime"];
                        int IgnoreDays = Config["IgnoreTime"];
                        IEnumerable<BO.GuestRequest> requests = expiredGR();
                        IEnumerable<BO.Order> orders = OrdersCreated(IgnoreDays);
                        orders = MailSentTime(orders, IgnoreDays);
                        foreach (var item in requests.ToList())
                        {
                            item.Status = BO.Request_Statut.EXPIRED;
                            UpdateGusetRequest(item);
                        }
                        foreach (var item in orders.ToList())
                        {
                            item.Status = BO.Order_Status.IGNORED_CLOSED;
                            UpdateOrder(item);
                        }
                    }
                    Thread.Sleep(3600000);//sleep for hour
                }
            }
             ).Start();
        }

       public void configHandler (Action action)
        {
            dal.configHandler(action);

        }
        public void CleanDiaryThread()
        {
            new Thread(() =>
            {
                while (true)
                {
                    if (DateTime.Now.Hour == 0) //12AM
                    {
                        DateTime temp = DateTime.Today;
                        temp = temp.AddMonths(-1);
                        IEnumerable<BO.HostingUnit> hostingUnits = GetHostingUnits();
                        foreach (var item in hostingUnits)
                        {
                            if (item.GetDate(temp))
                            {
                                item.SetDate(temp, false);
                                UpdateHostingUnit(item);
                            }
                        }
                    }
                    Thread.Sleep(3600000);//sleep for hour
                }
            }
             ).Start();
        }
        #endregion
        #region  LogcalFunctions
        public bool CheckDate(BO.GuestRequest guestRequest)
        {
            if (guestRequest.ReleaseDate <= guestRequest.EntryDate)
            {
                throw new ArgumentOutOfRangeException("The release date must be at least one day after the entry date");
            }
            else
            {
                DateTime temp = DateTime.Now;
                if (guestRequest.ReleaseDate >= temp.AddMonths(11))
                {
                    throw new ArgumentOutOfRangeException("Sorry, you can't place an order that ends elven months or more from today");
                }
            }
            return true;
        }
        public IEnumerable<BO.HostingUnit> CheckForAvailableHostingUnit(DateTime date, int days)
        {
            DateTime end = date.AddDays(days);
            BO.GuestRequest temp = new BO.GuestRequest() { EntryDate = date, ReleaseDate = end };
            return GetHostingUnits().Where(x => IsAvailableGuestRequest(temp, x));
        }
        public bool IsOpenOrder(BO.Order bOrder)
        {
            DO.Order order = dal.RecieveOrder(bOrder.Key);
            return order.Status != DO.Order_Status.CLIENT_CLOSED && order.Status != DO.Order_Status.IGNORED_CLOSED && order.Status != DO.Order_Status.IRRELEVANT;
        }
        /// <summary>
        ///  in this function we take all the orders that connected in to this hosting unit
        ///  and  check if  there any open order to this hosting unit  
        /// </summary>
        /// <param name="hostingUnit"></param>
        /// <returns></returns>
        public bool DeleteableHostingUnit(BO.HostingUnit hostingUnit)//
        {
            var orders = from item in dal.GetOrdersList(x => x.HostingUnitKey == hostingUnit.Key)
                         where IsOpenOrder(Conversions.CastingToBO_Order(item))
                         select item;
            return !orders.Any();//check if there is somthing in the orders
        }
        /// <summary>
        /// In this function we recieve all orders that belong to this Guest Request which status changed to "approved".
        /// It also recieves all the orders that belong to this hosting unit with schedule overlap and change their status to "irrelevant". 
        /// </summary>
        /// <param name="order"></param>
        public void CloseIrrelevantOrders(BO.Order order)//
        {
            var orders = from item in dal.GetOrdersList(x => x.Key != order.Key)
                         where item.GuestRequestKey == order.GuestRequest.GuestRequestKey ||
                         item.HostingUnitKey == order.HostingUnit.Key && !IsAvailableGuestRequest(Conversions.CastingToBOGuestRequest(dal.RecieveGuestRequest(item.GuestRequestKey)),
                         Conversions.CastingToBOHostingUnit(dal.RecieveHostingUnit(item.HostingUnitKey)))
                         select item;

            foreach (var item in orders.ToList())
            {
                item.Status = DO.Order_Status.IRRELEVANT;
                dal.UpdateOrder(item);
            }
        }
        public bool AbleToChangeCollectionClearance(BO.Host host)
        {
            var orders = dal.GetOrdersList(x => x.HostID == host.Person.Id).Where(x => IsOpenOrder(Conversions.CastingToBO_Order(x)));
            return !orders.Any();
        }
        public bool EmailPremissionCheck(BO.Host host)//
        {
            return dal.RecieveHost(host.Person.Id).CollectionClearance;
        }
        public bool IsAvailableGuestRequest(BO.GuestRequest guestRequest, BO.HostingUnit hostingUnit)//
        {
            DateTime temp = guestRequest.EntryDate;
            while (temp != guestRequest.ReleaseDate)
            {
                if (hostingUnit.GetDate(temp) == true)
                    return false;
                temp = temp.AddDays(1);
            }
            return true;
        }
        public void MarkDates(BO.Order order)//
        {
            DO.GuestRequest guestRequest = dal.RecieveGuestRequest(order.GuestRequest.GuestRequestKey);
            DO.HostingUnit dHostingUnit = dal.RecieveHostingUnit(order.HostingUnit.Key);
            DateTime temp = guestRequest.EntryDate;
            //now we mark this days in true 
            while (DateTime.Compare(temp, guestRequest.ReleaseDate) != 0)
            {
                dHostingUnit.SetDate(temp, true);
                temp = temp.AddDays(1);
            }
            dal.UpdateHostingUnit(dHostingUnit);
            guestRequest.Status = DO.Request_Statut.ORDERED;
            //dal.UpdateGusetRequestStatus(guestRequest);
        }
        public int CalculateCommision(BO.Order order)//
        {
            return dal.GetCommissionRate() * PassedDays(order.GuestRequest.EntryDate, order.GuestRequest.ReleaseDate);
        }
        public IEnumerable<BO.GuestRequest> MatchingRequirment(Func<BO.GuestRequest, bool> predicate)
        {
            return dal.GetGuestRequestsList().Select(x => Conversions.CastingToBOGuestRequest(x)).Where(predicate);
        }///
        public int NumberOfInvitationsSent(BO.GuestRequest guestRequest)//
        {
            return (dal.GetOrdersList(x => x.GuestRequestKey == guestRequest.GuestRequestKey)).Count();
        }
        public int NumberOfSentOrders(BO.HostingUnit hostingUnit)//
        {
            return dal.GetOrdersList(x => x.HostingUnitKey == hostingUnit.Key).Count();
        }
        public bool AbleToChangeOrderStatus(BO.Order order)//
        {
            DO.Order dOrder = dal.RecieveOrder(order.Key);
            return dOrder.Status != DO.Order_Status.APPROVED && dOrder.Status != DO.Order_Status.IRRELEVANT;
        }
        public IEnumerable<BO.Order> OrdersCreated(int days)//
        {
            return dal.GetOrdersList(x => PassedDays(x.OrderDate) >= days).Select(x => Conversions.CastingToBO_Order(x));
        }
        public IEnumerable<BO.Order> MailSentTime(IEnumerable<BO.Order> orders, int days)//
        {

            return (from item in orders
                    where item.SentDate == (default(DateTime)) || PassedDays(item.SentDate) >= days
                    select item
                     );
        }
        public int PassedDays(DateTime first, DateTime second = default)//
        {
            if (second == default(DateTime))
            {
                second = DateTime.Now;
            }
            return (second - first).Days;
        }
        public void SendEmail(BO.Order order)
        {

            BO.Person client = Conversions.CastingToBOPerson(dal.RecievePerson(order.GuestRequest.ClientID));
            string EmailAddress = client.MailAddress;
            MailMessage email = new MailMessage();
            email.To.Add(EmailAddress);
            email.From = new MailAddress("NoReply@jct.ac.il");
            email.Subject = "הזמנה";
            email.IsBodyHtml = true;
            string htmlbody = File.ReadAllText("html1.txt", Encoding.UTF8);
            email.BodyEncoding = Encoding.Unicode;
            email.Body = string.Format(htmlbody, order.Key, order.HostingUnit.HostingUnitName, order.HostingUnit.ToString(), client.MailAddress, order.HostingUnit.ImageLink1, order.HostingUnit.ImageLink2, order.HostingUnit.ImageLink3);
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
        /// <summary>
        /// geting all the orders for this guest request(that answered) and make them irrelevant
        /// </summary>
        /// <param name="order"></param>
        public void UpdateUserStatus(BO.Order order)

        {
            var guestRequest = dal.RecieveGuestRequest(order.GuestRequest.GuestRequestKey);
            guestRequest.Status = DO.Request_Statut.ORDERED;
            dal.UpdateGusetRequest(guestRequest);
            var orders = dal.GetOrdersList(x => x.CliendID == order.GuestRequest.ClientID && x.Key != order.Key);
            foreach (var item in orders.ToList())
            {
                if (AbleToChangeOrderStatus(Conversions.CastingToBO_Order(item)))
                {
                    item.Status = DO.Order_Status.IRRELEVANT;
                    dal.UpdateOrder(item);
                }
            }
        }
        public Client GetClient(int id)
        {
            Client client = new Client()
            {
                Details = GetPerson(id),
                Requests = GetGuestRequests(id)
            };
            return client;
        }
        public void PlaceOrder(BO.Order order)
        {
            MarkDates(order);
            order.Commission = CalculateCommision(order);
            CloseIrrelevantOrders(order);
            UpdateUserStatus(order);
            DO.Host host = dal.RecieveHost(order.HostID);
            host.TotalCommission += order.Commission;
            order.TotalCost = order.HostingUnit.PricePerNight * PassedDays(order.GuestRequest.EntryDate, order.GuestRequest.ReleaseDate);

            dal.UpdateHost(host);


        }
        public void updateConfig(string key, object value)
        {
            dal.setConfig(key, value);
        }
        internal void loadCfg()
        {
            Dictionary<string, object> CFG = dal.getConfig();
            Config = (from item in CFG where item.Key!="LastUpdate"
                      select item).ToDictionary(x => x.Key, x => int.Parse((string)x.Value.GetType().GetProperty("Value").GetValue(x.Value)));

        }
        #endregion
        IEnumerable<BO.GuestRequest> expiredGR()
        {
            return dal.GetGuestRequestsList().Where(x => x.Status == DO.Request_Statut.OPEN && x.EntryDate <= DateTime.Today).
                Select(x => Conversions.CastingToBOGuestRequest(x));
        }
    }
}
