using BindOpen.Data;
using BindOpen.Logging.Events;
using BindOpen.Logging.Loggers;
using System;
using System.Collections.Generic;

namespace BindOpen.Logging;

/// <summary>
/// 
/// </summary>
public interface IBdoCompleteLog : IBdoLog, ITChildClonable<IBdoCompleteLog, IBdoCompleteLog>, IBdoLoggerTracked
{
    /// <summary>
    /// 
    /// </summary>
    Predicate<IBdoLogEvent> EventFilter { get; set; }

    // Events

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    IBdoLogEvent this[int index] { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    IBdoLogEvent this[string id] { get; }

    IBdoLog InsertChild(
        BdoEventKinds kind = BdoEventKinds.Any,
        Action<IBdoLogEvent> updater = null);

    /// <summary>
    /// 
    /// </summary>
    void BuildTree();

    Q NewLog<Q>() where Q : IBdoLog, new();

    // Events

    IList<IBdoLogEvent> _Events { get; }

    IBdoLog AddEvent(IBdoLogEvent ev);

    IBdoLogEvent InsertEvent(
        BdoEventKinds kind = BdoEventKinds.Any,
        Action<IBdoLogEvent> updater = null);

    int RemoveEvents(
        Predicate<IBdoLogEvent> filter = null,
        bool isRecursive = true);
    IBdoProcessExecution InitExecution();
}