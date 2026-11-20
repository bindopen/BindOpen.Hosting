using BindOpen.Data;
using BindOpen.Data.Meta;
using System;

namespace BindOpen.Logging;

/// <summary>
/// This class represents a job.
/// </summary>
public class BdoProcessExecution : BdoObject, IBdoProcessExecution
{
    // ------------------------------------------
    // PROPERTIES
    // ------------------------------------------

    #region Properties

    public string CustomStatus { get; set; }

    public TimeSpan? Duration => StartDate == null ? null : (EndDate == null ? DateTime.Now : EndDate.Value).Subtract(StartDate.Value);

    public DateTime? EndDate { get; set; }
    public string Location { get; set; }
    public float ProgressIndex { get; set; }
    public float ProgressMax { get; set; }
    public DateTime? RestartDate { get; set; }
    public int ResultLevel { get; set; }
    public DateTime? StartDate { get; set; }
    public ProcessExecutionState State { get; set; }
    public ProcessExecutionStatus Status { get; set; }
    public IBdoMetaSet Detail { get; set; }

    #endregion

    // ------------------------------------------
    // CONSTRUCTORS
    // ------------------------------------------

    #region Constructors

    /// <summary>
    /// Instantiates a new instance of the BdoProcessExecution class.
    /// </summary>
    public BdoProcessExecution()
    {
    }

    #endregion

    public string Key() => Identifier;

    // ------------------------------------------
    // IIdentifiedPoco Implementation
    // ------------------------------------------

    #region IIdentifiedPoco

    /// <summary>
    /// 
    /// </summary>
    public string Identifier { get; set; }

    #endregion
}
