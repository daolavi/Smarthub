using AutoMapper;
using SmartHub.Repositories;
using System.Collections.Generic;
using System.Linq;
using SmartHub.Services.Models;

namespace SmartHub.Services
{
    public interface ITicketMagentoService
    {
        int CreateTicket(Models.TicketMagento ticket);

        void UpdateTicket(Models.TicketMagento ticket);

        IEnumerable<string> GetMagentoTicketIds(int connectionId);

        IEnumerable<Models.TicketMagento> GetMagentoTicketsByConnectionId(int connectionId);

        IEnumerable<Models.TicketMagento> GetMagentoTicketsForSyncByConnectionId(int connectionId);

        Models.TicketMagento Get(string ticketId);

        int Commit();
    }

    public class TicketMagentoService : ITicketMagentoService
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public TicketMagentoService(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        public int CreateTicket(Models.TicketMagento ticket)
        {
            var entity = mapper.Map<Repositories.Entities.TicketMagento>(ticket);
            entity = uow.TicketMagentoRepository.Add(entity);
            uow.Commit();
            return entity.Id;
        }

        public void UpdateTicket(Models.TicketMagento ticket)
        {
            var entity = mapper.Map<Repositories.Entities.TicketMagento>(ticket);
            uow.TicketMagentoRepository.Update(entity);
            uow.Commit();
        }

        public IEnumerable<string> GetMagentoTicketIds(int connectionId)
        {
            var ticketIds = uow.TicketMagentoRepository.Find(x => x.ConnectionId == connectionId).Select(x => x.MagentoId);
            return ticketIds;
        }

        public Models.TicketMagento Get(string ticketId)
        {
            var entity = uow.TicketMagentoRepository.Get(x => x.MagentoId == ticketId);
            if (entity != null)
            {
                return mapper.Map<Models.TicketMagento>(entity);
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<Models.TicketMagento> GetMagentoTicketsByConnectionId(int connectionId)
        {
            var entities = uow.TicketMagentoRepository.Find(x => x.ConnectionId == connectionId);
            return mapper.Map<IEnumerable<TicketMagento>>(entities);
        }

        public IEnumerable<Models.TicketMagento> GetMagentoTicketsForSyncByConnectionId(int connectionId)
        {
            var entities = uow.TicketMagentoRepository.Find(x => x.ConnectionId == connectionId && x.SyncStatus != Shared.Enums.SyncStatus.Ok);
            return mapper.Map<IEnumerable<TicketMagento>>(entities);
        }

        public int Commit()
        {
            return uow.Commit();
        }
    }
}
