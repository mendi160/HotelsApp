using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Host
    {
        //we inherit from person so we  have all details about him
        public Person Person { set; get; } 
        public BankBranch BankBranch { set; get; }
        public IEnumerable<HostingUnit>HostingUnits { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public int TotalCommission { set; get; }
        public int BankAccuontNumber { get; set; }
        public bool CollectionClearance { get; set; }
        public Status Status { get; set; }
        public string Website { get; set; }
        public override string ToString()
        {
            return "" + HostingUnits.Count();
        }
    }
}
