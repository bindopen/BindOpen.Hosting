using BindOpen.Data.Helpers;
using BindOpen.Data.Meta;
using BindOpen.Logging.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Logging;

/// <summary>
/// 
/// </summary>
public static partial class IBdoCompleteLogExtensions
{
    public static T WithDetail<T>(this T log, IBdoMetaSet detail)
        where T : IBdoLog
    {
        if (log != null)
        {
            log.Detail = detail;

            if (log is IBdoCompleteLog dynamicLog)
            {
                dynamicLog.Loggers?
                    .ForEach(q => q?.UpdateDetail(dynamicLog));
            }
        }

        return log;
    }

    public static IEnumerable<IBdoLog> Children<T>(
        this T log, Predicate<IBdoLog> filter = null, bool isRecursive = false)
        where T : IBdoLog
    {
        if (log is IBdoCompleteLog dynamicLog)
        {
            var children = (dynamicLog._Events?.Where(p => p.Log != null && filter?.Invoke(p.Log) != false).Select(p => p.Log).Cast<IBdoLog>() ?? Enumerable.Empty<IBdoLog>()).ToList();

            if (isRecursive)
            {
                var thisChildren = dynamicLog._Children;
                foreach (var child in thisChildren)
                {
                    children.AddRange(child?.Children(filter, isRecursive));
                }
            }

            return children;
        }

        return null;
    }

    public static IBdoLog Child<T>(
        this T log, Predicate<IBdoLog> filter = null, bool isRecursive = false)
        where T : IBdoLog
    {
        if (log is IBdoCompleteLog dynamicLog)
        {
            var children = dynamicLog._Children;

            if (children != null)
            {
                foreach (var child in children)
                {
                    if (filter?.Invoke(child) != false)
                        return child;

                    if (isRecursive)
                    {
                        var subChild = child?.Child(filter, true);
                        if (subChild != null) return subChild;
                    }
                }
            }
        }

        return null;
    }

    public static bool HasChild<T>(
        this T log, Predicate<IBdoLog> filter = null, bool isRecursive = false)
        where T : IBdoLog
    {
        if (log is IBdoCompleteLog dynamicLog)
        {
            return dynamicLog._Events?.Any(q => q.Log != null && filter?.Invoke(q.Log) != false || (isRecursive && (q.Log?.HasChild(filter, true) == true))) == true;
        }

        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="execution"></param>
    /// <returns></returns>
    public static T WithEventFilter<T>(
        this T log,
        Predicate<IBdoLogEvent> filter)
        where T : IBdoLog
    {
        if (log is IBdoCompleteLog dynamicLog)
        {
            dynamicLog.EventFilter = filter;
        }

        return log;
    }

    // Events

    /// <summary>
    /// 
    /// </summary>
    /// <param name="execution"></param>
    /// <returns></returns>
    public static T WithEvents<T>(
        this T log,
        params IBdoLogEvent[] events)
        where T : IBdoLog
    {
        if (log is IBdoCompleteLog dynamicLog && events != null)
        {
            dynamicLog.RemoveEvents();

            foreach (var ev in events)
            {
                dynamicLog.AddEvent(ev);
            }
        }

        return log;
    }


    public static BdoEventKinds GetMaxEventKind<T>(
        this T log,
        bool isRecursive = true,
        params BdoEventKinds[] kinds)
        where T : IBdoCompleteLog
    {
        return log?.Events(isRecursive, kinds).Select(p => p.Kind).ToList().Max() ?? BdoEventKinds.None;
    }


    public static IEnumerable<IBdoLogEvent> Events<T>(
        this T log,
        Predicate<IBdoLogEvent> filter = null,
        bool isRecursive = true)
        where T : IBdoLog
    {
        if (log is IBdoCompleteLog dynamicLog)
        {
            var events = dynamicLog._Events?.Where(q => filter == null || filter?.Invoke(q) == true).ToList() ?? new List<IBdoLogEvent>();

            if (dynamicLog._Children != null && isRecursive)
            {
                foreach (var child in dynamicLog._Children)
                {
                    events.AddRange((child as IBdoCompleteLog)?.Events(filter, isRecursive));
                }
            }

            return events;
        }

        return Enumerable.Empty<IBdoLogEvent>();
    }

    public static IEnumerable<IBdoLogEvent> Events<T>(
        this T log,
        bool isRecursive = true,
        params BdoEventKinds[] kinds)
        where T : IBdoLog
    {
        if (log is IBdoCompleteLog dynamicLog)
        {
            return dynamicLog.Events(q => kinds.Has(q.Kind), isRecursive);
        }

        return null;
    }

    /// <summary>
    /// Returns the event of this instance with the specified ID.
    /// </summary>
    /// <param name="id">The ID of the event to return.</param>
    /// <param name="isRecursive">Indicate whether the search is recursive.</param>
    /// <returns>The event of this instance with the specified ID.</returns>
    public static IBdoLogEvent Event<T>(
        this T log,
        Predicate<IBdoLogEvent> filter = null,
        bool isRecursive = false)
        where T : IBdoLog
    {
        if (log is IBdoCompleteLog dynamicLog)
        {
            IBdoLogEvent ev = dynamicLog._Events?.FirstOrDefault(q => filter == null || filter?.Invoke(q) != false);

            if (isRecursive)
            {
                foreach (var child in dynamicLog._Children)
                {
                    ev = (child as IBdoCompleteLog)?.Event(filter, true);
                    if (ev != null) return ev;
                }
            }

            return ev;
        }

        return null;
    }

    /// <summary>
    /// Returns the event of this instance with the specified ID.
    /// </summary>
    /// <param name="id">The ID of the event to return.</param>
    /// <param name="isRecursive">Indicate whether the search is recursive.</param>
    /// <returns>The event of this instance with the specified ID.</returns>
    public static IBdoLogEvent Event<T>(
        this T log,
        string id,
        bool isRecursive = false)
        where T : IBdoLog
    {
        if (!string.IsNullOrEmpty(id) && log is IBdoCompleteLog dynamicLog)
        {
            return dynamicLog.Event(q => q.BdoKeyEquals(id), isRecursive);
        }

        return null;
    }

    public static bool HasEvent<T>(
        this T log,
        Predicate<IBdoLogEvent> filter = null,
        bool isRecursive = true)
        where T : IBdoLog
    {
        if (log is IBdoCompleteLog dynamicLog)
        {
            bool hasEvent = dynamicLog._Events?.Any(q => filter == null || filter?.Invoke(q) != false) == true;

            if (!hasEvent && isRecursive && dynamicLog._Children != null)
            {
                foreach (var child in dynamicLog._Children)
                {
                    if (hasEvent = (child as IBdoCompleteLog)?.HasEvent(filter, isRecursive) ?? false)
                    {
                        return true;
                    }
                }
            }

            return hasEvent;
        }

        return false;
    }

    public static bool HasEvent<T>(
        this T log,
        bool isRecursive = true,
        params BdoEventKinds[] kinds)
        where T : IBdoLog
        => log.HasEvent(q => kinds.Has(q.Kind), isRecursive);

    // Execution

    public static IBdoCompleteLog WithExecution(this IBdoCompleteLog log, IBdoProcessExecution execution)
    {
        if (log != null)
        {
            log.Execution = execution;

            log.Loggers?
                .ForEach(q => q?.UpdateExecution(log));
        }

        return log;
    }

    public static void WithExecutionAsStarted(this IBdoCompleteLog log)
    {
        if (log == null) return;

        log.InitExecution();

        log.Execution.SetAsStarted();

        log.WithExecution(log.Execution);
    }

    /// <summary>
    /// Restarts this instance.
    /// </summary>
    public static void WithExecutionAsRestarted(this IBdoCompleteLog log)
    {
        if (log == null) return;

        log.InitExecution();

        log.Execution.SetAsRestarted();

        log.WithExecution(log.Execution);
    }

    /// <summary>
    /// Resumes this instance.
    /// </summary>
    public static void WithExecutionAsResumed(this IBdoCompleteLog log)
    {
        if (log == null) return;

        log.InitExecution();

        log.Execution.SetAsResumed();

        log.WithExecution(log.Execution);
    }

    /// <summary>
    /// Resumes this instance.
    /// </summary>
    public static void WithExecutionAsPaused(this IBdoCompleteLog log)
    {
        if (log == null) return;

        log.InitExecution();

        log.Execution.SetAsPaused();

        log.WithExecution(log.Execution);
    }

    public static void WithExecutionAsEnded(
        this IBdoCompleteLog log,
        ProcessExecutionStatus status = ProcessExecutionStatus.Completed,
        float? progressIndex = null)
    {
        if (log == null) return;

        log.InitExecution();

        log.Execution.SetAsEnded(status);

        if (progressIndex != null) log?.Execution.WithProgressIndex(progressIndex.Value);

        log.WithExecution(log.Execution);
    }
}