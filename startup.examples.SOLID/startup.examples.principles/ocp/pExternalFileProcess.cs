using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Xml.Serialization;
using startup.examples.transverseobjects.ocp;
using startup.examples.transverseobjects.common;

namespace startup.examples.principles.ocp
{
    public class pExternalFileProcess : IExternalProcess
    {
        public personas getObjetctfromXML(clsModel obj)
        {
            personas result = new personas();
            if (!string.IsNullOrEmpty(obj.strpath) && File.Exists(obj.strpath))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(personas));
                using (StreamReader reader = new StreamReader(obj.strpath))
                {
                    result = (personas)serializer.Deserialize(reader);
                }
            }

            return result;
        }
    }
}