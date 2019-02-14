using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Reporting.WinForms;

using startup.examples.transverseobjects.common;

namespace startup.examples.principles.isp
{
    public class rdlcImplementation : IrdlcImplementation, IReportProcess
    {
        public void CargarDataSourcePolizaLATAM(PolizaModelo Reporte, ref ReportViewer reportViewer)
        {
            throw new NotImplementedException();
        }
        public List<ComplementosPolizaBR> OrdenPolizaBrasil(List<ComplementosPolizaBR> origen)
        {
            throw new NotImplementedException();
        }
        public void AsignaParametros(ref ReportViewer reportViewer)
        {
            throw new NotImplementedException();
        }
        public byte[] BytesReportePolizaFromDocumentStorage(string reporteId, Utilerias.Enums.TipoDocumentoStorage tipoDocumento)
        {
            throw new NotImplementedException();
        }
        public byte[] ConcatenarReportes(byte[] ReporteOriginal, byte[] ReporteConcatenar)
        {
            throw new NotImplementedException();
        }
        public byte[] AgregarMarcaAgua(byte[] reporteFuente, string textoMarcaAgua)
        {
            throw new NotImplementedException();
        }
        public bool GenerarReporte()
        {
            throw new NotImplementedException();
        }
    }
}
