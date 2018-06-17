using AutoMapper;
using SmartHub.Repositories;


namespace SmartHub.Services
{
    public interface ITicketEbayService
    {
        Models.TicketEbay GetTicketById(int id);

        Models.TicketEbay GetTicketByEbayId(string ebayId);

        int CreateTicket(Models.TicketEbay ticket);

        void UpdateTicket(Models.TicketEbay ticket);

        int Commit();
    }

    public class TicketEbayService : ITicketEbayService
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public TicketEbayService(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        public Models.TicketEbay GetTicketById(int id)
        {
            var entity = uow.TicketEbayRepository.Get(t => t.Id == id);
            return mapper.Map<Models.TicketEbay>(entity);
        }

        public Models.TicketEbay GetTicketByEbayId(string ebayId)
        {
            var entity = uow.TicketEbayRepository.Get(t => t.EbayId == ebayId);
            return mapper.Map<Models.TicketEbay>(entity);
        }

        public int CreateTicket(Models.TicketEbay ticket)
        {
            var entity = mapper.Map<Repositories.Entities.TicketEbay >(ticket);
            entity = uow.TicketEbayRepository.Add(entity);
            uow.Commit();
            return entity.Id;
        }

        public void UpdateTicket(Models.TicketEbay ticket)
        {
            var entity = mapper.Map<Repositories.Entities.TicketEbay>(ticket);
            uow.TicketEbayRepository.Update(entity);
            uow.Commit();
        }

        public int Commit()
        {
            return uow.Commit();
        }
    }
}
