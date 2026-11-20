namespace BindOpen.Logging.Events;

/// <summary>
/// This static class provides extensions to IBdoLogEvent class.
/// </summary>
/// <seealso cref="IBdoLogEvent"/>
public static class IBdoLogEventExtensions
{
    /// <summary>
    /// Sets the log of the specified event.
    /// </summary>
    /// <param name="ev">The event to consider.</param>
    /// <param name="log">The log to consider</param>
    /// <returns>Returns the specified object.</returns>
    public static T WithLog<T>(
        this T ev,
        IBdoLog log)
        where T : IBdoLogEvent
    {
        if (ev != null)
        {
            ev.Log = log as IBdoCompleteLog;
        }

        return ev;
    }

    /// <summary>
    /// Sets the parent of the specified event.
    /// </summary>
    /// <param name="ev">The event to consider.</param>
    /// <param name="parent">The parent log to consider.</param>
    /// <returns>Returns the specified object.</returns>
    public static T WithParent<T>(
        this T ev,
        IBdoCompleteLog parent)
        where T : IBdoLogEvent
    {
        if (ev != null)
        {
            ev.Parent = parent;
        }

        return ev;
    }


    /// <summary>
    /// Sets the result code of the specified event.
    /// </summary>
    /// <param name="ev">The event to consider.</param>
    /// <param name="resultCode">The result code to consider.</param>
    /// <returns>Returns the specified object.</returns>
    public static T WithResultCode<T>(
        this T ev,
        string resultCode)
        where T : IBdoLogEvent
    {
        if (ev != null)
        {
            ev.ResultCode = resultCode;
        }

        return ev;
    }


    /// <summary>
    /// Sets the source of the specified object.
    /// </summary>
    /// <param name="ev">The event to consider.</param>
    /// <param name="source">The source to consider.</param>
    /// <returns>Returns the specified object.</returns>
    public static T WithSource<T>(
        this T ev,
        string source)
        where T : IBdoLogEvent
    {
        if (ev != null)
        {
            ev.Source = source;
        }

        return ev;
    }


    /// <summary>
    /// Sets the stack traces of the specified event.
    /// </summary>
    /// <param name="ev">The event to consider.</param>
    /// <param name="stackTraces">The stack traces to consider.</param>
    /// <returns>Returns the specified object.</returns>
    public static T WithStackTraces<T>(
        this T ev,
        params IBdoLogEventStackTrace[] stackTraces)
        where T : IBdoLogEvent
    {
        if (ev != null)
        {
            ev.StackTraces = stackTraces;
        }

        return ev;
    }

    /// <summary>
    /// Gets the maximum event kind of the specified event.
    /// </summary>
    /// <param name="ev">The event to consider.</param>
    /// <param name="isRecursive">Indicate whether the search is recursive.</param>
    /// <param name="kinds">The kinds to consider.</param>
    /// <returns>True if this instance has the specified events. False otherwise.</returns>
    public static BdoEventKinds GetMaxEventKind<T>(
        this T ev,
        bool isRecursive = true,
        params BdoEventKinds[] kinds)
        where T : IBdoLogEvent
    {
        BdoEventKinds eventKind = BdoEventKinds.None;

        if (ev?.Log != null)
        {
            eventKind = ev.Log.GetMaxEventKind(isRecursive, kinds);
        }

        if (eventKind == BdoEventKinds.None)
        {
            eventKind = ev?.Kind ?? BdoEventKinds.None;
        }

        return eventKind;
    }
}