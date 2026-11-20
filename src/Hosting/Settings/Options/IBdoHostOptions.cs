using BindOpen.Data;
using BindOpen.Data.Stores;
using BindOpen.Logging.Loggers;
using BindOpen.Scoping;
using System;
using System.Collections.Generic;

namespace BindOpen.Hosting.Settings
{
    /// <summary>
    /// The interface defines the base BDO host options.
    /// </summary>
    public interface IBdoHostOptions : IBdoObject
    {
        void Update();

        // Root ----------------------

        string RootFolderPath { get; }

        /// <summary>
        /// The root folder path.
        /// </summary>
        List<(Predicate<IBdoHostOptions> Predicate, string Path)> RootFolderPathAssignments { get; set; }

        // Library ----------------------

        string LibraryFolderPath { get; set; }

        // Extensions ----------------------

        /// <summary>
        /// The extension load options.
        /// </summary>
        IExtensionLoadOptions ExtensionLoadOptions { get; set; }

        // Depots ----------------------

        /// <summary>
        /// The depot sets of this instance.
        /// </summary>
        IBdoDepotStore DepotStore { get; set; }

        // Configuration ----------------------

        /// <summary>
        /// The settings.
        /// </summary>
        IBdoSettings Settings { get; set; }

        /// <summary>
        /// The settings file path of this instance.
        /// </summary>
        List<(string Path, bool IsRequired, ConfigurationFileExtenions FileKind)> ConfigurationFiles { get; set; }

        IBdoHostOptions SetSettings(Action<IBdoSettings> action = null);

        IBdoHostOptions SetSettings<T>(Action<T> action = null) where T : class, IBdoSettings, new();

        // Loggers ----------------------

        /// <summary>
        /// The logger of this instance.
        /// </summary>
        public Func<IBdoHost, IBdoLogger> LoggerInit { get; set; }

        // Trigger actions ----------------------

        public List<(HostBdoEventKinds EventKind, Action<IBdoHost> _Action)> EventActions { get; set; }
    }
}