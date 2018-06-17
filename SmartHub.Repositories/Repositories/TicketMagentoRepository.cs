using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHub.Repositories.Repositories.Interfaces;

namespace SmartHub.Repositories.Repositories
{
    public class TicketMagentoRepository : Repository<Entities.TicketMagento, SmartHubDbContext>, ITicketMagentoRepository
    {
        public TicketMagentoRepository(SmartHubDbContext context)
            : base(context)
        {
        }
    }
}
