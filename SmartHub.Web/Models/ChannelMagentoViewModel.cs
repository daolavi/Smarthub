using System;

namespace SmartHub.Web.Models
{
    public class ChannelMagentoViewModel
    {
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Username { get; set; }
    
        public string Password { get; set; }

        public string Host { get; set; }

        public string StoreId { get; set; }

        public DateTime? LastSyncedDateTicket { get; set; }

        public DateTime? LastSyncedDateMessage { get; set; }

        public string Message { get; set; }

        public bool IsConnected { get; set; }

        public string TimeZoneDisplayName { get; set; }
    }
}