using BindOpen.Data;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Logging
{
    /// <summary>
    /// This structures defines the stack trace of a task result.
    /// </summary>
    [XmlType("LogEventStackTrace", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
    public class LogEventStackTraceDto : IBdoDto
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The name of the full class.
        /// </summary>
        [JsonPropertyName("fullClassName")]
        [XmlElement("fullClassName")]
        public string FullClassName;

        /// <summary>
        /// The name of the called method.
        /// </summary>
        [JsonPropertyName("methodName")]
        [XmlElement("methodName")]
        public string MethodName;

        /// <summary>
        /// Parameters of the called method.
        /// </summary>
        [JsonPropertyName("methodParams")]
        [XmlElement("methodParams")]
        public string MethodParams;

        /// <summary>
        /// Path of the called file.
        /// </summary>
        [JsonPropertyName("filePath")]
        [XmlElement("filePath")]
        public string FilePath;

        /// <summary>
        /// Called line number.
        /// </summary>
        [JsonPropertyName("lineNumber")]
        [XmlElement("lineNumber")]
        public string LineNumber;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        public LogEventStackTraceDto()
        {
        }

        #endregion
    }
}
