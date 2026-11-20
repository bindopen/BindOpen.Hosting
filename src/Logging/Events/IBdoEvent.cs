using BindOpen.Data;
using System;

namespace BindOpen.Logging.Events;

/// <summary>
/// This interface defines an event.
/// </summary>
public interface IBdoEvent :
    IIdentified, INamed,
    ITitled, IDescribed, IReferenced,
    IDated, IBdoDetailed,
    IDisposable
{
    /// <summary>
    /// The criticality.
    /// </summary>
    Criticalities Criticality { get; set; }

    /// <summary>
    /// The date.
    /// </summary>
    DateTime? Date { get; set; }

    /// <summary>
    /// The kind.
    /// </summary>
    BdoEventKinds Kind { get; set; }

    /// <summary>
    /// The long description.
    /// </summary>
    string LongDescription { get; set; }
}