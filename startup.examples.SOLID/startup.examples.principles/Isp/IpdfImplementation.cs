using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Reporting.WinForms;

using startup.examples.transverseobjects.common;

namespace startup.examples.principles.Isp
{
    public interface IpdfImplementation
    {
        ReportViewer CargarDataSourcePoliza(PolizaModelo Reporte);
        List<ComplementosPolizaBR> ObtieneDatos(List<ComplementosPolizaBR> obj);
    }
}