using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHub.Repositories.Repositories.Interfaces;

namespace SmartHub.Repositories.Repositories
{
    public class TicketEmailRepository : Repository<Entities.TicketEmail, SmartHubDbContext>, ITicketEmailRepository
    {
        public TicketEmailRepository(SmartHubDbContext context)
            : base(context)
        {
        }
    }
}
