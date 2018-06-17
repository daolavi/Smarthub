using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHub.Repositories.Repositories.Interfaces;

namespace SmartHub.Repositories.Repositories
{
    public class TicketEmailMagentoRepository : Repository<Entities.TicketEmailMagento, SmartHubDbContext>, ITicketEmailMagentoRepository
    {
        public TicketEmailMagentoRepository(SmartHubDbContext context)
            : base(context)
        {
        }
    }
}
