using BindOpen.Data;
using BindOpen.Data.Helpers;
using BindOpen.Data.Stores;
using BindOpen.Logging.Loggers;
using BindOpen.Scoping;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Hosting.Settings
{
    /// <summary>
    /// This class represents a host options.
    /// </summary>
    public class BdoHostOptions : BdoObject, IBdoHostOptions
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        // Root ----------------------

        public string RootFolderPath => _rootFolderPath;

        private string _rootFolderPath;

        /// <summary>
        /// The root folder path.
        /// </summary>
        public List<(Predicate<IBdoHostOptions> Predicate, string Path)> RootFolderPathAssignments { get; set; }

        // Library ----------------------

        public string LibraryFolderPath { get; set; }

        // Extensions ----------------------

        /// <summary>
        /// The extension loading options.
        /// </summary>
        public IExtensionLoadOptions ExtensionLoadOptions { get; set; }

        // Depot initialization actions ----------------------

        /// <summary>
        /// The data store of this instance.
        /// </summary>
        public IBdoDepotStore DepotStore { get; set; }

        // Configuration ----------------------

        /// <summary>
        /// The settings.
        /// </summary>
        public IBdoSettings Settings { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ConfigurationFilePath { get; set; }

        /// <summary>
        /// The settings file path of this instance.
        /// </summary>
        public List<(string Path, bool IsRequired, ConfigurationFileExtenions FileKind)> ConfigurationFiles { get; set; }

        /// <summary>
        /// Sets the options settings applying action on it.
        /// </summary>
        /// <param key="optionsSettings">The options settings to consider.</param>
        /// <param key="action">The action to apply on the settings.</param>
        public IBdoHostOptions SetSettings(Action<IBdoSettings> action = null)
            => SetSettings<BdoHostSettings>(action);

        /// <summary>
        /// Sets the options settings applying action on it.
        /// </summary>
        /// <param key="optionsSettings">The options settings to consider.</param>
        /// <param key="action">The action to apply on the settings.</param>
        public IBdoHostOptions SetSettings<T>(Action<T> action = null)
            where T : class, IBdoSettings, new()
        {
            Settings = new T();
            action?.Invoke((T)Settings);

            return this;
        }

        // Loggers -------------------------

        /// <summary>
        /// The logger initialization function of this instance.
        /// </summary>
        public Func<IBdoHost, IBdoLogger> LoggerInit { get; set; }

        // Trigger actions ----------------------

        public List<(HostBdoEventKinds EventKind, Action<IBdoHost> _Action)> EventActions { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoHostOptions class.
        /// </summary>
        public BdoHostOptions() : base()
        {
            ExtensionLoadOptions = new ExtensionLoadOptions()
                .AddSource(DatasourceKind.Memory)
                .AddSource(DatasourceKind.Repository, (@".\" + BdoDefaultHostPaths.__DefaultLibraryFolderPath).ToPath());
        }

        #endregion


        /// <summary>
        /// Updates this instance.
        /// </summary>
        /// <param key="item">The item to consider.</param>
        /// <param key="specificationAreas">The specification areas to consider.</param>
        /// <param key="updateModes">The update modes to consider.</param>
        /// <returns>The log of the operation.</returns>
        /// <remarks>Put reference sets as null if you do not want to repair this instance.</remarks>
        public void Update()
        {
            var appRootFolderPath = FileHelper.GetAppRootFolderPath();
            var defaultConfifFilePath = @".\bindopen.xml";

            // Root path

            RootFolderPathAssignments ??= new();

            var assignments = new List<(Predicate<IBdoHostOptions> Predicate, string Path)>();

            foreach (var assignment in RootFolderPathAssignments)
            {
                var folderPath = assignment.Path;
                if (string.IsNullOrEmpty(folderPath))
                {
                    folderPath = appRootFolderPath;
                }

                folderPath = folderPath.GetConcatenatedPath(appRootFolderPath).EndingWith(@"\").ToPath();

                assignments.Add((assignment.Predicate, folderPath));
            }

            var trueAssignment = assignments?.LastOrDefault(p => p.Predicate(this) == true);
            if (!string.IsNullOrEmpty(trueAssignment?.Path))
            {
                _rootFolderPath = trueAssignment?.Path;
            }
            else
            {
                _rootFolderPath = appRootFolderPath;
            }

            // Configuration paths

            if (ConfigurationFiles?.Any() != true)
            {
                ConfigurationFiles = new() { (null, false, ConfigurationFileExtenions.Any) };
            }

            var configurationFiles = new List<(string Path, bool IsRequired, ConfigurationFileExtenions Extension)>();

            foreach (var file in ConfigurationFiles)
            {
                var filePath = file.Path;

                if (file.Path == null)
                {
                    filePath = defaultConfifFilePath;
                }

                filePath = filePath.GetConcatenatedPath(_rootFolderPath).ToPath();

                configurationFiles.Add((filePath, file.IsRequired, file.FileKind));
            }

            ConfigurationFiles = configurationFiles;

            // Library paths

            this.WithLibraryFolder(LibraryFolderPath.GetConcatenatedPath(_rootFolderPath).EndingWith(@"\").ToPath());

            ExtensionLoadOptions?.AddSource(DatasourceKind.Repository, LibraryFolderPath);
        }

        // ------------------------------------------
        // IDISPOSABLE METHODS
        // ------------------------------------------

        #region IDisposable_Methods

        private bool _isDisposed = false;

        /// <summary>
        /// Disposes this instance. 
        /// </summary>
        /// <param key="isDisposing">Indicates whether this instance is disposing</param>
        protected override void Dispose(bool isDisposing)
        {
            if (_isDisposed)
            {
                return;
            }

            ExtensionLoadOptions?.Dispose();
            DepotStore?.Dispose();

            _isDisposed = true;

            base.Dispose(isDisposing);
        }

        #endregion
    }
}