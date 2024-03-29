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

    // TicketMagento
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.21.1.0")]
    public partial class TicketMagentoConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<TicketMagento>
    {
        public TicketMagentoConfiguration()
            : this("dbo")
        {
        }

        public TicketMagentoConfiguration(string schema)
        {
            ToTable(schema + ".TicketMagento");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName(@"Id").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.MagentoId).HasColumnName(@"MagentoId").IsOptional().HasColumnType("nvarchar").HasMaxLength(255);
            Property(x => x.ConnectionId).HasColumnName(@"ConnectionId").IsRequired().HasColumnType("int");
            Property(x => x.Status).HasColumnName(@"Status").IsRequired().HasColumnType("int");
            Property(x => x.Type).HasColumnName(@"Type").IsRequired().HasColumnType("nvarchar").HasMaxLength(255);
            Property(x => x.Subject).HasColumnName(@"Subject").IsRequired().HasColumnType("nvarchar").HasMaxLength(1000);
            Property(x => x.ItemId).HasColumnName(@"ItemId").IsOptional().HasColumnType("nvarchar").HasMaxLength(255);
            Property(x => x.CreatorId).HasColumnName(@"CreatorId").IsRequired().HasColumnType("nvarchar").HasMaxLength(255);
            Property(x => x.CreatorEmail).HasColumnName(@"CreatorEmail").IsRequired().HasColumnType("nvarchar").HasMaxLength(255);
            Property(x => x.RecipientId).HasColumnName(@"RecipientId").IsRequired().HasColumnType("nvarchar").HasMaxLength(255);
            Property(x => x.CreatedDate).HasColumnName(@"CreatedDate").IsRequired().HasColumnType("datetime");
            Property(x => x.LastModifiedDate).HasColumnName(@"LastModifiedDate").IsOptional().HasColumnType("datetime");
            Property(x => x.Note).HasColumnName(@"Note").IsOptional().HasColumnType("nvarchar");
            Property(x => x.LastSynchronizedDate).HasColumnName(@"LastSynchronizedDate").IsOptional().HasColumnType("datetime");
            Property(x => x.SyncStatus).HasColumnName(@"SyncStatus").IsRequired().HasColumnType("int");
            Property(x => x.SyncErrorMessage).HasColumnName(@"SyncErrorMessage").IsOptional().HasColumnType("nvarchar");

            // Foreign keys
            HasRequired(a => a.Connection).WithMany(b => b.TicketMagentoes).HasForeignKey(c => c.ConnectionId).WillCascadeOnDelete(false); // FK_TicketMagento_Connection_ConnectionId
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
