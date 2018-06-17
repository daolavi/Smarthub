using AutoMapper;
using SmartHub.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace SmartHub.Services
{
    public interface IMessageEbayService
    {
        int CreateMessage(Models.MessageEbay message);

        void UpdateMessage(Models.MessageEbay message);

        IEnumerable<Models.MessageEbay> GetMessagesForSyncByConnectionId(int connectionId);

        int Commit();
    }

    public class MessageEbayService : IMessageEbayService
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public MessageEbayService(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        public int CreateMessage(Models.MessageEbay message)
        {
            var entity = mapper.Map<Repositories.Entities.MessageEbay>(message);
            entity = uow.MessageEbayRepository.Add(entity);
            uow.Commit();
            return entity.Id;
        }

        public void UpdateMessage(Models.MessageEbay message)
        {
            var entity = mapper.Map<Repositories.Entities.MessageEbay>(message);
            uow.MessageEbayRepository.Update(entity);
            uow.Commit();
        }

        public IEnumerable<Models.MessageEbay> GetMessagesForSyncByConnectionId(int connectionId)
        {
            var ticketIds = uow.TicketEbayRepository.Find(x => x.ConnectionId == connectionId).Select(x => x.Id);
            var messages = uow.MessageEbayRepository.Find(x => ticketIds.Contains(x.TicketId) && x.SyncStatus != Shared.Enums.SyncStatus.Ok);
            return mapper.Map<IEnumerable<Models.MessageEbay>>(messages);
        }

        public int Commit()
        {
            return uow.Commit();
        }
    }
}
