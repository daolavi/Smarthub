using System;
using System.Linq;
using AutoMapper;
using SmartHub.Channels.Gmail;
using SmartHub.Channels.Gmail.Models;
using SmartHub.Channels.Magento;
using SmartHub.Channels.Magento.Models;
using SmartHub.Helper;
using SmartHub.Services;
using SmartHub.Shared.Enums;
using SmartHub.Services.Models;
using GmailMessage = Google.Apis.Gmail.v1.Data.Message;

namespace SmartHub.Web.Processors.SyncProcessor
{
    public interface IEmailMagentoProcessor
    {
        void Proceed(Services.Models.Connection connection);
    }

    public class EmailMagentoProcessor : IEmailMagentoProcessor
    {
        private static int emailMagentoTaskIntervalInMinutes = ConfigHelper.AppSettings<int>("EmailMagentoTaskIntervalInMinutes", 10);

        private IMapper mapper;

        private IGmailService emailService;

        private IMagentoService magentoService;

        private IConnectionService connectionService;

        private IUserChannelService userChannelService;

        private IChannelEmailService channelEmailService;

        private IChannelMagentoService channelMagentoService;

        private ITicketEmailService ticketEmailService;

        private IMessageEmailService messageEmailService;

        private ITicketMagentoService ticketMagentoService;

        private IMessageMagentoService messageMagentoService;

        private ITicketEmailMagentoService ticketEmailMagentoService;

        private IMessageEmailMagentoService messageEmailMagentoService;

        public EmailMagentoProcessor(IMapper mapper,
                                   IGmailService emailService,
                                   IMagentoService magentoService,
                                   IConnectionService connectionService,
                                   IUserChannelService userChannelService,
                                   IChannelEmailService channelEmailService,
                                   IChannelMagentoService channelMagentoService,
                                   ITicketEmailService ticketEmailService,
                                   IMessageEmailService messageEmailService,
                                   ITicketMagentoService ticketMagentoService,
                                   IMessageMagentoService messageMagentoService,
                                   ITicketEmailMagentoService ticketEmailMagentoService,
                                   IMessageEmailMagentoService messageEmailMagentoService)
        {
            this.mapper = mapper;
            this.emailService = emailService;
            this.magentoService = magentoService;
            this.connectionService = connectionService;
            this.userChannelService = userChannelService;
            this.channelEmailService = channelEmailService;
            this.channelMagentoService = channelMagentoService;
            this.ticketEmailService = ticketEmailService;
            this.messageEmailService = messageEmailService;
            this.ticketMagentoService = ticketMagentoService;
            this.messageMagentoService = messageMagentoService;
            this.ticketEmailMagentoService = ticketEmailMagentoService;
            this.messageEmailMagentoService = messageEmailMagentoService;
        }

        public void Proceed(Services.Models.Connection connection)
        {
            TextBuffer.WriteLine("EmailMagentoProcessor - Start");
            TextBuffer.WriteLine("EmailMagentoProcessor - Proceed Connection " + connection.Id);

            var syncTime = DateTime.UtcNow;

            connection.Status = ConnectionStatus.Synchronizing;
            connectionService.Update(connection);

            var userChannelEmail = userChannelService.GetUserChannel(connection.UserChannelSource);
            var userChannelMagento = userChannelService.GetUserChannel(connection.UserChannelTarget);

            var channelEmail = channelEmailService.GetChannel(userChannelEmail.ChannelId);
            var channelMagento = channelMagentoService.GetChannel(userChannelMagento.ChannelId);
            var channelMagento_TimeZoneOffset =
                TimeZoneInfo.GetSystemTimeZones()
                    .FirstOrDefault(x => x.DisplayName == channelMagento.TimeZoneDisplayName)
                    .BaseUtcOffset;

            var emailTokenResponse = emailService.RefreshToken(channelEmail.RefreshToken);
            if (emailTokenResponse.HasError)
            {
                connection.Status = ConnectionStatus.Error;
                connection.Message = "Invalid Email Token";
                connection.LastSyncedDate = syncTime;
                connection.NextSyncedDate = connection.LastSyncedDate.Value.AddMinutes(emailMagentoTaskIntervalInMinutes);
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
                connection.NextSyncedDate = connection.LastSyncedDate.Value.AddMinutes(emailMagentoTaskIntervalInMinutes);
                connection.Counter++;
                connectionService.Update(connection);
                return;
            }
            var magentoToken = magentoTokenCall.Result;

            #region Email to Magento
            //1. Pull Email and persist to local db
            #region Pull Email from Gmail

            var lastSyncedDate = channelEmail.LastSyncedDate ?? DateTime.UtcNow;
            var emailMessagesResponse = emailService.GetMessages(emailTokenResponse.Result, lastSyncedDate,DateTime.UtcNow);
            if (emailMessagesResponse.HasError)
            {
                channelEmail.Message += emailMessagesResponse.Error;
            }
            else
            {
                var emailMessages = emailMessagesResponse.Result;
                foreach (var emailMessage in emailMessages)
                {
                    
                }
            }

            #endregion

            #endregion


            connection.Status = ConnectionStatus.Connected;
            connection.LastSyncedDate = syncTime;
            connection.NextSyncedDate = connection.LastSyncedDate.Value.AddMinutes(emailMagentoTaskIntervalInMinutes);
            connection.Counter++;
            connectionService.Update(connection);

            TextBuffer.WriteLine("EmailMagentoProcessor - End Connection " + connection.Id);
        }
    }
}