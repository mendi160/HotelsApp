using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
     enum ConfigDataAccess
     {
          INTERNAL,WRITEABLE,READABLE

     }
     class ConfigData
     {
        public  object Value { get; set; }
         public ConfigDataAccess Access { get; set; }
     }
}
