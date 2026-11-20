using BindOpen.Data;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Logging
{
    /// <summary>
    /// This class represents a job.
    /// </summary>
    public class ProcessExecutionDto : IBdoDto
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        [JsonPropertyName("custom.status")]
        [XmlElement("custom.status")]
        public string CustomStatus { get; set; }

        [JsonPropertyName("end.date")]
        [XmlElement("end.date")]
        public string EndDate { get; set; }

        [JsonPropertyName("location")]
        [XmlElement("location")]
        public string Location { get; set; }

        [JsonPropertyName("progressIndex")]
        [XmlElement("progressIndex")]
        public float ProgressIndex { get; set; }

        [JsonPropertyName("progressMax")]
        [XmlElement("progressMax")]
        public float ProgressMax { get; set; }

        [JsonPropertyName("restartDate")]
        [XmlElement("restartDate")]
        public string RestartDate { get; set; }

        [JsonPropertyName("resultLevel")]
        [XmlElement("resultLevel")]
        public int ResultLevel { get; set; }

        [JsonPropertyName("startDate")]
        [XmlElement("startDate")]
        public string StartDate { get; set; }

        [JsonPropertyName("date")]
        [XmlElement("date")]
        public ProcessExecutionState State { get; set; }

        [JsonPropertyName("status")]
        [XmlElement("status")]
        public ProcessExecutionStatus Status { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ProcessExecutionDto class.
        /// </summary>
        public ProcessExecutionDto()
        {
        }

        #endregion
    }
}
