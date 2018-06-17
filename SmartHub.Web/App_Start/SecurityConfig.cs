using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace SmartHub.Web.App_Start
{
    public static class SecurityConfig
    {
        public static void RegisterProtocol()
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls |
                                                              SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
        }
    }
}