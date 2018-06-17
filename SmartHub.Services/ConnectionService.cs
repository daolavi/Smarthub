using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SmartHub.Repositories;
using SmartHub.Shared.Enums;

namespace SmartHub.Services
{
    public interface IConnectionService
    {
        IEnumerable<Models.Connection> GetConnectionsForSync();

        void Update(Models.Connection connection);

        int Create(Models.Connection connection);

        IEnumerable<Models.Connection> GetConnectionsByUserChannelId(int channelId);

        int Commit();
    }

    public class ConnectionService : IConnectionService
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public ConnectionService(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        public IEnumerable<Models.Connection> GetConnectionsForSync()
        {
            var result = uow.ConnectionRepository.GetAll().Where(x => x.Status != ConnectionStatus.Synchronizing && x.IsActive);
            return mapper.Map<IEnumerable<Models.Connection>>(result);
        }

        public void Update(Models.Connection connection)
        {
            var entity = mapper.Map<Repositories.Entities.Connection>(connection);
            uow.ConnectionRepository.Update(entity);
            uow.Commit();
        }

        public int Create(Models.Connection connection)
        {
            var entity = mapper.Map<Repositories.Entities.Connection>(connection);
            entity = uow.ConnectionRepository.Add(entity);
            uow.Commit();
            return entity.Id;
        }

        public IEnumerable<Models.Connection> GetConnectionsByUserChannelId(int channelId)
        {
            var result = uow.ConnectionRepository.GetAll().Where(x => x.UserChannelSource == channelId || x.UserChannelTarget == channelId);
            return mapper.Map<IEnumerable<Models.Connection>>(result);
        }

        public int Commit()
        {
            return uow.Commit();
        }
    }
}
