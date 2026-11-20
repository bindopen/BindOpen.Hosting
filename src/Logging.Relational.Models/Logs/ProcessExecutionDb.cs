using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BindOpen.Logging;

/// <summary>
/// This class represents a class reference database entity.
/// </summary>
public class ProcessExecutionDb : IBdoDb
{
    // --------------------------------------------------
    // PROPERTIES
    // --------------------------------------------------

    #region Properties

    /// <summary>
    /// The class name of this instance.
    /// </summary>
    [Key]
    [Column("ProcessExecutionId")]
    public string Identifier { get; set; }

    public string CustomStatus { get; set; }

    public DateTime? EndDate { get; set; }
    public string Location { get; set; }
    public float ProgressIndex { get; set; }
    public float ProgressMax { get; set; }
    public DateTime? RestartDate { get; set; }
    public int ResultLevel { get; set; }
    public DateTime? StartDate { get; set; }
    public ProcessExecutionState State { get; set; }
    public ProcessExecutionStatus Status { get; set; }

    #endregion

    // --------------------------------------------------
    // CONSTRUCTORS
    // --------------------------------------------------

    #region Constructors

    /// <summary>
    /// Instantiates a new instance of the ProcessExecutionDb class.
    /// </summary>
    public ProcessExecutionDb()
    {
    }

    #endregion
}
