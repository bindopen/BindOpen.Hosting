using Microsoft.Extensions.Logging;

namespace BindOpen.Logging.Loggers;

/// <summary>
/// 
/// </summary>
public interface ITBdoExternalLogger<T> : ITBdoLogger<T>
    where T : IBdoLoggerFormater, new()
{
    /// <summary>
    /// Sets the native logger.
    /// </summary>
    /// <param name="nativeLogger">The native logger to consider.</param>
    /// <returns>True if this instance has the specified events. False otherwise.</returns>
    IBdoLogger SetExternal(ILogger nativeLogger);
}