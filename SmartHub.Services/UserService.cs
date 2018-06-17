using AutoMapper;
using SmartHub.Repositories;
using SmartHub.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHub.Services
{
    public interface IUserService
    {
        int UserExists(string email);

        Models.User GetUser(int userId);

        int Create(Models.User user);

        int Commit();
    }

    public class UserService : IUserService
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public UserService(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        public int UserExists(string email)
        {
            int userId = uow.UserRepository.Find(u => u.Account.Equals(email)).Select(u => u.Id).SingleOrDefault();
            //user doesn't exist if userId is 0

            return userId;
        }

        public Models.User GetUser(int userId)
        {
            var result = uow.UserRepository.Get(userId);
            return mapper.Map<Models.User>(result);
        }

        public int Create(Models.User user)
        {
            var entity = mapper.Map<User>(user);
            entity = uow.UserRepository.Add(entity);
            uow.Commit();
            return entity.Id;
        }

        public int Commit()
        {
            return uow.Commit();
        }
    }
}
