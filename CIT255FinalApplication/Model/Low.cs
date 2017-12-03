using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Models
{
    [XmlRoot(ElementName = "low")]
    public class Low
    {
        [XmlElement(ElementName = "fahrenheit")]
        public string Fahrenheit { get; set; }
        [XmlElement(ElementName = "celsius")]
        public string Celsius { get; set; }
    }
}
