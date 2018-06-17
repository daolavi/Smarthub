using AutoMapper;
using SmartHub.Repositories;
using SmartHub.Repositories.Entities;


namespace SmartHub.Services
{
    public interface ITicketEmailMagentoService
    {
        int Create(int ticketEmailId, int ticketMagentoId);

        Models.TicketEmailMagento GetByTicketMagentoId(int ticketMagentoId);

        int Commit();
    }

    public class TicketEmailMagentoService : ITicketEmailMagentoService
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public TicketEmailMagentoService(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        public int Create(int ticketEmailId, int ticketMagentoId)
        {
            var entity = new TicketEmailMagento()
            {
                IdEmail = ticketEmailId,
                IdMagento = ticketMagentoId,
            };
            entity = uow.TicketEmailMagentoRepository.Add(entity);
            uow.Commit();
            return entity.Id;
        }

        public Models.TicketEmailMagento GetByTicketMagentoId(int ticketMagentoId)
        {
            var entity = uow.TicketEmailMagentoRepository.Get(x => x.IdMagento == ticketMagentoId);
            if (entity != null)
            {
                return mapper.Map<Models.TicketEmailMagento>(entity);
            }
            else
            {
                return null;
            }
        }

        public int Commit()
        {
            return uow.Commit();
        }
    }
}
