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
    /// <summary>
    /// 
    /// </summary>
    public static IEnumerable<IBdoLogEvent> Checkpoints(
        this IBdoLog log,
        bool isRecursive = true,
        Predicate<IBdoLogEvent> filter = null)
    {
        return (log as IBdoCompleteLog)?.Events(q => q.Kind == BdoEventKinds.Checkpoint && (filter?.Invoke(q) != false), isRecursive) ?? Enumerable.Empty<IBdoLogEvent>();
    }

    /// <summary>
    /// 
    /// </summary>
    public static IEnumerable<IBdoLogEvent> Errors(
        this IBdoLog log,
        bool isRecursive = true,
        Predicate<IBdoLogEvent> filter = null)
    {
        return (log as IBdoCompleteLog)?.Events(q => q.Kind == BdoEventKinds.Error && (filter?.Invoke(q) != false), isRecursive) ?? Enumerable.Empty<IBdoLogEvent>();
    }

    /// <summary>
    /// 
    /// </summary>
    public static IEnumerable<IBdoLogEvent> Exceptions(
        this IBdoLog log,
        bool isRecursive = true,
        Predicate<IBdoLogEvent> filter = null)
    {
        return (log as IBdoCompleteLog)?.Events(q => q.Kind == BdoEventKinds.Exception && (filter?.Invoke(q) != false), isRecursive) ?? Enumerable.Empty<IBdoLogEvent>();
    }

    /// <summary>
    /// 
    /// </summary>
    public static IEnumerable<IBdoLogEvent> Messages(
        this IBdoLog log,
        bool isRecursive = true,
        Predicate<IBdoLogEvent> filter = null)
    {
        return (log as IBdoCompleteLog)?.Events(q => q.Kind == BdoEventKinds.Message && (filter?.Invoke(q) != false), isRecursive) ?? Enumerable.Empty<IBdoLogEvent>();
    }

    /// <summary>
    /// 
    /// </summary>
    public static IEnumerable<IBdoLogEvent> Warnings(
        this IBdoLog log,
        bool isRecursive = true,
        Predicate<IBdoLogEvent> filter = null)
    {
        return (log as IBdoCompleteLog)?.Events(q => q.Kind == BdoEventKinds.Warning && (filter?.Invoke(q) != false), isRecursive) ?? Enumerable.Empty<IBdoLogEvent>();
    }
}