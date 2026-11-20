using BindOpen.Data.Meta;
using BindOpen.Logging.Events;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BindOpen.Logging;

/// <summary>
/// This class represents a class reference database entity.
/// </summary>
public class LogDb : IBdoDb
{
    // --------------------------------------------------
    // PROPERTIES
    // --------------------------------------------------

    #region Properties

    /// <summary>
    /// The log identifier of this instance.
    /// </summary>
    [Key]
    [Column("LogId")]
    public string Identifier { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string ResultCode { get; set; }

    #endregion

    // ------------------------------------------
    // ITDetailedPoco Implementation
    // ------------------------------------------

    #region ITDetailedPoco

    /// <summary>
    /// The class reference of this instance.
    /// </summary>
    [ForeignKey(nameof(ParentId))]
    public LogDb Parent { get; set; }

    /// <summary>
    /// The conifugration identifier of this instance.
    /// </summary>
    public string ParentId { get; set; }

    /// <summary>
    /// The title of this instance.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// The description of this instance.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// The metaset of this instance.
    /// </summary>
    [ForeignKey(nameof(ProcessExecutionId))]
    public ProcessExecutionDb ProcessExecution { get; set; }

    /// <summary>
    /// The metaset identifier of this instance.
    /// </summary>
    public string ProcessExecutionId { get; set; }

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
    /// The class reference of this instance.
    /// </summary>
    [ForeignKey(nameof(TaskConfigurationId))]
    public ConfigurationDb TaskConfiguration { get; set; }

    /// <summary>
    /// The conifugration identifier of this instance.
    /// </summary>
    public string TaskConfigurationId { get; set; }

    /// <summary>
    /// The events of this instance.
    /// </summary>
    [NotMapped]
    public List<LogEventDb> Events { get; set; }

    #endregion

    // --------------------------------------------------
    // CONSTRUCTORS
    // --------------------------------------------------

    #region Constructors

    /// <summary>
    /// Instantiates a new instance of the LogDb class.
    /// </summary>
    public LogDb()
    {
    }

    #endregion
}
