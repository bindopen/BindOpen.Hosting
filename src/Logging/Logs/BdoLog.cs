using BindOpen.Data;
using BindOpen.Data.Helpers;
using BindOpen.Data.Meta;
using BindOpen.Logging.Events;
using BindOpen.Logging.Loggers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Logging;

/// <summary>
/// This class represents a logger of tasks.
/// </summary>
public class BdoLog : BdoObject, IBdoCompleteLog
{
    List<IBdoLogEvent> _events;

    public IList<IBdoLogEvent> _Events => _events;

    // ------------------------------------------
    // CONSTRUCTORS
    // ------------------------------------------

    #region Constructors

    /// <summary>
    /// Instantiates a new instance of the Log class.
    /// </summary>
    public BdoLog() : base()
    {
    }

    #endregion

    // ------------------------------------------
    // IBdoCompleteLog Implementation
    // ------------------------------------------

    #region IBdoCompleteLog

    public List<IBdoLogger> Loggers { get; set; }

    public int RemoveEvents(
        Predicate<IBdoLogEvent> filter = null,
        bool isRecursive = true)
    {
        var i = 0;

        i += _events?.RemoveAll(q => filter?.Invoke(q) != false) ?? 0;

        if (isRecursive)
        {
            foreach (var child in _Children)
            {
                i += RemoveEvents(filter, isRecursive);
            }
        }

        return i;
    }

    #endregion

    // ------------------------------------------
    // ITParent implementation
    // ------------------------------------------

    #region ITParent

    /// <summary>
    /// Parent of this instance.
    /// </summary>
    public IBdoLog Parent { get; set; }

    public ITBdoSet<IBdoLog> _Children
    {
        get => BdoData.NewItemSet(this.Children()?.ToArray());
        set
        {
            RemoveEvents(q => q.Log != null, false);

            foreach (var log in value)
            {
                AddChild(log);
            }
        }
    }

    public Q InsertChild<Q>(Action<Q> updater) where Q : IBdoLog, new()
    {
        var child = NewLog<Q>();
        updater?.Invoke(child);

        InsertEvent(BdoEventKinds.Any).WithLog(child);

        return child;
    }

    public void RemoveChildren(Predicate<IBdoLog> filter = null, bool isRecursive = false)
    {
        _events?.RemoveAll(p => filter?.Invoke(p?.Log) != true);
    }

    #endregion

    // ------------------------------------------
    // IBdoLog Implementation
    // ------------------------------------------

    #region IBdoLog

    /// <summary>
    /// Execution of this instance.
    /// </summary>
    public string ResultCode { get; set; }

    // Execution ----------------------------------

    /// <summary>
    /// Execution of this instance.
    /// </summary>
    public IBdoProcessExecution Execution { get; set; }

    /// <summary>
    /// Execution of this instance.
    /// </summary>
    public IBdoProcessExecution InitExecution() => Execution = BdoLogging.NewExecution();

    // Task ----------------------------------

    /// <summary>
    /// The task of this instance.
    /// </summary>
    public IBdoConfiguration TaskConfig { get; set; }

    /// <summary>
    /// Function that filters event.
    /// </summary>
    public Predicate<IBdoLogEvent> EventFilter { get; set; }

    // Events ----------------------------------

    /// <summary>
    /// The event with the specified ID.
    /// </summary>
    /// <param name="id"></param>
    public IBdoLogEvent this[string id] => this.Event(id, false);

    /// <summary>
    /// The event with the specified ID.
    /// </summary>
    /// <param name="index"></param>
    public IBdoLogEvent this[int index] => _events?.GetAt(index);

    // Tree ----------------------------------

    /// <summary>
    /// 
    /// </summary>
    public void Sanitize()
    {
        if (this.HasErrorOrExceptionOrWarning() == false)
        {
            RemoveEvents(true, BdoEventKinds.Checkpoint);
        }
    }

    public IBdoLog InsertChild(BdoEventKinds kind = BdoEventKinds.Any, Action<IBdoLogEvent> updater = null)
    {
        var child = NewLog<BdoLog>();

        InsertEvent(kind, updater).WithLog(child);

        return child;
    }

    public IBdoLog InsertChild(
        BdoEventKinds kind,
        string title,
        string description = null,
        DateTime? date = null,
        string resultCode = null)
    {
        var child = InsertChild(kind, q => q
            .WithTitle(title)
            .WithDescription(description)
            .WithDate(date)
            .WithResultCode(resultCode));

        return child;
    }

    public IBdoLog AddChild(
        IBdoLog child = null,
        BdoEventKinds kind = BdoEventKinds.Any,
        string title = null,
        string description = null,
        DateTime? date = null,
        string resultCode = null)
    {
        var ev = InsertEvent(kind, q => q
            .WithTitle(title)
            .WithDescription(description)
            .WithDate(date)
            .WithResultCode(resultCode)
            .WithLog(child ?? BdoLogging.NewLog()));

        return this;
    }

    // Events

    public bool HasEvent(bool isRecursive = true, params BdoEventKinds[] kinds)
        => this.HasEvent(q => kinds.Has(q.Kind), isRecursive);

    public int RemoveEvents(bool isRecursive = true, params BdoEventKinds[] kinds)
    {
        return this.RemoveEvents(q => kinds.Has(q.Kind), isRecursive);
    }

    public IBdoLogEvent InsertEvent(
        BdoEventKinds kind = BdoEventKinds.Any,
        Action<IBdoLogEvent> updater = null)
    {
        var ev = new BdoLogEvent();
        updater?.Invoke(ev);
        ev.WithKind(kind);

        AddEvent(ev);

        return ev;
    }

    public IBdoLog AddEvent(
        BdoEventKinds kind,
        string title,
        string description = null,
        DateTime? date = null,
        string resultCode = null)
    {
        InsertEvent(kind, q => q
            .WithTitle(title)
            .WithDescription(description)
            .WithDate(date)
            .WithResultCode(resultCode));

        return this;
    }

    public IBdoLog AddEvent(IBdoLogEvent ev)
    {
        if (ev != null && EventFilter?.Invoke(ev) != false)
        {
            ev.WithParent(this);
            _events ??= new List<IBdoLogEvent>();
            _events.Add(ev);

            if (Loggers != null)
            {
                Loggers?.ForEach(q => q?.LogEvent(ev));
            }
        }

        return this;
    }

    // Tree

    public Q NewLog<Q>() where Q : IBdoLog, new()
    {
        return BdoLogging.NewLog<Q>(this);
    }

    IBdoLog IBdoLog.NewLog()
    {
        return BdoLogging.NewLog(this);
    }

    public void BuildTree()
    {
        foreach (var ev in _events)
        {
            ev.WithParent(this);
            if (ev.Log != null)
            {
                ev.Log.WithParent(this);
                ev.Log.BuildTree();
            }
        }
    }

    #endregion

    // ------------------------------------------
    // IReferenced Implementation
    // ------------------------------------------

    #region IReferenced

    /// <summary>
    /// 
    /// </summary>
    public string Key() => Name ?? Identifier;

    #endregion

    // ------------------------------------------
    // IIdentifiedPoco Implementation
    // ------------------------------------------

    #region IIdentifiedPoco

    /// <summary>
    /// 
    /// </summary>
    public string Identifier { get; set; }

    #endregion

    // ------------------------------------------
    // INamedPoco Implementation
    // ------------------------------------------

    #region INamedPoco

    /// <summary>
    /// 
    /// </summary>
    public string Name { get; set; }

    #endregion

    // ------------------------------------------
    // ITDetailedPoco Implementation
    // ------------------------------------------

    #region ITDetailedPoco

    /// <summary>
    /// 
    /// </summary>
    public IBdoMetaSet Detail { get; set; }

    #endregion

    // ------------------------------------------
    // IDescribed Implementation
    // ------------------------------------------

    #region IDescribed

    /// <summary>
    /// 
    /// </summary>
    public string Description { get; set; }

    #endregion

    // ------------------------------------------
    // IDisplayNamed Implementation
    // ------------------------------------------

    #region IDisplayNamed

    /// <summary>
    /// 
    /// </summary>
    public string Title { get; set; }

    #endregion

    // ------------------------------------------
    // IBdoObject interface
    // ------------------------------------------

    #region IBdoObject

    /// <summary>
    /// Clones this instance.
    /// </summary>
    /// <returns>Returns a cloned instance.</returns>
    public override object Clone()
    {
        return Clone(null);
    }

    public IBdoCompleteLog Clone(IBdoCompleteLog parent)
    {
        var cloned = base.Clone() as BdoLog;

        cloned.Parent = parent;
        cloned.TaskConfig = TaskConfig?.Clone<IBdoConfiguration>();
        cloned.Detail = Detail?.Clone<IBdoMetaSet>();
        cloned.WithEvents(_events?.Select(p => p.Clone(cloned)).ToArray());
        cloned.Execution = Execution?.Clone<IBdoProcessExecution>();

        return cloned;
    }

    #endregion

    // ------------------------------------------
    // IDISPOSABLE METHODS
    // ------------------------------------------

    #region IDisposable_Methods

    private bool _isDisposed = false;

    /// <summary>
    /// Disposes this instance. 
    /// </summary>
    /// <param name="isDisposing">Indicates whether this instance is disposing</param>
    protected override void Dispose(bool isDisposing)
    {
        if (_isDisposed)
        {
            return;
        }

        TaskConfig?.Dispose();
        Detail?.Dispose();

        _isDisposed = true;

        base.Dispose(isDisposing);
    }

    #endregion
}