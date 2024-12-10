using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace additions
{
    public class CalenderHelp
    {
        public static string MiladiToShamsi(DateTime dateTime )
        {

            PersianCalendar p = new PersianCalendar();
            int year = p.GetYear(dateTime);
            int month = p.GetMonth(dateTime);
            int day = p.GetDayOfMonth(dateTime);
            var result = string.Concat(year, month,day);
            return result;
        }
        public static (string,string) MiladiToShamsiBoth(DateTime dateTime)
        {
            PersianCalendar p = new PersianCalendar();
            int year = p.GetYear(dateTime);
            int month = p.GetMonth(dateTime);
            int day = p.GetDayOfMonth(dateTime);
            var result = string.Concat(year,month.ToString("D2") , "01");
            var result2 = string.Concat(year, month.ToString("D2"), "30");
            return new (result,result2);
        }
    }
}
