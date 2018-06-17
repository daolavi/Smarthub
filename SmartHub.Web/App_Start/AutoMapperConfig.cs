using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartHub.Services;
using AutoMapper;
using SmartHub.Web.Models;

namespace SmartHub.Web.App_Start
{
    public static class AutoMapperConfig
    {
        public static void Register(IMapperConfigurationExpression config)
        {
            ServiceMapper.Register(config);
            SmartHubMapper.Register(config);
        }
    }
}