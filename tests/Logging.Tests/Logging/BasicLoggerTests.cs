using BindOpen.Data;
using BindOpen.Logging.Tests;
using NUnit.Framework;

namespace BindOpen.Logging.Loggers;

[TestFixture, Order(400)]
public class BasicLoggerTests
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

    private void PopulateLog(IBdoLog log)
    {
        if (log != null)
        {
            for (int i = 0; i < _testData.itemNumber; i++)
            {
                log.AddError("Error" + i);
                log.AddException("Exception" + i);
                log.AddMessage("Message" + i);
                log.AddWarning("Warning" + i);
                log.InsertChild(q => q.WithTitle("Child" + i))
                    .AddError("Error" + i + "-1");
            }

            var subLog = log?.InsertChild(BdoEventKinds.Message, "Initializing Child...");
            var childLog = subLog?.InsertChild(BdoEventKinds.Message, "Loading Child configuration...");
        }
    }

    [Test, Order(1)]
    public void RootLogTest()
    {
        var logger = BdoLogging.NewLogger<BdoTraceLogger>();

        var log = logger.NewRootLog();

        var newLog = logger.NewRootLog();

        Assert.That(log?.Identifier == newLog?.Identifier, "Root log must be the same");
    }

    [Test, Order(2)]
    public void TraceLoggerTest()
    {
        var logger = BdoLogging.NewLogger<BdoTraceLogger>();

        var log = logger.NewRootLog().AddChild();
        PopulateLog(log);

        logger.Log(log);
    }

    [Test, Order(3)]
    public void ConsoleLoggerTest()
    {
        var logger = BdoLogging.NewLogger<BdoConsoleLogger>();

        var log = logger.NewRootLog();
        PopulateLog(log);

        logger.Log(log);
    }
}
