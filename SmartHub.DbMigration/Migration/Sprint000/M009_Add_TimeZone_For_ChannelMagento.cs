namespace SmartHub.DbMigration.Migration.Sprint000
{
    using FluentMigrator;

    [SmartHubMigration(1, 0, 9, "daolam", "Add TimeZone for Channel Magento")]
    public class M009_Add_TimeZone_For_ChannelMagento : Migration
    {
        private const string Table_ChannelMagento = "ChannelMagento";

        public override void Down()
        {
            Delete.Column("TimeZoneDisplayName").FromTable(Table_ChannelMagento);
        }

        public override void Up()
        {
            Alter.Table(Table_ChannelMagento).AddColumn("TimeZoneDisplayName").AsString(255).NotNullable();
        }
    }
}
