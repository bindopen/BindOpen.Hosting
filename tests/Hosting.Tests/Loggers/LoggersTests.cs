using BindOpen.Logging;
using BindOpen.Logging.Loggers;
using NUnit.Framework;

namespace BindOpen.Hosting.Loggers
{
    /// <summary>
    /// 
    /// </summary>
    [TestFixture]
    public class LoggersTests
    {
        /// <summary>
        /// 
        /// </summary>
        [OneTimeSetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void DefaultConfigurationTest()
        {
            var bdoHost = BdoHosting.NewHost(
                options => options
                    .SetLogger(q => BdoLogging.NewLogger<BdoTraceLogger>()));
            bdoHost.Start();

            Assert.That(bdoHost.State == ProcessExecutionState.Pending, "Application host not load failed");
        }
    }
}
