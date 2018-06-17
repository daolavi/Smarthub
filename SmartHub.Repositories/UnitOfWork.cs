using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHub.Repositories.Repositories;
using SmartHub.Repositories.Repositories.Interfaces;

namespace SmartHub.Repositories
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly SmartHubDbContext _context;

        private Lazy<IConnectionRepository> _connections;

        private Lazy<IUserChannelRepository> _userChannels;

        private Lazy<IChannelEbayRepository> _channelEbays;

        private Lazy<IChannelMagentoRepository> _channelMagento;

        private Lazy<IChannelEmailRepository> _channelEmail;

        private Lazy<IMessageEbayRepository> _messageEbay;

        private Lazy<IMessageMagentoRepository> _messageMagento;

        private Lazy<IMessageEmailRepository> _messageEmail;

        private Lazy<IMessageEbayMagentoRepository> _messageEbayMagento;

        private Lazy<IMessageEmailMagentoRepository> _messageEmailMagento;

        private Lazy<ITicketEbayRepository> _ticketEbay;

        private Lazy<ITicketMagentoRepository> _ticketMagento;

        private Lazy<ITicketEmailRepository> _ticketEmail;

        private Lazy<ITicketEbayMagentoRepository> _ticketEbayMagento;

        private Lazy<ITicketEmailMagentoRepository> _ticketEmailMagento;

        private Lazy<IUserRepository> _user;

        public UnitOfWork(SmartHubDbContext context)
        {
            this._context = context;

            _connections = new Lazy<IConnectionRepository>(() => new ConnectionRepository(context));

            _userChannels = new Lazy<IUserChannelRepository>(() => new UserChannelRepository(context));

            _channelEbays = new Lazy<IChannelEbayRepository>(()=> new ChannelEbayRepository(context));

            _channelMagento = new Lazy<IChannelMagentoRepository>(() => new ChannelMagentoRepository(context));

            _channelEmail = new Lazy<IChannelEmailRepository>(() => new ChannelEmailRepository(context));

            _messageEbay = new Lazy<IMessageEbayRepository>(() => new MessageEbayRepository(context));

            _messageMagento = new Lazy<IMessageMagentoRepository>(() => new MessageMagentoRepository(context));

            _messageEmail = new Lazy<IMessageEmailRepository>(() => new MessageEmailRepository(context));

            _messageEbayMagento = new Lazy<IMessageEbayMagentoRepository>(() => new MessageEbayMagentoRepository(context));

            _messageEmailMagento = new Lazy<IMessageEmailMagentoRepository>(()=> new MessageEmailMagentoRepository(context));

            _ticketEbay = new Lazy<ITicketEbayRepository>(() => new TicketEbayRepository(context));

            _ticketMagento = new Lazy<ITicketMagentoRepository>(() => new TicketMagentoRepository(context));

            _ticketEmail = new Lazy<ITicketEmailRepository>(()=> new TicketEmailRepository(context));

            _ticketEbayMagento = new Lazy<ITicketEbayMagentoRepository>(() => new TicketEbayMagentoRepository(context));

            _ticketEmailMagento = new Lazy<ITicketEmailMagentoRepository>(()=> new TicketEmailMagentoRepository(context));

            _user = new Lazy<IUserRepository>(() => new UserRepository(context));
        }

        public IConnectionRepository ConnectionRepository => _connections.Value;

        public IUserChannelRepository UserChannelRepository => _userChannels.Value;

        public IChannelEbayRepository ChannelEbayRepository => _channelEbays.Value;

        public IChannelMagentoRepository ChannelMagentoRepository => _channelMagento.Value;

        public IChannelEmailRepository ChannelEmailRepository => _channelEmail.Value;

        public IMessageEbayRepository MessageEbayRepository => _messageEbay.Value;

        public IMessageMagentoRepository MessageMagentoRepository => _messageMagento.Value;

        public IMessageEmailRepository MessageEmailRepository => _messageEmail.Value;

        public IMessageEbayMagentoRepository MessageEbayMagentoRepository => _messageEbayMagento.Value;

        public IMessageEmailMagentoRepository MessageEmailMagentoRepository => _messageEmailMagento.Value;

        public ITicketEbayRepository TicketEbayRepository => _ticketEbay.Value;

        public ITicketMagentoRepository TicketMagentoRepository => _ticketMagento.Value;

        public ITicketEmailRepository TicketEmailRepository => _ticketEmail.Value;

        public ITicketEbayMagentoRepository TicketEbayMagentoRepository => _ticketEbayMagento.Value;

        public ITicketEmailMagentoRepository TicketEmailMagentoRepository => _ticketEmailMagento.Value;

        public IUserRepository UserRepository => _user.Value;

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
