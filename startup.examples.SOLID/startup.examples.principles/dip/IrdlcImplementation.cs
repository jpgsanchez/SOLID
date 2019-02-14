using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Reporting.WinForms;

using startup.examples.transverseobjects.common;

namespace startup.examples.principles.dip
{
    public interface IrdlcImplementation : IBaseReport
    {
        void CargarDataSourcePolizaLATAM(PolizaModelo Reporte, ref ReportViewer reportViewer);
        List<ComplementosPolizaBR> OrdenPolizaBrasil(List<ComplementosPolizaBR> origen);
    }
}