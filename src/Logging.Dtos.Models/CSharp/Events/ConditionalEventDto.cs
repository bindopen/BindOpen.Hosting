using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace BindOpen.Logging.Events
{
    /// <summary>
    /// This class represents a conditional event.
    /// </summary>
    [XmlType("ConditionalEvent", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
    [XmlRoot(ElementName = "event", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel", IsNullable = false)]
    public class ConditionalEventDto : EventDto
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Condition script of this instance.
        /// </summary>
        [JsonPropertyName("conditionScript")]
        [XmlElement("conditionScript")]
        public string ConditionScript { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoConditionalEventDto class.
        /// </summary>
        public ConditionalEventDto() : base()
        {
        }

        #endregion
    }
}
