using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Model
{
    [XmlRoot(ElementName = "simpleforecast")]
    public class Simpleforecast
    {
        [XmlElement(ElementName = "forecastdays")]
        public Forecastdays Forecastdays { get; set; }
    }
}
