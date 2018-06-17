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

    // ChannelEmail
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.21.1.0")]
    public partial class ChannelEmailConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<ChannelEmail>
    {
        public ChannelEmailConfiguration()
            : this("dbo")
        {
        }

        public ChannelEmailConfiguration(string schema)
        {
            ToTable(schema + ".ChannelEmail");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName(@"Id").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.CreatedDate).HasColumnName(@"CreatedDate").IsRequired().HasColumnType("datetime");
            Property(x => x.Message).HasColumnName(@"Message").IsOptional().HasColumnType("nvarchar");
            Property(x => x.AccessToken).HasColumnName(@"AccessToken").IsOptional().HasColumnType("nvarchar");
            Property(x => x.ExpiresInSeconds).HasColumnName(@"ExpiresInSeconds").IsOptional().HasColumnType("bigint");
            Property(x => x.IdToken).HasColumnName(@"IdToken").IsOptional().HasColumnType("nvarchar");
            Property(x => x.Issued).HasColumnName(@"Issued").IsOptional().HasColumnType("datetime");
            Property(x => x.IssuedUtc).HasColumnName(@"IssuedUtc").IsOptional().HasColumnType("datetime");
            Property(x => x.RefreshToken).HasColumnName(@"RefreshToken").IsOptional().HasColumnType("nvarchar");
            Property(x => x.Scope).HasColumnName(@"Scope").IsOptional().HasColumnType("nvarchar");
            Property(x => x.TokenType).HasColumnName(@"TokenType").IsOptional().HasColumnType("nvarchar");
            Property(x => x.LastSyncedDate).HasColumnName(@"LastSyncedDate").IsOptional().HasColumnType("datetime");
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
