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

    // MessageEmail
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.21.1.0")]
    public partial class MessageEmail
    {
        public int Id { get; set; } // Id (Primary key)
        public string EmailId { get; set; } // EmailId (length: 255)
        public string SenderId { get; set; } // SenderId (length: 255)
        public string SenderEmail { get; set; } // SenderEmail (length: 255)
        public int TicketId { get; set; } // TicketId
        public string Message { get; set; } // Message
        public System.DateTime CreatedDate { get; set; } // CreatedDate
        public System.DateTime? LastModifiedDate { get; set; } // LastModifiedDate
        public string Note { get; set; } // Note
        public System.DateTime? LastSynchronizedDate { get; set; } // LastSynchronizedDate
        public Shared.Enums.SyncStatus SyncStatus { get; set; } // SyncStatus
        public string SyncErrorMessage { get; set; } // SyncErrorMessage

        // Reverse navigation
        public virtual System.Collections.Generic.ICollection<MessageEmailMagento> MessageEmailMagentoes { get; set; } // MessageEmailMagento.FK_MessageEmailMagento_MessageEmail_IdEmail

        // Foreign keys
        public virtual TicketEmail TicketEmail { get; set; } // FK_MessageEmail_TicketEmail_TicketId

        public MessageEmail()
        {
            MessageEmailMagentoes = new System.Collections.Generic.List<MessageEmailMagento>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
