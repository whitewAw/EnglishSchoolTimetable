using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using BusinessLogicLayer.Infrastructure;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;

using DataAccessLayer.Repositories;
using System.Web.Mvc;

namespace BroadwayEnglishSchoolTimetable.Util
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<BLService>().As<IBLService>();
            builder.RegisterModule(new ServiceModule("name = BESTDBEntity"));
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
        
    }
}