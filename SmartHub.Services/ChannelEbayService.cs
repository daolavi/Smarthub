using AutoMapper;
using SmartHub.Repositories;
using SmartHub.Repositories.Entities;

namespace SmartHub.Services
{
    public interface IChannelEbayService
    {
        Models.ChannelEbay GetChannel(int id);

        void Update(Models.ChannelEbay channel);

        int Create(Models.ChannelEbay channel);

        int Commit();
    }

    public class ChannelEbayService : IChannelEbayService
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public ChannelEbayService(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        public Models.ChannelEbay GetChannel(int id)
        {
            var result = uow.ChannelEbayRepository.Get(id);
            return mapper.Map<Models.ChannelEbay>(result);
        }

        public void Update(Models.ChannelEbay channel)
        {
            var entity = mapper.Map<ChannelEbay>(channel);
            uow.ChannelEbayRepository.Update(entity);
            uow.Commit();
        }

        public int Create(Models.ChannelEbay channel)
        {
            var entity = mapper.Map<ChannelEbay>(channel);
            entity = uow.ChannelEbayRepository.Add(entity);
            uow.Commit();
            return entity.Id;
        }

        public int Commit()
        {
            return uow.Commit();
        }
    }
}
