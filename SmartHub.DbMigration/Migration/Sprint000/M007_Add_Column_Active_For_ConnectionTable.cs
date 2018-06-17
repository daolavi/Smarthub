namespace SmartHub.DbMigration.Migration.Sprint000
{
    using FluentMigrator;

    [SmartHubMigration(1, 0, 7, "daolam", "Add Column Active for Connection")]
    public class M007_Add_Column_Active_For_ConnectionTable : Migration
    {
        private const string Table_Connection = "Connection";
        private const string Table_UserChannel = "UserChannel";

        public override void Down()
        {
            Delete.Column("IsActive").FromTable(Table_Connection);
            Delete.Column("IsActive").FromTable(Table_UserChannel);
        }

        public override void Up()
        {
            Alter.Table(Table_Connection).AddColumn("IsActive").AsBoolean().NotNullable().WithDefaultValue(true);
            Alter.Table(Table_UserChannel).AddColumn("IsActive").AsBoolean().NotNullable().WithDefaultValue(true);
        }
    }
}
