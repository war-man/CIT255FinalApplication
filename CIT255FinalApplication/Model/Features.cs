using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Model
{
    [XmlRoot(ElementName = "features")]
    public class Features
    {
        [XmlElement(ElementName = "feature")]
        public string Feature { get; set; }
    }
}
