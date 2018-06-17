using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHub.Channels.Magento.Models
{
    public class CreateTicketRequest
    {
        public string connection_id { get; set; }

        public string store_id { get; set; }

        public string department_id { get; set; }

        public string agent_id { get; set; }

        public string status { get; set; }

        public string type { get; set; }

        public string item_id { get; set; }

        public string subject { get; set; }

        public string creator_id { get; set; }

        public string creator_name { get; set; }

        public string creator_email { get; set; }

        public string recipient_id { get; set; }

        public string created_at { get; set; }

        public string last_modified_at { get; set; }

        public string note { get; set; }
    }
}
