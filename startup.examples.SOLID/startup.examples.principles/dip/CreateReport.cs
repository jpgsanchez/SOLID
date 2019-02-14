using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Reporting.WinForms;

using startup.examples.principles.dip;
using startup.examples.transverseobjects.common;

namespace startup.examples.principles.dip
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
                    cr = new rdlcReport(Reporte, PersonalizaReporte);
                    cr.ObtenerBytesReportePoliza();
                    break;
                case "html":
                    cr = new htmlReport(Reporte, PersonalizaReporte);
                    cr.ObtenerBytesReportePoliza();
                    break;
                case "pdf":
                    cr = new pdfReport(Reporte, PersonalizaReporte);
                    cr.ObtenerBytesReportePoliza();
                    break;
                default:
                    throw new NotImplementedException("report not exists");
            }

            return cr.Datos;
        }

        public byte[] ObtenerBytesReportePolizaRdlyPdf(PolizaModelo Reporte, PersonalizaReporte PersonalizaReporte)
        {
            List<ICommonReport> lst = new List<ICommonReport>();

            lst.Add(new rdlcReport(Reporte, PersonalizaReporte));
            lst.Add(new htmlReport(Reporte, PersonalizaReporte));

            BaseReport Ibase = new BaseReport(lst);
            return Ibase.CreateReport();
        }

        public byte[] ObtenerBytesReportePolizaHtmlyPdf(PolizaModelo Reporte, PersonalizaReporte PersonalizaReporte)
        {
            List<ICommonReport> lst = new List<ICommonReport>();

            lst.Add(new htmlReport(Reporte, PersonalizaReporte));
            lst.Add(new pdfReport(Reporte, PersonalizaReporte));

            BaseReport Ibase = new BaseReport(lst);
            return Ibase.CreateReport();
        }
    }
}