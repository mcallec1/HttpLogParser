using NUnit.Framework;
using HttpLogParser.Services;
using HttpLogParser.Models;
using System.Collections.Generic;

namespace HttpLogParser.Tests
{
    [TestFixture]
    public class LogAnalyserTests
    {
        [Test]
        public void AnalyzeLogEntries_MultipleEntries_ReturnsCorrectUniqueIPCount()
        {
            // Arrange
            var logEntries = new List<LogItem>
            {
                new LogItem { IPAddress = "10.0.0.1", URL = "/page1" },
                new LogItem { IPAddress = "10.0.0.2", URL = "/page2" },
                new LogItem { IPAddress = "10.0.0.1", URL = "/page3" },
                new LogItem { IPAddress = "10.0.0.3", URL = "/page1" },
                new LogItem { IPAddress = "10.0.0.2", URL = "/page2" },
                new LogItem { IPAddress = "10.0.0.4", URL = "/page4" },
            };

            // Act
            LogAnalysisResult result = LogAnalyser.AnalyzeLogEntries(logEntries);

            // Assert
            Assert.AreEqual(4, result.UniqueIPCount);
        }

        [Test]
        public void AnalyzeLogEntries_NoEntries_ReturnsZeroCounts()
        {
            // Arrange
            var logEntries = new List<LogItem>();

            // Act
            LogAnalysisResult result = LogAnalyser.AnalyzeLogEntries(logEntries);

            // Assert
            Assert.AreEqual(0, result.UniqueIPCount);
            Assert.IsEmpty(result.TopUrls);
            Assert.IsEmpty(result.TopIPs);
        }

        [Test]
        public void AnalyzeLogEntries_TiesInTopUrls_ReturnsCorrectTopUrls()
        {
            // Arrange
            var logEntries = new List<LogItem>
            {
                new LogItem { IPAddress = "192.168.0.1", URL = "/common" },
                new LogItem { IPAddress = "192.168.0.2", URL = "/common" },
                new LogItem { IPAddress = "192.168.0.3", URL = "/unique1" },
                new LogItem { IPAddress = "192.168.0.4", URL = "/unique2" },
            };

            // Act
            LogAnalysisResult result = LogAnalyser.AnalyzeLogEntries(logEntries);

            // Assert
            Assert.AreEqual(4, result.UniqueIPCount);
            Assert.AreEqual(3, result.TopUrls.Count);
            Assert.AreEqual("/common", result.TopUrls[0].Url);
            Assert.AreEqual(2, result.TopUrls[0].Count);
        }
    }
}
