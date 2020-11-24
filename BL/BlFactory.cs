﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{/// <summary>
/// factory of bl implementation
/// </summary>
    public static class BlFactory
    {
        public static IBl GetBL()
        {
            return new BL.BLImp();
        }
    }
}
