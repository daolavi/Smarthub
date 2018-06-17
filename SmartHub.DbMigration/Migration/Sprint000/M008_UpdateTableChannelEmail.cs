namespace SmartHub.DbMigration.Migration.Sprint000
{
    using FluentMigrator;

    [SmartHubMigration(1, 0, 8, "daolam", "Update table Channel Email")]
    public class M008_UpdateTableChannelEmail : Migration
    {
        private const string Table_ChannelEmail = "ChannelEmail";

        public override void Down()
        {
            Delete.Column("Message").FromTable(Table_ChannelEmail);
            Delete.Column("AccessToken").FromTable(Table_ChannelEmail);
            Delete.Column("ExpiresInSeconds").FromTable(Table_ChannelEmail);
            Delete.Column("IdToken").FromTable(Table_ChannelEmail);
            Delete.Column("Issued").FromTable(Table_ChannelEmail);
            Delete.Column("IssuedUtc").FromTable(Table_ChannelEmail);
            Delete.Column("RefreshToken").FromTable(Table_ChannelEmail);
            Delete.Column("Scope").FromTable(Table_ChannelEmail);
            Delete.Column("TokenType").FromTable(Table_ChannelEmail);
        }

        public override void Up()
        {
            Alter.Table(Table_ChannelEmail).AddColumn("Message").AsMaxString().Nullable();
            Alter.Table(Table_ChannelEmail).AddColumn("AccessToken").AsMaxString().Nullable();
            Alter.Table(Table_ChannelEmail).AddColumn("ExpiresInSeconds").AsInt64().Nullable();
            Alter.Table(Table_ChannelEmail).AddColumn("IdToken").AsMaxString().Nullable();
            Alter.Table(Table_ChannelEmail).AddColumn("Issued").AsDateTime().Nullable();
            Alter.Table(Table_ChannelEmail).AddColumn("IssuedUtc").AsDateTime().Nullable();
            Alter.Table(Table_ChannelEmail).AddColumn("RefreshToken").AsMaxString().Nullable();
            Alter.Table(Table_ChannelEmail).AddColumn("Scope").AsMaxString().Nullable();
            Alter.Table(Table_ChannelEmail).AddColumn("TokenType").AsMaxString().Nullable();
        }
    }
}
