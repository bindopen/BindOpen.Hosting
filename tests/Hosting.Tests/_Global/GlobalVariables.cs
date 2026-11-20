using BindOpen.Data.Helpers;
using BindOpen.Hosting.Settings;
using Microsoft.Extensions.Configuration;
using System;

namespace BindOpen.Hosting.Tests
{
    public static class GlobalVariables
    {
        static string _workingFolder = null;
        static IBdoHost _bdoHost = null;
        static IConfiguration _netCoreConfiguration;

        public static string WorkingFolder
        {
            get
            {
                string workingFolder = _workingFolder;
                if (workingFolder == null)
                    _workingFolder = workingFolder =
                        ((_bdoHost?.GetKnownPath(BdoHostPathKind.RootFolder) ?? AppDomain.CurrentDomain.BaseDirectory)
                        .EndingWith(@"\") + @"bdo\temp\").ToPath();

                return workingFolder;
            }
        }

        public static IBdoHost AppHost
        {
            get
            {
                _bdoHost ??= BdoHosting.NewHost(
                    options => options
                        .ThrowExceptionOnInitFailure());

                _bdoHost?.Start();

                return _bdoHost;
            }
        }

        public static IConfiguration NetCoreConfiguration
        {
            get
            {
                if (_netCoreConfiguration != null)
                {
                    return _netCoreConfiguration;
                }

                var builder = new ConfigurationBuilder()
                    .SetBasePath(AppHost?.GetKnownPath(BdoHostPathKind.RootFolder))
                    .AddJsonFile(@"bdo\config\appsettings.json".ToPath(), optional: true, reloadOnChange: true);

                return _netCoreConfiguration = builder.Build();
            }
        }
    }

}
