using System;

namespace SmartHub.Web.Models
{
    public class ChannelEbayViewModel
    {
        public int Id { get; set; }

        public string Token { get; set; }

        public DateTime? ExpiredDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Message { get; set; }

        public DateTime? LastSyncedDateMemberMessage { get; set; }

        public bool IsConnected { get; set; }
    }
}