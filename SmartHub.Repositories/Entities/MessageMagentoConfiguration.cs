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

    // MessageMagento
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.21.1.0")]
    public partial class MessageMagentoConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<MessageMagento>
    {
        public MessageMagentoConfiguration()
            : this("dbo")
        {
        }

        public MessageMagentoConfiguration(string schema)
        {
            ToTable(schema + ".MessageMagento");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName(@"Id").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.MagentoId).HasColumnName(@"MagentoId").IsOptional().HasColumnType("nvarchar").HasMaxLength(255);
            Property(x => x.SenderId).HasColumnName(@"SenderId").IsRequired().HasColumnType("nvarchar").HasMaxLength(255);
            Property(x => x.SenderEmail).HasColumnName(@"SenderEmail").IsRequired().HasColumnType("nvarchar").HasMaxLength(255);
            Property(x => x.TicketId).HasColumnName(@"TicketId").IsRequired().HasColumnType("int");
            Property(x => x.Message).HasColumnName(@"Message").IsOptional().HasColumnType("nvarchar");
            Property(x => x.CreatedDate).HasColumnName(@"CreatedDate").IsRequired().HasColumnType("datetime");
            Property(x => x.LastModifiedDate).HasColumnName(@"LastModifiedDate").IsOptional().HasColumnType("datetime");
            Property(x => x.Note).HasColumnName(@"Note").IsOptional().HasColumnType("nvarchar");
            Property(x => x.LastSynchronizedDate).HasColumnName(@"LastSynchronizedDate").IsOptional().HasColumnType("datetime");
            Property(x => x.SyncStatus).HasColumnName(@"SyncStatus").IsRequired().HasColumnType("int");
            Property(x => x.SyncErrorMessage).HasColumnName(@"SyncErrorMessage").IsOptional().HasColumnType("nvarchar");

            // Foreign keys
            HasRequired(a => a.TicketMagento).WithMany(b => b.MessageMagentoes).HasForeignKey(c => c.TicketId).WillCascadeOnDelete(false); // FK_MessageMagento_TicketMagento_TicketId
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
