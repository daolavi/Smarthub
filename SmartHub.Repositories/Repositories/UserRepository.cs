using SmartHub.Repositories.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHub.Repositories.Repositories
{
    public class UserRepository : Repository<Entities.User, SmartHubDbContext>, IUserRepository
    {
        public UserRepository(SmartHubDbContext context)
            : base(context)
        { }
    }
}
