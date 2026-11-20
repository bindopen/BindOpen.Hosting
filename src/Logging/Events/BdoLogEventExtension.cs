using BindOpen.Data;
using BindOpen.Logging.Events;
using BindOpen.Logging.Loggers;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Logging;

/// <summary>
/// This class represents a log event extension.
/// </summary>
public static class BdoLogEventExtension
{
    // Gets -------------------------

    /// <summary>
    /// Gets the specified events of this instance.
    /// </summary>
    /// <param name="logEvents">The log events to consider.</param>
    /// <param name="kinds">The event kinds to consider.</param>
    /// <returns>True if this instance has the specified events. False otherwise.</returns>
    public static List<IBdoLogEvent> GetEvents(
        this List<IBdoLogEvent> logEvents,
        params BdoEventKinds[] kinds)
    {
        return logEvents == null ? new List<IBdoLogEvent>() : logEvents.Where(p => kinds.Length == 0 || kinds.Contains(p.Kind)).ToList();
    }

    /// <summary>
    /// Gets the warnings of this instance.
    /// </summary>
    /// <param name="logEvents">The log events to consider.</param>
    /// <returns>True if this instance has the specified events. False otherwise.</returns>
    public static List<IBdoLogEvent> GetWarnings(
        this List<IBdoLogEvent> logEvents)
    {
        return logEvents.GetEvents(BdoEventKinds.Warning);
    }

    /// <summary>
    /// Gets the errors of this instance.
    /// </summary>
    /// <param name="logEvents">The log events to consider.</param>
    /// <returns>True if this instance has the specified events. False otherwise.</returns>
    public static List<IBdoLogEvent> GetErrors(
        this List<IBdoLogEvent> logEvents)
    {
        return logEvents.GetEvents(BdoEventKinds.Error);
    }

    /// <summary>
    /// Gets the exceptions of this instance.
    /// </summary>
    /// <param name="logEvents">The log events to consider.</param>
    /// <returns>True if this instance has the specified events. False otherwise.</returns>
    public static List<IBdoLogEvent> GetExceptions(
        this List<IBdoLogEvent> logEvents)
    {
        return logEvents.GetEvents(BdoEventKinds.Exception);
    }

    /// <summary>
    /// Gets the messages of this instance.
    /// </summary>
    /// <param name="logEvents">The log events to consider.</param>
    /// <returns>True if this instance has the specified events. False otherwise.</returns>
    public static List<IBdoLogEvent> GetMessages(
        this List<IBdoLogEvent> logEvents)
    {
        return logEvents.GetEvents(BdoEventKinds.Message);
    }

    /// <summary>
    /// Gets the errors or exceptions of this instance.
    /// </summary>
    /// <param name="logEvents">The log events to consider.</param>
    /// <returns>True if this instance has the specified events. False otherwise.</returns>
    public static List<IBdoLogEvent> GetErrorOrExceptions(
        this List<IBdoLogEvent> logEvents)
    {
        return logEvents.GetEvents(BdoEventKinds.Error, BdoEventKinds.Exception);
    }

    /// <summary>
    /// Gets the warnings, errors or exceptions of this instance.
    /// </summary>
    /// <param name="logEvents">The log events to consider.</param>
    /// <returns>True if this instance has the specified events. False otherwise.</returns>
    public static List<IBdoLogEvent> GetErrorOrExceptionOrWarnings(
        this List<IBdoLogEvent> logEvents)
    {
        return logEvents.GetEvents(BdoEventKinds.Warning, BdoEventKinds.Error, BdoEventKinds.Exception);
    }

    /// <summary>
    /// Executes this instance.
    /// </summary>
    /// <param name="events">The events to consider.</param>
    /// <returns>Returns the output value of the execution.</returns>
    public static ProcessExecutionStatus GetStatus(this List<IBdoLogEvent> events)
    {
        if (events != null)
        {
            if (events.HasExceptions())
                return ProcessExecutionStatus.Stopped_Exception;
            else if (events.HasExceptions())
                return ProcessExecutionStatus.Stopped_Error;
            else
                return ProcessExecutionStatus.Completed;
        }

        return ProcessExecutionStatus.None;
    }

    // Has --------------------------

    /// <summary>
    /// Indicates whether this instance has the specified events.
    /// </summary>
    /// <param name="logEvents">The log events to consider.</param>
    /// <param name="kinds">The event kinds to consider.</param>
    /// <returns>True if this instance has the specified events. False otherwise.</returns>
    public static bool Has(
        this List<IBdoLogEvent> logEvents,
        params BdoEventKinds[] kinds)
    {
        return logEvents?.Any(p => kinds.Contains(p.Kind)) ?? false;
    }

    /// <summary>
    /// Indicates whether this instance has any warnings.
    /// </summary>
    /// <param name="logEvents">The log events to consider.</param>
    /// <returns>True if this instance has the specified events. False otherwise.</returns>
    public static bool HasWarnings(
        this List<IBdoLogEvent> logEvents)
    {
        return logEvents.Has(BdoEventKinds.Warning);
    }

    /// <summary>
    /// Indicates whether this instance has any errors.
    /// </summary>
    /// <param name="logEvents">The log events to consider.</param>
    /// <returns>True if this instance has the specified events. False otherwise.</returns>
    public static bool HasErrors(
        this List<IBdoLogEvent> logEvents)
    {
        return logEvents.Has(BdoEventKinds.Error);
    }

    /// <summary>
    /// Indicates whether this instance has any exceptions.
    /// </summary>
    /// <param name="logEvents">The log events to consider.</param>
    /// <returns>True if this instance has the specified events. False otherwise.</returns>
    public static bool HasExceptions(
        this List<IBdoLogEvent> logEvents)
    {
        return logEvents.Has(BdoEventKinds.Exception);
    }

    /// <summary>
    /// Indicates whether this instance has any messages.
    /// </summary>
    /// <param name="logEvents">The log events to consider.</param>
    /// <returns>True if this instance has the specified events. False otherwise.</returns>
    public static bool HasMessages(
        this List<IBdoLogEvent> logEvents)
    {
        return logEvents.Has(BdoEventKinds.Message);
    }

    /// <summary>
    /// Indicates whether this instance has any errors or exceptions.
    /// </summary>
    /// <param name="logEvents">The log events to consider.</param>
    /// <returns>True if this instance has the specified events. False otherwise.</returns>
    public static bool HasErrorsOrExceptions(
        this List<IBdoLogEvent> logEvents)
    {
        return logEvents.Has(BdoEventKinds.Error, BdoEventKinds.Exception);
    }

    /// <summary>
    /// Indicates whether this instance has any warnings, errors or exceptions.
    /// </summary>
    /// <param name="logEvents">The log events to consider.</param>
    /// <returns>True if this instance has the specified events. False otherwise.</returns>
    public static bool HasErrorOrExceptionOrWarnings(
        this List<IBdoLogEvent> logEvents)
    {
        return logEvents.Has(BdoEventKinds.Warning, BdoEventKinds.Error, BdoEventKinds.Exception);
    }

    /// <summary>
    /// Converts the specified event to string.
    /// </summary>
    /// <param name="ev">The event to consider.</param>
    /// <returns>The string corresponding to the specified event using the specified formater.</returns>
    public static string ToString<T>(this IBdoLogEvent ev)
        where T : IBdoLoggerFormater, new()
    {
        var formater = new T();
        return formater.Format(ev);
    }
}
