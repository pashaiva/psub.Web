using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Infrastructure.Utilities
{
    public static class DateTimeUtilities
    {
        public static List<QuarterMonth> GetQuarterMonths(DateTime finish, int backDelay = 11)
        {
            const int monthsInQuarter = 3;
            var result = new List<QuarterMonth>();
            var monthNames = new CultureInfo("ru-RU").DateTimeFormat.MonthNames;

            var start = new DateTime(finish.Year, finish.Month, 1);
            for (int monthDelay = backDelay; monthDelay >= 0; monthDelay--)
            {
                var current = start.AddMonths(-1 * monthDelay);
                bool firstMonthInQuarter = monthDelay == backDelay || current.Month % monthsInQuarter == 1;

                var quarterMonth = new QuarterMonth
                {
                    FirstDay = current,
                    MonthName = monthNames[current.Month - 1],
                    QuarterName = string.Format("{0} кв, {1}", Math.Ceiling((double)current.Month / monthsInQuarter), current.Year.ToString(CultureInfo.InvariantCulture)),
                    IsBeginOfQuarter = firstMonthInQuarter,
                    MonthsInQuarter = new List<DateTime>()
                };
                result.Add(quarterMonth);

                if (firstMonthInQuarter)
                {
                    quarterMonth.MonthsInQuarter.Add(current);
                }
                else
                {
                    var beginOfLastQuarter = result.Last(x => x.IsBeginOfQuarter);
                    if (beginOfLastQuarter != null)
                    {
                        beginOfLastQuarter.MonthsInQuarter.Add(current);
                    }
                }
            }
            return result;
        }
    }

    #region models

    public class QuarterMonth
    {
        public DateTime FirstDay { get; set; }
        public string MonthName { get; set; }
        public string QuarterName { get; set; }
        public bool IsBeginOfQuarter { get; set; }
        public List<DateTime> MonthsInQuarter { get; set; }
    }

    #endregion
}
