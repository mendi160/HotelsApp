using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Config
    {
        public int HostingUnitKey { set; get; }
        public int GuestRequestKey { set; get; }
        public int OrderKey { set; get; }
        public int TotalHostingUnit { set; get; }
        public int TotalGuestRequest { set; get; }
        public int StandbyTime { set; get; }
        public int TotalHosts { set; get; }
        public int TotalPersons { set; get; }
        public int TotalOrders { set; get; }
        public int Commission { set; get; }
    }
}
