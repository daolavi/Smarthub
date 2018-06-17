using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHub.Repositories.Entities;

namespace SmartHub.Repositories
{
    [ResolvedFor(typeof(SmartHubDbContext))]
    public class SmartHubDbContext : DbContext
    {
        static SmartHubDbContext()
        {
            Database.SetInitializer<SmartHubDbContext>(null);
        }

        public SmartHubDbContext()
            : base("Name=SmartHubDbContext")
        {
        }

        public SmartHubDbContext(string connectionString)
            : base(connectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new UserChannelConfiguration());
            modelBuilder.Configurations.Add(new ConnectionConfiguration());
            modelBuilder.Configurations.Add(new ChannelEbayConfiguration());
            modelBuilder.Configurations.Add(new ChannelAmazonConfiguration());
            modelBuilder.Configurations.Add(new ChannelMagentoConfiguration());
            modelBuilder.Configurations.Add(new ChannelEmailConfiguration());
            modelBuilder.Configurations.Add(new TicketEbayConfiguration());
            modelBuilder.Configurations.Add(new TicketMagentoConfiguration());
            modelBuilder.Configurations.Add(new TicketEbayMagentoConfiguration());
            modelBuilder.Configurations.Add(new MessageEbayConfiguration());
            modelBuilder.Configurations.Add(new MessageMagentoConfiguration());
            modelBuilder.Configurations.Add(new MessageEbayMagentoConfiguration());
            modelBuilder.Configurations.Add(new MessageEmailConfiguration());
            modelBuilder.Configurations.Add(new MessageEmailMagentoConfiguration());
            modelBuilder.Configurations.Add(new TicketEmailConfiguration());
            modelBuilder.Configurations.Add(new TicketEmailMagentoConfiguration());
        }
    }
}
