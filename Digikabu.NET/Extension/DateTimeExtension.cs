﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digikabu.NET.Extension
{
    public static class DateTimeExtension
    {
       
            public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
            {
                int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
                return dt.AddDays(-1 * diff).Date;
            }
        
    }
}
