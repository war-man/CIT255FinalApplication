using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Model
{
    [XmlRoot(ElementName = "response")]
    public class Response
    {
        [XmlElement(ElementName = "version")]
        public string Version { get; set; }
        [XmlElement(ElementName = "termsofService")]
        public string TermsofService { get; set; }
        [XmlElement(ElementName = "features")]
        public Features Features { get; set; }
        [XmlElement(ElementName = "forecast")]
        public Forecast Forecast { get; set; }
    }
}