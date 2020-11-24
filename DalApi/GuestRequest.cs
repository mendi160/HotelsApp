using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public  class GuestRequest
    {
        public int ClientID { get; set; }
        public int GuestRequestKey { get; set; }
        public Request_Statut Status { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime ReleaseDate { get; set; }
        public Location Area { get; set; }
        public string  SubArea { get; set; }
        public Hosting_Type Type { get; set; }
        public int Adults { get; set; }
        public int children { get; set; }
        public Preferences Pool { get; set; }
        public Preferences Jacuzzi { get; set; }
        public Preferences Garden { get; set; }
        public Preferences ChildrensAttractions { get; set; }
        public override string ToString()
        {
            return base.ToString();
        }

    }
}
