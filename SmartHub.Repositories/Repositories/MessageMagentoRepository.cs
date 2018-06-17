using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHub.Repositories.Repositories.Interfaces;

namespace SmartHub.Repositories.Repositories
{
    public class MessageMagentoRepository : Repository<Entities.MessageMagento, SmartHubDbContext>, IMessageMagentoRepository
    {
        public MessageMagentoRepository(SmartHubDbContext context)
            : base(context)
        {
        }
    }
}
