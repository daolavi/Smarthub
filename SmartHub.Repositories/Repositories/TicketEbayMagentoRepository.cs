using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHub.Repositories.Repositories.Interfaces;

namespace SmartHub.Repositories.Repositories
{
    public class TicketEbayMagentoRepository : Repository<Entities.TicketEbayMagento, SmartHubDbContext>, ITicketEbayMagentoRepository
    {
        public TicketEbayMagentoRepository(SmartHubDbContext context)
            : base(context)
        {
        }
    }
}
