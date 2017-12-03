using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Model
{
    [XmlRoot(ElementName = "forecastday")]
    public class Forecastday
    {
        [XmlElement(ElementName = "period")]
        public int Period { get; set; }
        [XmlElement(ElementName = "icon")]
        public string Icon { get; set; }
        [XmlElement(ElementName = "icon_url")]
        public string Icon_url { get; set; }
        [XmlElement(ElementName = "title")]
        public string Title { get; set; }
        [XmlElement(ElementName = "fcttext")]
        public string Fcttext { get; set; }
        [XmlElement(ElementName = "fcttext_metric")]
        public string Fcttext_metric { get; set; }
        [XmlElement(ElementName = "pop")]
        public int Pop { get; set; }
        [XmlElement(ElementName = "date")]
        public Date Date { get; set; }
        [XmlElement(ElementName = "high")]
        public High High { get; set; }
        [XmlElement(ElementName = "low")]
        public Low Low { get; set; }
        [XmlElement(ElementName = "conditions")]
        public string Conditions { get; set; }
        [XmlElement(ElementName = "skyicon")]
        public string Skyicon { get; set; }
        [XmlElement(ElementName = "qpf_allday")]
        public Qpf_allday Qpf_allday { get; set; }
        [XmlElement(ElementName = "qpf_day")]
        public Qpf_day Qpf_day { get; set; }
        [XmlElement(ElementName = "qpf_night")]
        public Qpf_night Qpf_night { get; set; }
        [XmlElement(ElementName = "snow_allday")]
        public Snow_allday Snow_allday { get; set; }
        [XmlElement(ElementName = "snow_day")]
        public Snow_day Snow_day { get; set; }
        [XmlElement(ElementName = "snow_night")]
        public Snow_night Snow_night { get; set; }
        [XmlElement(ElementName = "maxwind")]
        public Maxwind Maxwind { get; set; }
        [XmlElement(ElementName = "avewind")]
        public Avewind Avewind { get; set; }
        [XmlElement(ElementName = "avehumidity")]
        public string Avehumidity { get; set; }
        [XmlElement(ElementName = "maxhumidity")]
        public string Maxhumidity { get; set; }
        [XmlElement(ElementName = "minhumidity")]
        public string Minhumidity { get; set; }
        /// <summary>
        /// this is a bool that does not come from the API, but is for internal use in the application
        /// </summary>
        public bool IsPlantingDay { get; set; }
    }
}
