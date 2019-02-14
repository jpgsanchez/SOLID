using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Reporting.WinForms;

using startup.examples.principles.srp;
using startup.examples.transverseobjects.common;

namespace startup.examples.principles.srp
{
    public class CreateReport
    {
        public ResultPoliza ObtenerBytesReportePoliza(PolizaModelo Reporte, PersonalizaReporte PersonalizaReporte)
        {
            ResultPoliza objresult = new ResultPoliza();
            string extensionReporte = Path.GetExtension(Reporte.RutaReporte);
            switch (extensionReporte)
            {
                case "rdlc":
                    rdlcReport objrdlc = new rdlcReport();
                    objresult = objrdlc.ObtenerBytesReportePoliza(Reporte, PersonalizaReporte);
                    break;
                case "html":
                    htmlReport objhtml = new htmlReport();
                    objresult = objhtml.ObtenerBytesReportePoliza(Reporte, PersonalizaReporte);
                    break;
                default:
                    break;
            }

            return objresult;
        }
    }
}