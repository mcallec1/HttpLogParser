using HttpLogParser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpLogParser.Services
{
    public static class LogAnalyser
    {
        public static LogAnalysisResult AnalyzeLogEntries(List<LogItem> logEntries)
        {
            // Note: the value 3 is hardcoded here which isn't necessarily the best approach.
            //       should suffice for this exercise
            return new LogAnalysisResult
            {
                UniqueIPCount = CountUniqueIPs(logEntries),
                TopUrls = GetTopUrls(logEntries, 3),
                TopIPs = GetTopIPs(logEntries, 3)
            };
        }

        private static int CountUniqueIPs(List<LogItem> logEntries)
        {
            return logEntries.Select(le => le.IPAddress).Distinct().Count();
        }

        private static List<(string Url, int Count)> GetTopUrls(List<LogItem> logEntries, int count)
        {
            return logEntries
                .GroupBy(le => le.URL)
                .OrderByDescending(g => g.Count())
                .Take(count)
                .Select(g => (g.Key, g.Count()))
                .ToList();
        }

        private static List<(string IP, int Count)> GetTopIPs(List<LogItem> logEntries, int count)
        {
            return logEntries
                .GroupBy(e => e.IPAddress)
                .OrderByDescending(g => g.Count())
                .Take(count)
                .Select(g => (g.Key, g.Count()))
                .ToList();
        }
    }
}
