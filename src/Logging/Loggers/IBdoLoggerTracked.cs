using System.Collections.Generic;

namespace BindOpen.Logging.Loggers;

/// <summary>
/// This interface defines an object tracked by logger.
/// </summary>
public interface IBdoLoggerTracked
{
    /// <summary>
    /// The logger.
    /// </summary>
    List<IBdoLogger> Loggers { get; set; }
}