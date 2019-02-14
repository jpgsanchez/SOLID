using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Reporting.WinForms;

using startup.examples.principles.lsp;
using startup.examples.transverseobjects.common;

namespace startup.examples.principles.lsp
{
    public class CreateReport
    {
        public byte[] ObtenerBytesReportePoliza(PolizaModelo Reporte, PersonalizaReporte PersonalizaReporte)
        {
            ICommonReport cr = null;
            string extensionReporte = Path.GetExtension(Reporte.RutaReporte);
            switch (extensionReporte)
            {
                case "rdlc":
                    cr = new rdlcReport();
                    cr.ObtenerBytesReportePoliza(Reporte, PersonalizaReporte);
                    break;
                case "html":
                    cr = new htmlReport();
                    cr.ObtenerBytesReportePoliza(Reporte, PersonalizaReporte);
                    break;
                case "pdf":
                    cr = new pdfReport();
                    cr.ObtenerBytesReportePoliza(Reporte, PersonalizaReporte);
                    break;
                default:
                    throw new NotImplementedException("report not exists");
            }

            return cr.Datos;
        }
    }
}