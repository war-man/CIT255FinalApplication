using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace Model
{
    [XmlRoot(ElementName = "qpf_night")]
    public class Qpf_night
    {
        [XmlElement(ElementName = "in")]
        public string In { get; set; }
        [XmlElement(ElementName = "mm")]
        public string Mm { get; set; }
    }
}
