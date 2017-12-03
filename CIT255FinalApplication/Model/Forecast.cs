using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Model
{
    [XmlRoot(ElementName = "forecast")]
    public class Forecast
    {
        [XmlElement(ElementName = "txt_forecast")]
        public Txt_forecast Txt_forecast { get; set; }
        [XmlElement(ElementName = "simpleforecast")]
        public Simpleforecast Simpleforecast { get; set; }
    }
}
