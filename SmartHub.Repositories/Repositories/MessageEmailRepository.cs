using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHub.Repositories.Repositories.Interfaces;

namespace SmartHub.Repositories.Repositories
{
    public class MessageEmailRepository : Repository<Entities.MessageEmail, SmartHubDbContext>, IMessageEmailRepository
    {
        public MessageEmailRepository(SmartHubDbContext context)
            : base(context)
        {
        }
    }
}
