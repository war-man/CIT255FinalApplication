using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Model
{
    [XmlRoot(ElementName = "high")]
    public class High
    {
        [XmlElement(ElementName = "fahrenheit")]
        public string Fahrenheit { get; set; }
        [XmlElement(ElementName = "celsius")]
        public string Celsius { get; set; }
    }
}
