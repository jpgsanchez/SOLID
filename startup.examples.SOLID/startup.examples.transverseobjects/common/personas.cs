using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Serialization;

namespace startup.examples.transverseobjects.common
{
    /// <remarks/>
    [Serializable]
    [XmlTypeAttribute(AnonymousType = true)]
    [XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class personas
    {
        /// <remarks/>
        [XmlElementAttribute("persona")]
        public List<persona> persona { get; set; }
    }
}