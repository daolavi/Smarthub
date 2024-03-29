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
    public partial class ChannelEbayConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<ChannelEbay>
    {
        public ChannelEbayConfiguration()
            : this("dbo")
        {
        }

        public ChannelEbayConfiguration(string schema)
        {
            ToTable(schema + ".ChannelEbay");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName(@"Id").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.Token).HasColumnName(@"Token").IsOptional().HasColumnType("nvarchar");
            Property(x => x.ExpiredDate).HasColumnName(@"ExpiredDate").IsOptional().HasColumnType("datetime");
            Property(x => x.CreatedDate).HasColumnName(@"CreatedDate").IsRequired().HasColumnType("datetime");
            Property(x => x.Message).HasColumnName(@"Message").IsOptional().HasColumnType("nvarchar");
            Property(x => x.LastSyncedDateMemberMessage).HasColumnName(@"LastSyncedDate_MemberMessage").IsOptional().HasColumnType("datetime");
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
