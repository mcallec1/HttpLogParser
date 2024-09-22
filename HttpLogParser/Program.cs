using HttpLogParser.Models;
using HttpLogParser.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace HttpLogParser
{
    public class Program
    {
        static void Main(string[] args)
        {
            string logFilePath = "programming-task-example-data.log";

            // read the log file and populate a list of log entries 
            List<LogItem> logEntries = LogParser.ParseLogFile(logFilePath);

            // store the results in an object.  this will give us some flexibility in how we use them
            LogAnalysisResult analysisResult = LogAnalyser.AnalyzeLogEntries(logEntries);

            // spec doesn't mention what to do with the results so just output to console.
            OutputResults(analysisResult);
        }

        static void OutputResults(LogAnalysisResult result)
        {
            Console.WriteLine($"Unique IP addresses: {result.UniqueIPCount}");

            Console.WriteLine($"\nTop {result.TopIPs.Count} most visited URLs:");
            for (int i = 0; i < result.TopUrls.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {result.TopUrls[i].Url} ({result.TopUrls[i].Count})");
            }

            Console.WriteLine($"\nTop {result.TopIPs.Count} most active IP addresses:");
            for (int i = 0; i < result.TopIPs.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {result.TopIPs[i].IP} ({result.TopIPs[i].Count})");
            }
        }
    }
}