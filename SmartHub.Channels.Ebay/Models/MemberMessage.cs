using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHub.Channels.Ebay.Models
{
    public class MemberMessage
    {
        public string ItemId { get; set; }

        public string ParentMessageId { get; set; }

        public string MessageId { get; set; }

        public string MessageStatus { get; set; }

        public string QuestionType { get; set; }

        public string SenderId { get; set; }

        public string SenderEmail { get; set; }

        public string RecipientId { get; set; }

        public string Body { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime LastModifiedDate { get; set; }
    }
}
