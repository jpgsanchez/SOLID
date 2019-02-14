using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using startup.examples.transverseobjects.common;

namespace startup.examples.principles.lsp
{
    public class rdlcReport : ICommonReport
    {
        public byte[] Datos { get; set; }
        public void ObtenerBytesReportePoliza(PolizaModelo Reporte, PersonalizaReporte PersonalizaReporte)
        {
            Datos = new ResultPoliza().Datos;
        }
    }
}