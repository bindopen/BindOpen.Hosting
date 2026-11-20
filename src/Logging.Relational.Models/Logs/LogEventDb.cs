using BindOpen.Data;
using BindOpen.Data.Meta;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BindOpen.Logging.Events;

/// <summary>
/// This class represents a class reference database entity.
/// </summary>
public class LogEventDb : IBdoDb
{
    // --------------------------------------------------
    // PROPERTIES
    // --------------------------------------------------

    #region Properties

    /// <summary>
    /// The class name of this instance.
    /// </summary>
    [Key]
    [Column("EventId")]

    public string Identifier { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Name { get; set; }

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
    public DateTime? Date { get; set; }

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

    /// <summary>
    /// The metaset of this instance.
    /// </summary>
    [ForeignKey(nameof(DetailId))]
    public MetaSetDb Detail { get; set; }

    /// <summary>
    /// The metaset identifier of this instance.
    /// </summary>
    public string DetailId { get; set; }

    /// <summary>
    /// Result code of this instance.
    /// </summary>
    public string ResultCode { get; set; }

    /// <summary>
    /// Source of this instance.
    /// </summary>
    public string Source { get; set; }

    ///// <summary>
    ///// Dto detail of this instance.
    ///// </summary>
    //public IList<IBdoLogEventStackTrace> StackTraces { get; set; }

    // Tree ----------------------------------

    /// <summary>
    /// The metaset of this instance.
    /// </summary>
    [ForeignKey(nameof(LogId))]
    public LogDb Log { get; set; }

    /// <summary>
    /// The metaset identifier of this instance.
    /// </summary>
    public string LogId { get; set; }

    #endregion

    // --------------------------------------------------
    // CONSTRUCTORS
    // --------------------------------------------------

    #region Constructors

    /// <summary>
    /// Instantiates a new instance of the LogEventDb class.
    /// </summary>
    public LogEventDb()
    {
    }

    #endregion
}
