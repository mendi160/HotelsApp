//Yehonatan Eliyahu 311555387
//Mendi Shneorson 204290688
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class HostingUnit
    {
          public int Key { get; set; }
          public int Owner { get; set; }
          public int PricePerNight { get; set; }
          public string HostingUnitName { get; set; }
          public Location Area { get; set; }
          public bool[,] Diary { get; set; }
          public Status Status { get; set; }
          public bool WIFI { get; set; }
          public bool SwimmingPool { get; set; }
          public bool Kitchen { get; set; }
          public bool TV { get; set; }
          public bool Jacuzzi { get; set; }
          public string ImageLink1 { get; set; }
          public string ImageLink2 { get; set; }
          public string ImageLink3 { get; set; }

          public override string ToString()
          {
               return base.ToString();  
          }
        
    }
}
