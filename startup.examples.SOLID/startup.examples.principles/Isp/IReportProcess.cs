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
    public interface IReportProcess
    {
        void AsignaParametros(ref ReportViewer reportViewer);
        byte[] BytesReportePolizaFromDocumentStorage(string reporteId, Utilerias.Enums.TipoDocumentoStorage tipoDocumento);
        byte[] ConcatenarReportes(byte[] ReporteOriginal, byte[] ReporteConcatenar);
        byte[] AgregarMarcaAgua(byte[] reporteFuente, string textoMarcaAgua);
    }
}
