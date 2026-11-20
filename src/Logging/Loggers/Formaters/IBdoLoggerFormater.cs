using BindOpen.Logging.Events;

namespace BindOpen.Logging.Loggers;

/// <summary>
/// This interface defines the logger formater.
/// </summary>
public interface IBdoLoggerFormater
{
    /// <summary>
    /// Converts the log to the string.
    /// </summary>
    /// <param name="log">The log to consider.</param>
    /// <returns>Returns the string that converts the specified log.</returns>
    string Format(IBdoLog log, string indent = "");

    string FormatExecution(IBdoLog log, string indent = "");

    string FormatDetail(IBdoLog log, string indent = "");

    /// <summary>
    /// Converts the log event to the string.
    /// </summary>
    /// <param name="ev">The event to consider.</param>
    /// <returns>Returns the string that converts the specified log event.</returns>
    string Format(IBdoLogEvent ev, string indent = "");

    string FormatDetail(IBdoLogEvent ev, string indent = "");
}