using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Model
{
    [XmlRoot(ElementName = "txt_forecast")]
    public class Txt_forecast
    {
        [XmlElement(ElementName = "date")]
        public string Date { get; set; }
        [XmlElement(ElementName = "forecastdays")]
        public Forecastdays Forecastdays { get; set; }
    }
}
