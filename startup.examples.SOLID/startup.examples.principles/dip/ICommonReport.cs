using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using startup.examples.transverseobjects.common;

namespace startup.examples.principles.dip
{
    public interface ICommonReport
    {
        byte[] Datos { get; set; }

        PolizaModelo _Reporte { get; set; }
        PersonalizaReporte _PersonalizaReporte { get; set; }

        void ObtenerBytesReportePoliza();
    }
}