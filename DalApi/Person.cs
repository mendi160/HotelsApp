using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
   public class Person
    {
        public int Id { get; set; }
        public string UserType { get; set; }
        public ID IdType { get;  set; }
        public Status Status { get; set; }
        public string Password { get;  set; }
        public string FirstName { get;  set; }
        public string LastName { get;  set; }
        public int PhoneNumber { get;  set; }
        public string MailAddress { get;  set; }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
