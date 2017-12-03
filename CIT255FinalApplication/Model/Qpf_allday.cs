using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Models
{
    [XmlRoot(ElementName = "qpf_allday")]
    public class Qpf_allday
    {
        [XmlElement(ElementName = "in")]
        public string In { get; set; }
        [XmlElement(ElementName = "mm")]
        public string Mm { get; set; }
    }
}
