using BindOpen.Logging.Tests;
using NUnit.Framework;

namespace BindOpen.Logging.Loggers;

[TestFixture, Order(400)]
public class LoggerLogTests
{
    private readonly string _filePath_serilog = GlobalVariables.WorkingFolder + "Debug.txt";

    private dynamic _testData;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _testData = new
        {
            itemNumber = 1000
        };
    }

    [Test, Order(1)]
    public void RootLogTest()
    {
        var logger = BdoLogging.NewLogger<BdoTraceLogger>();

        var log = logger.NewRootLog();

        log.WithExecutionAsStarted();

        Assert.That(log?.Execution?.Status == ProcessExecutionStatus.Processing, "Root log must be the same");
    }
}
