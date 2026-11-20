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
    public static IBdoLogEvent InsertCheckpoint(this IBdoLog log, Action<IBdoLogEvent> updater = null)
    {
        return (log as IBdoCompleteLog)?.InsertEvent(BdoEventKinds.Checkpoint, updater);
    }

    /// <summary>
    /// 
    /// </summary>
    public static IBdoLogEvent InsertError(this IBdoLog log, Action<IBdoLogEvent> updater = null)
    {
        return (log as IBdoCompleteLog)?.InsertEvent(BdoEventKinds.Error, updater);
    }

    /// <summary>
    /// 
    /// </summary>
    public static IBdoLogEvent InsertException(this IBdoLog log, Action<IBdoLogEvent> updater = null)
    {
        return (log as IBdoCompleteLog)?.InsertEvent(BdoEventKinds.Exception, updater);
    }

    /// <summary>
    /// 
    /// </summary>
    public static IBdoLogEvent InsertMessage(this IBdoLog log, Action<IBdoLogEvent> updater = null)
    {
        return (log as IBdoCompleteLog)?.InsertEvent(BdoEventKinds.Message, updater);
    }

    /// <summary>
    /// 
    /// </summary>
    public static IBdoLogEvent InsertWarning(this IBdoLog log, Action<IBdoLogEvent> updater = null)
    {
        return (log as IBdoCompleteLog)?.InsertEvent(BdoEventKinds.Warning, updater);
    }
}