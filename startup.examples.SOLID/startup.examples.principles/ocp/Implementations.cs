using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Reporting.WinForms;

using startup.examples.transverseobjects.common;

namespace startup.examples.principles.ocp
{
    public class Implementations : IImplementation
    {
        public void AsignaParametros(ref ReportViewer reportViewer) { }
        public void AsignaParametros(ref string cadena, PolizaModelo Reporte) { }
        public void CargarDataSourcePolizaLATAM(PolizaModelo Reporte, ref ReportViewer reportViewer) { }
        public List<ComplementosPolizaBR> OrdenPolizaBrasil(List<ComplementosPolizaBR> origen) { return new List<ComplementosPolizaBR>(); }
        public byte[] BytesReportePolizaFromDocumentStorage(string reporteId, Utilerias.Enums.TipoDocumentoStorage tipoDocumento) { return new byte[0]; }
        public byte[] ConcatenarReportes(byte[] ReporteOriginal, byte[] ReporteConcatenar) { return new byte[0]; }
        public EmisionNuevos ConsultarDatosAdicionalesFianza(EmisionNuevos emision) { return new EmisionNuevos(); }
        public string ObtieneRutaComplementoBrasil(string ruta) { return string.Empty; }
        public string ObtieneRutaComplementoBrasil17(string ruta) { return string.Empty; }
        public void BytesReportePolizaToDocumentStorage(string nombreDocumento, string reporteId, byte[] bytes, bool marcaAgua, Utilerias.Enums.TipoDocumentoStorage tipoDocumento) { }
        public byte[] ReporteComplementoBrasil(PolizaModelo Reporte, ComplementosPolizaBR complementosPoliza, Utilerias.Enums.Paises pais = Utilerias.Enums.Paises.Brasil) { return new byte[0]; }
        public byte[] ConvertirHTMLEnPDF(PersonalizaReporte PersonalizaReporte) { return new byte[0]; }
        public byte[] AgregarClausulasAEmision(byte[] ReporteOrdenEmision, PersonalizaReporte PersonalizaReporte) { return new byte[0]; }
        public List<PremioBrasil> ConsultarPremioBrasil(PremioBrasil param) { return new List<PremioBrasil>(); }
        public byte[] AgregarMarcaAgua(byte[] reporteFuente, string textoMarcaAgua) { return new byte[0]; }
        public int CalcularFactorVencimiento(DateTime fecha) { return 0; }
        public CodigoBarraBoleto ConstruirCodigoBarra(CodigoBarraBoleto param, string P_OFICINA, string culture) { return new CodigoBarraBoleto(); }
        public byte[] ObtenerBytesCodigoBarra(string strEntrada) { return new byte[0]; }
    }
}