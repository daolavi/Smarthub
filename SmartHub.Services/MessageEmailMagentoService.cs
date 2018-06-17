using System.Collections.Generic;
using System.Security.Permissions;
using AutoMapper;
using SmartHub.Repositories;
using SmartHub.Repositories.Entities;


namespace SmartHub.Services
{
    public interface IMessageEmailMagentoService
    {
        int Create(int messageEmailId, int messageMagentoId);

        IEnumerable<MessageEmailMagento> CreateRange(IEnumerable<Models.MessageEmailMagento> models);

        int Commit();
    }

    public class MessageEmailMagentoService : IMessageEmailMagentoService
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public MessageEmailMagentoService(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        public int Create(int messageEmailId, int messageMagentoId)
        {
            var entity = new MessageEmailMagento()
            {
                IdEmail = messageEmailId,
                IdMagento = messageMagentoId,
            };
            entity = uow.MessageEmailMagentoRepository.Add(entity);
            uow.Commit();
            return entity.Id;
        }

        public IEnumerable<MessageEmailMagento> CreateRange(IEnumerable<Models.MessageEmailMagento> models)
        {
            var entities = mapper.Map<IEnumerable<MessageEmailMagento>>(models);
            var result = uow.MessageEmailMagentoRepository.AddRange(entities);
            uow.Commit();
            return result;
        }

        public int Commit()
        {
            return uow.Commit();
        }
    }
}
