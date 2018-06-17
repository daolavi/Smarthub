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

    // MessageEbayMagento
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.21.1.0")]
    public partial class MessageEbayMagento
    {
        public int Id { get; set; } // Id (Primary key)
        public int IdEbay { get; set; } // IdEbay
        public int IdMagento { get; set; } // IdMagento

        // Foreign keys
        public virtual MessageEbay MessageEbay { get; set; } // FK_MessageEbayMagento_MessageEbay_IdEbay
        public virtual MessageMagento MessageMagento { get; set; } // FK_MessageEbayMagento_MessageMagento_IdMagento

        public MessageEbayMagento()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
