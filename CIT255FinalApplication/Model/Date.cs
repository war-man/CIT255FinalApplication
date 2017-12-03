using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Model
{
    [XmlRoot(ElementName = "date")]
    public class Date
    {
        [XmlElement(ElementName = "epoch")]
        public string Epoch { get; set; }
        [XmlElement(ElementName = "pretty_short")]
        public string Pretty_short { get; set; }
        [XmlElement(ElementName = "pretty")]
        public string Pretty { get; set; }
        [XmlElement(ElementName = "day")]
        public string Day { get; set; }
        [XmlElement(ElementName = "month")]
        public string Month { get; set; }
        [XmlElement(ElementName = "year")]
        public string Year { get; set; }
        [XmlElement(ElementName = "yday")]
        public string Yday { get; set; }
        [XmlElement(ElementName = "hour")]
        public string Hour { get; set; }
        [XmlElement(ElementName = "min")]
        public string Min { get; set; }
        [XmlElement(ElementName = "sec")]
        public string Sec { get; set; }
        [XmlElement(ElementName = "isdst")]
        public string Isdst { get; set; }
        [XmlElement(ElementName = "monthname")]
        public string Monthname { get; set; }
        [XmlElement(ElementName = "monthname_short")]
        public string Monthname_short { get; set; }
        [XmlElement(ElementName = "weekday_short")]
        public string Weekday_short { get; set; }
        [XmlElement(ElementName = "weekday")]
        public string Weekday { get; set; }
        [XmlElement(ElementName = "ampm")]
        public string Ampm { get; set; }
        [XmlElement(ElementName = "tz_short")]
        public string Tz_short { get; set; }
        [XmlElement(ElementName = "tz_long")]
        public string Tz_long { get; set; }
    }
}
