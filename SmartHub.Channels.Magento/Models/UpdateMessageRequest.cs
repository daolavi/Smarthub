using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHub.Channels.Magento.Models
{
    public class UpdateMessageRequest
    {
        public string message_id { get; set; }

        public string message { get; set; }

        public string type { get; set; }

        public string modified_date { get; set; }

        public string note { get; set; }
    }
}
