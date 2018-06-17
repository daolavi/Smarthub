using AutoMapper;
using SmartHub.Repositories;
using SmartHub.Repositories.Entities;

namespace SmartHub.Services
{
    public interface IChannelEmailService
    {
        Models.ChannelEmail GetChannel(int id);

        void Update(Models.ChannelEmail channel);

        int Create(Models.ChannelEmail channel);

        int Commit();
    }

    public class ChannelEmailService : IChannelEmailService
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public ChannelEmailService(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        public Models.ChannelEmail GetChannel(int id)
        {
            var result = uow.ChannelEmailRepository.Get(id);
            return mapper.Map<Models.ChannelEmail>(result);
        }

        public void Update(Models.ChannelEmail channel)
        {
            var entity = mapper.Map<ChannelEmail>(channel);
            uow.ChannelEmailRepository.Update(entity);
            uow.Commit();
        }

        public int Create(Models.ChannelEmail channel)
        {
            var entity = mapper.Map<ChannelEmail>(channel);
            entity = uow.ChannelEmailRepository.Add(entity);
            uow.Commit();
            return entity.Id;
        }

        public int Commit()
        {
            return uow.Commit();
        }
    }
}
