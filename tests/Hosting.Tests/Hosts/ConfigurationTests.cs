using BindOpen.Hosting.Tests;
using BindOpen.Logging;
using NUnit.Framework;

namespace BindOpen.Hosting
{
    /// <summary>
    /// 
    /// </summary>
    [TestFixture]
    public class ConfigurationTests
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
                    .SetSettings(q => q.Kernel.ApplicationInstanceName = "host-test"));
            bdoHost.Start();

            Assert.That(bdoHost.State == ProcessExecutionState.Pending, "Application host not load failed");

            Assert.That(bdoHost.Options?.Settings?.Kernel?.ApplicationInstanceName == "host-test", "Application host not load failed");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void CastingConfigTest()
        {
            var bdoHost = BdoHosting.NewHost(
                options => options
                    .SetRootFolder(@".\")
                    .SetSettings<TestSettings>());
            bdoHost.Start();

            Assert.That(bdoHost.Options.Settings is TestSettings, "Application host not load failed");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void KernelConfigFileTest()
        {
            var bdoHost = BdoHosting.NewHost(
                options => options
                    .SetRootFolder(@".\")
                    .SetSettings<TestSettings>()
                    .AddConfigurationFile());
            bdoHost.Start();

            var settings = bdoHost.Options.GetSettings<TestSettings>();

            //Assert.That(settings.Kernel.ApplicationInstanceName == "test.console", "Application host not load failed");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void SeveralFilesTest()
        {
            var bdoHost = BdoHosting.NewHost(
                options => options
                    .SetRootFolder(@".\bdo\config")
                    .SetSettings<TestSettings>()
                    .AddConfigurationFile(@".\appconfig.xml", true)
                    .AddConfigurationFile(@".\appconfig2.xml", true)
                    .ThrowExceptionOnInitFailure());
            bdoHost.Start();

            var settings = bdoHost.Options.GetSettings<TestSettings>();

            Assert.That(settings.FolderPath == "_folderPath", "Application host not load failed");
            Assert.That(settings.NewsUris.Count == 2, "Application host not load failed");
        }
    }
}
