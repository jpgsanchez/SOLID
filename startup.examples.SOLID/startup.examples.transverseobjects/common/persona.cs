using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Serialization;

namespace startup.examples.transverseobjects.common
{
    [XmlTypeAttribute(AnonymousType = true)]
    public class persona
    {
        public string nombre { get; set; }

        /// <remarks/>
        public byte edad { get; set; }

        /// <remarks/>
        public string sexo { get; set; }

        /// <remarks/>
        [XmlAttributeAttribute()]
        public byte id { get; set; }
    }
}