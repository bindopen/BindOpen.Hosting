using BindOpen.Data.Helpers;
using BindOpen.Logging;
using NUnit.Framework;
using System.IO;

namespace BindOpen.Hosting.Settings
{
    /// <summary>
    /// 
    /// </summary>
    [TestFixture]
    public class PathTests
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
        public void RootFolderTest()
        {
            var bdoHost = BdoHosting.NewHost(
                options => options
                    .SetRootFolder("rootFolder")
                    .AddRootFolder(s => false, "rootFolderA")
                    .AddRootFolder(s => true, "rootFolderB")
                    .WithLibraryFolder(@".\bdo\lib"));
            bdoHost.Start();

            Assert.That(bdoHost.State == ProcessExecutionState.Pending, "Application host not load failed");

            var rootFolderPath = bdoHost.GetKnownPath(BdoHostPathKind.RootFolder);

            Assert.That(rootFolderPath == @"rootFolderB\".ToPath(), "Bad library folder path");

            bdoHost = BdoHosting.NewHost(
                options => options
                    .SetRootFolder("rootFolder")
                    .AddRootFolder(s => false, "rootFolderA")
                    .WithLibraryFolder(@".\bdo\lib"));
            bdoHost.Start();

            Assert.That(bdoHost.State == ProcessExecutionState.Pending, "Application host not load failed");

            rootFolderPath = bdoHost.GetKnownPath(BdoHostPathKind.RootFolder);

            Assert.That(rootFolderPath == @"rootFolder\".ToPath(), "Bad library folder path");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void DefaultRootFolderTest()
        {
            var bdoHost = BdoHosting.NewHost(
                options => options
                    .SetRootFolder(@".\default")
                    .AddRootFolder(s => false, "rootFolderA")
                    .WithLibraryFolder(@".\bdo\lib"));
            bdoHost.Start();

            Assert.That(bdoHost.State == ProcessExecutionState.Pending, "Application host not load failed");

            var rootFolderPath = bdoHost.GetKnownPath(BdoHostPathKind.RootFolder);

            var path = Path.Combine(FileHelper.GetAppRootFolderPath(), @"default".ToPath()).EndingWith(@"\");

            Assert.That(rootFolderPath == path, "Bad library folder path");

            // No root folder set initially

            bdoHost = BdoHosting.NewHost(
                options => options
                    .AddRootFolder(s => false, "rootFolderA")
                    .WithLibraryFolder(@".\bdo\lib"));
            bdoHost.Start();

            Assert.That(bdoHost.State == ProcessExecutionState.Pending, "Application host not load failed");

            rootFolderPath = bdoHost.GetKnownPath(BdoHostPathKind.RootFolder);

            path = FileHelper.GetAppRootFolderPath().EndingWith(@"\");

            Assert.That(rootFolderPath == path, "Bad library folder path");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void LibraryFolderTest()
        {
            var bdoHost = BdoHosting.NewHost(
                options => options
                    .SetRootFolder("./")
                    .WithLibraryFolder(@".\bdo\lib"));
            bdoHost.Start();

            Assert.That(bdoHost.State == ProcessExecutionState.Pending, "Bad library folder path");

            var libraryPath = bdoHost.GetKnownPath(BdoHostPathKind.LibraryFolder);

            var path = Path.Combine(bdoHost.GetKnownPath(BdoHostPathKind.RootFolder), @"bdo\lib".ToPath()).EndingWith(@"\");

            Assert.That(libraryPath == path, "Bad library folder path");
        }
    }
}