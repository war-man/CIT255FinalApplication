using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Models
{
    [XmlRoot(ElementName = "snow_day")]
    public class Snow_day
    {
        [XmlElement(ElementName = "in")]
        public string In { get; set; }
        [XmlElement(ElementName = "cm")]
        public string Cm { get; set; }
    }
}