using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHub.Repositories.Repositories.Interfaces;

namespace SmartHub.Repositories.Repositories
{
    public class ChannelMagentoRepository : Repository<Entities.ChannelMagento, SmartHubDbContext>, IChannelMagentoRepository
    {
        public ChannelMagentoRepository(SmartHubDbContext context)
            : base(context)
        {
        }
    }
}
