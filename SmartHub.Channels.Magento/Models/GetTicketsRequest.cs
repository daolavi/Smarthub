using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHub.Channels.Magento.Models
{
    public class GetTicketsRequest
    {
        public string connection_id { get; set; }

        public string from { get; set; }

        public string to { get; set; }
    }
}
