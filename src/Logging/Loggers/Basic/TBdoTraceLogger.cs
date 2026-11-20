using BindOpen.Data;
using BindOpen.Logging.Events;
using System.Diagnostics;

namespace BindOpen.Logging.Loggers;

/// <summary>
/// This class represents a logger.
/// </summary>
public class TBdoTraceLogger<T> : TBdoLogger<T>
    where T : IBdoLoggerFormater, new()
{
    /// <summary>
    /// Initializes a new instance of the BdoLogger class.
    /// </summary>
    public TBdoTraceLogger() : base()
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="ev"></typeparam>
    public override IResultItem Log(IBdoLog item, IBdoLog log = null)
    {
        if (item != null)
        {
            var st = _formater?.Format(item);
            Trace.Write(st);
            return BdoData.NewResultItem(ResourceStatus.Created);
        }

        return BdoData.NewResultItem(ResourceStatus.None);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="ev"></typeparam>
    public override IResultItem UpdateExecution(IBdoLog item, IBdoLog log = null)
    {
        if (item?.Execution != null)
        {
            var st = _formater?.FormatExecution(item);
            Trace.Write(st);
            return BdoData.NewResultItem(ResourceStatus.Created);
        }

        return BdoData.NewResultItem(ResourceStatus.None);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="ev"></typeparam>
    public override IResultItem UpdateDetail(IBdoLog item, IBdoLog log = null)
    {
        if (item?.Detail != null)
        {
            var st = _formater?.FormatDetail(item);
            Trace.Write(st);
            return BdoData.NewResultItem(ResourceStatus.Created);
        }

        return BdoData.NewResultItem(ResourceStatus.None);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="ev"></typeparam>
    public override IResultItem LogEvent(IBdoLogEvent item, IBdoLog log = null)
    {
        if (item != null)
        {
            var st = _formater?.Format(item);
            Trace.Write(st);
            return BdoData.NewResultItem(ResourceStatus.Created);
        }

        return BdoData.NewResultItem(ResourceStatus.None);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="ev"></typeparam>
    public override IResultItem UpdateEventDetail(IBdoLogEvent item, IBdoLog log = null)
    {
        if (item?.Detail != null)
        {
            var st = _formater?.FormatDetail(item);
            Trace.Write(st);
            return BdoData.NewResultItem(ResourceStatus.Created);
        }

        return BdoData.NewResultItem(ResourceStatus.None);
    }
}
