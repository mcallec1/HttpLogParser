using HttpLogParser.Models;
using HttpLogParser.Services;

namespace HttpLogParser.Tests
{
    [TestFixture]
    public class LogParserTests
    {
        [Test]
        public void ParseLogLine_ValidLogLine_ReturnsCorrectLogItem()
        {
            // Arrange
            string logLine = @"177.71.128.21 - - [09/May/2018:16:00:38 +0000] ""GET /incentivize HTTP/1.1"" 302 4622 ""-"" ""Mozilla/5.0""";

            // Act
            LogItem logItem = LogParser.ParseLogLine(logLine);

            // Assert
            Assert.IsNotNull(logItem);
            Assert.AreEqual("177.71.128.21", logItem.IPAddress);
            Assert.AreEqual("/incentivize", logItem.URL);
            Assert.AreEqual("GET", logItem.Method);
            Assert.AreEqual(302, logItem.StatusCode);
            Assert.AreEqual(4622, logItem.Size);
            Assert.AreEqual("Mozilla/5.0", logItem.UserAgent);
            Assert.AreEqual(new DateTime(2018, 5, 9, 16, 0, 38), logItem.DateTime);
        }

        [Test]
        public void ParseLogLine_InvalidLogLine_ReturnsNull()
        {
            // Arrange
            string logLine = "Invalid log line";

            // Act
            LogItem logItem = LogParser.ParseLogLine(logLine);

            // Assert
            Assert.IsNull(logItem);
        }

        [Test]
        public void ParseLogLine_LogLineWithExtraData_ReturnsCorrectLogItem()
        {
            // Arrange
            string logLine = @"127.0.0.1 - - [10/Oct/2020:13:55:36 +0000] ""GET /home HTTP/1.1"" 200 1234 ""-"" ""TestAgent/1.0"" junk extra";

            // Act
            LogItem logItem = LogParser.ParseLogLine(logLine);

            // Assert
            Assert.IsNotNull(logItem);
            Assert.AreEqual("127.0.0.1", logItem.IPAddress);
            Assert.AreEqual("/home", logItem.URL);
            Assert.AreEqual("GET", logItem.Method);
            Assert.AreEqual(200, logItem.StatusCode);
            Assert.AreEqual(1234, logItem.Size);
            Assert.AreEqual("TestAgent/1.0", logItem.UserAgent);
            Assert.AreEqual(new DateTime(2020, 10, 10, 13, 55, 36), logItem.DateTime);
        }

    }
}