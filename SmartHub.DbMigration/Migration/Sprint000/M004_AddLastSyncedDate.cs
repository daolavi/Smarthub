using System.ComponentModel;

namespace SmartHub.DbMigration.Migration.Sprint000
{
    using FluentMigrator;

    [SmartHubMigration(1, 0, 4, "daolam", "Update table Connection and table ChannelEbay")]
    public class M004_AddLastSyncedDate : Migration
    {
        private const string Table_Connection = "Connection";

        private const string Table_ChannelEbay = "ChannelEbay";

        public override void Down()
        {
            Rename.Column("CreatedDate").OnTable(Table_Connection).To("CreateDate");
            Rename.Column("NextSyncedDate").OnTable(Table_Connection).To("NextSyncDate");
            Delete.Column("Message").FromTable(Table_Connection);

            Delete.Column("Message").FromTable(Table_ChannelEbay);
            Delete.Column("LastSyncedDate_MemberMessage").FromTable(Table_ChannelEbay);
        }

        public override void Up()
        {
            Rename.Column("CreateDate").OnTable(Table_Connection).To("CreatedDate");
            Rename.Column("NextSyncDate").OnTable(Table_Connection).To("NextSyncedDate");
            Alter.Table(Table_Connection).AddColumn("Message").AsMaxString().Nullable();

            Alter.Table(Table_ChannelEbay).AddColumn("Message").AsMaxString().Nullable();
            Alter.Table(Table_ChannelEbay).AddColumn("LastSyncedDate_MemberMessage").AsDateTime().Nullable();
        }
    }
}
