using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHub.Repositories.Repositories.Interfaces;

namespace SmartHub.Repositories.Repositories
{
    public class MessageEbayRepository : Repository<Entities.MessageEbay, SmartHubDbContext>, IMessageEbayRepository
    {
        public MessageEbayRepository(SmartHubDbContext context)
            : base(context)
        {
        }
    }
}
