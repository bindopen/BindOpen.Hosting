﻿using BindOpen.Kernel.Hosting.Tests;
using BindOpen.Kernel.Logging;
using NUnit.Framework;

namespace BindOpen.Kernel.Hosting
{
    /// <summary>
    /// 
    /// </summary>
    [TestFixture]
    public class AppHostTests
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
        public void TestAppHost()
        {
            Assert.That(GlobalVariables.AppHost.State == ProcessExecutionState.Pending, "Application host not load failed");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void TestAppHostWithNoOptions()
        {
            var bdoHost = BdoHosting.NewHost();
            bdoHost.Start();

            Assert.That(bdoHost.State == ProcessExecutionState.Pending, "Application host not load failed");
        }
    }
}
