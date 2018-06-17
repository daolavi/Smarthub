using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHub.Repositories.Repositories.Interfaces;

namespace SmartHub.Repositories.Repositories
{
    public class MessageEmailMagentoRepository : Repository<Entities.MessageEmailMagento, SmartHubDbContext>, IMessageEmailMagentoRepository
    {
        public MessageEmailMagentoRepository(SmartHubDbContext context)
            : base(context)
        {
        }
    }
}
