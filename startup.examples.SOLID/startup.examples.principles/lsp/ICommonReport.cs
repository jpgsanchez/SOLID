using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using startup.examples.transverseobjects.common;

namespace startup.examples.principles.lsp
{
    public interface ICommonReport
    {
        byte[] Datos { get; set; }
        void ObtenerBytesReportePoliza(PolizaModelo Reporte, PersonalizaReporte PersonalizaReporte);
    }
}