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
    public interface IImplementation
    {
        void AsignaParametros(ref ReportViewer reportViewer);
        void AsignaParametros(ref string cadena, PolizaModelo Reporte);
        void CargarDataSourcePolizaLATAM(PolizaModelo Reporte, ref ReportViewer reportViewer);
        List<ComplementosPolizaBR> OrdenPolizaBrasil(List<ComplementosPolizaBR> origen);
        byte[] BytesReportePolizaFromDocumentStorage(string reporteId, Utilerias.Enums.TipoDocumentoStorage tipoDocumento);
        byte[] ConcatenarReportes(byte[] ReporteOriginal, byte[] ReporteConcatenar);
        EmisionNuevos ConsultarDatosAdicionalesFianza(EmisionNuevos emision);
        string ObtieneRutaComplementoBrasil(string ruta);
        string ObtieneRutaComplementoBrasil17(string ruta);
        void BytesReportePolizaToDocumentStorage(string nombreDocumento, string reporteId, byte[] bytes, bool marcaAgua, Utilerias.Enums.TipoDocumentoStorage tipoDocumento);
        byte[] ReporteComplementoBrasil(PolizaModelo Reporte, ComplementosPolizaBR complementosPoliza, Utilerias.Enums.Paises pais = Utilerias.Enums.Paises.Brasil);
        byte[] ConvertirHTMLEnPDF(PersonalizaReporte PersonalizaReporte);
        byte[] AgregarClausulasAEmision(byte[] ReporteOrdenEmision, PersonalizaReporte PersonalizaReporte);
        List<PremioBrasil> ConsultarPremioBrasil(PremioBrasil param);
        byte[] AgregarMarcaAgua(byte[] reporteFuente, string textoMarcaAgua);
        int CalcularFactorVencimiento(DateTime fecha);
        CodigoBarraBoleto ConstruirCodigoBarra(CodigoBarraBoleto param, string P_OFICINA, string culture);
        byte[] ObtenerBytesCodigoBarra(string strEntrada);
    }
}