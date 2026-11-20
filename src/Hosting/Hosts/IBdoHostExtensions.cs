using BindOpen.Data.Helpers;
using BindOpen.Hosting.Settings;
using System;

namespace BindOpen.Hosting
{
    /// <summary>
    /// This class represents a host.
    /// </summary>
    public static class IBdoHostExtensions
    {
        /// <summary>
        /// Sets the specfied options
        /// </summary>
        /// <param key="options"></param>
        /// <returns>Returns this instance.</returns>
        public static T WithSettings<T>(this T host, IBdoHostOptions options)
            where T : IBdoHost
        {
            if (host != null)
            {
                host.Options = options;
            }

            return host;
        }

        /// <summary>
        /// Runs the specified action.
        /// </summary>
        /// <param key="action">The action to consider.</param>
        /// <returns>Returns this instance.</returns>
        public static T RunAction<T>(this T host, Action<IBdoHost> action)
            where T : IBdoHost
        {
            if (host != null)
            {
                action?.Invoke(host);
            }

            return host;
        }

        /// <summary>
        /// Returns the path of the application temporary folder.
        /// </summary>
        /// <param key="pathKind">The kind of paths.</param>
        /// <returns>The path of the application temporary folder.</returns>
        public static string GetKnownPath<T>(this T host, BdoHostPathKind pathKind)
            where T : IBdoHost
        {
            string path = null;

            if (host != null)
            {
                switch (pathKind)
                {
                    case BdoHostPathKind.RootFolder:
                        path = host.Options?.RootFolderPath;
                        break;
                    case BdoHostPathKind.LibraryFolder:
                        path = host.Options?.LibraryFolderPath;
                        if (string.IsNullOrEmpty(path))
                        {
                            path = BdoDefaultHostPaths.__DefaultLibraryFolderPath.GetConcatenatedPath(host.GetKnownPath(BdoHostPathKind.RootFolder));
                        }
                        break;
                }
            }

            return (string.IsNullOrEmpty(path) ? null : path).ToPath();
        }
    }
}