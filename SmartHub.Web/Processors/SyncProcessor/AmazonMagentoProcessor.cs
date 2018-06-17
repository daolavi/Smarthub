using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using SmartHub.Services;

namespace SmartHub.Web.Processors.SyncProcessor
{
    public interface IAmazonMagentoProcessor
    {
        void Proceed(Services.Models.Connection connection);
    }

    public class AmazonMagentoProcessor : IAmazonMagentoProcessor
    {
        public AmazonMagentoProcessor()
        {

        }

        public void Proceed(Services.Models.Connection connection)
        {

        }
    }
}