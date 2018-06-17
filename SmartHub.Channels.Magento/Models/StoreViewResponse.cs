using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHub.Channels.Magento.Models
{
    public class StoreViewResponse
    {
        public int id { get; set; }

        public string code { get; set; }

        public string name { get; set; }

        public int website_id { get; set; }

        public int store_group_id { get; set; }
    }
}
