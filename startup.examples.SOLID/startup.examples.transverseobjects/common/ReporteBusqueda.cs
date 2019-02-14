using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace startup.examples.transverseobjects.common
{
    [Serializable]
    public class ReporteBusqueda
    {
        public string RutaReporte { get; set; }

        public List<ParametrosReportes> Parametros { get; set; }

        public bool Adjuntar { get; set; }

        public List<string> NombreFuenteDatos { get; set; }

        public string tipo { get; set; }
    }

    [Serializable]
    public class ParametrosReportes
    {
        public string Nombre { get; set; }
        public string Valor { get; set; }
        public int NO_BOLETO { get; set; }
    }
}