using BindOpen.Data;
using BindOpen.Data.Helpers;
using BindOpen.Data.Meta;
using BindOpen.Hosting.Settings;
using BindOpen.Logging;
using BindOpen.Logging.Loggers;
using BindOpen.Scoping;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BindOpen.Hosting
{
    /// <summary>
    /// This class represents a host.
    /// </summary>
    public partial class BdoHost : BdoScope, IBdoHost
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoHost class.
        /// </summary>
        /// <param key="log"></param>
        public BdoHost()
        {
        }

        #endregion

        // ------------------------------------------
        // IBdoHost Implementation
        // ------------------------------------------

        #region IBdoHost

        /// <summary>
        /// The options of this instance.
        /// </summary>
        public IBdoHostOptions Options { get; set; }

        public IBdoLogger Logger { get; set; }

        public List<IBdoLogger> Loggers { get; set; }

        public ProcessExecutionState State => _state;

        protected ProcessExecutionState _state;

        /// <summary>
        /// Starts the application.
        /// </summary>
        /// <returns>Returns true if this instance is started.</returns>
        public virtual void Start()
        {
            // we start the application instance

            _state = ProcessExecutionState.Pending;

            // we initialize this instance

            Initialize();

            //log?.Sanitize();

            var log = Logger?.NewRootLog();
            log?.AddEvent(BdoEventKinds.Message, "Host starting...");

            if (_state == ProcessExecutionState.Pending)
            {
                log?.AddEvent(BdoEventKinds.Message, "Host started successfully");
                InitSucceeds();
            }
            else
            {
                log?.AddEvent(BdoEventKinds.Message, "Host loaded with errors");
                Stop();
                InitFails();
            }

            Logger?.Log(log);
        }

        /// <summary>
        /// Indicates the application ends.
        /// </summary>
        public virtual void Stop()
        {
            // we unload the host (syncrhonously for the moment)
            _state = ProcessExecutionState.Ended;
            Clear();

            var log = Logger?.NewRootLog();
            log?.AddEvent(BdoEventKinds.Message, q => q.WithTitle("Host ended"));
            Logger?.Log(log);
        }

        // Trigger actions --------------------------------------

        public event EventHandler OnInitSucceeds;

        /// <summary>
        /// Indicates that this instance has successfully started.
        /// </summary>
        private void InitSucceeds()
        {
            InvokeTriggerAction(HostBdoEventKinds.OnInitSuccess);

            OnInitSucceeds?.Invoke(this, new EventArgs());
        }

        public event EventHandler OnInitFails;

        /// <summary>
        /// Indicates that this instance has not successfully started.
        /// </summary>
        private void InitFails()
        {
            InvokeTriggerAction(HostBdoEventKinds.OnInitFailure);

            OnInitFails?.Invoke(this, new EventArgs());
        }

        public event EventHandler OnExecutionSucceeds;

        /// <summary>
        /// Indicates that this instance completes.
        /// </summary>
        private void ExecutionSucceeds()
        {
            InvokeTriggerAction(HostBdoEventKinds.OnExecutionSucess);

            OnExecutionSucceeds?.Invoke(this, new EventArgs());
        }

        public event EventHandler OnExecutionFails;

        /// <summary>
        /// Indicates that this instance fails.
        /// </summary>
        private void ExecutionFails()
        {
            InvokeTriggerAction(HostBdoEventKinds.OnExecutionFailure);

            OnExecutionFails?.Invoke(this, new EventArgs());
        }

        private void InvokeTriggerAction(HostBdoEventKinds eventKind)
        {
            var action = Options?.EventActions?.FirstOrDefault(q => (eventKind & HostBdoEventKinds.Any) == (q.EventKind & HostBdoEventKinds.Any));
            action?._Action?.Invoke(this);
        }

        // Paths --------------------------------------

        /// <summary>
        /// Initializes information.
        /// </summary>
        /// <returns>Returns the log of the task.</returns>
        protected virtual bool Initialize()
        {
            var loaded = true;

            // we update options (specially paths)

            Options.Update();

            // we set the logger

            Logger = Options.LoggerInit?.Invoke(this);

            var log = Logger?.NewRootLog();

            // we launch the standard initialization of service

            var subLog = log?.InsertChild(BdoEventKinds.Message, "Initializing host...");

            IBdoLog childLog = null;

            DataStore.Add(("$host", this));

            try
            {
                // we load the host config

                childLog = subLog?.InsertChild(BdoEventKinds.Message, "Loading host configuration...");

                Options.Settings ??= BdoData.NewMetaWrapper<BdoHostSettings>(this);

                if (Options?.ConfigurationFiles != null)
                {
                    foreach (var file in Options.ConfigurationFiles)
                    {
                        if (loaded)
                        {
                            var path = file.Path.GetConcatenatedPath(this.GetKnownPath(BdoHostPathKind.RootFolder));

                            if (!File.Exists(path))
                            {
                                loaded &= !file.IsRequired;
                                subLog?.AddEvent(
                                    file.IsRequired ? BdoEventKinds.Error : BdoEventKinds.Warning,
                                    "Host config file ('" + BdoDefaultHostPaths.__DefaultHostConfigFileName + "') not found");
                            }
                            else
                            {
                                ConfigurationDto configDto = null;
                                var fileExtension = ConfigurationFileExtenions.Any;

                                if (fileExtension == ConfigurationFileExtenions.Any)
                                {
                                    fileExtension = (Path.GetExtension(path)?.ToLower()) switch
                                    {
                                        ".json" => ConfigurationFileExtenions.Json,
                                        _ => ConfigurationFileExtenions.Xml,
                                    };
                                }

                                switch (fileExtension)
                                {
                                    case ConfigurationFileExtenions.Json:
                                        configDto = JsonHelper.LoadJson<ConfigurationDto>(path, log);
                                        break;
                                    case ConfigurationFileExtenions.Xml:
                                        configDto = XmlHelper.LoadXml<ConfigurationDto>(path, log);
                                        break;
                                }

                                var config = configDto.ToPoco();

                                Options.Settings.UpdateDetail(config);
                                Options.Settings.UpdateProperties();

                                if (childLog?.HasEvent(BdoEventKinds.Error, BdoEventKinds.Exception) != true)
                                {
                                    childLog?.AddEvent(BdoEventKinds.Message, "Host config loaded");
                                }
                            }
                        }
                    }
                }

                if (loaded)
                {
                    // we load extensions

                    childLog = subLog?.InsertChild(BdoEventKinds.Message, "Loading extensions...");

                    loaded &= this.LoadExtensions(
                        q => q = Options.ExtensionLoadOptions
                            .AddSource(DatasourceKind.Repository, this.GetKnownPath(BdoHostPathKind.LibraryFolder)),
                        childLog);
                }

                if (_state == ProcessExecutionState.Pending)
                {
                    // we load the data store

                    Clear();

                    DepotStore = Options?.DepotStore;

                    childLog = subLog?.InsertChild(BdoEventKinds.Message, "Loading data store...");
                    if (DepotStore == null)
                    {
                        childLog?.AddEvent(BdoEventKinds.Message, title: "No data store registered");
                    }
                    else
                    {
                        loaded &= DepotStore.LoadLazy(this, childLog);

                        if (childLog?.HasEvent(BdoEventKinds.Error, BdoEventKinds.Exception) != true)
                        {
                            childLog?.AddEvent(BdoEventKinds.Message, "Data store loaded (" + DepotStore.Depots.Count + " depots added)");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                subLog?.AddException(ex);
            }
            finally
            {
            }

            Logger?.Log(log);

            _state = loaded ? ProcessExecutionState.Pending : ProcessExecutionState.Ended;

            return loaded;
        }

        #endregion
    }
}