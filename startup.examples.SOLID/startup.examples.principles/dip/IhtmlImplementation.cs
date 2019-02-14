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
    public interface IhtmlImplementation : IBaseReport
    {
        void AsignaParametros(ref string cadena, PolizaModelo Reporte);

        byte[] ConvertirHTMLEnPDF(PersonalizaReporte PersonalizaReporte);
    }
}