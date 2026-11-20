using BindOpen.Data;
using BindOpen.Logging.Loggers;
using System;

namespace BindOpen.Logging;

/// <summary>
/// This class represents a log extension.
/// </summary>
public static class BdoLogExtension
{
    /// <summary>
    /// Converts the specified log to string.
    /// </summary>
    /// <param name="log">The log to consider.</param>
    /// <returns>The string corresponding to the specified log using the specified formater.</returns>
    public static string ToString<T>(this IBdoCompleteLog log)
        where T : IBdoLoggerFormater, new()
    {
        var formater = new T();
        return formater.Format(log);
    }

    /// <summary>
    /// 
    /// </summary>
    public static T WithParent<T>(
        this T log,
        IBdoLog parent)
        where T : IBdoLog
    {
        if (log != null)
        {
            log.Parent = parent;
        }

        return log;
    }

    /// <summary>
    /// 
    /// </summary>
    public static T WithChildren<T>(
        this T log,
        params IBdoLog[] children)
        where T : IBdoLog
    {
        if (log != null)
        {
            log._Children = BdoData.NewItemSet(children);
        }

        return log;
    }

    public static T AddChildren<T>(this T log, params IBdoLog[] children) where T : IBdoLog
    {
        if (log != null)
        {
            log._Children ??= BdoData.NewItemSet<IBdoLog>();
            foreach (var child in children)
            {
                log._Children.Add(child);
            }
        }

        return log;
    }

    public static IBdoLog InsertChild(this IBdoLog log, Action<IBdoLog> updater)
    {
        var child = log?.InsertChild<BdoLog>(updater);

        return child;
    }
}
