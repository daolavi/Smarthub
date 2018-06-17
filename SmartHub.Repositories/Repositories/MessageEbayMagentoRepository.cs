﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHub.Repositories.Repositories.Interfaces;

namespace SmartHub.Repositories.Repositories
{
    public class MessageEbayMagentoRepository : Repository<Entities.MessageEbayMagento, SmartHubDbContext>, IMessageEbayMagentoRepository
    {
        public MessageEbayMagentoRepository(SmartHubDbContext context)
            : base(context)
        {
        }
    }
}
