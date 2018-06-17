using AutoMapper;
using SmartHub.Repositories;
using SmartHub.Repositories.Entities;


namespace SmartHub.Services
{
    public interface ITicketEbayMagentoService
    {
        int Create(int ticketEbayId, int ticketMagentoId);

        Models.TicketEbayMagento GetByTicketMagentoId(int ticketMagentoId);

        int Commit();
    }

    public class TicketEbayMagentoService : ITicketEbayMagentoService
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public TicketEbayMagentoService(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        public int Create(int ticketEbayId, int ticketMagentoId)
        {
            var entity = new TicketEbayMagento()
            {
                IdEbay = ticketEbayId,
                IdMagento = ticketMagentoId,
            };
            entity = uow.TicketEbayMagentoRepository.Add(entity);
            uow.Commit();
            return entity.Id;
        }

        public Models.TicketEbayMagento GetByTicketMagentoId(int ticketMagentoId)
        {
            var entity = uow.TicketEbayMagentoRepository.Get(x => x.IdMagento == ticketMagentoId);
            if (entity != null)
            {
                return mapper.Map<Models.TicketEbayMagento>(entity);
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
