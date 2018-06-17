using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using SmartHub.Repositories;
using SmartHub.Repositories.Entities;

namespace SmartHub.Services
{
    public interface IChannelMagentoService
    {
        Models.ChannelMagento GetChannel(int id);

        void Update(Models.ChannelMagento channel);

        int Create(Models.ChannelMagento channel);

        int Commit();
    }

    public class ChannelMagentoService : IChannelMagentoService
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public ChannelMagentoService(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        public Models.ChannelMagento GetChannel(int id)
        {
            var result = uow.ChannelMagentoRepository.Get(id);
            return mapper.Map<Models.ChannelMagento>(result);
        }

        public void Update(Models.ChannelMagento channel)
        {
            var entity = mapper.Map<ChannelMagento>(channel);
            uow.ChannelMagentoRepository.Update(entity);
            uow.Commit();
        }

        public int Create(Models.ChannelMagento channel)
        {
            var entity = mapper.Map<ChannelMagento>(channel);
            entity = uow.ChannelMagentoRepository.Add(entity);
            uow.Commit();
            return entity.Id;
        }

        public int Commit()
        {
            return uow.Commit();
        }
    }
}
