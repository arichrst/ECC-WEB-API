using System;

namespace ECC
{
    public static class Formatter
    {
        public static string To_ddMMMyyyy(this DateTime date)
        {
            return date.ToString("dd MMM yyyy");
        }

        public static string To_ddMMMyyyymmhhss(this DateTime date)
        {
            return date.ToString("dd MMM yyyy mm:hh:ss");
        }

        public static string TimeAgo(this DateTime date, DateTime endDate)
        {
            var tmp = (int)(endDate-date).TotalSeconds;
            if(tmp/(3600*24) > 7)
                return date.To_ddMMMyyyy();
            else if(tmp/(3600*24) > 1 && tmp/(3600*24) <= 7)
                return date.To_ddMMMyyyy();
            else if(tmp/(3600*24) > 1 && tmp/(3600*24) <= 7)
                return (tmp/(3600*24)).ToString() + " days ago";
            else if(tmp/(3600*24) > 0 && tmp/(3600*24) <= 1)
                return "tomorrow";
            else if(tmp/3600 > 0 && tmp/3600 < 24)
                return (tmp/3600).ToString() + " hours ago";
            else if(tmp/60 > 0 && tmp/60 < 60)
                return (tmp/60).ToString() + " minutes ago";
            else
                return (tmp).ToString() + " seconds ago";
        }
    }
}