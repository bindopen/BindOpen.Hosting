using BindOpen.Logging.Events;
using System;

namespace BindOpen.Logging;

/// <summary>
/// 
/// </summary>
public static partial class IBdoCompleteLogExtensions
{
    /// <summary>
    /// 
    /// </summary>
    public static T AddEvent<T>(this T log, BdoEventKinds eventKind, Action<IBdoLogEvent> updater = null)
        where T : IBdoLog
    {
        (log as IBdoCompleteLog)?.InsertEvent(eventKind, updater);
        return log;
    }

    /// <summary>
    /// 
    /// </summary>
    public static T AddCheckpoint<T>(this T log, Action<IBdoLogEvent> updater = null)
        where T : IBdoLog
    {
        log?.AddEvent(BdoEventKinds.Checkpoint, updater);
        return default;
    }

    public static T AddCheckpoint<T>(
        this T log,
        string title,
        string description = null,
        DateTime? date = null,
        string resultCode = null)
        where T : IBdoLog
    {
        log?.AddEvent(BdoEventKinds.Checkpoint, title, description, date, resultCode);
        return default;
    }

    /// <summary>
    /// 
    /// </summary>
    public static T AddError<T>(this T log, Action<IBdoLogEvent> updater = null)
        where T : IBdoLog
    {
        log?.AddEvent(BdoEventKinds.Error, updater);
        return log;
    }

    public static T AddError<T>(
        this T log,
        string title,
        string description = null,
        DateTime? date = null,
        string resultCode = null)
        where T : IBdoLog
    {
        log?.AddEvent(BdoEventKinds.Error, title, description, date, resultCode);
        return log;
    }

    /// <summary>
    /// 
    /// </summary>
    public static T AddException<T>(this T log, Action<IBdoLogEvent> updater = null)
        where T : IBdoLog
    {
        log?.AddEvent(BdoEventKinds.Exception, updater);
        return log;
    }

    public static T AddException<T>(
        this T log,
        string title,
        string description = null,
        DateTime? date = null,
        string resultCode = null)
        where T : IBdoLog
    {
        log?.AddEvent(BdoEventKinds.Exception, title, description, date, resultCode);
        return log;
    }

    /// <summary>
    /// 
    /// </summary>
    public static T AddMessage<T>(this T log, Action<IBdoLogEvent> updater = null)
        where T : IBdoLog
    {
        log?.AddEvent(BdoEventKinds.Message, updater);
        return log;
    }

    public static T AddMessage<T>(
        this T log,
        string title,
        string description = null,
        DateTime? date = null,
        string resultCode = null)
        where T : IBdoLog
    {
        log?.AddEvent(BdoEventKinds.Message, title, description, date, resultCode);
        return log;
    }

    /// <summary>
    /// 
    /// </summary>
    public static T AddWarning<T>(this T log, Action<IBdoLogEvent> updater = null)
        where T : IBdoLog
    {
        log?.AddEvent(BdoEventKinds.Warning, updater);
        return log;
    }

    public static T AddWarning<T>(
        this T log,
        string title,
        string description = null,
        DateTime? date = null,
        string resultCode = null)
        where T : IBdoLog
    {
        log?.AddEvent(BdoEventKinds.Warning, title, description, date, resultCode);
        return log;
    }
}