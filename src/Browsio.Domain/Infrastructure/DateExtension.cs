using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Browsio.Domain
{
    using Incoding.Extensions;

    public static class DateExtension
    {
        public static string TimeSpanToString(this DateTime dateTime)
        {
            TimeSpan timeSpan = DateTime.Now.Subtract(dateTime);
            if (timeSpan.TotalSeconds > 60)
            {
                foreach (var pair in new Dictionary<string, double>
                                         {
                                                 { "month", timeSpan.Days / 30 },
                                                 { "day", timeSpan.TotalDays },
                                                 { "hour", timeSpan.TotalHours },
                                                 { "minute", timeSpan.TotalMinutes },
                                         })
                {

                    var count = Math.Truncate(pair.Value);
                    if (count <= 0)
                        continue;

                    var countStr = count == 1 ? "one" : count.ToString();
                    var manyStr = count == 1 ? "" : "s";
                    return "{0} {1}{2} ago".F(countStr, pair.Key, manyStr);

                }
            }
            return "just now"; ;
        }
    }
}