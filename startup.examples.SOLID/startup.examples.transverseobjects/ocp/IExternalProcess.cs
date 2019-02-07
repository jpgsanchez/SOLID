using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using startup.examples.transverseobjects.common;

namespace startup.examples.transverseobjects.ocp
{
    public interface IExternalProcess
    {
        personas getObjetctfromXML(clsModel obj);
    }
}