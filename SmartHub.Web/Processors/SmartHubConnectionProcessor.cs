using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hangfire;
using SmartHub.Helper;
using SmartHub.Services;
using SmartHub.Shared.Enums;
using SmartHub.Web.Processors.SyncProcessor;

namespace SmartHub.Web.Processors
{
    public interface ISmartHubConnectionProcessor
    {
        void Proceed();
    }

    public class SmartHubConnectionProcessor : ISmartHubConnectionProcessor
    {
        private IConnectionService connectionService;

        private IUserChannelService userChannelService;

        private int concurrencyConnection;

        public SmartHubConnectionProcessor(IConnectionService connectionService, IUserChannelService userChannelService)
        {
            this.connectionService = connectionService;
            this.userChannelService = userChannelService;

            concurrencyConnection = ConfigHelper.AppSettings<int>("ConcurrencyConnection", 5);
        }

        public void Proceed()
        {
            TextBuffer.WriteLine("SmartHubConnectionProcessor - Start");

            var connectedConnections = connectionService.GetConnectionsForSync();
            var now = DateTime.UtcNow;

            var dueConnections = connectedConnections.Where(x => !x.NextSyncedDate.HasValue || x.NextSyncedDate.Value <= now)
                .OrderBy(x => x.NextSyncedDate)
                .Take(concurrencyConnection);

            foreach (var connection in dueConnections)
            {
                var sourceChannel = userChannelService.GetUserChannel(connection.UserChannelSource);
                var targetChannel = userChannelService.GetUserChannel(connection.UserChannelTarget);
                var processingConnection = connection;

                if (sourceChannel.ChannelType == Shared.Enums.ChannelType.Ebay && targetChannel.ChannelType == Shared.Enums.ChannelType.Magento)
                {
                    BackgroundJob.Enqueue<IEbayMagentoProcessor>(p => p.Proceed(processingConnection));
                } else if (sourceChannel.ChannelType == Shared.Enums.ChannelType.Amazon && targetChannel.ChannelType == Shared.Enums.ChannelType.Magento)
                {
                    BackgroundJob.Enqueue<IAmazonMagentoProcessor>(p => p.Proceed(processingConnection));
                } else if (sourceChannel.ChannelType == Shared.Enums.ChannelType.Email && targetChannel.ChannelType == Shared.Enums.ChannelType.Magento)
                {
                    BackgroundJob.Enqueue<IEmailMagentoProcessor>(p => p.Proceed(processingConnection));
                }
            }

            TextBuffer.WriteLine("SmartHubConnectionProcessor - End");
        }
    }
}