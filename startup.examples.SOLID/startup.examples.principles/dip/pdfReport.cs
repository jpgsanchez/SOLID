using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using startup.examples.transverseobjects.common;

namespace startup.examples.principles.dip
{
    public class pdfReport : ICommonReport
    {
        public byte[] Datos { get; set; }

        public PolizaModelo _Reporte { get; set; }

        public PersonalizaReporte _PersonalizaReporte { get; set; }

        public pdfReport(PolizaModelo Reporte, PersonalizaReporte PersonalizaReporte)
        {
            _Reporte = Reporte;
            _PersonalizaReporte = PersonalizaReporte;
        }

        public void ObtenerBytesReportePoliza()
        {
            Datos = new ResultPoliza().Datos;
        }
    }
}