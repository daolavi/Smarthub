namespace SmartHub.DbMigration.Migration.Sprint000
{
    using FluentMigrator;

    [SmartHubMigration(1, 0, 6, "daolam", "Add LastSyncedDate for ChannelMagento")]
    public class M006_AddLastSyncedDateForChannelMagento : Migration
    {
        private const string Table_ChannelMagento = "ChannelMagento";

        public override void Down()
        {
            Delete.Column("LastSyncedDate_Ticket").FromTable(Table_ChannelMagento);
            Delete.Column("LastSyncedDate_Message").FromTable(Table_ChannelMagento);
            Delete.Column("Message").FromTable(Table_ChannelMagento);
        }

        public override void Up()
        {
            Alter.Table(Table_ChannelMagento).AddColumn("LastSyncedDate_Ticket").AsDateTime().Nullable();
            Alter.Table(Table_ChannelMagento).AddColumn("LastSyncedDate_Message").AsDateTime().Nullable();
            Alter.Table(Table_ChannelMagento).AddColumn("Message").AsMaxString().Nullable();
        }
    }
}
