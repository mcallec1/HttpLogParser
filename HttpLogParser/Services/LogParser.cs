using HttpLogParser.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HttpLogParser.Services
{
    public static class LogParser
    {
        // Regular expression to parse log lines with named groups
        // I try to avoid regex, but it was the most suitable tool for the job.
        private static readonly Regex LogPattern = new Regex(
            @"^(?<ip>\S+) \S+ \S+ \[(?<datetime>[^\]]+)\] ""(?<method>\S+) (?<url>\S+).*"" (?<status>\d{3}) (?<size>\d+)( ""(?<referrer>[^""]*)"" ""(?<useragent>[^""]*)"")?",
            RegexOptions.Compiled);

        public static List<LogItem> ParseLogFile(string logFilePath)
        {
            var logEntries = new List<LogItem>();

            // loop through each line in the logfile and populate a list of LogItem
            // Note: I could have done the calculations within this loop to satisfy the
            //       requirements of this exercise, but I chose to first load a list and then do the analysis later
            //       This is to accommodate future change in requirements.
            // Note 2: I could have also just grabbed the IP Address and URL from the log file, but grabbing all fields in case requirements change

            // Note on the above. I don't believe it always makes sense to future proof your code because it can create unecessary work that
            //  may never be used. In this case, it doesn't require a lot of additional work so I made the decision to do it here.
            foreach (var line in File.ReadLines(logFilePath))
            {
                // Note: the regx pattern will ignore any extra data on a row, eg one of the rows has 'junk extra' appended
                //       the row should still be included in the results, but the 'junk extra' will be ignored.
                var match = LogPattern.Match(line);
                if (match.Success)
                {
                    logEntries.Add(new LogItem
                    {
                        IPAddress = match.Groups["ip"].Value,
                        URL = GetDomainOnlyFromUrl(match.Groups["url"].Value),
                        Method = match.Groups["method"].Value,
                        StatusCode = int.Parse(match.Groups["status"].Value),
                        Size = int.Parse(match.Groups["size"].Value),
                        UserAgent = match.Groups["useragent"].Value,
                        DateTime = ParseDateTime(match.Groups["datetime"].Value)
                    });
                }
                else
                {
                    // log any unmatched rows
                    Console.WriteLine($"Warning: Unable to parse log line: {line}");
                }
            }

            return logEntries;
        }

        private static string GetDomainOnlyFromUrl(string url)
        {
            // more regex, but simplest way to achieve what I need here.
            return Regex.Replace(url, @"^https?:\/\/[^\/]+", "", RegexOptions.IgnoreCase);
        }

        private static DateTime ParseDateTime(string dateTimeStr)
        {
            return DateTime.ParseExact(dateTimeStr, "dd/MMM/yyyy:HH:mm:ss zzz", CultureInfo.InvariantCulture);
        }
    }
}
