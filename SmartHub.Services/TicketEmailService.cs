using AutoMapper;
using SmartHub.Repositories;


namespace SmartHub.Services
{
    public interface ITicketEmailService
    {
        Models.TicketEmail GetTicketById(int id);

        Models.TicketEmail GetTicketByEmailId(string EmailId);

        int CreateTicket(Models.TicketEmail ticket);

        void UpdateTicket(Models.TicketEmail ticket);

        int Commit();
    }

    public class TicketEmailService : ITicketEmailService
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public TicketEmailService(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        public Models.TicketEmail GetTicketById(int id)
        {
            var entity = uow.TicketEmailRepository.Get(t => t.Id == id);
            return mapper.Map<Models.TicketEmail>(entity);
        }

        public Models.TicketEmail GetTicketByEmailId(string EmailId)
        {
            var entity = uow.TicketEmailRepository.Get(t => t.EmailId == EmailId);
            return mapper.Map<Models.TicketEmail>(entity);
        }

        public int CreateTicket(Models.TicketEmail ticket)
        {
            var entity = mapper.Map<Repositories.Entities.TicketEmail>(ticket);
            entity = uow.TicketEmailRepository.Add(entity);
            uow.Commit();
            return entity.Id;
        }

        public void UpdateTicket(Models.TicketEmail ticket)
        {
            var entity = mapper.Map<Repositories.Entities.TicketEmail>(ticket);
            uow.TicketEmailRepository.Update(entity);
            uow.Commit();
        }

        public int Commit()
        {
            return uow.Commit();
        }
    }
}
