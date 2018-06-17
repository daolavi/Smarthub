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

    // ChannelMagento
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.21.1.0")]
    public partial class ChannelMagento
    {
        public int Id { get; set; } // Id (Primary key)
        public System.DateTime CreatedDate { get; set; } // CreatedDate
        public string Username { get; set; } // Username (length: 255)
        public string Password { get; set; } // Password (length: 255)
        public string Host { get; set; } // Host (length: 255)
        public string StoreId { get; set; } // StoreId (length: 50)
        public System.DateTime? LastSyncedDateTicket { get; set; } // LastSyncedDate_Ticket
        public System.DateTime? LastSyncedDateMessage { get; set; } // LastSyncedDate_Message
        public string Message { get; set; } // Message
        public string TimeZoneDisplayName { get; set; } // TimeZoneDisplayName (length: 255)

        public ChannelMagento()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
