using BindOpen.Data;
using BindOpen.Data.Helpers;
using BindOpen.Logging;
using BindOpen.Logging.Events;
using System;

namespace BindOpen.Logging.Loggers;

/// <summary>
/// This class represents a logger format.
/// </summary>
public class BdoSnapLoggerFormater : IBdoLoggerFormater
{
    // Log

    /// <summary>
    /// Converts the log to the string.
    /// </summary>
    /// <param name="log">The log to consider.</param>
    /// <returns>Returns the string that converts the specified log.</returns>
    public string Format(IBdoLog log, string indent = "")
    {
        if (log != null)
        {
            var st = log.Title + (!string.IsNullOrEmpty(log.Description) ? " | " + log.Description : "");

            st = string.IsNullOrEmpty(st) ? "" : indent + "o " + st + Environment.NewLine;

            if (log is IBdoCompleteLog dynamicLog && dynamicLog._Events != null)
            {
                foreach (var ev in dynamicLog._Events)
                {
                    var stEv = Format(ev, indent + " ");
                    st += string.IsNullOrEmpty(stEv) ? "" : stEv;
                }
            }

            return st;
        }

        return null;
    }

    public string FormatExecution(IBdoLog log, string indent = "")
    {
        if (log != null)
        {
            var st = log?.Execution?.Status.ToString();

            st = string.IsNullOrEmpty(st) ? "" : indent + "o (Log ID=" + log?.Identifier + ")" + st + Environment.NewLine;

            return st;
        }

        return null;
    }

    public string FormatDetail(IBdoLog log, string indent = "")
    {
        if (log != null)
        {
            var st = log?.Detail?.ToString();

            st = string.IsNullOrEmpty(st) ? "" : indent + "o (Log ID=" + log?.Identifier + ")" + st + Environment.NewLine;

            return st;
        }

        return null;
    }

    // Event

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="ev"></typeparam>
    public string Format(IBdoLogEvent ev, string indent = "")
    {
        if (ev != null)
        {
            var title = ev.Title ?? ev.Log?.Title;
            var description = ev.Description ?? ev.Log?.Description;

            var st = ev.Date.ToString(DataValueTypes.Date) ?? "";
            st += ev.Kind == BdoEventKinds.Any ? "" : ev.Kind.ToString() + ": ";
            st += string.IsNullOrEmpty(title) ? "" : title;
            st += description == null ? "" : (string.IsNullOrEmpty(title) ? "" : " | ") + description;

            if (ev.Log != null)
            {
                st = string.IsNullOrEmpty(st) ? "" : indent + "o " + st + Environment.NewLine;

                if (ev.Log is IBdoCompleteLog dynamicLog && dynamicLog._Events != null)
                {
                    foreach (var childEv in dynamicLog._Events)
                    {
                        var stEv = Format(childEv, indent + " ");
                        st += string.IsNullOrEmpty(stEv) ? "" : stEv;
                    }
                }
            }
            else
            {
                st = string.IsNullOrEmpty(st) ? "" : indent + "- " + st + Environment.NewLine;
            }

            return st;
        }

        return null;
    }

    public string FormatDetail(IBdoLogEvent ev, string indent = "")
    {
        if (ev != null)
        {
            var st = ev?.Detail.ToString();

            st = string.IsNullOrEmpty(st) ? "" : indent + "o (Event ID=" + ev?.Identifier + ")" + st + Environment.NewLine;

            return st;
        }

        return null;
    }
}
