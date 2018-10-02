using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BroadwayEnglishSchoolTimetable.Startup))]
namespace BroadwayEnglishSchoolTimetable
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
