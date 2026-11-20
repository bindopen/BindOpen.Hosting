using BindOpen.Logging.Events;

namespace BindOpen.Logging;

/// <summary>
/// This structures defines the stack trace of a task result.
/// </summary>
public class BdoLogEventStackTrace : IBdoLogEventStackTrace
{
    /// <summary>
    /// The name of the full class.
    /// </summary>
    public string FullClassName { get; set; }

    /// <summary>
    /// The name of the called method.
    /// </summary>
    public string MethodName { get; set; }

    /// <summary>
    /// Parameters of the called method.
    /// </summary>
    public string MethodParams { get; set; }

    /// <summary>
    /// Path of the called file.
    /// </summary>
    public string FilePath { get; set; }

    /// <summary>
    /// Called line number.
    /// </summary>
    public string LineNumber { get; set; }

    public BdoLogEventStackTrace()
    {
    }
}
