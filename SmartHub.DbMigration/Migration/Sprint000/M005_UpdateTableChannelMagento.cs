using System.ComponentModel;

namespace SmartHub.DbMigration.Migration.Sprint000
{
    using FluentMigrator;

    [SmartHubMigration(1, 0, 5, "daolam", "Update table Channel Magento")]
    public class M005_UpdateTableChannelMagento : Migration
    {
        private const string Table_ChannelMagento = "ChannelMagento";

        public override void Down()
        {
            Delete.Column("Username").FromTable(Table_ChannelMagento);
            Delete.Column("Password").FromTable(Table_ChannelMagento);
            Delete.Column("Host").FromTable(Table_ChannelMagento);
            Delete.Column("StoreId").FromTable(Table_ChannelMagento);
        }

        public override void Up()
        {
            Alter.Table(Table_ChannelMagento).AddColumn("Username").AsString(255).NotNullable();
            Alter.Table(Table_ChannelMagento).AddColumn("Password").AsString(255).NotNullable();
            Alter.Table(Table_ChannelMagento).AddColumn("Host").AsString(255).NotNullable();
            Alter.Table(Table_ChannelMagento).AddColumn("StoreId").AsString(50).NotNullable();
        }
    }
}
