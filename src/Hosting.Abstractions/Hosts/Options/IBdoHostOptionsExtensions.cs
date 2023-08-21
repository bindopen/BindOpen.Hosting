﻿using BindOpen.System.Data.Helpers;
using BindOpen.System.Hosting.Hosts;

namespace BindOpen.System.Hosting
{
    /// <summary>
    /// This class represents a options options.
    /// </summary>
    public static class IBdoHostOptionsExtensions
    {
        /// <summary>
        /// Set the root folder.
        /// </summary>
        /// <param key="predicate">The condition that must be satisfied.</param>
        /// <param key="rootFolderPath">The root folder path.</param>
        /// <returns>Returns the options option.</returns>
        public static T WithLibraryFolder<T>(this T settings, string path)
            where T : IBdoHostOptions
        {
            if (settings != null)
            {
                settings.LibraryFolderPath = path;
            }

            return settings;
        }

        /// <summary>
        /// Set the root folder.
        /// </summary>
        /// <param key="predicate">The condition that must be satisfied.</param>
        /// <param key="rootFolderPath">The root folder path.</param>
        /// <returns>Returns the options option.</returns>
        public static T GetSettings<T>(this IBdoHostOptions options)
            where T : IBdoSettings
        {
            return options.Settings.As<T>();
        }
    }
}