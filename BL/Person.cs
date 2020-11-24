//Yehonatan Eliyahu 311555387
//Mendi Shneorson 204290688
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Person
    {
        public int Id { get; set; }
        public ID IdType { get; set; }
        public Status Status { get; set; }
        public string UserType { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PhoneNumber { get; set; }
        public string MailAddress { get; set; }
        public override string ToString()
        {
            return $"שם פרטי: {FirstName} \n שם משפחה: {LastName} \n מספר זהות {Id} \n אימייל: { MailAddress} \n מספר טלפון { PhoneNumber} \n";
        }
    }
}
