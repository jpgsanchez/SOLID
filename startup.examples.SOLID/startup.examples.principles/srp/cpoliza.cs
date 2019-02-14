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
    public class cpoliza
    {
        public byte[] generapoliza(PolizaModelo Reporte, PersonalizaReporte PersonalizaReporte, ComplementosPolizaBR item, string documentoId)
        {
            Implementations clsImplementations = new Implementations();
            byte[]  bytesLocal = clsImplementations.ReporteComplementoBrasil(Reporte, item);
            clsImplementations.BytesReportePolizaToDocumentStorage(item.Reporte.ToString(), documentoId, bytesLocal, PersonalizaReporte.MostrarMarcaAgua, Utilerias.Enums.TipoDocumentoStorage.POLAT);

            return bytesLocal;
        }
    }
}
