using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHub.Repositories.Repositories.Interfaces;

namespace SmartHub.Repositories.Repositories
{
    public class ChannelEmailRepository : Repository<Entities.ChannelEmail, SmartHubDbContext>, IChannelEmailRepository
    {
        public ChannelEmailRepository(SmartHubDbContext context)
            : base(context)
        {
        }
    }
}
