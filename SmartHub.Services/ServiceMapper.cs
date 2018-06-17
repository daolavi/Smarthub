using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace SmartHub.Services
{
    public static class ServiceMapper
    {
        public static void Register(IMapperConfigurationExpression config)
        {
            config.CreateMap<Repositories.Entities.Connection, Models.Connection>().ReverseMap();
            config.CreateMap<Repositories.Entities.UserChannel, Models.UserChannel>().ReverseMap();
            config.CreateMap<Repositories.Entities.ChannelEbay, Models.ChannelEbay>().ReverseMap();
            config.CreateMap<Repositories.Entities.ChannelMagento, Models.ChannelMagento>().ReverseMap();
            config.CreateMap<Repositories.Entities.ChannelEmail, Models.ChannelEmail>().ReverseMap();
            config.CreateMap<Repositories.Entities.TicketEbay, Models.TicketEbay>().ReverseMap();
            config.CreateMap<Repositories.Entities.TicketMagento, Models.TicketMagento>().ReverseMap();
            config.CreateMap<Repositories.Entities.TicketEmail, Models.TicketEmail>().ReverseMap();
            config.CreateMap<Repositories.Entities.TicketEbayMagento, Models.TicketEbayMagento>().ReverseMap();
            config.CreateMap<Repositories.Entities.TicketEmailMagento, Models.TicketEmailMagento>().ReverseMap();
            config.CreateMap<Repositories.Entities.MessageEbay, Models.MessageEbay>().ReverseMap();
            config.CreateMap<Repositories.Entities.MessageMagento, Models.MessageMagento>().ReverseMap();
            config.CreateMap<Repositories.Entities.MessageEmail, Models.MessageEmail>().ReverseMap();
            config.CreateMap<Repositories.Entities.MessageEbayMagento, Models.MessageEbayMagento>().ReverseMap();
            config.CreateMap<Repositories.Entities.MessageEmailMagento, Models.MessageEmailMagento>().ReverseMap();
            config.CreateMap<Repositories.Entities.User, Models.User>().ReverseMap();
        }
    }
}
