using BindOpen.Data;
using BindOpen.Data.Helpers;
using BindOpen.Data.Stores;
using BindOpen.Hosting.Settings;
using BindOpen.Logging.Loggers;
using System;
using System.Collections.Generic;

namespace BindOpen.Hosting
{
    /// <summary>
    /// This class represents a options options.
    /// </summary>
    public static class BdoHostOptionsExtensions
    {
        // Paths -------------------------------------------

        /// <summary>
        /// Set the root folder.
        /// </summary>
        /// <param key="predicate">The condition that must be satisfied.</param>
        /// <param key="rootFolderPath">The root folder path.</param>
        /// <returns>Returns the options option.</returns>
        public static T AddRootFolder<T>(this T options, Predicate<IBdoHostOptions> predicate, string rootFolderPath)
            where T : IBdoHostOptions
        {
            if (options != null)
            {
                options.RootFolderPathAssignments ??= new List<(Predicate<IBdoHostOptions> Predicate, string RootFolderPath)>();
                options.RootFolderPathAssignments.Add((predicate, rootFolderPath));
            }

            return options;
        }

        /// <summary>
        /// Sets the specified root folder path.
        /// </summary>
        /// <param key="path">The path to consider.</param>
        /// <returns>Returns this instance.</returns>
        public static T SetRootFolder<T>(this T options, string path)
            where T : IBdoHostOptions
        {
            if (options != null)
            {
                options.RootFolderPathAssignments ??= new List<(Predicate<IBdoHostOptions> Predicate, string RootFolderPath)>();
                options.RootFolderPathAssignments.Add((_ => true, path?.EndingWith(@"\").ToPath()));
            }

            return options;
        }


        // Settings -------------------------------------------

        /// <summary>
        /// Set the specified options options file path.
        /// </summary>
        /// <param key="path">The options file path.</param>
        /// <param key="isRequired">Indicates whether the options options file is required.</param>
        /// <returns>Returns the options option.</returns>
        public static T AddConfigurationFile<T>(
            this T options,
            string path = null,
            bool isRequired = false,
            ConfigurationFileExtenions extension = ConfigurationFileExtenions.Any)
            where T : IBdoHostOptions
        {
            if (options != null)
            {
                options.ConfigurationFiles ??= [];
                options.ConfigurationFiles.Add((path, isRequired, extension));
            }

            return options;
        }

        // Logs -------------------------------------------

        /// <summary>
        /// Adds the specified logger.
        /// </summary>
        /// <param key="initLogger">The logger initialization to consider.</param>
        /// <returns>Returns the options option.</returns>
        public static T SetLogger<T>(this T options, Func<IBdoHost, IBdoLogger> initLogger)
            where T : IBdoHostOptions
        {
            if (options != null)
            {
                options.LoggerInit = initLogger;
            }

            return options;
        }

        // Trigger actions -------------------------------------------

        /// <summary>  
        /// The action that is executed when the start of this instance succedes.
        /// </summary>
        /// <param key="action">The action to execute.</param>
        public static T ExecuteOnInitSuccess<T>(this T options, Action<IBdoHost> action)
            where T : IBdoHostOptions
        {
            if (options != null)
            {
                options.EventActions ??= [];
                options.EventActions.Add((HostBdoEventKinds.OnInitSuccess, action));
            }

            return options;
        }

        /// <summary>
        /// The action that is executed when the start of this instance fails.
        /// </summary>
        /// <param key="action">The action to execute.</param>
        public static T ExecuteOnInitFailure<T>(this T options, Action<IBdoHost> action)
            where T : IBdoHostOptions
        {
            if (options != null)
            {
                options.EventActions ??= new();
                options.EventActions.Add((HostBdoEventKinds.OnInitFailure, action));
            }

            return options;
        }

        /// <summary>
        /// The action that is executed when this instance is successfully completed.
        /// </summary>
        /// <param key="action">The action to execute.</param>
        public static T ExecuteOnExecutionSuccess<T>(this T options, Action<IBdoHost> action)
            where T : IBdoHostOptions
        {
            if (options != null)
            {
                options.EventActions ??= new();
                options.EventActions.Add((HostBdoEventKinds.OnExecutionSucess, action));
            }

            return options;
        }

        /// <summary>
        /// The action that is executed when this instance execution fails.
        /// </summary>
        /// <param key="action">The action to execute.</param>
        public static T ExecuteOnExecutionFailure<T>(this T options, Action<IBdoHost> action)
            where T : IBdoHostOptions
        {
            if (options != null)
            {
                options.EventActions ??= new();
                options.EventActions.Add((HostBdoEventKinds.OnExecutionFailure, action));
            }

            return options;
        }

        /// <summary>
        /// Throws an exception when start fails.
        /// </summary>
        public static T ThrowExceptionOnInitFailure<T>(this T options, bool throwException = false)
            where T : IBdoHostOptions
        {
            if (options != null)
            {
                options.ExecuteOnInitFailure(_ => throw new BdoHostLoadException("BindOpen options failed while loading"));
            }

            return options;
        }

        /// <summary>
        /// Throws an exception when start fails.
        /// </summary>
        public static T ThrowExceptionOnExecutionFailure<T>(this T options, bool throwException = false)
            where T : IBdoHostOptions
        {
            if (options != null)
            {
                options.ExecuteOnExecutionFailure(_ => throw new BdoHostLoadException("BindOpen options failed while loading"));
            }

            return options;
        }

        // Depots -------------------------------------------

        /// <summary>
        /// Adds the data store executing the specified action.
        /// </summary>
        /// <param key="action">The action to execute on the created data store.</param>
        public static T AddDepotStore<T>(this T options, Action<IBdoDepotStore> action = null)
            where T : IBdoHostOptions
        {
            if (options != null)
            {
                options.DepotStore ??= BdoData.NewDepotStore();
                action?.Invoke(options.DepotStore);
            }

            return options;
        }
    }
}