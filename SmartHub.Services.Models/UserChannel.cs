using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHub.Services.Models
{
    public class UserChannel
    {
        public int Id { get; set; } // Id (Primary key)
        public int UserId { get; set; } // UserId
        public int ChannelId { get; set; } // ChannelId
        public Shared.Enums.ChannelType ChannelType { get; set; } // ChannelType
        public bool IsActive { get; set; } // IsActive
    }
}
