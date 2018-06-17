using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac.Core;
using AutoMapper;
using SmartHub.Channels.Ebay;
using SmartHub.Services.Models;

namespace SmartHub.Web.Models
{
    public static class SmartHubMapper
    {
        public static void Register(IMapperConfigurationExpression config)
        {
            config.CreateMap<TicketEbay, TicketMagento>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.SyncStatus, opt => opt.Ignore())
                .ForMember(x => x.SyncErrorMessage, opt => opt.Ignore())
                .ForMember(x => x.LastSynchronizedDate, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.SyncStatus, opt => opt.Ignore())
                .ForMember(x => x.SyncErrorMessage, opt => opt.Ignore())
                .ForMember(x => x.LastSynchronizedDate, opt => opt.Ignore());

            config.CreateMap<MessageEbay, MessageMagento>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.SyncStatus, opt => opt.Ignore())
                .ForMember(x => x.SyncErrorMessage, opt => opt.Ignore())
                .ForMember(x => x.LastSynchronizedDate, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.SyncStatus, opt => opt.Ignore())
                .ForMember(x => x.SyncErrorMessage, opt => opt.Ignore())
                .ForMember(x => x.LastSynchronizedDate, opt => opt.Ignore());

            config.CreateMap<ChannelEbay, ChannelEbayViewModel>().ReverseMap();
            config.CreateMap<ChannelMagento, ChannelMagentoViewModel>().ReverseMap();
            config.CreateMap<ChannelEmail, ChannelEmailViewModel>().ReverseMap();

            config.CreateMap<ChannelEmail, SmartHub.Channels.Gmail.Models.Token>().ReverseMap();
        }
    }
}