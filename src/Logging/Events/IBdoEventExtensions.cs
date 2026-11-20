using BindOpen.Data;
using System;

namespace BindOpen.Logging.Events;

/// <summary>
/// This static class provides extensions to IBdoEvent class.
/// </summary>
/// <seealso cref="IBdoEvent"/>
public static class IBdoEventExtensions
{
    /// <summary>
    /// Sets the criticality of the specified object.
    /// </summary>
    /// <param name="ev">The event to consider.</param>
    /// <param name="criticality">The criticality to consider.</param>
    /// <returns>Returns the specified object.</returns>
    public static T WithCriticality<T>(
        this T ev,
        Criticalities criticality)
        where T : IBdoEvent
    {
        if (ev != null)
        {
            ev.Criticality = criticality;
        }

        return ev;
    }

    /// <summary>
    /// Sets the date of the specified object.
    /// </summary>
    /// <param name="ev">The event to consider.</param>
    /// <param name="date">The date to consider.</param>
    /// <returns>Returns the specified object.</returns>
    public static T WithDate<T>(
        this T ev,
        DateTime? date)
        where T : IBdoEvent
    {
        if (ev != null)
        {
            ev.Date = date;
        }

        return ev;
    }

    /// <summary>
    /// Sets the kind of the specified object.
    /// </summary>
    /// <param name="ev">The event to consider.</param>
    /// <param name="kind">The kind to consider.</param>
    /// <returns>Returns the specified object.</returns>
    public static T WithKind<T>(
        this T ev,
        BdoEventKinds kind)
        where T : IBdoEvent
    {
        if (ev != null)
        {
            ev.Kind = kind;
        }

        return ev;
    }

    /// <summary>
    /// Sets the long description of the specified object.
    /// </summary>
    /// <param name="ev">The event to consider.</param>
    /// <param name="longDescription">The long description to consider.</param>
    /// <returns>Returns the specified object.</returns>
    public static T WithLongDescription<T>(
        this T ev,
        string longDescription)
        where T : IBdoEvent
    {
        if (ev != null)
        {
            ev.LongDescription = longDescription;
        }

        return ev;
    }
}