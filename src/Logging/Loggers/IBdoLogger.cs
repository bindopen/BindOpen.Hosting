using BindOpen.Data;
using BindOpen.Logging.Events;

namespace BindOpen.Logging.Loggers;

/// <summary>
/// This interface defines a logger.
/// </summary>
public partial interface IBdoLogger : IBdoObject
{
    /// <summary>
    /// The identifier of the root log.
    /// </summary>
    string RootLogId { get; }

    /// <summary>
    /// Creates a new root log.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    IBdoCompleteLog NewRootLog(string id = null);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="ev"></typeparam>
    IResultItem Log(IBdoLog item, IBdoLog log = null);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="ev"></typeparam>
    IResultItem UpdateExecution(IBdoLog item, IBdoLog log = null);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="ev"></typeparam>
    IResultItem UpdateDetail(IBdoLog item, IBdoLog log = null);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="ev"></typeparam>
    IResultItem LogEvent(IBdoLogEvent ev, IBdoLog log = null);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="ev"></typeparam>
    IResultItem UpdateEventDetail(IBdoLogEvent ev, IBdoLog log = null);
}