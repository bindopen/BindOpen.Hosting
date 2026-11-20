namespace BindOpen.Logging.Loggers;

/// <summary>
/// 
/// </summary>
public interface ITBdoLogger<T> : IBdoLogger
    where T : IBdoLoggerFormater, new()
{
    /// <summary>
    /// Initializes a new instance of the BdoLogger class.
    /// </summary>
    public T Formater { get; }
}