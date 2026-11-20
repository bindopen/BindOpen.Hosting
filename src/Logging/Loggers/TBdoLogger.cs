using BindOpen.Data;
using BindOpen.Logging.Events;
using Microsoft.Extensions.Logging;
using System;

namespace BindOpen.Logging.Loggers;

/// <summary>
/// This class represents a logger.
/// </summary>
public abstract class TBdoLogger<T> : BdoObject, ITBdoLogger<T>
    where T : IBdoLoggerFormater, new()
{
    protected T _formater;

    /// <summary>
    /// Initializes a new instance of the BdoLogger class.
    /// </summary>
    public T Formater => _formater;

    /// <summary>
    /// Initializes a new instance of the BdoLogger class.
    /// </summary>
    public TBdoLogger()
    {
        _formater = new T();
    }

    public string _rootLogId;

    public string RootLogId { get => _rootLogId; protected set => _rootLogId = value; }

    public virtual IBdoCompleteLog NewRootLog(string id = null)
    {
        id ??= _rootLogId;

        var rootLog = BdoData.New<BdoLog>().WithId(id).WithLoggers(this);
        _rootLogId = rootLog?.Identifier;

        return rootLog;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="ev"></typeparam>
    public abstract IResultItem Log(IBdoLog item, IBdoLog log = null);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="ev"></typeparam>
    public abstract IResultItem UpdateExecution(IBdoLog item, IBdoLog log = null);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="ev"></typeparam>
    public abstract IResultItem UpdateDetail(IBdoLog item, IBdoLog log = null);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="ev"></typeparam>
    public abstract IResultItem LogEvent(IBdoLogEvent ev, IBdoLog log = null);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="ev"></typeparam>
    public abstract IResultItem UpdateEventDetail(IBdoLogEvent ev, IBdoLog log = null);

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
        throw new NotImplementedException();
    }
}
