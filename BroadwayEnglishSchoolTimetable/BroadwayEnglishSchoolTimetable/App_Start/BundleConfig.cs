using System.Web;
using System.Web.Optimization;

namespace BroadwayEnglishSchoolTimetable
{
    public class BundleConfig
    {
        // Дополнительные сведения об объединении см. на странице https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-3.2.1.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Используйте версию Modernizr для разработчиков, чтобы учиться работать. Когда вы будете готовы перейти к работе,
            // готово к выпуску, используйте средство сборки по адресу https://modernizr.com, чтобы выбрать только необходимые тесты.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/respond.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/site.css"));


            bundles.Add(new StyleBundle("~/Content/fullcalendarcss").Include(
                "~/Content/fullcalendar.min.css",
                "~/Content/scheduler.min.css"
                ));


            bundles.Add(new ScriptBundle("~/bundles/fullcalendarjs").Include(
                      "~/Scripts/moment.min.js",
                      "~/Scripts/fullcalendar.min.js",
                       //"~/Scripts/locale-all.js",
                       "~/Scripts/es-us.js",
                        "~/Scripts/ru.js",

                      "~/Scripts/scheduler.min.js"
             ));

        }
    }
}

