using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
   public class BankBranch
    {
        public int BankNumber { get; set; }
        public string BankName { get; set; }
        public int BranchNumber { get; set; }
        public string BranchAddress { get; set; }
        public string BranchCity { get; set; }
        public override string ToString()
        {
            return $"שם הבנק: {BankName} \n מספר הבנק {BankNumber} \n מספר הסניף {BranchNumber} \n כתובת: {BranchAddress} ,{BranchCity} ";
        }
    }
}
