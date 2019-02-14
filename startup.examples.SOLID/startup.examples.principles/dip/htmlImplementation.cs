using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using startup.examples.principles.dip;
using startup.examples.transverseobjects.common;

namespace startup.examples.principles.dip
{
    public class htmlImplementation : IhtmlImplementation
    {
        public void AsignaParametros(ref string cadena, transverseobjects.common.PolizaModelo Reporte)
        {
            throw new NotImplementedException();
        }

        public byte[] ConvertirHTMLEnPDF(transverseobjects.common.PersonalizaReporte PersonalizaReporte)
        {
            throw new NotImplementedException();
        }

        byte[] IBaseReport.GenerarReporte()
        {
            throw new NotImplementedException();
        }
    }
}
