using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHub.Repositories.Repositories.Interfaces;

namespace SmartHub.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IConnectionRepository ConnectionRepository { get;}

        IUserChannelRepository UserChannelRepository { get; }

        IChannelEbayRepository ChannelEbayRepository { get; }

        IChannelMagentoRepository ChannelMagentoRepository { get; }

        IChannelEmailRepository ChannelEmailRepository { get; }

        IMessageEbayRepository MessageEbayRepository { get; }

        IMessageMagentoRepository MessageMagentoRepository { get; }

        IMessageEmailRepository MessageEmailRepository { get; }

        IMessageEbayMagentoRepository MessageEbayMagentoRepository { get; }

        IMessageEmailMagentoRepository MessageEmailMagentoRepository { get; }

        ITicketEbayRepository TicketEbayRepository { get; }

        ITicketMagentoRepository TicketMagentoRepository { get; }

        ITicketEmailRepository TicketEmailRepository { get; }

        ITicketEbayMagentoRepository TicketEbayMagentoRepository { get; }

        ITicketEmailMagentoRepository TicketEmailMagentoRepository { get; }

        IUserRepository UserRepository { get; }

        int Commit();
    }
}
