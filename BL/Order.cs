using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
     public class Order
    {
        public int Key { get; set; }
        public GuestRequest GuestRequest { get; set; }
        public HostingUnit HostingUnit { get; set; }
        public string ClientFirstName { get; set; }
        public string ClientLastName { get; set; }
        public int HostID { get; set; }
        public Order_Status Status { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime SentDate { get; set; }
        public DateTime CloseDate { get; set; }
        public int Commission { get; set; }
         public int TotalCost { set; get; }
    }
}
