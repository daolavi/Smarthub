namespace SmartHub.DbMigration.Migration.Sprint000
{
    using FluentMigrator;

    [SmartHubMigration(1, 0, 10, "daolam", "Add tables for EmailMagento")]
    public class M010_Add_Tables_For_EmailMagento : Migration
    {
        private const string Table_ChannelEmail = "ChannelEmail";

        private const string Table_Connection = "Connection";
        private const string Table_Connection_Column_Id = "Id";

        private const string Table_EmailTicket = "TicketEmail";
        private const string Table_EmailTicket_Column_Id = "Id";
        private const string Table_EmailTicket_Column_EmailId = "EmailId";
        private const string Table_EmailTicket_Column_ConnectionId = "ConnectionId";
        private const string Table_EmailTicket_Column_Status = "Status";
        private const string Table_EmailTicket_Column_Type = "Type";
        private const string Table_EmailTicket_Column_Subject = "Subject";
        private const string Table_EmailTicket_Column_ItemId = "ItemId";
        private const string Table_EmailTicket_Column_CreatorId = "CreatorId";
        private const string Table_EmailTicket_Column_CreatorEmail = "CreatorEmail";
        private const string Table_EmailTicket_Column_RecipientId = "RecipientId";
        private const string Table_EmailTicket_Column_CreatedDate = "CreatedDate";
        private const string Table_EmailTicket_Column_LastModifiedDate = "LastModifiedDate";
        private const string Table_EmailTicket_Column_Note = "Note";
        private const string Table_EmailTicket_Column_LastSynchronizedDate = "LastSynchronizedDate";
        private const string Table_EmailTicket_Column_SyncStatus = "SyncStatus";
        private const string Table_EmailTicket_Column_SyncErrorMessage = "SyncErrorMessage";

        private const string Table_EmailMessage = "MessageEmail";
        private const string Table_EmailMessage_Column_Id = "Id";
        private const string Table_EmailMessage_Column_EmailId = "EmailId";
        private const string Table_EmailMessage_Column_SenderId = "SenderId";
        private const string Table_EmailMessage_Column_SenderEmail = "SenderEmail";
        private const string Table_EmailMessage_Column_TicketId = "TicketId";
        private const string Table_EmailMessage_Column_Message = "Message";
        private const string Table_EmailMessage_Column_CreatedDate = "CreatedDate";
        private const string Table_EmailMessage_Column_LastModifiedDate = "LastModifiedDate";
        private const string Table_EmailMessage_Column_Note = "Note";
        private const string Table_EmailMessage_Column_LastSynchronizedDate = "LastSynchronizedDate";
        private const string Table_EmailMessage_Column_SyncStatus = "SyncStatus";
        private const string Table_EmailMessage_Column_SyncErrorMessage = "SyncErrorMessage";

        private const string Table_EmailMagento_Ticket = "TicketEmailMagento";
        private const string Table_EmailMagento_Ticket_Column_Id = "Id";
        private const string Table_EmailMagento_Ticket_Column_IdEmail = "IdEmail";
        private const string Table_EmailMagento_Ticket_Column_IdMagento = "IdMagento";

        private const string Table_EmailMagento_Message = "MessageEmailMagento";
        private const string Table_EmailMagento_Message_Column_Id = "Id";
        private const string Table_EmailMagento_Message_Column_IdEmail = "IdEmail";
        private const string Table_EmailMagento_Message_Column_IdMagento = "IdMagento";

        private const string Table_MagentoTicket = "TicketMagento";
        private const string Table_MagentoTicket_Column_Id = "Id";

        private const string Table_MagentoMessage = "MessageMagento";
        private const string Table_MagentoMessage_Column_Id = "Id";

        public override void Down()
        {
            Delete.Column("LastSyncedDate").FromTable(Table_ChannelEmail);
            Delete.Table(Table_EmailMagento_Ticket);
            Delete.Table(Table_EmailMagento_Message);
            Delete.Table(Table_EmailMessage);
            Delete.Table(Table_EmailTicket);
        }

        public override void Up()
        {
            Create.Table(Table_EmailTicket)
                 .WithColumn(Table_EmailTicket_Column_Id).AsInt32().PrimaryKey().Identity()
                 .WithColumn(Table_EmailTicket_Column_EmailId).AsString(255).Nullable()
                 .WithColumn(Table_EmailTicket_Column_ConnectionId).AsInt32().NotNullable().ForeignKey($"FK_{Table_EmailTicket}_{Table_Connection}_{Table_EmailTicket_Column_ConnectionId}", Table_Connection, Table_Connection_Column_Id)
                 .WithColumn(Table_EmailTicket_Column_Status).AsInt32().NotNullable()
                 .WithColumn(Table_EmailTicket_Column_Type).AsString(255).NotNullable()
                 .WithColumn(Table_EmailTicket_Column_Subject).AsString(1000).NotNullable()
                 .WithColumn(Table_EmailTicket_Column_ItemId).AsString(255).Nullable()
                 .WithColumn(Table_EmailTicket_Column_CreatorId).AsString(255).NotNullable()
                 .WithColumn(Table_EmailTicket_Column_CreatorEmail).AsString(255).NotNullable()
                 .WithColumn(Table_EmailTicket_Column_RecipientId).AsString(255).NotNullable()
                 .WithColumn(Table_EmailTicket_Column_CreatedDate).AsDateTime().NotNullable()
                 .WithColumn(Table_EmailTicket_Column_LastModifiedDate).AsDateTime().Nullable()
                 .WithColumn(Table_EmailTicket_Column_Note).AsMaxString().Nullable()
                 .WithColumn(Table_EmailTicket_Column_LastSynchronizedDate).AsDateTime().Nullable()
                 .WithColumn(Table_EmailTicket_Column_SyncStatus).AsInt32().NotNullable()
                 .WithColumn(Table_EmailTicket_Column_SyncErrorMessage).AsMaxString().Nullable();

            Create.Table(Table_EmailMessage)
                .WithColumn(Table_EmailMessage_Column_Id).AsInt32().PrimaryKey().Identity()
                .WithColumn(Table_EmailMessage_Column_EmailId).AsString(255).Nullable()
                .WithColumn(Table_EmailMessage_Column_SenderId).AsString(255).NotNullable()
                .WithColumn(Table_EmailMessage_Column_SenderEmail).AsString(255).NotNullable()
                .WithColumn(Table_EmailMessage_Column_TicketId).AsInt32().NotNullable().ForeignKey($"FK_{Table_EmailMessage}_{Table_EmailTicket}_{Table_EmailMessage_Column_TicketId}", Table_EmailTicket, Table_EmailTicket_Column_Id)
                .WithColumn(Table_EmailMessage_Column_Message).AsMaxString().Nullable()
                .WithColumn(Table_EmailMessage_Column_CreatedDate).AsDateTime().NotNullable()
                .WithColumn(Table_EmailMessage_Column_LastModifiedDate).AsDateTime().Nullable()
                .WithColumn(Table_EmailMessage_Column_Note).AsMaxString().Nullable()
                .WithColumn(Table_EmailMessage_Column_LastSynchronizedDate).AsDateTime().Nullable()
                .WithColumn(Table_EmailMessage_Column_SyncStatus).AsInt32().NotNullable()
                .WithColumn(Table_EmailMessage_Column_SyncErrorMessage).AsMaxString().Nullable();

            Create.Table(Table_EmailMagento_Ticket)
               .WithColumn(Table_EmailMagento_Ticket_Column_Id).AsInt32().PrimaryKey().Identity()
               .WithColumn(Table_EmailMagento_Ticket_Column_IdEmail).AsInt32().NotNullable().ForeignKey($"FK_{Table_EmailMagento_Ticket}_{Table_EmailTicket}_{Table_EmailMagento_Ticket_Column_IdEmail}", Table_EmailTicket, Table_EmailTicket_Column_Id)
               .WithColumn(Table_EmailMagento_Ticket_Column_IdMagento).AsInt32().NotNullable().ForeignKey($"FK_{Table_EmailMagento_Ticket}_{Table_MagentoTicket}_{Table_EmailMagento_Ticket_Column_IdMagento}", Table_MagentoTicket, Table_MagentoTicket_Column_Id);

            Create.Table(Table_EmailMagento_Message)
                .WithColumn(Table_EmailMagento_Message_Column_Id).AsInt32().PrimaryKey().Identity()
                .WithColumn(Table_EmailMagento_Message_Column_IdEmail).AsInt32().NotNullable().ForeignKey($"FK_{Table_EmailMagento_Message}_{Table_EmailMessage}_{Table_EmailMagento_Message_Column_IdEmail}", Table_EmailMessage, Table_EmailMessage_Column_Id)
                .WithColumn(Table_EmailMagento_Message_Column_IdMagento).AsInt32().NotNullable().ForeignKey($"FK_{Table_EmailMagento_Message}_{Table_MagentoMessage}_{Table_EmailMagento_Message_Column_IdMagento}", Table_MagentoMessage, Table_MagentoMessage_Column_Id);

            Alter.Table(Table_ChannelEmail).AddColumn("LastSyncedDate").AsDateTime().Nullable();
        }
    }
}
