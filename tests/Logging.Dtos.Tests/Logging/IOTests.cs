using BindOpen.Data;
using NUnit.Framework;
using System.IO;
using System.Linq;

namespace BindOpen.Logging.Dtos.Tests;

[TestFixture, Order(400)]
public class IOTests
{
    private readonly string _filePath_xml = GlobalVariables.WorkingFolder + "Log.xml";
    private readonly string _filePath_json = GlobalVariables.WorkingFolder + "Log.json";

    private IBdoCompleteLog _log = null;

    private dynamic _testData;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _testData = new
        {
            itemNumber = 1000
        };
    }

    private void Test(IBdoCompleteLog log)
    {
        Assert.That(log.Errors(false).Count() == _testData.itemNumber, string.Format("Bad insertion of errors ({0} expected; {1} found)", _testData.itemNumber, _log.Errors().Count()));
        Assert.That(log.Exceptions().Count() == _testData.itemNumber, string.Format("Bad insertion of exceptions ({0} expected; {1} found)", _testData.itemNumber, _log.Exceptions().Count()));
        Assert.That(log.Messages().Count() == _testData.itemNumber, string.Format("Bad insertion of messages ({0} expected; {1} found)", _testData.itemNumber, _log.Messages().Count()));
        Assert.That(log.Warnings().Count() == _testData.itemNumber, string.Format("Bad insertion of warnings ({0} expected; {1} found)", _testData.itemNumber, _log.Warnings().Count()));
        Assert.That(log.Children().Count() == _testData.itemNumber, string.Format("Bad insertion of sub logs ({0} expected; {1} found)", _testData.itemNumber, _log.Children().Count()));
    }

    [Test, Order(1)]
    public void CreateTest()
    {
        _log = BdoLogging.NewLog()
            .WithDetail(
                BdoData.NewMeta("string", DataValueTypes.Text, "stringValue"),
                BdoData.NewMeta("int", DataValueTypes.Integer, 1500)
            );

        for (int i = 0; i < _testData.itemNumber; i++)
        {
            _log.AddError("Error" + i);
            _log.AddException("Exception" + i);
            _log.AddMessage("Message" + i);
            _log.AddWarning("Warning" + i);
            _log.AddChild(BdoLogging.NewLog()
                .WithTitle("Child" + i)
                .AddError("Child_Error" + i + "_1")
            );
        }

        Test(_log);
    }

    // Xml

    [Test, Order(2)]
    public void SaveXmlTest()
    {
        if (_log == null)
        {
            CreateTest();
        }

        var log = BdoLogging.NewLog();
        _log.ToDto()?.SaveXml(_filePath_xml, log);

        string xml = string.Empty;
        if (log.HasErrorOrException())
        {
            xml = ". Result was '" + log.ToDto().ToXml() + "'";
        }
        Assert.That(!log.HasErrorOrException(), "Log saving failed" + xml);
    }

    [Test, Order(3)]
    public void LoadXmlTest()
    {
        if (_log == null || !File.Exists(_filePath_xml))
        {
            SaveXmlTest();
        }

        BdoLog log = BdoLogging.NewLog();
        _log = XmlHelper.LoadXml<LogDto>(_filePath_xml, log: log).ToPoco();

        string xml = string.Empty;
        if (log.HasErrorOrException())
        {
            xml = ". Result was '" + log.ToDto().ToXml() + "'";
        }
        Assert.That(_log.HasErrorOrException(), "Error while loading log" + xml);

        Test(_log);
    }

    // Json

    [Test, Order(2)]
    public void SaveJsonTest()
    {
        if (_log == null)
        {
            CreateTest();
        }

        var log = BdoLogging.NewLog();
        _log.ToDto()?.SaveJson(_filePath_json, log);

        string xml = string.Empty;
        if (log.HasErrorOrException())
        {
            xml = ". Result was '" + log.ToDto().ToJson() + "'";
        }
        Assert.That(!log.HasErrorOrException(), "Log saving failed" + xml);
    }

    [Test, Order(3)]
    public void LoadJsonTest()
    {
        if (_log == null || !File.Exists(_filePath_json))
        {
            SaveJsonTest();
        }

        BdoLog log = BdoLogging.NewLog();
        _log = JsonHelper.LoadJson<LogDto>(_filePath_json, log: log).ToPoco();

        string xml = string.Empty;
        if (log.HasErrorOrException())
        {
            xml = ". Result was '" + log.ToDto().ToJson() + "'";
        }
        Assert.That(_log.HasErrorOrException(), "Error while loading log" + xml);

        Test(_log);
    }
}
