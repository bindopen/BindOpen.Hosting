using BindOpen.Data;
using BindOpen.Data.Meta;
using System;

namespace BindOpen.Logging;

/// <summary>
/// This class represents the process execution.
/// </summary>
public class ProcessExecution : BdoObject, IBdoProcessExecution
{
    // ------------------------------------------
    // VARIABLES
    // ------------------------------------------

    #region Variables

    private ProcessExecutionStatus _status = ProcessExecutionStatus.None;
    private ProcessExecutionState _state = ProcessExecutionState.None;

    #endregion

    // ------------------------------------------
    // PROPERTIES
    // ------------------------------------------

    #region Properties

    /// <summary>
    /// Identifier of this instance.
    /// </summary>
    public string Identifier { get; set; }

    // Detail -------------------------------------

    /// <summary>
    /// Detail of this instance.
    /// </summary>
    public IBdoMetaSet Detail { get; set; } = new BdoMetaSet();

    // Location -------------------------------------

    /// <summary>
    /// Location of this instance.
    /// </summary>
    public string Location { get; set; }

    // Progression -------------------------------------

    /// <summary>
    /// Progression index of this instance. By default it is set to 0.
    /// </summary>
    /// <seealso cref="ProgressMax"/>
    public float ProgressIndex { get; set; }

    /// <summary>
    /// Maximum progression of this instance. By default, it is set to 100.
    /// </summary>
    /// <seealso cref="ProgressIndex"/>
    public float ProgressMax { get; set; }

    // States -------------------------------------

    /// <summary>
    /// Status of this instance.
    /// </summary>
    public ProcessExecutionStatus Status
    {
        get { return _status; }
        set
        {
            _status = value;
            ProcessExecutionState processExecutionState;
            if (_state != (processExecutionState = _status.ToState()))
            {
                _state = processExecutionState;
            }
        }
    }

    /// <summary>
    /// State of this instance.
    /// </summary>
    public ProcessExecutionState State
    {
        get { return _state; }
        set
        {
            _state = value;
            if (!_state.ToStatuses().Contains(_status))
                _status = _state.ToDefaultStatus();
        }
    }

    /// <summary>
    /// Custom status of this instance.
    /// </summary>
    public string CustomStatus { get; set; }

    // Time -------------------------------------

    /// <summary>
    /// Start date of this instance.
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// Re-start date of this instance.
    /// </summary>
    public DateTime? RestartDate { get; set; }

    /// <summary>
    /// End date of this instance.
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// End date of this instance.
    /// </summary>
    public TimeSpan? Duration
    {
        get
        {
            if (StartDate != null && EndDate != null)
            {
                return EndDate.Value.Subtract(StartDate.Value);
            }

            return null;
        }
    }

    // Result -------------------------------------

    /// <summary>
    /// Result level of this instance. Over a certain value the result can be considered 
    /// as satisfying.
    /// </summary>
    public int ResultLevel { get; set; }

    #endregion

    // ------------------------------------------
    // CONSTRUCTORS
    // ------------------------------------------

    #region Constructors

    /// <summary>
    /// Instantiates a new instance of the ProcessExecution class.
    /// </summary>
    public ProcessExecution()
    {
    }

    #endregion

    // ------------------------------------------
    // IDISPOSABLE METHODS
    // ------------------------------------------

    #region IDisposable_Methods

    private bool _isDisposed = false;

    /// <summary>
    /// Disposes this instance. 
    /// </summary>
    /// <param name="isDisposing">Indicates whether this instance is disposing</param>
    protected override void Dispose(bool isDisposing)
    {
        if (_isDisposed)
        {
            return;
        }

        _isDisposed = true;

        base.Dispose(isDisposing);
    }

    public string Key()
    {
        throw new NotImplementedException();
    }

    #endregion
}