using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autofac;
using AutoMapper;

namespace SmartHub.Web.App_Start
{
    public static class AutofacConfig
    {
        public static void Register(IContainer container)
        {
            var builder = new ContainerBuilder();

            var mapper = new MapperConfiguration(cfg => AutoMapperConfig.Register(cfg)).CreateMapper();

            builder.RegisterInstance(mapper).As<IMapper>().SingleInstance();

            builder.Update(container);
        }
    }
}