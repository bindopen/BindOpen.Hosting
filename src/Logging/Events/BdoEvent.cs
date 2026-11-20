using BindOpen.Data;
using BindOpen.Data.Meta;
using System;

namespace BindOpen.Logging.Events;

/// <summary>
/// This class represents an event.
/// </summary>
public class BdoEvent : BdoObject, IBdoEvent
{
    // ------------------------------------------
    // CONVERTERS
    // ------------------------------------------

    #region Converters

    /// <summary>
    /// Converts from string.
    /// </summary>
    /// <param name="st">The string to consider.</param>
    public static implicit operator BdoEvent(string st)
    {
        return BdoLogging.NewEvent(BdoEventKinds.Message).WithTitle(st) as BdoEvent;
    }

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <param name="ev">The event to consider.</param>
    public static implicit operator string(BdoEvent ev)
    {
        return ev?.Title;
    }

    #endregion

    // ------------------------------------------
    // CONSTRUCTORS
    // ------------------------------------------

    #region Constructors

    /// <summary>
    /// Instantiates a new instance of the BdoEvent class.
    /// </summary>
    public BdoEvent()
    {
    }

    #endregion

    // ------------------------------------------
    // IReferenced Implementation
    // ------------------------------------------

    #region IReferenced

    /// <summary>
    /// 
    /// </summary>
    public string Key() => Identifier;

    #endregion

    // ------------------------------------------
    // IIdentifiedPoco Implementation
    // ------------------------------------------

    #region IIdentifiedPoco

    /// <summary>
    /// 
    /// </summary>
    public string Identifier { get; set; }

    #endregion

    // ------------------------------------------
    // INamedPoco Implementation
    // ------------------------------------------

    #region INamedPoco

    /// <summary>
    /// 
    /// </summary>
    public string Name { get; set; }

    #endregion

    // ------------------------------------------
    // IBdoEvent implementation
    // ------------------------------------------

    #region IBdoEvent

    // General ----------------------------------

    /// <summary>
    /// The display name of this instance.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// The description of this instance.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Kind of this instance.
    /// </summary>
    public BdoEventKinds Kind { get; set; } = BdoEventKinds.None;

    /// <summary>
    /// Creation date of this instance.
    /// </summary>
    public DateTime? Date
    {
        get { return CreationDate; }
        set
        {
            CreationDate = value;
        }
    }

    /// <summary>
    /// Creation date of this instance.
    /// </summary>
    public DateTime? CreationDate { get; set; }

    /// <summary>
    /// Last modification date of this instance.
    /// </summary>
    public DateTime? LastModificationDate { get; set; }

    /// <summary>
    /// Long description of this instance.
    /// </summary>
    public string LongDescription { get; set; }

    /// <summary>
    /// Criticality of this instance.
    /// </summary>
    public Criticalities Criticality { get; set; } = Criticalities.None;

    #endregion

    // ------------------------------------------
    // IDetailedDataItem implementation
    // ------------------------------------------

    #region IDetailedDataItem

    /// <summary>
    /// Detail of this instance.
    /// </summary>
    public IBdoMetaSet Detail { get; set; }

    #endregion

    // ------------------------------------------
    // IBdoObject interface
    // ------------------------------------------

    #region IBdoObject

    /// <summary>
    /// Clones this instance.
    /// </summary>
    /// <returns>Returns a cloned instance.</returns>
    public override object Clone()
    {
        var cloned = base.Clone<BdoEvent>();

        cloned.Detail = Detail?.Clone<BdoMetaSet>();

        return cloned;
    }

    #endregion
}
