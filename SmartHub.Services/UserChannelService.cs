using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SmartHub.Repositories;
using SmartHub.Repositories.Entities;

namespace SmartHub.Services
{
    public interface IUserChannelService
    {
        IEnumerable<Models.UserChannel> FindUserChannels(int userId);

        Models.UserChannel GetUserChannel(int userChannelId);

        int Create(Models.UserChannel userChannel);

        void Update(Models.UserChannel userChannel);

        int Commit();
    }

    public class UserChannelService : IUserChannelService
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public UserChannelService(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        public IEnumerable<Models.UserChannel> FindUserChannels(int userId)
        {
            IEnumerable<UserChannel> userChannel = uow.UserChannelRepository.Find(u => u.UserId == userId);   
            return mapper.Map<IEnumerable<Models.UserChannel>>(userChannel);
        }

        public Models.UserChannel GetUserChannel(int userChannelId)
        {
            var result = uow.UserChannelRepository.Get(userChannelId);
            return mapper.Map<Models.UserChannel>(result);
        }

        public int Create(Models.UserChannel userChannel)
        {
            var entity = mapper.Map<UserChannel>(userChannel);
            entity = uow.UserChannelRepository.Add(entity);
            uow.Commit();
            return entity.Id;
        }

        public void Update(Models.UserChannel userChannel)
        {
            var entity = mapper.Map<UserChannel>(userChannel);
            uow.UserChannelRepository.Update(entity);
            uow.Commit();
        }

        public int Commit()
        {
            return uow.Commit();
        }
    }
}