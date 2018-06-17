using System.CodeDom;

namespace SmartHub.DbMigration.Migration.Sprint000
{
    using FluentMigrator;

    [SmartHubMigration(1, 0, 3, "daolam", "Add Ticket tables")]
    public class M003_AddTicketTables : Migration
    {
        private const string Table_Connection = "Connection";
        private const string Table_Connection_Column_Id = "Id";

        private const string Table_MagentoTicket = "TicketMagento";
        private const string Table_MagentoTicket_Column_Id = "Id";
        private const string Table_MagentoTicket_Column_MagentoId = "MagentoId";
        private const string Table_MagentoTicket_Column_ConnectionId = "ConnectionId";
        private const string Table_MagentoTicket_Column_Status = "Status";
        private const string Table_MagentoTicket_Column_Type = "Type";
        private const string Table_MagentoTicket_Column_Subject = "Subject";
        private const string Table_MagentoTicket_Column_ItemId = "ItemId";
        private const string Table_MagentoTicket_Column_CreatorId = "CreatorId";
        private const string Table_MagentoTicket_Column_CreatorEmail = "CreatorEmail";
        private const string Table_MagentoTicket_Column_RecipientId = "RecipientId";
        private const string Table_MagentoTicket_Column_CreatedDate = "CreatedDate";
        private const string Table_MagentoTicket_Column_LastModifiedDate = "LastModifiedDate";
        private const string Table_MagentoTicket_Column_Note = "Note";
        private const string Table_MagentoTicket_Column_LastSynchronizedDate = "LastSynchronizedDate";
        private const string Table_MagentoTicket_Column_SyncStatus = "SyncStatus";
        private const string Table_MagentoTicket_Column_SyncErrorMessage = "SyncErrorMessage";
        
        private const string Table_MagentoMessage = "MessageMagento";
        private const string Table_MagentoMessage_Column_Id = "Id";
        private const string Table_MagentoMessage_Column_MagentoId = "MagentoId";
        private const string Table_MagentoMessage_Column_SenderId = "SenderId";
        private const string Table_MagentoMessage_Column_SenderEmail = "SenderEmail";
        private const string Table_MagentoMessage_Column_TicketId = "TicketId";
        private const string Table_MagentoMessage_Column_Message = "Message";
        private const string Table_MagentoMessage_Column_CreatedDate = "CreatedDate";
        private const string Table_MagentoMessage_Column_LastModifiedDate = "LastModifiedDate";
        private const string Table_MagentoMessage_Column_Note = "Note";
        private const string Table_MagentoMessage_Column_LastSynchronizedDate = "LastSynchronizedDate";
        private const string Table_MagentoMessage_Column_SyncStatus = "SyncStatus";
        private const string Table_MagentoMessage_Column_SyncErrorMessage = "SyncErrorMessage";

        private const string Table_EbayTicket = "TicketEbay";
        private const string Table_EbayTicket_Column_Id = "Id";
        private const string Table_EbayTicket_Column_EbayId = "EbayId";
        private const string Table_EbayTicket_Column_ConnectionId = "ConnectionId";
        private const string Table_EbayTicket_Column_Status = "Status";
        private const string Table_EbayTicket_Column_Type = "Type";
        private const string Table_EbayTicket_Column_Subject = "Subject";
        private const string Table_EbayTicket_Column_ItemId = "ItemId";
        private const string Table_EbayTicket_Column_CreatorId = "CreatorId";
        private const string Table_EbayTicket_Column_CreatorEmail = "CreatorEmail";
        private const string Table_EbayTicket_Column_RecipientId = "RecipientId";
        private const string Table_EbayTicket_Column_CreatedDate = "CreatedDate";
        private const string Table_EbayTicket_Column_LastModifiedDate = "LastModifiedDate";
        private const string Table_EbayTicket_Column_Note = "Note";
        private const string Table_EbayTicket_Column_LastSynchronizedDate = "LastSynchronizedDate";
        private const string Table_EbayTicket_Column_SyncStatus = "SyncStatus";
        private const string Table_EbayTicket_Column_SyncErrorMessage = "SyncErrorMessage";

        private const string Table_EbayMessage = "MessageEbay";
        private const string Table_EbayMessage_Column_Id = "Id";
        private const string Table_EbayMessage_Column_EbayId = "EbayId";
        private const string Table_EbayMessage_Column_SenderId = "SenderId";
        private const string Table_EbayMessage_Column_SenderEmail = "SenderEmail";
        private const string Table_EbayMessage_Column_TicketId = "TicketId";
        private const string Table_EbayMessage_Column_Message = "Message";
        private const string Table_EbayMessage_Column_CreatedDate = "CreatedDate";
        private const string Table_EbayMessage_Column_LastModifiedDate = "LastModifiedDate";
        private const string Table_EbayMessage_Column_Note = "Note";
        private const string Table_EbayMessage_Column_LastSynchronizedDate = "LastSynchronizedDate";
        private const string Table_EbayMessage_Column_SyncStatus = "SyncStatus";
        private const string Table_EbayMessage_Column_SyncErrorMessage = "SyncErrorMessage";

        private const string Table_EbayMagento_Ticket = "TicketEbayMagento";
        private const string Table_EbayMagento_Ticket_Column_Id = "Id";
        private const string Table_EbayMagento_Ticket_Column_IdEbay = "IdEbay";
        private const string Table_EbayMagento_Ticket_Column_IdMagento = "IdMagento";

        private const string Table_EbayMagento_Message = "MessageEbayMagento";
        private const string Table_EbayMagento_Message_Column_Id = "Id";
        private const string Table_EbayMagento_Message_Column_IdEbay = "IdEbay";
        private const string Table_EbayMagento_Message_Column_IdMagento = "IdMagento";

        public override void Down()
        {
            Delete.Table(Table_EbayMagento_Ticket);

            Delete.Table(Table_EbayMagento_Message);

            Delete.Table(Table_MagentoMessage);

            Delete.Table(Table_MagentoTicket);

            Delete.Table(Table_EbayMessage);

            Delete.Table(Table_EbayTicket);
        }

        public override void Up()
        {
            Create.Table(Table_MagentoTicket)
                .WithColumn(Table_MagentoTicket_Column_Id).AsInt32().PrimaryKey().Identity()
                .WithColumn(Table_MagentoTicket_Column_MagentoId).AsString(255).Nullable()
                .WithColumn(Table_MagentoTicket_Column_ConnectionId).AsInt32().NotNullable().ForeignKey($"FK_{Table_MagentoTicket}_{Table_Connection}_{Table_MagentoTicket_Column_ConnectionId}", Table_Connection, Table_Connection_Column_Id)
                .WithColumn(Table_MagentoTicket_Column_Status).AsInt32().NotNullable()
                .WithColumn(Table_MagentoTicket_Column_Type).AsString(255).NotNullable()
                .WithColumn(Table_MagentoTicket_Column_Subject).AsString(1000).NotNullable()
                .WithColumn(Table_MagentoTicket_Column_ItemId).AsString(255).Nullable()
                .WithColumn(Table_MagentoTicket_Column_CreatorId).AsString(255).NotNullable()
                .WithColumn(Table_MagentoTicket_Column_CreatorEmail).AsString(255).NotNullable()
                .WithColumn(Table_MagentoTicket_Column_RecipientId).AsString(255).NotNullable()
                .WithColumn(Table_MagentoTicket_Column_CreatedDate).AsDateTime().NotNullable()
                .WithColumn(Table_MagentoTicket_Column_LastModifiedDate).AsDateTime().Nullable()
                .WithColumn(Table_MagentoTicket_Column_Note).AsMaxString().Nullable()
                .WithColumn(Table_MagentoTicket_Column_LastSynchronizedDate).AsDateTime().Nullable()
                .WithColumn(Table_MagentoTicket_Column_SyncStatus).AsInt32().NotNullable()
                .WithColumn(Table_MagentoTicket_Column_SyncErrorMessage).AsMaxString().Nullable();

            Create.Table(Table_MagentoMessage)
                .WithColumn(Table_MagentoMessage_Column_Id).AsInt32().PrimaryKey().Identity()
                .WithColumn(Table_MagentoMessage_Column_MagentoId).AsString(255).Nullable()
                .WithColumn(Table_MagentoMessage_Column_SenderId).AsString(255).NotNullable()
                .WithColumn(Table_MagentoMessage_Column_SenderEmail).AsString(255).NotNullable()
                .WithColumn(Table_MagentoMessage_Column_TicketId).AsInt32().NotNullable().ForeignKey($"FK_{Table_MagentoMessage}_{Table_MagentoTicket}_{Table_MagentoMessage_Column_TicketId}", Table_MagentoTicket, Table_MagentoTicket_Column_Id)
                .WithColumn(Table_MagentoMessage_Column_Message).AsMaxString().Nullable()
                .WithColumn(Table_MagentoMessage_Column_CreatedDate).AsDateTime().NotNullable()
                .WithColumn(Table_MagentoMessage_Column_LastModifiedDate).AsDateTime().Nullable()
                .WithColumn(Table_MagentoMessage_Column_Note).AsMaxString().Nullable()
                .WithColumn(Table_MagentoMessage_Column_LastSynchronizedDate).AsDateTime().Nullable()
                .WithColumn(Table_MagentoMessage_Column_SyncStatus).AsInt32().NotNullable()
                .WithColumn(Table_MagentoMessage_Column_SyncErrorMessage).AsMaxString().Nullable();

            Create.Table(Table_EbayTicket)
                .WithColumn(Table_EbayTicket_Column_Id).AsInt32().PrimaryKey().Identity()
                .WithColumn(Table_EbayTicket_Column_EbayId).AsString(255).Nullable()
                .WithColumn(Table_EbayTicket_Column_ConnectionId).AsInt32().NotNullable().ForeignKey($"FK_{Table_EbayTicket}_{Table_Connection}_{Table_EbayTicket_Column_ConnectionId}", Table_Connection, Table_Connection_Column_Id)
                .WithColumn(Table_EbayTicket_Column_Status).AsInt32().NotNullable()
                .WithColumn(Table_EbayTicket_Column_Type).AsString(255).NotNullable()
                .WithColumn(Table_EbayTicket_Column_Subject).AsString(1000).NotNullable()
                .WithColumn(Table_EbayTicket_Column_ItemId).AsString(255).Nullable()
                .WithColumn(Table_EbayTicket_Column_CreatorId).AsString(255).NotNullable()
                .WithColumn(Table_EbayTicket_Column_CreatorEmail).AsString(255).NotNullable()
                .WithColumn(Table_EbayTicket_Column_RecipientId).AsString(255).NotNullable()
                .WithColumn(Table_EbayTicket_Column_CreatedDate).AsDateTime().NotNullable()
                .WithColumn(Table_EbayTicket_Column_LastModifiedDate).AsDateTime().Nullable()
                .WithColumn(Table_EbayTicket_Column_Note).AsMaxString().Nullable()
                .WithColumn(Table_EbayTicket_Column_LastSynchronizedDate).AsDateTime().Nullable()
                .WithColumn(Table_EbayTicket_Column_SyncStatus).AsInt32().NotNullable()
                .WithColumn(Table_EbayTicket_Column_SyncErrorMessage).AsMaxString().Nullable();

            Create.Table(Table_EbayMessage)
                .WithColumn(Table_EbayMessage_Column_Id).AsInt32().PrimaryKey().Identity()
                .WithColumn(Table_EbayMessage_Column_EbayId).AsString(255).Nullable()
                .WithColumn(Table_EbayMessage_Column_SenderId).AsString(255).NotNullable()
                .WithColumn(Table_EbayMessage_Column_SenderEmail).AsString(255).NotNullable()
                .WithColumn(Table_EbayMessage_Column_TicketId).AsInt32().NotNullable().ForeignKey($"FK_{Table_EbayMessage}_{Table_EbayTicket}_{Table_EbayMessage_Column_TicketId}", Table_EbayTicket, Table_EbayTicket_Column_Id)
                .WithColumn(Table_EbayMessage_Column_Message).AsMaxString().Nullable()
                .WithColumn(Table_EbayMessage_Column_CreatedDate).AsDateTime().NotNullable()
                .WithColumn(Table_EbayMessage_Column_LastModifiedDate).AsDateTime().Nullable()
                .WithColumn(Table_EbayMessage_Column_Note).AsMaxString().Nullable()
                .WithColumn(Table_EbayMessage_Column_LastSynchronizedDate).AsDateTime().Nullable()
                .WithColumn(Table_EbayMessage_Column_SyncStatus).AsInt32().NotNullable()
                .WithColumn(Table_EbayMessage_Column_SyncErrorMessage).AsMaxString().Nullable();

            Create.Table(Table_EbayMagento_Ticket)
                .WithColumn(Table_EbayMagento_Ticket_Column_Id).AsInt32().PrimaryKey().Identity()
                .WithColumn(Table_EbayMagento_Ticket_Column_IdEbay).AsInt32().NotNullable().ForeignKey($"FK_{Table_EbayMagento_Ticket}_{Table_EbayTicket}_{Table_EbayMagento_Ticket_Column_IdEbay}", Table_EbayTicket, Table_EbayTicket_Column_Id)
                .WithColumn(Table_EbayMagento_Ticket_Column_IdMagento).AsInt32().NotNullable().ForeignKey($"FK_{Table_EbayMagento_Ticket}_{Table_MagentoTicket}_{Table_EbayMagento_Ticket_Column_IdMagento}", Table_MagentoTicket, Table_MagentoTicket_Column_Id);

            Create.Table(Table_EbayMagento_Message)
                .WithColumn(Table_EbayMagento_Message_Column_Id).AsInt32().PrimaryKey().Identity()
                .WithColumn(Table_EbayMagento_Message_Column_IdEbay).AsInt32().NotNullable().ForeignKey($"FK_{Table_EbayMagento_Message}_{Table_EbayMessage}_{Table_EbayMagento_Message_Column_IdEbay}", Table_EbayMessage, Table_EbayMessage_Column_Id)
                .WithColumn(Table_EbayMagento_Message_Column_IdMagento).AsInt32().NotNullable().ForeignKey($"FK_{Table_EbayMagento_Message}_{Table_MagentoMessage}_{Table_EbayMagento_Message_Column_IdMagento}", Table_MagentoMessage, Table_MagentoMessage_Column_Id);
        }
    }
}
