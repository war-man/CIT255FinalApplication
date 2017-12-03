using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Model
{
    [XmlRoot(ElementName = "xml")]
    public class Xml
    {
        [XmlElement(ElementName = "response")]
        public Response Response { get; set; }
    }
}
