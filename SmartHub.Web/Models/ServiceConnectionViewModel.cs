using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartHub.Services.Models;

namespace SmartHub.Web.Models
{
    public class ServiceConnectionViewModel
    {
        public ChannelEbayViewModel channelEbay { get; set; }

        public ChannelMagentoViewModel channelMagento { get; set; }

        public ChannelEmailViewModel channelEmail { get; set; }
    }
}