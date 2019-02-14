using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using startup.examples.transverseobjects.common;

namespace startup.examples.transverseobjects.ocp
{
    public interface IReadFile
    {
        string getTextonFile(clsModel obj);

        byte[] getAllBytesfromFile(clsModel obj);
    }
}