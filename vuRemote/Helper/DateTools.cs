using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vuRemote
{
    public class DateTools
    {

        public DateTools() { }

        public static string PrintDayOfWeek(DayOfWeek day)
        {
            switch (day)
            {
                case DayOfWeek.Monday:
                    return "Mo";
                case DayOfWeek.Tuesday:
                    return "Di";
                case DayOfWeek.Wednesday:
                    return "Mi";
                case DayOfWeek.Thursday:
                    return "Do";
                case DayOfWeek.Friday:
                    return "Fr";
                case DayOfWeek.Saturday:
                    return "Sa";
                case DayOfWeek.Sunday:
                    return "So";
            }
            return "  ";

        }
    }
}
