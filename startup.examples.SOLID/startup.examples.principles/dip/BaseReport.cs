using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Reporting.WinForms;

using startup.examples.principles.dip;
using startup.examples.transverseobjects.common;

namespace startup.examples.principles.dip
{
    public class BaseReport
    {
        List<ICommonReport> _lstreport { get; set; }
        public BaseReport(List<ICommonReport> lstreport)
        { _lstreport = lstreport; }

        public byte[] CreateReport()
        {
            byte[] result = null;
            try
            {
                foreach (ICommonReport item in _lstreport)
                {
                    item.ObtenerBytesReportePoliza();
                    result = ConcatenarReportes(item.Datos);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        byte[] ConcatenarReportes(byte[] arr)
        { return arr; }
    }
}