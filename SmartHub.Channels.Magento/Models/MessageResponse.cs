using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHub.Channels.Magento.Models
{
    public class MessageResponse
    {
        public string ticket_id { get; set; }

        public string message_id { get; set; }

        public string message { get; set; }

        public string type { get; set; }

        public string sender_id { get; set; }

        public string sender_name { get; set; }

        public string sender_email { get; set; }

        public string created_date { get; set; }

        public string last_modified_date { get; set; }

        public string note { get; set; }
    }
}
