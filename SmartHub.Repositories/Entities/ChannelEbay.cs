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

    // ChannelEbay
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.21.1.0")]
    public partial class ChannelEbay
    {
        public int Id { get; set; } // Id (Primary key)
        public string Token { get; set; } // Token
        public System.DateTime? ExpiredDate { get; set; } // ExpiredDate
        public System.DateTime CreatedDate { get; set; } // CreatedDate
        public string Message { get; set; } // Message
        public System.DateTime? LastSyncedDateMemberMessage { get; set; } // LastSyncedDate_MemberMessage

        public ChannelEbay()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
