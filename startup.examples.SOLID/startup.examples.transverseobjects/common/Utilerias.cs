using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace startup.examples.transverseobjects.common
{
    public class Utilerias
    {
        public class Enums
        {
            public enum TipoDocumentoStorage
            {
                POLAT = 0
            }

            public enum Paises
            {
                Mexico = 1,
                Argentina = 2,
                Colombia = 3,
                Brasil = 4,
                Peru = 5,
                NoValido = 6,
                Panama = 7,
                Chile = 8
            }
        }
    }

    public enum ReportesComplementosBR
    {
        AnexaPoliza = 1,
        DemostrativoFraccionamiento,
        DemostrativoComision,
        Boletos,
        CartaFianza,
        ConvenioPago,
        Cupones,
        Factura,
        Coaseguro,
        Recibo,
        Poliza,
        Clausulas,
        EndosoTexto,
        AumentoSuma,
        DocumentosEmitidos,
        AnexoCoaseguroCedido,
        InformacionImportante,
        Certificado
    }
}