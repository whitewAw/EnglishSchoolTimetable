using BroadwayEnglishSchoolTimetable.Util;
using BusinessLogicLayer.Services;

using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BroadwayEnglishSchoolTimetable
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AutofacConfig.ConfigureContainer();
            App_Start.AutoMapperConfig.Initialize();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
         
        }
    }
}
