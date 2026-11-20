namespace BindOpen.Logging.Loggers;

/// <summary>
/// 
/// </summary>
public static class IBdoLoggerTrackedExtensions
{
    /// <summary>
    /// 
    /// </summary>
    public static T WithLoggers<T>(
        this T tracked,
        params IBdoLogger[] loggers)
        where T : IBdoLoggerTracked
    {
        if (tracked != null)
        {
            tracked.Loggers = [.. loggers];
        }

        return tracked;
    }
}