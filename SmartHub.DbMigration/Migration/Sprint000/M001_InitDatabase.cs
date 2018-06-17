using System.ComponentModel;

namespace SmartHub.DbMigration.Migration.Sprint000
{
    using FluentMigrator;

    [SmartHubMigration(1, 0, 1, "daolam", "Init Database")]
    public class M001_InitDatabase : Migration
    {
        private const string Table_User = "User";
        private const string Table_User_Column_Id = "Id";
        private const string Table_User_Column_FirstName = "Firstname";
        private const string Table_User_Column_LastName = "LastName";
        private const string Table_User_Column_Account = "Account";
        private const string Table_User_Column_Password = "Password";

        private const string Table_ChannelEbay = "ChannelEbay";
        private const string Table_ChannelEbay_Column_Id = "Id";
        private const string Table_ChannelEbay_Column_Token = "Token";
        private const string Table_ChannelEbay_Column_ExpiredDate = "ExpiredDate";
        private const string Table_ChannelEbay_Column_CreatedDate = "CreatedDate";

        private const string Table_ChannelMagento = "ChannelMagento";
        private const string Table_ChannelMagento_Column_Id = "Id";
        private const string Table_ChannelMagento_Column_CreatedDate = "CreatedDate";

        private const string Table_ChannelAmazon = "ChannelAmazon";
        private const string Table_ChannelAmazon_Column_Id = "Id";
        private const string Table_ChannelAmazon_Column_CreatedDate = "CreatedDate";

        private const string Table_ChannelEmail = "ChannelEmail";
        private const string Table_ChannelEmail_Column_Id = "Id";
        private const string Table_ChannelEmail_Column_CreatedDate = "CreatedDate";

        private const string Table_UserChannel = "UserChannel";
        private const string Table_UserChannel_Column_Id = "Id";
        private const string Table_UserChannel_Column_UserId = "UserId";
        private const string Table_UserChannel_Column_ChannelId = "ChannelId";
        private const string Table_UserChannel_Column_ChannelType = "ChannelType";

        private const string Table_Connection = "Connection";
        private const string Table_Connection_Column_Id = "Id";
        private const string Table_Connection_Column_UserChannelSource = "UserChannelSource";
        private const string Table_Connection_Column_UserChannelTarget = "UserChannelTarget";
        private const string Table_Connection_Column_CreatedDate = "CreateDate";
        private const string Table_Connection_Column_LastSyncDate = "LastSyncedDate";
        private const string Table_Connection_Column_NextSyncDate = "NextSyncDate";
        private const string Table_Connection_Column_Status = "Status";
        private const string Table_Connection_Column_Counter = "Counter";

        public override void Down()
        {
            Delete.Table(Table_Connection);

            Delete.Table(Table_UserChannel);

            Delete.Table(Table_ChannelEbay);

            Delete.Table(Table_ChannelMagento);

            Delete.Table(Table_ChannelAmazon);

            Delete.Table(Table_ChannelEmail);

            Delete.Table(Table_User);
        }

        public override void Up()
        {
            Create.Table(Table_User)
                .WithColumn(Table_User_Column_Id).AsInt32().PrimaryKey().Identity()
                .WithColumn(Table_User_Column_FirstName).AsString(255).NotNullable()
                .WithColumn(Table_User_Column_LastName).AsString(255).NotNullable()
                .WithColumn(Table_User_Column_Account).AsString(255).NotNullable()
                .WithColumn(Table_User_Column_Password).AsString(255).NotNullable();

            Create.Table(Table_ChannelEbay)
                .WithColumn(Table_ChannelEbay_Column_Id).AsInt32().PrimaryKey().Identity()
                .WithColumn(Table_ChannelEbay_Column_Token).AsMaxString().Nullable()
                .WithColumn(Table_ChannelEbay_Column_ExpiredDate).AsDateTime().Nullable()
                .WithColumn(Table_ChannelEbay_Column_CreatedDate).AsDateTime().NotNullable();

            Create.Table(Table_ChannelMagento)
                .WithColumn(Table_ChannelMagento_Column_Id).AsInt32().PrimaryKey().Identity()
                .WithColumn(Table_ChannelMagento_Column_CreatedDate).AsDateTime().NotNullable();

            Create.Table(Table_ChannelAmazon)
                .WithColumn(Table_ChannelAmazon_Column_Id).AsInt32().PrimaryKey().Identity()
                .WithColumn(Table_ChannelAmazon_Column_CreatedDate).AsDateTime().NotNullable();

            Create.Table(Table_ChannelEmail)
                .WithColumn(Table_ChannelEmail_Column_Id).AsInt32().PrimaryKey().Identity()
                .WithColumn(Table_ChannelEmail_Column_CreatedDate).AsDateTime().NotNullable();

            Create.Table(Table_UserChannel)
                .WithColumn(Table_UserChannel_Column_Id).AsInt32().PrimaryKey().Identity()
                .WithColumn(Table_UserChannel_Column_UserId).AsInt32().NotNullable().ForeignKey($"FK_{Table_UserChannel}_{Table_User}_{Table_UserChannel_Column_UserId}", Table_User, Table_User_Column_Id)
                .WithColumn(Table_UserChannel_Column_ChannelId).AsInt32().NotNullable()
                .WithColumn(Table_UserChannel_Column_ChannelType).AsInt32().NotNullable();

            Create.Table(Table_Connection)
                .WithColumn(Table_Connection_Column_Id).AsInt32().PrimaryKey().Identity()
                .WithColumn(Table_Connection_Column_UserChannelSource).AsInt32().NotNullable().ForeignKey($"FK_{Table_Connection}_{Table_UserChannel}_{Table_UserChannel_Column_Id}_Source", Table_UserChannel, Table_UserChannel_Column_Id)
                .WithColumn(Table_Connection_Column_UserChannelTarget).AsInt32().NotNullable().ForeignKey($"FK_{Table_Connection}_{Table_UserChannel}_{Table_UserChannel_Column_Id}_Target", Table_UserChannel, Table_UserChannel_Column_Id)
                .WithColumn(Table_Connection_Column_CreatedDate).AsDateTime().NotNullable()
                .WithColumn(Table_Connection_Column_LastSyncDate).AsDateTime().Nullable()
                .WithColumn(Table_Connection_Column_NextSyncDate).AsDateTime().Nullable()
                .WithColumn(Table_Connection_Column_Status).AsInt32().NotNullable()
                .WithColumn(Table_Connection_Column_Counter).AsInt32().NotNullable();
        }
    }
}
