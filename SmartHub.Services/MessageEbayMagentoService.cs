using System.Collections.Generic;
using System.Security.Permissions;
using AutoMapper;
using SmartHub.Repositories;
using SmartHub.Repositories.Entities;


namespace SmartHub.Services
{
    public interface IMessageEbayMagentoService
    {
        int Create(int messageEbayId, int messageMagentoId);

        IEnumerable<MessageEbayMagento> CreateRange(IEnumerable<Models.MessageEbayMagento> models);

        int Commit();
    }

    public class MessageEbayMagentoService : IMessageEbayMagentoService
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public MessageEbayMagentoService(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        public int Create(int messageEbayId, int messageMagentoId)
        {
            var entity = new MessageEbayMagento()
            {
                IdEbay = messageEbayId,
                IdMagento = messageMagentoId,
            };
            entity = uow.MessageEbayMagentoRepository.Add(entity);
            uow.Commit();
            return entity.Id;
        }

        public IEnumerable<MessageEbayMagento> CreateRange(IEnumerable<Models.MessageEbayMagento> models)
        {
            var entities = mapper.Map<IEnumerable<MessageEbayMagento>>(models);
            var result = uow.MessageEbayMagentoRepository.AddRange(entities);
            uow.Commit();
            return result;
        }

        public int Commit()
        {
            return uow.Commit();
        }
    }
}
