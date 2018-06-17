﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHub.Repositories.Repositories.Interfaces;

namespace SmartHub.Repositories.Repositories
{
    public class UserChannelRepository : Repository<Entities.UserChannel, SmartHubDbContext>, IUserChannelRepository
    {
        public UserChannelRepository(SmartHubDbContext context)
            : base(context)
        {
        }
    }
}
