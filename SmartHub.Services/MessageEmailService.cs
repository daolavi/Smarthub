using AutoMapper;
using SmartHub.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace SmartHub.Services
{
    public interface IMessageEmailService
    {
        int CreateMessage(Models.MessageEmail message);

        void UpdateMessage(Models.MessageEmail message);

        IEnumerable<Models.MessageEmail> GetMessagesForSyncByConnectionId(int connectionId);

        int Commit();
    }

    public class MessageEmailService : IMessageEmailService
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public MessageEmailService(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        public int CreateMessage(Models.MessageEmail message)
        {
            var entity = mapper.Map<Repositories.Entities.MessageEmail>(message);
            entity = uow.MessageEmailRepository.Add(entity);
            uow.Commit();
            return entity.Id;
        }

        public void UpdateMessage(Models.MessageEmail message)
        {
            var entity = mapper.Map<Repositories.Entities.MessageEmail>(message);
            uow.MessageEmailRepository.Update(entity);
            uow.Commit();
        }

        public IEnumerable<Models.MessageEmail> GetMessagesForSyncByConnectionId(int connectionId)
        {
            var ticketIds = uow.TicketEmailRepository.Find(x => x.ConnectionId == connectionId).Select(x => x.Id);
            var messages = uow.MessageEmailRepository.Find(x => ticketIds.Contains(x.TicketId) && x.SyncStatus != Shared.Enums.SyncStatus.Ok);
            return mapper.Map<IEnumerable<Models.MessageEmail>>(messages);
        }

        public int Commit()
        {
            return uow.Commit();
        }
    }
}
