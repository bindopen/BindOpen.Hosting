using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace BindOpen.Logging.Events
{
    /// <summary>
    /// This class represents a log event.
    /// </summary>
    [XmlType("LogEvent", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
    [XmlRoot(ElementName = "logEvent", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel", IsNullable = false)]
    public class LogEventDto : EventDto
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        // ----------------------------------

        /// <summary>
        /// Result code of this instance.
        /// </summary>
        [JsonPropertyName("resultCode")]
        [XmlElement("resultCode")]
        [DefaultValue(null)]
        public string ResultCode { get; set; }

        /// <summary>
        /// Source of this instance.
        /// </summary>
        [JsonPropertyName("source")]
        [XmlElement("source")]
        public string Source { get; set; }

        /// <summary>
        /// Stack traces of this instance.
        /// </summary>
        [JsonPropertyName("stack.trace")]
        [XmlArray("stack.traces")]
        [XmlArrayItem("stack.trace")]
        public List<LogEventStackTraceDto> StackTraces { get; set; }

        /// <summary>
        /// The log of this instance.
        /// </summary>
        [JsonPropertyName("log")]
        [XmlElement("log")]
        public LogDto Log { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoLogEventDto class.
        /// </summary>
        public LogEventDto()
        {
        }

        #endregion
    }
}
