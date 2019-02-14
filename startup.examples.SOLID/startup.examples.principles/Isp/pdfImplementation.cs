using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace startup.examples.principles.Isp
{
    public class pdfImplementation : IpdfImplementation, IReportProcess
    {
        public Microsoft.Reporting.WinForms.ReportViewer CargarDataSourcePoliza(transverseobjects.common.PolizaModelo Reporte)
        {
            throw new NotImplementedException();
        }

        public List<transverseobjects.common.ComplementosPolizaBR> ObtieneDatos(List<transverseobjects.common.ComplementosPolizaBR> obj)
        {
            throw new NotImplementedException();
        }

        void IReportProcess.AsignaParametros(ref Microsoft.Reporting.WinForms.ReportViewer reportViewer)
        {
            throw new NotImplementedException();
        }

        byte[] IReportProcess.BytesReportePolizaFromDocumentStorage(string reporteId, transverseobjects.common.Utilerias.Enums.TipoDocumentoStorage tipoDocumento)
        {
            throw new NotImplementedException();
        }

        byte[] IReportProcess.ConcatenarReportes(byte[] ReporteOriginal, byte[] ReporteConcatenar)
        {
            throw new NotImplementedException();
        }

        byte[] IReportProcess.AgregarMarcaAgua(byte[] reporteFuente, string textoMarcaAgua)
        {
            throw new NotImplementedException();
        }
    }
}