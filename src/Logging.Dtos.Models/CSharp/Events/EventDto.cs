using BindOpen.Data;
using BindOpen.Data.Meta;
using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace BindOpen.Logging.Events
{
    /// <summary>
    /// This class represents an event.
    /// </summary>
    [XmlType("Event", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
    [XmlRoot(ElementName = "event", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel", IsNullable = false)]
    [XmlInclude(typeof(ConditionalEventDto))]
    public class EventDto : IBdoDto
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        // General ----------------------------------

        /// <summary>
        /// The title of this instance.
        /// </summary>
        [JsonPropertyName("title")]
        [XmlElement("title")]
        public string Title { get; set; }

        /// <summary>
        /// The description of this instance.
        /// </summary>
        [JsonPropertyName("description")]
        [XmlElement("description")]
        public string Description { get; set; }

        /// <summary>
        /// Kind of this instance.
        /// </summary>
        [JsonPropertyName("kind")]
        [XmlAttribute("kind")]
        [DefaultValue(BdoEventKinds.Any)]
        public BdoEventKinds Kind { get; set; } = BdoEventKinds.Other;

        /// <summary>
        /// Creation date of this instance.
        /// </summary>
        [JsonPropertyName("date")]
        [XmlAttribute("date")]
        [DefaultValue("")]
        public string Date { get; set; }

        /// <summary>
        /// Long description of this instance.
        /// </summary>
        [JsonPropertyName("longDescription")]
        [XmlElement("longDescription")]
        public string LongDescription { get; set; }

        /// <summary>
        /// Detail of this instance.
        /// </summary>
        [JsonPropertyName("detail")]
        [XmlElement("detail")]
        public MetaSetDto Detail { get; set; }

        /// <summary>
        /// Criticality of this instance.
        /// </summary>
        [JsonPropertyName("criticality")]
        [XmlElement("criticality")]
        [DefaultValue(Criticalities.None)]
        public Criticalities Criticality { get; set; } = Criticalities.None;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoEventDto class.
        /// </summary>
        public EventDto()
        {
        }

        #endregion
    }
}
