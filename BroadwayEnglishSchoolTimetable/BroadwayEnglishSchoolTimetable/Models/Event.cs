using System;

namespace BroadwayEnglishSchoolTimetable.Controllers
{
    public class ColorEvent
    {
        public static string individual { get { return "Lime"; } }
        public static string group { get { return "#00D5FF"; } }
        public static string canceled { get { return "Purple"; } }
    }

    public class ColorEventText
    {
        public static string individual { get { return "black"; } }
        public static string group { get { return "black"; } }
        public static string canceled { get { return "white"; } }
    }

    public class classEvent
    {
        public int id;
        public int resourceId;
        public string title;
        public string color = "white";
        public string textColor = "black";
        public string start;
        public string end;

        public static string ConvertToUnixTimestamp(DateTime date)
        {
            //DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            //TimeSpan diff = date.ToUniversalTime() - origin;
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = date - origin;
            return (Math.Floor(diff.TotalSeconds)).ToString();
        }
    }

    enum FormEdication
    {
        Group = 1,
        Private = 2,
        GroupPrivate = 3
    }

}