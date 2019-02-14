using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Reporting.WinForms;

using startup.examples.transverseobjects.common;

namespace startup.examples.principles.srp
{
    public class rdlcReport
    {
        public bool ParcelasShenaRPPA;

        public ResultPoliza ObtenerBytesReportePoliza(PolizaModelo Reporte, PersonalizaReporte PersonalizaReporte)
        {
            Implementations clsImplementations = new Implementations();

            string documentoId = "";
            ResultPoliza result = new ResultPoliza();
            string extensionReporte = "";
            extensionReporte = Path.GetExtension(Reporte.RutaReporte);
            byte[] bytesLocal = null;
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            clsImplementations.CargarDataSourcePolizaLATAM(Reporte, ref reportViewer);
            reportViewer.LocalReport.ReportPath = Reporte.RutaReporte;
            PersonalizaReporte.Pais = Reporte.Pais;
            try
            {
                clsImplementations.AsignaParametros(ref reportViewer);

                var ListaComplementosOrden = clsImplementations.OrdenPolizaBrasil(Reporte.COMPLEMENTOS);
                byte[] reporteConcatenado = null;
                for (int i = 0; i < ListaComplementosOrden.Count(); i++)
                {
                    ComplementosPolizaBR item = ListaComplementosOrden[i];
                    if (Reporte.FIANZASIS != null)
                        documentoId = string.Format("{0}{1}", Reporte.FIANZASIS.First().ID_DOCUMENTACION, (int)item.Reporte);
                    else
                        documentoId = "0";

                    byte[] bytesStorage = null;
                    if (documentoId.Length > 1)
                        bytesStorage = clsImplementations.BytesReportePolizaFromDocumentStorage(documentoId, Utilerias.Enums.TipoDocumentoStorage.POLAT);

                    if (bytesStorage != null)
                    {
                        bytesLocal = bytesStorage;
                        reporteConcatenado = (reporteConcatenado == null) ? bytesLocal : clsImplementations.ConcatenarReportes(reporteConcatenado, bytesLocal);
                    }
                    else
                    {
                        switch (item.Reporte)
                        {
                            case ReportesComplementosBR.Boletos:
                                cboleto objboleto = new cboleto();
                                reporteConcatenado = objboleto.generaboleto(Reporte, PersonalizaReporte, item);
                                break;
                            case ReportesComplementosBR.AnexaPoliza:
                            case ReportesComplementosBR.DemostrativoFraccionamiento:
                                cpoliza objpoliza = new cpoliza();
                                bytesLocal = objpoliza.generapoliza(Reporte, PersonalizaReporte, item, documentoId);
                                reporteConcatenado = (reporteConcatenado == null) ? bytesLocal : clsImplementations.ConcatenarReportes(reporteConcatenado, bytesLocal);

                                break;
                            case ReportesComplementosBR.DemostrativoComision:
                                cdemostrativacomisio objcdemostrativacomisio = new cdemostrativacomisio();
                                bytesLocal = objcdemostrativacomisio.generademostrativacomisio(Reporte, PersonalizaReporte, item, documentoId);
                                reporteConcatenado = (reporteConcatenado == null) ? bytesLocal : clsImplementations.ConcatenarReportes(reporteConcatenado, bytesLocal);
                                break;
                        }
                    }
                }
                byte[] resultBytes = null;
                if (reporteConcatenado != null)
                    resultBytes = reporteConcatenado;
                if (resultBytes != null)
                { result.Datos = PersonalizaReporte.MostrarMarcaAgua ? clsImplementations.AgregarMarcaAgua(resultBytes, PersonalizaReporte.TextoMarcaAgua) : resultBytes; }
            }
            catch (Exception ex)
            { throw ex; }

            return result;
        }
    }
}