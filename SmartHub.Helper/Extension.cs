using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHub.Helper
{
    public static class Extension
    {
        public static long ToUnixTime(this DateTime dateTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToInt64((dateTime.ToUniversalTime() - epoch).TotalSeconds);
        }
    }
}
