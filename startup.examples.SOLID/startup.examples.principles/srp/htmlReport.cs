using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Reporting.WinForms;

using startup.examples.transverseobjects.common;

namespace startup.examples.principles.srp
{
    public class htmlReport
    {
        public ResultPoliza ObtenerBytesReportePoliza(PolizaModelo Reporte, PersonalizaReporte PersonalizaReporte)
        {
            Implementations clsImplementations = new Implementations();

            ResultPoliza result = new ResultPoliza();
            string extensionReporte = "";
            extensionReporte = Path.GetExtension(Reporte.RutaReporte);
            byte[] datosReporte = null;
            Area area = PersonalizaReporte.AreaRectangle;
            area.Y = 0.5f;
            string certificadoHtml = System.IO.File.ReadAllText(Reporte.RutaReporte);
            clsImplementations.AsignaParametros(ref certificadoHtml, Reporte);
            datosReporte = clsImplementations.ConvertirHTMLEnPDF(
                   new PersonalizaReporte
                   {
                       Html = certificadoHtml,
                       MostrarMarcaAgua = PersonalizaReporte.MostrarMarcaAgua,
                       RutaImagenHeader = PersonalizaReporte.RutaImagenHeader,
                       RutaTipoLetra = PersonalizaReporte.RutaTipoLetra,
                       TextoHeader = PersonalizaReporte.TextoHeader,
                       TextoHeaderTam = PersonalizaReporte.TextoHeaderTam,
                       TextoMarcaAgua = PersonalizaReporte.TextoMarcaAgua,
                       TextoFooter = PersonalizaReporte.TextoFooter,
                       MostrarTextoFooter = PersonalizaReporte.MostrarTextoFooter,
                       MostrarTextoHeader = PersonalizaReporte.MostrarTextoHeader,
                       Pais = Reporte.Pais,
                       AreaRectangle = area
                   });
            result.Datos = datosReporte;

            return result;
        }
    }
}