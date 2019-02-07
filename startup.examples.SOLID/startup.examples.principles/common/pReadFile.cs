using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

using startup.examples.transverseobjects.common;
using System.Xml.Serialization;

namespace startup.examples.principles.common
{
    public class pReadFile
    {
        public string getTextonFile(clsModel obj)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(obj.strpath) && File.Exists(obj.strpath))
            {
                result = File.ReadAllText(obj.strpath);
            }

            return result;
        }

        public byte[] getAllBytesfromFile(clsModel obj)
        {
            byte[] result = new byte[0];
            if (!string.IsNullOrEmpty(obj.strpath) && File.Exists(obj.strpath))
            {
                using (var stream = File.OpenRead(obj.strpath))
                {
                    using (var binaryreader = new BinaryReader(stream))
                    {
                        result = binaryreader.ReadBytes((int)stream.Length);
                    }
                }
            }

            return result;
        }

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