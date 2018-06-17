﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHub.Repositories.Repositories.Interfaces;

namespace SmartHub.Repositories.Repositories
{
    public class ChannelEbayRepository : Repository<Entities.ChannelEbay, SmartHubDbContext>, IChannelEbayRepository
    {
        public ChannelEbayRepository(SmartHubDbContext context)
            : base(context)
        {
        }
    }
}
