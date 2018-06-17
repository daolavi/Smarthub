using AutoMapper;
using SmartHub.Repositories;
using System.Collections.Generic;
using SmartHub.Services.Models;
using System.Linq;

namespace SmartHub.Services
{
    public interface IMessageMagentoService
    {
        int CreateMessage(Models.MessageMagento message);

        void UpdateMessage(Models.MessageMagento message);

        bool IsExisting(string magentoMessageId);

        IEnumerable<Models.MessageMagento> GetMessagesByTicketId(int ticketId);

        IEnumerable<Models.MessageMagento> GetMessagesForSyncByTicketId(int ticketId);

        int Commit();
    }

    public class MessageMagentoService : IMessageMagentoService
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public MessageMagentoService(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        public int CreateMessage(Models.MessageMagento message)
        {
            var entity = mapper.Map<Repositories.Entities.MessageMagento>(message);
            entity = uow.MessageMagentoRepository.Add(entity);
            uow.Commit();
            return entity.Id;
        }

        public void UpdateMessage(Models.MessageMagento message)
        {
            var entity = mapper.Map<Repositories.Entities.MessageMagento>(message);
            uow.MessageMagentoRepository.Update(entity);
            uow.Commit();
        }

        public bool IsExisting(string magentoMessageId)
        {
            var entity = uow.MessageMagentoRepository.Find(x => x.MagentoId == magentoMessageId);
            return (entity != null && entity.Count() > 0);
        }

        public IEnumerable<Models.MessageMagento> GetMessagesByTicketId(int ticketId)
        {
            var entities = uow.MessageMagentoRepository.Find(x => x.TicketId == ticketId);
            return mapper.Map<IEnumerable<MessageMagento>>(entities);
        }

        public IEnumerable<Models.MessageMagento> GetMessagesForSyncByTicketId(int ticketId)
        {
            var entities = uow.MessageMagentoRepository.Find(x => x.TicketId == ticketId && x.SyncStatus != Shared.Enums.SyncStatus.Ok);
            return mapper.Map<IEnumerable<MessageMagento>>(entities);
        }

        public int Commit()
        {
            return uow.Commit();
        }
    }
}
