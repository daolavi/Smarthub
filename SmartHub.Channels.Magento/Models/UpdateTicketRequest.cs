using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHub.Channels.Magento.Models
{
    public class UpdateTicketRequest
    {
        public string ticket_id { get; set; }

        public string status { get; set; }

        public string type { get; set; }

        public string item_id { get; set; }

        public string subject { get; set; }

        public string updated_by_id { get; set; }

        public string last_modified_at { get; set; }

        public string note { get; set; }
    }
}
