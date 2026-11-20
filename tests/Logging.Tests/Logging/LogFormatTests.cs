using BindOpen.Data;
using BindOpen.Logging.Loggers;
using BindOpen.Logging.Tests;
using NUnit.Framework;
using System;

namespace BindOpen.Logging
{
    [TestFixture, Order(401)]
    public class LogFormatTests
    {
        private readonly string _filePath = GlobalVariables.WorkingFolder + "Log.xml";

        private dynamic _testData;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var log = BdoLogging.NewLog();
            for (int i = 0; i < 2; i++)
            {
                log
                    .AddError("Error" + i)
                    .AddException("Exception" + i)
                    .AddMessage("Message" + i)
                    .AddWarning("Warning" + i)
                    .AddChild(
                        BdoLogging.NewLog().WithTitle("Sub log" + i)
                    );
            }

            _testData = new
            {
                log
            };
        }

        [Test, Order(1)]
        public void LogToStringTest()
        {
            BdoLog log = _testData.log;
            var st = log.ToString<BdoSnapLoggerFormater>();
            var st_expected =
                " - Error: Error0" + Environment.NewLine +
                " - Exception: Exception0" + Environment.NewLine +
                " - Message: Message0" + Environment.NewLine +
                " - Warning: Warning0" + Environment.NewLine +
                " o Sub log0" + Environment.NewLine +
                " - Error: Error1" + Environment.NewLine +
                " - Exception: Exception1" + Environment.NewLine +
                " - Message: Message1" + Environment.NewLine +
                " - Warning: Warning1" + Environment.NewLine +
                " o Sub log1" + Environment.NewLine;

            Assert.That(st == st_expected, "Bad ToString function.");
        }

        [Test, Order(2)]
        public void LogEventToStringTest()
        {
            BdoLog log = _testData.log;
            var logEvent = log._Events?[0];
            var st = logEvent.ToString<BdoSnapLoggerFormater>();
            var st_expected = "- Error: Error0" + Environment.NewLine;

            Assert.That(st == st_expected, "Bad ToString function.");
        }
    }
}
