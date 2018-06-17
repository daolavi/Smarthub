using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using eBay.Service.Core.Soap;
using SmartHub.Helper;
using SmartHub.Services;
using SmartHub.Shared.Enums;
using SmartHub.Channels.Ebay;
using SmartHub.Channels.Magento;
using SmartHub.Channels.Magento.Models;
using SmartHub.Services.Models;

namespace SmartHub.Web.Processors.SyncProcessor
{
    public interface IEbayMagentoProcessor
    {
        void Proceed(Services.Models.Connection connection);
    }

    public class EbayMagentoProcessor : IEbayMagentoProcessor
    {
        private static int ebayMagentoTaskIntervalInMinutes = ConfigHelper.AppSettings<int>("EbayMagentoTaskIntervalInMinutes", 10);

        private IMapper mapper;

        private IEbayService ebayService;

        private IMagentoService magentoService;

        private IConnectionService connectionService;

        private IUserChannelService userChannelService;

        private IChannelEbayService channelEbayService;

        private IChannelMagentoService channelMagentoService;

        private ITicketEbayService ticketEbayService;

        private IMessageEbayService messageEbayService;

        private ITicketMagentoService ticketMagentoService;

        private IMessageMagentoService messageMagentoService;

        private ITicketEbayMagentoService ticketEbayMagentoService;

        private IMessageEbayMagentoService messageEbayMagentoService;

        public EbayMagentoProcessor(IMapper mapper,
                                    IEbayService ebayService, 
                                    IMagentoService magentoService,
                                    IConnectionService connectionService, 
                                    IUserChannelService userChannelService,
                                    IChannelEbayService channelEbayService,
                                    IChannelMagentoService channelMagentoService,
                                    ITicketEbayService ticketEbayService,
                                    IMessageEbayService messageEbayService,
                                    ITicketMagentoService ticketMagentoService,
                                    IMessageMagentoService messageMagentoService,
                                    ITicketEbayMagentoService ticketEbayMagentoService,
                                    IMessageEbayMagentoService messageEbayMagentoService)
        {
            this.mapper = mapper;
            this.ebayService = ebayService;
            this.magentoService = magentoService;
            this.connectionService = connectionService;
            this.userChannelService = userChannelService;
            this.channelEbayService = channelEbayService;
            this.channelMagentoService = channelMagentoService;
            this.ticketEbayService = ticketEbayService;
            this.messageEbayService = messageEbayService;
            this.ticketMagentoService = ticketMagentoService;
            this.messageMagentoService = messageMagentoService;
            this.ticketEbayMagentoService = ticketEbayMagentoService;
            this.messageEbayMagentoService = messageEbayMagentoService;
        }

        public void Proceed(Services.Models.Connection connection)
        {
            TextBuffer.WriteLine("EbayMagentoProcessor - Start");
            TextBuffer.WriteLine("EbayMagentoProcessor - Proceed Connection " + connection.Id);

            var syncTime = DateTime.UtcNow;

            connection.Status = ConnectionStatus.Synchronizing;
            connectionService.Update(connection);

            var userChannelEbay = userChannelService.GetUserChannel(connection.UserChannelSource);
            var userChannelMagento = userChannelService.GetUserChannel(connection.UserChannelTarget);

            var channelEbay = channelEbayService.GetChannel(userChannelEbay.ChannelId);
            var channelMagento = channelMagentoService.GetChannel(userChannelMagento.ChannelId);
            var channelMagento_TimeZoneOffset =
                TimeZoneInfo.GetSystemTimeZones()
                    .FirstOrDefault(x => x.DisplayName == channelMagento.TimeZoneDisplayName)
                    .BaseUtcOffset;

            if (!ebayService.IsValidToken(channelEbay.Token).Result)
            {
                connection.Status = ConnectionStatus.Error;
                connection.Message = "Invalid Ebay Token";
                connection.LastSyncedDate = syncTime;
                connection.NextSyncedDate = connection.LastSyncedDate.Value.AddMinutes(ebayMagentoTaskIntervalInMinutes);
                connection.Counter++;
                connectionService.Update(connection);
                return;
            }

            var magentoTokenCall = magentoService.GetToken(channelMagento.Host, channelMagento.Username, channelMagento.Password);
            if (magentoTokenCall.HasError)
            {
                connection.Status = ConnectionStatus.Error;
                connection.Message = "Invalid Magento Token";
                connection.LastSyncedDate = syncTime;
                connection.NextSyncedDate = connection.LastSyncedDate.Value.AddMinutes(ebayMagentoTaskIntervalInMinutes);
                connection.Counter++;
                connectionService.Update(connection);
                return;
            }
            var magentoToken = magentoTokenCall.Result;


            #region Ebay to Magento
            //1. Pull Ebay MemberMessage and persist to local DB
            #region Pull MemberMessage from Ebay
            var lastSyncDate_Ebay_MemberMessage = channelEbay.LastSyncedDateMemberMessage ?? DateTime.UtcNow;
            var ebayMessageApi = ebayService.GetMemberMessage(channelEbay.Token, lastSyncDate_Ebay_MemberMessage, DateTime.UtcNow, MessageTypeCodeType.All,MessageStatusTypeCodeType.Unanswered);
            var ebayTickets = new List<TicketEbay>();
            var ebayMessages = new List<MessageEbay>();
            if (ebayMessageApi.HasError)
            {
                channelEbay.Message += ebayMessageApi.Error;
            }
            else
            {
                var memberMessages = ebayMessageApi.Result;
                foreach (var memberMessage in memberMessages)
                {
                    var ticketEbay = GetOrCreateTicketEbay(memberMessage, connection, syncTime);
                    ebayTickets.Add(ticketEbay);

                    var messageEbay = CreateMessageEbay(memberMessage, ticketEbay, syncTime);
                    ebayMessages.Add(messageEbay);
                }
            }

            channelEbay.LastSyncedDateMemberMessage = syncTime;
            channelEbayService.Update(channelEbay);
            #endregion

            //2. Transfer Ebay MemberMessage to Magento local DB
            #region Transfer Ebay MemberMessage to Magento local DB
            foreach (var ebayTicket in ebayTickets)
            {
                var magentoTicket = mapper.Map<TicketMagento>(ebayTicket);
                magentoTicket.Id = ticketMagentoService.CreateTicket(magentoTicket);

                ticketEbayMagentoService.Create(ebayTicket.Id, magentoTicket.Id);
                var ebayMessagesFilterByTicketId = ebayMessages.Where(x => x.TicketId == ebayTicket.Id);
                foreach (var ebayMessage in ebayMessagesFilterByTicketId)
                {
                    var magentoMessage = mapper.Map<MessageMagento>(ebayMessage);
                    magentoMessage.TicketId = magentoTicket.Id;
                    magentoMessage.Id = messageMagentoService.CreateMessage(magentoMessage);

                    messageEbayMagentoService.Create(ebayMessage.Id, magentoMessage.Id);
                }

            }
            #endregion

            //3. Push Magento Ticket and Message from local DB to remote site
            #region Push Magento Ticket and Message to Remote Site
            var magentoTickets = ticketMagentoService.GetMagentoTicketsForSyncByConnectionId(connection.Id);
            foreach (var magentoTicket in magentoTickets)
            {
                var createTicketRequest = CreateTicketMagentoRequest(magentoTicket, connection, channelMagento);
                var result = magentoService.CreateTicket(channelMagento.Host,magentoToken,createTicketRequest);
                if (result.HasError)
                {
                    magentoTicket.SyncStatus = SyncStatus.Error;
                    magentoTicket.SyncErrorMessage = result.Error;
                }
                else
                {
                    magentoTicket.SyncStatus = SyncStatus.Ok;
                    magentoTicket.SyncErrorMessage = string.Empty;
                    magentoTicket.MagentoId = result.Result.ToString();

                    var magentoMessages = messageMagentoService.GetMessagesForSyncByTicketId(magentoTicket.Id);
                    foreach (var magentoMessage in magentoMessages)
                    {
                        var sendMessageRequest = SendMessageMagentoRequest(magentoMessage, magentoTicket);
                        var sendMsgResult = magentoService.SendMessage(channelMagento.Host, magentoToken, sendMessageRequest);
                        if (sendMsgResult.HasError)
                        {
                            magentoMessage.SyncStatus = SyncStatus.Error;
                            magentoMessage.SyncErrorMessage = sendMsgResult.Error;
                        }
                        else
                        {
                            magentoMessage.SyncStatus = SyncStatus.Ok;
                            magentoMessage.SyncErrorMessage = string.Empty;
                            magentoMessage.MagentoId = sendMsgResult.Result.ToString();
                        }
                        magentoMessage.LastSynchronizedDate = syncTime;
                        messageMagentoService.UpdateMessage(magentoMessage);
                    }
                }
                magentoTicket.LastSynchronizedDate = syncTime;

                ticketMagentoService.UpdateTicket(magentoTicket);
            }
            #endregion

            #endregion

            #region Magento to Ebay

            //4. Pull Ticket and Message from Magento
            #region Pull Ticket/Message from Magento
            var lastSyncDate_Magento_Ticket = channelMagento.LastSyncedDateTicket ?? DateTime.UtcNow;
            var getTicketsMagentoRequest = new GetTicketsRequest()
            {
                connection_id = connection.Id.ToString(),
                from = lastSyncDate_Magento_Ticket.ToString(Shared.Constants.DateTimeFormat_Magento),
                to = syncTime.ToString(Shared.Constants.DateTimeFormat_Magento),
            };
            var getTicketsResult = magentoService.GetTickets(channelMagento.Host, magentoToken, getTicketsMagentoRequest);
            if (getTicketsResult.HasError)
            {
                channelMagento.Message += getTicketsResult.Error;
            }
            else
            {
                channelMagento.LastSyncedDateTicket = syncTime;
            }

            var lastSyncDate_Magento_Message = channelMagento.LastSyncedDateMessage ?? DateTime.UtcNow;
            var getMessagesMagentoRequest = new GetMessagesRequest()
            {
                connection_id = connection.Id.ToString(),
                from = lastSyncDate_Magento_Message.ToString(Shared.Constants.DateTimeFormat_Magento),
                to = syncTime.ToString(Shared.Constants.DateTimeFormat_Magento),
            };
            var getMessagesResult = magentoService.GetMessages(channelMagento.Host, magentoToken, getMessagesMagentoRequest);
            if (getMessagesResult.HasError)
            {
                channelMagento.Message += ". " + getTicketsResult.Error;
            }
            else
            {
                channelMagento.LastSyncedDateMessage = syncTime;
            }
            channelMagentoService.Update(channelMagento);
            #endregion

            //5. Persist Message reply by admin to local db
            #region Persist Message reply by admin to local db
            var pulledMagentMessages = new List<MessageMagento>();
            if (!getMessagesResult.HasError)
            {
                foreach (var magentoMessage in getMessagesResult.Result)
                {
                    var ticket = ticketMagentoService.Get(magentoMessage.ticket_id);
                    if (ticket != null && !messageMagentoService.IsExisting(magentoMessage.message_id))
                    {
                        var message = new MessageMagento()
                        {
                            MagentoId = magentoMessage.message_id,
                            SenderId = ticket.RecipientId,
                            SenderEmail = string.Empty,
                            TicketId = ticket.Id,
                            Message = magentoMessage.message,
                            CreatedDate = DateTime.Parse(magentoMessage.created_date),
                            LastModifiedDate = DateTime.Parse(magentoMessage.last_modified_date),
                            Note = string.Empty,
                            LastSynchronizedDate = syncTime,
                            SyncStatus = SyncStatus.Ok,
                            SyncErrorMessage = string.Empty,
                        };

                        message.Id = messageMagentoService.CreateMessage(message);
                        pulledMagentMessages.Add(message);
                    }
                }
            }
            #endregion

            //6. Transfer Magento message to Ebay message
            #region Transfer Magento message to Ebay message
            foreach(var message in pulledMagentMessages)
            {
                var ebayMessage = mapper.Map<MessageEbay>(message);
                var ebayTicketId = ticketEbayMagentoService.GetByTicketMagentoId(message.TicketId).IdEbay;
                ebayMessage.TicketId = ebayTicketId;

                ebayMessage.Id = messageEbayService.CreateMessage(ebayMessage);
            }
            #endregion

            //7. Push Transfered ebay message to remote site
            #region Push Tranfered ebay message to remote site
            var transferEbayMessages = messageEbayService.GetMessagesForSyncByConnectionId(connection.Id);
            foreach (var message in transferEbayMessages)
            {
                var ticket = ticketEbayService.GetTicketById(message.TicketId);
                var ebayMessage = new Channels.Ebay.Models.MemberMessage()
                {
                    ItemId = ticket.ItemId,
                    ParentMessageId = ticket.EbayId,
                    SenderId = message.SenderId,
                    SenderEmail = message.SenderEmail,
                    RecipientId = ticket.CreatorId,
                    Body = message.Message,
                    CreatedDate = message.CreatedDate,
                };

                var result = ebayService.SendMessage(channelEbay.Token, ticket.Subject, ticket.EbayId, ebayMessage);
                if (result.HasError)
                {
                    message.SyncStatus = SyncStatus.Error;
                    message.Message = result.Error;
                }
                else
                {
                    message.SyncStatus = SyncStatus.Ok;
                    message.Message = string.Empty;
                }
                message.LastSynchronizedDate = syncTime;
                messageEbayService.UpdateMessage(message);
            }
            #endregion
            
            #endregion

            connection.Status = ConnectionStatus.Connected;
            connection.LastSyncedDate = syncTime;
            connection.NextSyncedDate = connection.LastSyncedDate.Value.AddMinutes(ebayMagentoTaskIntervalInMinutes);
            connection.Counter++;
            connectionService.Update(connection);

            TextBuffer.WriteLine("EbayMagentoProcessor - End Connection " + connection.Id);
        }

        TicketEbay GetOrCreateTicketEbay(Channels.Ebay.Models.MemberMessage memberMessage, Connection connection, DateTime syncTime)
        {
            TicketEbay ticketEbay;
            if (string.IsNullOrEmpty(memberMessage.ParentMessageId))
            {
                ticketEbay = new TicketEbay
                {
                    EbayId = memberMessage.MessageId,
                    ConnectionId = connection.Id,
                    Status = TicketStatus.open,
                    Type = TicketEbayType.Message.ToString(),
                    Subject = memberMessage.QuestionType,
                    ItemId = memberMessage.ItemId,
                    CreatorId = memberMessage.SenderId,
                    CreatorEmail = memberMessage.SenderEmail,
                    RecipientId = memberMessage.RecipientId,
                    CreatedDate = memberMessage.CreatedDate,
                    LastModifiedDate = memberMessage.LastModifiedDate,
                    Note = string.Empty,
                    LastSynchronizedDate = syncTime,
                    SyncStatus = SyncStatus.Ok,
                    SyncErrorMessage = string.Empty
                };

                ticketEbay.Id = ticketEbayService.CreateTicket(ticketEbay);
            }
            else
            {
                ticketEbay = ticketEbayService.GetTicketByEbayId(memberMessage.ParentMessageId);
            }
            return ticketEbay;
        }

        MessageEbay CreateMessageEbay(Channels.Ebay.Models.MemberMessage memberMessage, TicketEbay ticketEbay, DateTime syncTime)
        {
            var messageEbay = new MessageEbay
            {
                EbayId = memberMessage.MessageId,
                SenderId = memberMessage.SenderId,
                SenderEmail = memberMessage.SenderEmail,
                TicketId = ticketEbay.Id,
                Message = memberMessage.Body,
                CreatedDate = memberMessage.CreatedDate,
                LastModifiedDate = memberMessage.LastModifiedDate,
                Note = string.Empty,
                LastSynchronizedDate = syncTime,
                SyncStatus = SyncStatus.Ok,
                SyncErrorMessage = string.Empty
            };
            messageEbay.Id = messageEbayService.CreateMessage(messageEbay);
            return messageEbay;
        }

        CreateTicketRequest CreateTicketMagentoRequest(TicketMagento magentoTicket, Connection connection, ChannelMagento channelMagento)
        {
            var createTicketRequest = new CreateTicketRequest()
            {
                connection_id = connection.Id.ToString(),
                store_id = channelMagento.StoreId,
                department_id = "1", //TODO : need to clarify with customer's site
                agent_id = "1", //TODO : need to clarify with customer's site
                status = magentoTicket.Status.ToString(),
                type = magentoTicket.Type,
                item_id = magentoTicket.ItemId,
                subject = magentoTicket.Subject,
                creator_id = magentoTicket.CreatorId,
                creator_name = magentoTicket.CreatorId,
                creator_email = magentoTicket.CreatorEmail,
                recipient_id = magentoTicket.RecipientId,
                created_at = magentoTicket.CreatedDate.ToString(Shared.Constants.DateTimeFormat_Magento),
                last_modified_at = magentoTicket.LastModifiedDate?.ToString(Shared.Constants.DateTimeFormat_Magento) ?? magentoTicket.CreatedDate.ToString(Shared.Constants.DateTimeFormat_Magento),
                note = string.Empty,
            };
            return createTicketRequest;
        }

        SendMessageRequest SendMessageMagentoRequest(MessageMagento magentoMessage,TicketMagento magentoTicket)
        {
            var sendMessageRequest = new SendMessageRequest()
            {
                message = magentoMessage.Message,
                type = "customer-reply",
                sender_id = magentoMessage.SenderId,
                sender_name = magentoMessage.SenderId,
                sender_email = magentoMessage.SenderEmail,
                ticket_id = magentoTicket.MagentoId,
                created_at = magentoMessage.CreatedDate.ToString(Shared.Constants.DateTimeFormat_Magento),
                last_modified_at =
                            magentoMessage.LastModifiedDate?.ToString(Shared.Constants.DateTimeFormat_Magento) ??
                            magentoMessage.CreatedDate.ToString(Shared.Constants.DateTimeFormat_Magento),
                note = string.Empty,
            };
            return sendMessageRequest;
        }
    }
}