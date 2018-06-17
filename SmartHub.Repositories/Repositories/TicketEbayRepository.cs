﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHub.Repositories.Repositories.Interfaces;

namespace SmartHub.Repositories.Repositories
{
    public class TicketEbayRepository : Repository<Entities.TicketEbay, SmartHubDbContext>, ITicketEbayRepository
    {
        public TicketEbayRepository(SmartHubDbContext context)
            : base(context)
        {
        }
    }
}
