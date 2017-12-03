using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Models
{
    [XmlRoot(ElementName = "avewind")]
    public class Avewind
    {
        [XmlElement(ElementName = "mph")]
        public string Mph { get; set; }
        [XmlElement(ElementName = "kph")]
        public string Kph { get; set; }
        [XmlElement(ElementName = "dir")]
        public string Dir { get; set; }
        [XmlElement(ElementName = "degrees")]
        public string Degrees { get; set; }
    }
}
