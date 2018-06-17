// <auto-generated>
// ReSharper disable ConvertPropertyToExpressionBody
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable InconsistentNaming
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable RedundantNameQualifier
// ReSharper disable RedundantOverridenMember
// ReSharper disable UseNameofExpression
// TargetFrameworkVersion = 4.51
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

namespace SmartHub.Repositories.Entities
{

    // TicketEbay
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.21.1.0")]
    public partial class TicketEbay
    {
        public int Id { get; set; } // Id (Primary key)
        public string EbayId { get; set; } // EbayId (length: 255)
        public int ConnectionId { get; set; } // ConnectionId
        public Shared.Enums.TicketStatus Status { get; set; } // Status
        public string Type { get; set; } // Type (length: 255)
        public string Subject { get; set; } // Subject (length: 1000)
        public string ItemId { get; set; } // ItemId (length: 255)
        public string CreatorId { get; set; } // CreatorId (length: 255)
        public string CreatorEmail { get; set; } // CreatorEmail (length: 255)
        public string RecipientId { get; set; } // RecipientId (length: 255)
        public System.DateTime CreatedDate { get; set; } // CreatedDate
        public System.DateTime? LastModifiedDate { get; set; } // LastModifiedDate
        public string Note { get; set; } // Note
        public System.DateTime? LastSynchronizedDate { get; set; } // LastSynchronizedDate
        public Shared.Enums.SyncStatus SyncStatus { get; set; } // SyncStatus
        public string SyncErrorMessage { get; set; } // SyncErrorMessage

        // Reverse navigation
        public virtual System.Collections.Generic.ICollection<MessageEbay> MessageEbays { get; set; } // MessageEbay.FK_MessageEbay_TicketEbay_TicketId
        public virtual System.Collections.Generic.ICollection<TicketEbayMagento> TicketEbayMagentoes { get; set; } // TicketEbayMagento.FK_TicketEbayMagento_TicketEbay_IdEbay

        // Foreign keys
        public virtual Connection Connection { get; set; } // FK_TicketEbay_Connection_ConnectionId

        public TicketEbay()
        {
            MessageEbays = new System.Collections.Generic.List<MessageEbay>();
            TicketEbayMagentoes = new System.Collections.Generic.List<TicketEbayMagento>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
