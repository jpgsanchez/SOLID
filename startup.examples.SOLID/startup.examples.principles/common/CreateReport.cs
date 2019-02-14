using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Reporting.WinForms;

using startup.examples.transverseobjects.common;

namespace startup.examples.principles.common
{
    public class CreateReport
    {
        public bool ParcelasShenaRPPA;
        bool reporteChubb = false;
        bool reporteChubb17 = false;

        public ResultPoliza ObtenerBytesReportePoliza(PolizaModelo Reporte, PersonalizaReporte PersonalizaReporte)
        {
            string documentoId = "";
            ResultPoliza result = new ResultPoliza();
            string extensionReporte = "";
            extensionReporte = Path.GetExtension(Reporte.RutaReporte);
            byte[] bytesLocal = null;
            if (extensionReporte == ".rdlc")
            {
                #region "Reportes RDLC"
                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;
                ReportViewer reportViewer = new ReportViewer();
                reportViewer.ProcessingMode = ProcessingMode.Local;
                CargarDataSourcePolizaLATAM(Reporte, ref reportViewer);
                reportViewer.LocalReport.ReportPath = Reporte.RutaReporte;

                PersonalizaReporte.Pais = Reporte.Pais;

                try
                {
                    #region "Parametros"
                    if (Reporte.Parametros != null && Reporte.Parametros.Count() > 0)
                    {
                        var paramReports = reportViewer.LocalReport.GetParameters();
                        foreach (var item in paramReports)
                        {
                            var param = Reporte.Parametros.Find(x => x.Nombre == item.Name);
                            if (param != null && !string.IsNullOrEmpty(param.Nombre))
                                reportViewer.LocalReport.SetParameters(new ReportParameter(param.Nombre, param.Valor));
                        }
                    }
                    #endregion
                    #region "Brasil"
                    else if (Reporte.Pais.Equals("Brasil"))
                    {
                        var ListaComplementosOrden = OrdenPolizaBrasil(Reporte.COMPLEMENTOS);
                        byte[] reporteConcatenado = null;
                        /*var datosReporte = reportViewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
                        var reporteClausulas = new CustomReports().AgregarClausulasAEmision(datosReporte, PersonalizaReporte);*/

                        for (int i = 0; i < ListaComplementosOrden.Count(); i++)
                        {
                            ComplementosPolizaBR item = ListaComplementosOrden[i];
                            if (Reporte.FIANZASIS != null)
                                documentoId = string.Format("{0}{1}", Reporte.FIANZASIS.First().ID_DOCUMENTACION, (int)item.Reporte);
                            else
                                documentoId = "0";

                            byte[] bytesStorage = null;
                            if (documentoId.Length > 1)
                                bytesStorage = BytesReportePolizaFromDocumentStorage(documentoId, Utilerias.Enums.TipoDocumentoStorage.POLAT);

                            if (bytesStorage != null)
                            {
                                bytesLocal = bytesStorage;
                                reporteConcatenado = (reporteConcatenado == null) ? bytesLocal : ConcatenarReportes(reporteConcatenado, bytesLocal);
                            }
                            else
                            {

                                switch (item.Reporte)
                                {
                                    case ReportesComplementosBR.Boletos:
                                        #region Boletos
                                        bytesLocal = null;
                                        var temp = Reporte.Parametros;
                                        Reporte.Parametros.Clear();
                                        var SenhaRPPA = ConsultarDatosAdicionalesFianza(new EmisionNuevos()
                                        {
                                            P_NO_FIANZA = Reporte.DESCRIPCION.FirstOrDefault().MFZA_NO_FIANZA,
                                            P_NO_INCLUSION = Reporte.DESCRIPCION.FirstOrDefault().MFZA_NO_INCLUSION,
                                            P_NO_MOVIMIENTO = Reporte.DESCRIPCION.FirstOrDefault().MFZA_NO_MOVIMIENTO,
                                            P_CVE_DETALLE = 44
                                        });

                                        if (SenhaRPPA != null && !string.IsNullOrEmpty(SenhaRPPA.RETURN_VALUE))
                                        {
                                            if (Reporte.PARCELAS.Any(x => x.NO_PARCIALIDAD == 1))
                                            {
                                                Reporte.PARCELAS.Remove(Reporte.PARCELAS.First(x => x.NO_PARCIALIDAD == 1));
                                                ParcelasShenaRPPA = true;
                                            }
                                        }
                                        byte[] boletosBytes = null;
                                        foreach (Parcelas itemP in Reporte.PARCELAS)
                                        {
                                            if (reporteChubb)
                                            {
                                                Reporte.RutaReporte = ObtieneRutaComplementoBrasil(item.Ruta);
                                            }
                                            else if (reporteChubb17)
                                            {
                                                Reporte.RutaReporte = ObtieneRutaComplementoBrasil17(item.Ruta);
                                            }
                                            else
                                            {
                                                Reporte.RutaReporte = item.Ruta;
                                            }
                                            var parcialidad = "";
                                            Reporte.Pais = "BrasilBoleto";
                                            if (Reporte.Parametros.Exists(x => x.Nombre == "rpNumeroParcela"))
                                            {

                                                if (itemP.NO_PARCIALIDAD.ToString().Length == 1)
                                                {
                                                    parcialidad = "0";
                                                }
                                                Reporte.Parametros.Find(x => x.Nombre == "rpNumeroParcela").Valor = parcialidad + itemP.NO_PARCIALIDAD.ToString();
                                            }
                                            else
                                            {
                                                if (itemP.NO_PARCIALIDAD.ToString().Length == 1)
                                                {
                                                    parcialidad = "0";
                                                }
                                                Reporte.Parametros.Add(new ParametrosReportes
                                                {
                                                    Nombre = "rpNumeroParcela",
                                                    Valor = parcialidad + itemP.NO_PARCIALIDAD.ToString()
                                                });
                                            }
                                            if (Reporte.Parametros.Exists(x => x.Nombre == "rpFechaVencimiento"))
                                            {
                                                Reporte.Parametros.Find(x => x.Nombre == "rpFechaVencimiento").Valor =
                                                    itemP.FECHA_PAGO.ToShortDateString();
                                            }
                                            else
                                            {
                                                Reporte.Parametros.Add(new ParametrosReportes
                                                {
                                                    Nombre = "rpFechaVencimiento",
                                                    Valor = itemP.FECHA_PAGO.ToShortDateString()
                                                });
                                            }
                                            if (Reporte.Parametros.Exists(x => x.Nombre == "rpValorParcela"))
                                            {
                                                Reporte.Parametros.Find(x => x.Nombre == "rpValorParcela").Valor =
                                                    itemP.TOTAL.ToString();
                                            }
                                            else
                                            {
                                                Reporte.Parametros.Add(new ParametrosReportes
                                                {
                                                    Nombre = "rpValorParcela",
                                                    Valor = itemP.TOTAL.ToString()
                                                });
                                            }
                                            bytesLocal = ObtenerBytesReportePoliza(Reporte, PersonalizaReporte).Datos;
                                            if (boletosBytes == null)
                                            {
                                                boletosBytes = bytesLocal;
                                            }
                                            else
                                            {
                                                boletosBytes = ConcatenarReportes(boletosBytes, bytesLocal);
                                            }

                                        }
                                        bytesLocal = boletosBytes;
                                        BytesReportePolizaToDocumentStorage(item.Reporte.ToString(), documentoId, bytesLocal, PersonalizaReporte.MostrarMarcaAgua, Utilerias.Enums.TipoDocumentoStorage.POLAT);
                                        //}
                                        reporteConcatenado = (reporteConcatenado == null) ? bytesLocal : ConcatenarReportes(reporteConcatenado, bytesLocal);
                                        #endregion
                                        break;
                                    case ReportesComplementosBR.AnexaPoliza:
                                    case ReportesComplementosBR.DemostrativoFraccionamiento:
                                        #region Anexa Poliza y DemostrativoFraccionamiento
                                        bytesLocal = null;
                                        //bytesLocal = BytesReportePolizaFromDocumentStorage(documentoId);
                                        //if (bytesLocal == null)
                                        //{
                                        bytesLocal = this.ReporteComplementoBrasil(Reporte, item);
                                        BytesReportePolizaToDocumentStorage(item.Reporte.ToString(), documentoId, bytesLocal, PersonalizaReporte.MostrarMarcaAgua, Utilerias.Enums.TipoDocumentoStorage.POLAT);
                                        //}
                                        reporteConcatenado = (reporteConcatenado == null) ? bytesLocal : ConcatenarReportes(reporteConcatenado, bytesLocal);
                                        #endregion
                                        break;
                                    case ReportesComplementosBR.DemostrativoComision:
                                        #region DemostrativoComision
                                        byte[] comisionBytes = null;
                                        bytesLocal = null;
                                        //bytesLocal = BytesReportePolizaFromDocumentStorage(documentoId);
                                        //if (bytesLocal == null)
                                        //{
                                        PolizaModelo repBroker = new PolizaModelo();
                                        repBroker.DESCRIPCION = Reporte.DESCRIPCION;
                                        repBroker.DATOSFIANZA = Reporte.DATOSFIANZA;
                                        repBroker.FIANZASIS = Reporte.FIANZASIS;
                                        repBroker.PARCELAS = Reporte.PARCELAS;
                                        repBroker.DATOSEMISORES = Reporte.DATOSEMISORES;
                                        repBroker.NombreFuenteDatos = Reporte.NombreFuenteDatos;
                                        repBroker.COASEGURADOS = Reporte.COASEGURADOS;
                                        var listaEmisores = Reporte.EMISORES;
                                        repBroker.Pais = "Brasil";
                                        for (int f = 0; f < listaEmisores.Count; f++)
                                        {
                                            EmisoresRep emisor = listaEmisores[f];
                                            repBroker.EMISORES = new List<EmisoresRep>();
                                            repBroker.EMISORES.Add(emisor);
                                            repBroker.PARCELASCOMISIONES = Reporte.PARCELASCOMISIONES.Any(x => x.AGENTE == emisor.EXFL_NO_EMISOR) ? Reporte.PARCELASCOMISIONES.Where(x => x.AGENTE == emisor.EXFL_NO_EMISOR).ToList() : new List<ParcelasComision>();

                                            bytesLocal = this.ReporteComplementoBrasil(repBroker, item, Utilerias.Enums.Paises.Brasil);
                                            if (comisionBytes == null)
                                            {
                                                comisionBytes = bytesLocal;
                                            }
                                            else
                                            {
                                                comisionBytes = ConcatenarReportes(comisionBytes, bytesLocal);
                                            }
                                        }
                                        bytesLocal = comisionBytes;
                                        BytesReportePolizaToDocumentStorage(item.Reporte.ToString(), documentoId, bytesLocal, PersonalizaReporte.MostrarMarcaAgua, Utilerias.Enums.TipoDocumentoStorage.POLAT);
                                        //}
                                        reporteConcatenado = (reporteConcatenado == null) ? bytesLocal : ConcatenarReportes(reporteConcatenado, bytesLocal);
                                        #endregion
                                        break;
                                    case ReportesComplementosBR.Coaseguro:
                                        #region Coaseguro
                                        byte[] coaseguroBytes = null;
                                        //bytesLocal = BytesReportePolizaFromDocumentStorage(documentoId);
                                        //if (bytesLocal == null)
                                        //{
                                        PolizaModelo repCoaseguro = new PolizaModelo();
                                        repCoaseguro.Pais = "Brasil";
                                        repCoaseguro.DESCRIPCION = Reporte.DESCRIPCION;
                                        repCoaseguro.DATOSFIANZA = Reporte.DATOSFIANZA;
                                        repCoaseguro.FIANZASIS = Reporte.FIANZASIS;
                                        repCoaseguro.DATOSEMISORES = Reporte.DATOSEMISORES;
                                        repCoaseguro.PRODUCTOS = Reporte.PRODUCTOS;
                                        repCoaseguro.PRODUCTOSMOVIMIENTO = Reporte.PRODUCTOSMOVIMIENTO;
                                        repCoaseguro.SOLICITUD_CAJA = Reporte.SOLICITUD_CAJA;
                                        repCoaseguro.EMISORES = Reporte.EMISORES;
                                        repCoaseguro.NombreFuenteDatos = Reporte.NombreFuenteDatos;
                                        repCoaseguro.ANEXOS = Reporte.ANEXOS;
                                        repCoaseguro.PARCELAS = Reporte.PARCELAS;
                                        repCoaseguro.COASEGURADOS = Reporte.COASEGURADOS;
                                        var listaCoasegurados = Reporte.COASEGURADOS;
                                        for (int f = 0; f < listaCoasegurados.Count; f++)
                                        {
                                            CotizacionCoaseguro coasegurado = listaCoasegurados[f];
                                            item.Parametros =
                                                new List<ParametrosReportes>
                                                    ();
                                            item.Parametros.Add(
                                                new ParametrosReportes()
                                                {
                                                    Nombre = "rpLiderCoaseguro",
                                                    Valor = ""
                                                });
                                            item.Parametros.Add(
                                                new ParametrosReportes()
                                                {
                                                    Nombre = "rpNombreCoasegurador",
                                                    Valor = coasegurado.COFI_NO_COASEGURADOR + " - " + coasegurado.COAS_NOMBRE
                                                });
                                            item.Parametros.Add(
                                                new ParametrosReportes()
                                                {
                                                    Nombre = "rpPctParticipacion",
                                                    Valor = coasegurado.COFI_PCT_PARTICIPACION.ToString()
                                                });
                                            item.Parametros.Add(
                                              new ParametrosReportes()
                                              {
                                                  Nombre = "rpPctComision",
                                                  Valor = coasegurado.MVCO_PCT_COMISION.ToString()
                                              });
                                            item.Parametros.Add(
                                                new ParametrosReportes()
                                                {
                                                    Nombre = "rpImpAsegurada",
                                                    Valor = coasegurado.COFI_VALOR_ASEGURADO.ToString()
                                                });
                                            bytesLocal = this.ReporteComplementoBrasil(repCoaseguro, item);
                                            if (coaseguroBytes == null)
                                            {
                                                coaseguroBytes = bytesLocal;
                                            }
                                            else
                                            {
                                                coaseguroBytes = ConcatenarReportes(coaseguroBytes, bytesLocal);
                                            }
                                        }
                                        bytesLocal = coaseguroBytes;
                                        BytesReportePolizaToDocumentStorage(item.Reporte.ToString(), documentoId, bytesLocal, PersonalizaReporte.MostrarMarcaAgua, Utilerias.Enums.TipoDocumentoStorage.POLAT);
                                        //}
                                        reporteConcatenado = (reporteConcatenado == null) ? bytesLocal : ConcatenarReportes(reporteConcatenado, bytesLocal);
                                        #endregion
                                        break;
                                    case ReportesComplementosBR.Clausulas:
                                        #region Clausulas
                                        if (Reporte.DETALLE[0].TXFZ_TEXTO != "")
                                        {
                                            PersonalizaReporte.Html = Reporte.DETALLE[0].TXFZ_TEXTO;
                                            PersonalizaReporte.Html = PersonalizaReporte.Html.Replace("<p></p>", "<br/>");
                                            PersonalizaReporte.Html = PersonalizaReporte.Html.Replace("<table>", "<table border=\"1\" cellspacing=\"0\" cellpadding=\"0\"");

                                            if (reporteConcatenado == null)
                                            {
                                                reporteConcatenado = ConvertirHTMLEnPDF(PersonalizaReporte);
                                            }
                                            else
                                            {
                                                reporteConcatenado = AgregarClausulasAEmision(reporteConcatenado, PersonalizaReporte);
                                            }

                                            bytesLocal = ConvertirHTMLEnPDF(PersonalizaReporte);

                                            reporteConcatenado = Reporte.DocumentosAdjuntos.Where(byteAdjunto => byteAdjunto != null).Aggregate(reporteConcatenado, (current, byteAdjunto) => ConcatenarReportes(current, byteAdjunto));
                                            BytesReportePolizaToDocumentStorage(item.Reporte.ToString(), documentoId, bytesLocal, PersonalizaReporte.MostrarMarcaAgua, Utilerias.Enums.TipoDocumentoStorage.POLAT);
                                        }
                                        else
                                        {
                                            if (Reporte.DocumentosAdjuntos.Count > 0)
                                            {
                                                if (reporteConcatenado != null)
                                                {
                                                    reporteConcatenado = Reporte.DocumentosAdjuntos.Where(byteAdjunto => byteAdjunto != null).Aggregate(reporteConcatenado, (abyte, byteAdjunto) =>
                                                    {
                                                        bytesLocal = byteAdjunto;
                                                        return ConcatenarReportes(abyte, byteAdjunto);
                                                    });
                                                }
                                                else
                                                {
                                                    reporteConcatenado = Reporte.DocumentosAdjuntos.Where(byteAdjunto => byteAdjunto != null).Aggregate((abyte, byteAdjunto) =>
                                                    {
                                                        bytesLocal = byteAdjunto;
                                                        return ConcatenarReportes(abyte, byteAdjunto);
                                                    });
                                                }

                                                BytesReportePolizaToDocumentStorage(item.Reporte.ToString(), documentoId, bytesLocal, PersonalizaReporte.MostrarMarcaAgua, Utilerias.Enums.TipoDocumentoStorage.POLAT);
                                            }

                                        }
                                        #endregion
                                        break;
                                    case ReportesComplementosBR.Poliza:
                                        #region Póliza
                                        bytesLocal = null;
                                        //bytesLocal = BytesReportePolizaFromDocumentStorage(documentoId);
                                        //if (bytesLocal == null)
                                        //{
                                        bytesLocal = reportViewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
                                        //}
                                        reporteConcatenado = (reporteConcatenado == null) ? bytesLocal : ConcatenarReportes(reporteConcatenado, bytesLocal);
                                        if (Reporte.RutaReporte.Contains("BrasilPoliza"))
                                        {
                                            Reporte.PREMIOBRASIL = ConsultarPremioBrasil(new PremioBrasil()
                                            {
                                                P_NO_FIANZA = Reporte.Fianza,
                                                P_NO_INCLUSION = Reporte.Inclusion,
                                                P_NO_MOVIMIENTO = Reporte.Movimiento,
                                                P_MONTO_PREMIO = Reporte.ANEXOS.FirstOrDefault().PRIMA_TOTAL
                                            });
                                            var parameter = new List<ParametrosReportes>();
                                            parameter.Add(new ParametrosReportes() { Nombre = "rpTipoDocto", Valor = (Reporte.ANEXOS != null && Reporte.ANEXOS.Any()) ? Reporte.ANEXOS.FirstOrDefault().TIPO_DOCUMENTO : "" });
                                            parameter.Add(new ParametrosReportes() { Nombre = "rpShowCoaseguro", Valor = (Reporte.COASEGURADOS != null && Reporte.COASEGURADOS.Any()) ? "1" : "0" });

                                            var clausulasRebranding = ReporteComplementoBrasil(new PolizaModelo()
                                            {
                                                PARCELAS = Reporte.PARCELAS,
                                                DATOSFIANZA = Reporte.DATOSFIANZA,
                                                DESCRIPCION = Reporte.DESCRIPCION,
                                                FIANZASIS = Reporte.FIANZASIS,
                                                PREMIOBRASIL = Reporte.PREMIOBRASIL,
                                                COASEGURADOS = Reporte.COASEGURADOS
                                            }, new ComplementosPolizaBR() { Ruta = Reporte.RutaReporte.Replace("BrasilPoliza", "BrasilClausulas"), Parametros = parameter }, Utilerias.Enums.Paises.Brasil);
                                            reporteConcatenado = (reporteConcatenado == null) ? clausulasRebranding : ConcatenarReportes(reporteConcatenado, clausulasRebranding);
                                        }

                                        BytesReportePolizaToDocumentStorage(item.Reporte.ToString(), documentoId, reporteConcatenado, PersonalizaReporte.MostrarMarcaAgua, Utilerias.Enums.TipoDocumentoStorage.POLAT);

                                        #endregion
                                        break;
                                    case ReportesComplementosBR.DocumentosEmitidos:
                                        #region DocumentosEmitidos
                                        bytesLocal = null;
                                        //bytesLocal = BytesReportePolizaFromDocumentStorage(documentoId);
                                        //if (bytesLocal == null)
                                        //{
                                        var repDocumentos = new PolizaModelo
                                        {
                                            EMISORES = Reporte.EMISORES,
                                            PARCELAS = Reporte.PARCELAS,
                                            COASEGURADOS = Reporte.COASEGURADOS,
                                            DATOSFIANZA = Reporte.DATOSFIANZA,
                                            DESCRIPCION = Reporte.DESCRIPCION,
                                            FIANZASIS = Reporte.FIANZASIS,
                                            Parametros = item.Parametros,
                                            NombreFuenteDatos = Reporte.NombreFuenteDatos,
                                            Pais = "Brasil"
                                        };
                                        bytesLocal = this.ReporteComplementoBrasil(repDocumentos, item);
                                        BytesReportePolizaToDocumentStorage(item.Reporte.ToString(), documentoId, bytesLocal, PersonalizaReporte.MostrarMarcaAgua, Utilerias.Enums.TipoDocumentoStorage.POLAT);
                                        //}
                                        reporteConcatenado = (reporteConcatenado == null) ? bytesLocal : ConcatenarReportes(reporteConcatenado, bytesLocal);
                                        #endregion
                                        break;
                                    case ReportesComplementosBR.AnexoCoaseguroCedido:
                                        #region AnexoCoaseguroCedido
                                        bytesLocal = null;
                                        //bytesLocal = BytesReportePolizaFromDocumentStorage(documentoId);
                                        //if (bytesLocal == null)
                                        //{
                                        item.Parametros = new List<ParametrosReportes>();
                                        if (Reporte.DATOSFIANZA.Any())
                                        {
                                            item.Parametros.Add(new ParametrosReportes() { Nombre = "rpRamo", Valor = String.Format("{0} - {1}", Reporte.DATOSFIANZA.First().CVE_RAMO, Reporte.DATOSFIANZA.First().DSP_RAMO) });
                                        }
                                        if (Reporte.DESCRIPCION.Any())
                                        {
                                            item.Parametros.Add(new ParametrosReportes() { Nombre = "rpAsegurado", Valor = String.Format("{0} - {1}", Reporte.DESCRIPCION.First().BENEFICIARIO_SIS, Reporte.DESCRIPCION.First().NOMBRE_BENEFICIARIO) });
                                        }
                                        if (Reporte.FIANZASIS.Any())
                                        {
                                            item.Parametros.Add(new ParametrosReportes() { Nombre = "rpPoliza", Valor = Reporte.FIANZASIS.First().NO_FIANZA_SIS.ToString() });
                                            item.Parametros.Add(new ParametrosReportes() { Nombre = "rpEndoso", Valor = Reporte.FIANZASIS.First().NO_ENDOSO_SIS.ToString() });
                                        }
                                        bytesLocal = this.ReporteComplementoBrasil(Reporte, item);
                                        BytesReportePolizaToDocumentStorage(item.Reporte.ToString(), documentoId, bytesLocal, PersonalizaReporte.MostrarMarcaAgua, Utilerias.Enums.TipoDocumentoStorage.POLAT);
                                        //}
                                        reporteConcatenado = (reporteConcatenado == null) ? bytesLocal : ConcatenarReportes(reporteConcatenado, bytesLocal);
                                        #endregion
                                        break;
                                }
                            }
                        }
                        byte[] resultBytes = null;
                        if (reporteConcatenado != null)
                            resultBytes = reporteConcatenado;
                        if (resultBytes != null)
                        {
                            result.Datos = PersonalizaReporte.MostrarMarcaAgua ? AgregarMarcaAgua(resultBytes, PersonalizaReporte.TextoMarcaAgua) : resultBytes;
                        }
                    }
                    else if (Reporte.Pais.Equals("BrasilBoleto"))
                    {
                        #region Procesamiento Boletos BRasil

                        string nombreBanco = Reporte.PARCELAS.First().DSP_TIPO_BANCO;
                        decimal numMoneda = Reporte.DATOSFIANZA.First().NO_MONEDA;
                        CodigoBarraBoleto parametrosCodigoBarra = new CodigoBarraBoleto()
                        {
                            MonoedaId_1 = "9",
                            BancoId_3 = Reporte.PARCELAS.First().TIPO_BANCO == null ? "000" : Reporte.PARCELAS.First().TIPO_BANCO,
                            FactorVencimiento_4 = CalcularFactorVencimiento(Reporte.PARCELAS[Convert.ToInt32(Reporte.Parametros.Find(x => x.Nombre == "rpNumeroParcela").Valor) - (ParcelasShenaRPPA ? 2 : 1)].FECHA_PAGO).ToString(),
                            ValorTitulo_10 = string.Format("{0:0000000000}", Convert.ToDecimal((Reporte.PARCELAS[Convert.ToInt32(Reporte.Parametros.Find(x => x.Nombre == "rpNumeroParcela").Valor) - (ParcelasShenaRPPA ? 2 : 1)].TOTAL * 100).ToString())),
                            NossoNumero = Reporte.PARCELAS[Convert.ToInt32(Reporte.Parametros.Find(x => x.Nombre == "rpNumeroParcela").Valor) - (ParcelasShenaRPPA ? 2 : 1)].NO_BOLETO.ToString(),
                            CodigoProducto_1 = "4",
                            IOFR_2 = string.Format("{0:00}", Convert.ToInt32("90")),
                            CuentaCosmo_10 = "0495130011",
                            Cartera_3 = Reporte.PARCELAS[Convert.ToInt32(Reporte.Parametros.Find(x => x.Nombre == "rpNumeroParcela").Valor) - (ParcelasShenaRPPA ? 2 : 1)].NO_CARTERA.ToString(),
                            MonedaPoliza = numMoneda
                        };

                        var resultado = ConstruirCodigoBarra(parametrosCodigoBarra, Reporte.DATOSFIANZA[0].CVE_OFICINA, Reporte.Cultura);
                        reportViewer.LocalReport.SetParameters(new ReportParameter("CodBarraCAE", Convert.ToBase64String(ObtenerBytesCodigoBarra(resultado.Codigo.NumeroBarra))));
                        reportViewer.LocalReport.SetParameters(new ReportParameter("rpDigitoCodigoBarra", resultado.Codigo.NumeroBarraDigito));
                        reportViewer.LocalReport.SetParameters(new ReportParameter("rpSerieBoleto", resultado.Codigo.RepresentacionNumerica));
                        reportViewer.LocalReport.SetParameters(new ReportParameter("rpNumeroBoleto", resultado.Codigo.NossoNumero));
                        reportViewer.LocalReport.SetParameters(new ReportParameter("rpDigitoNumeroBoleto", resultado.Codigo.NossoNumeroDigito));
                        reportViewer.LocalReport.SetParameters(new ReportParameter("rpNombreBanco", nombreBanco));
                        reportViewer.LocalReport.SetParameters(new ReportParameter("rpClaveBanco", resultado.Codigo.BancoId));
                        reportViewer.LocalReport.SetParameters(new ReportParameter("rpClaveBancoDigito", resultado.Codigo.BancoDigito));
                        reportViewer.LocalReport.SetParameters(new ReportParameter("rpClaveBancoDigito", resultado.Codigo.BancoDigito));
                        reportViewer.LocalReport.SetParameters(new ReportParameter("rpCodigoCedente", resultado.Codigo.CodigoCedente));
                        reportViewer.LocalReport.SetParameters(new ReportParameter("rpCodigoCedenteDigito", resultado.Codigo.CodigoCedenteDigito));
                        reportViewer.LocalReport.SetParameters(new ReportParameter("rpAgencia", resultado.Codigo.Agencia));
                        reportViewer.LocalReport.SetParameters(new ReportParameter("rpAgenciaDigito", resultado.Codigo.AgenciaDigito));
                        reportViewer.LocalReport.SetParameters(new ReportParameter("rpInstrucciones", resultado.Codigo.Instrucciones));
                        reportViewer.LocalReport.SetParameters(new ReportParameter("rpCartera", resultado.Codigo.Cartera));
                        result.Datos = reportViewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

                        #endregion
                    }
                    #endregion
                    #region "Colombia"
                    else if (Reporte.Pais.Equals("Colombia"))
                    {

                        //Aqui va un código igual o mas culero!!!

                    }
                    #endregion
                }
                catch (Exception ex)
                { throw ex; }
                #endregion
            }
            else
            {
                byte[] datosReporte = null;
                #region "Reportes HTML"
                switch (Reporte.Cultura)
                {
                    case "es-CO":
                        #region Reporte HTML Colombia
                        //Pendiente mejora
                        string certificadoHtml = System.IO.File.ReadAllText(Reporte.RutaReporte);
                        Area area = PersonalizaReporte.AreaRectangle;
                        area.Y = 0.5f;
                        string firmaDefault = "<img src=\"data:image/gif;base64,iVBORw0KGgoAAAANSUhEUgAAAKgAAAA2CAIAAADcRUXQAAAABGdBTUEAALGPC/xhBQAAAAlwSFlzAAAOxAAADsQBlSsOGwAAFVJJREFUeF7t23eMVlW3BnBsX0g06iUaE2OwRqPG3mPBXhANKkQxGk0sqKBiRVTsKChYAAVbBGygWGIXbLEFEBVr1GCsiGLv/Xp/c54z55vLwDRfGBN5/thZZ+211l5l77X3O2KHvxbhX4lFhf+XYlHh/6VYVPh / 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Gn0mCkiuaRmzOmASy1hJVmCTPEQ3BjO2EN6nHTp0sDvPOOMML5IYrHxrOWpfeH4krf/5z3/ee+89CcW0DzKrNekESyyxROphVgpSP64D9QgTQOMkpwx+//33PqvSJn3ZAZFhLUSFJAVhtx122GEem3zLcqYq4OQdOmrUKDlFHH/88RStaF0gQMzqpoxhovNpJCCc+Jaq5yUxFxhhlm60KvgERqBkzQcJWYp69uwpqKhUvrUctS+8+BPbMsssIxE+4xymM3fIIYc89dRTb7311j777MN1YtVTpa4CRQA4tGIHrZwiFDBrAo5NkqqCyUI+8X0iSj/qwUjE/C5PcnHmShMOIxa69dZbH3rooYcffphv7vipU6eaIsBCZDgAaVqVKYuaIpOdx2fAtGhhvoSpeBiHqdPFDBFrc6k0BgGR2qBPPPFEFQWioluI2hdeXoT94Ycf6kgSwVEQ6pVXXpm/g/I73nft2lVyk9Mq7LFjx55//vlmq7+YJstuDbNkwrQKmziZJYywLk4EKuAwrkNecskltLIzcDIb4Bup25r333//Mcccs+22206bNm277bbjHqSuVhcRWC6lle74aXWzZMz6NIXfuBg4ffr0sfWreBHx2RStKsD5gSckTz311PjcZtS+8N988w3vX3nllV133ZWLApMO/M6dOydCHNlB+72k7YsWEjbmFVdcMWDAgNjBufTSSwmnqBtuuKGf2qbcbeTxkzspJqlOaDuGQEMQs+L06dNtKZ8UjSQRFaizzNq999579913b7HFFlakuMsuu5j1ZqYS94yffvqpCyscZeAYWILDmB4HAwcODJ8uTgUyNroo1l577examSGT/CAIsBnh+YGiV6fdmcJTCb+1WCAnXtYcrwsvvDCF4euYMWP8UpILgeX/szE1efLkNdZYw+Ua742YCi93LNDyat1777133nln/N13310nsJnIM8Jgly5dHnzwQZI+8U844YQdd9yRZNwIshDoN5LFGatkuYZIGYyuAws9+uijaFqe0KrCfkbMiy66SD848MADTzzxRBcWJjCYNnb66aefcsopvXr1EmnVXSpQ33fffYmxEK8ohmiIUno+YPOII45gqtouGRsH1TRaV3gL8MzaWW+ekGs+6eqff/55Pgl74aPxHVYZEbxREsXgPOXTSFKxnRiH2EbJZXHOOedY0U9wS5u67LLLCPsx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\" alt=\"Base64 encoded image\" width=\"150\" height=\"60\" />";
                        string firmaDIAN = "<br/><br/><p style=\"text - align:left;\">___________________________________________</p><p style=\"text-align:left;\">@NombreFirmante</p>";
                        firmaDIAN = firmaDIAN.Replace("@NombreFirmante", Reporte.NombreFirmante);
                        firmaDIAN = firmaDIAN + "<p style=\"text-align:left;\">No. Documento:" + Reporte.NumeroDocumentDIAN + "</p>";
                        certificadoHtml = certificadoHtml.Replace("@NumPoliza", Reporte.FIANZASIS.Count() == 0 ? "-" : string.Format("{0}/{1}", Reporte.FIANZASIS.First().NO_FIANZA_SIS.ToString(), Reporte.FIANZASIS.First().NO_ENDOSO_SIS));
                        certificadoHtml = certificadoHtml.Replace("@Afianzado", Reporte.DESCRIPCION.First().NOMBRE_FIADO);
                        certificadoHtml = certificadoHtml.Replace("@AseguradoBeneficiario", Reporte.DESCRIPCION.First().NOMBRE_ASEGURADO);
                        certificadoHtml = certificadoHtml.Replace("@Fecha", DateTime.Now.ToString("d' de 'MMMM' de 'yyyy"));
                        certificadoHtml = certificadoHtml.Replace("@Expedicion", DateTime.Now.ToString("yyyy/MM/dd"));
                        certificadoHtml = certificadoHtml.Replace("@Firmante", string.IsNullOrEmpty(Reporte.NombreFirmante) ? firmaDefault : firmaDIAN);
                        datosReporte = ConvertirHTMLEnPDF(
                               new PersonalizaReporte
                               {
                                   Html = certificadoHtml,
                                   MostrarMarcaAgua = PersonalizaReporte.MostrarMarcaAgua,
                                   RutaImagenHeader = PersonalizaReporte.RutaImagenHeader,
                                   RutaTipoLetra = PersonalizaReporte.RutaTipoLetra,
                                   TextoHeader = PersonalizaReporte.TextoHeader,
                                   TextoHeaderTam = PersonalizaReporte.TextoHeaderTam,
                                   TextoMarcaAgua = PersonalizaReporte.TextoMarcaAgua,
                                   TextoFooter = PersonalizaReporte.TextoFooter,
                                   MostrarTextoFooter = PersonalizaReporte.MostrarTextoFooter,
                                   MostrarTextoHeader = PersonalizaReporte.MostrarTextoHeader,
                                   Pais = Reporte.Pais,
                                   AreaRectangle = area
                               });
                        result.Datos = datosReporte;
                        #endregion
                        break;
                    case "es-CL":
                        //Pendiente mejora
                        #region "Reporte HTML Chile"

                        //Aqui va un código igual o mas culero!!!

                        #endregion
                        break;
                }
                #endregion
            }

            return result;
        }

        void CargarDataSourcePolizaLATAM(PolizaModelo Reporte, ref ReportViewer reportViewer) { }

        List<ComplementosPolizaBR> OrdenPolizaBrasil(List<ComplementosPolizaBR> origen) { return new List<ComplementosPolizaBR>(); }

        byte[] BytesReportePolizaFromDocumentStorage(string reporteId, Utilerias.Enums.TipoDocumentoStorage tipoDocumento) { return new byte[0]; }

        byte[] ConcatenarReportes(byte[] ReporteOriginal, byte[] ReporteConcatenar) { return new byte[0]; }

        EmisionNuevos ConsultarDatosAdicionalesFianza(EmisionNuevos emision) { return new EmisionNuevos(); }

        string ObtieneRutaComplementoBrasil(string ruta) { return string.Empty; }

        string ObtieneRutaComplementoBrasil17(string ruta) { return string.Empty; }

        void BytesReportePolizaToDocumentStorage(string nombreDocumento, string reporteId, byte[] bytes, bool marcaAgua, Utilerias.Enums.TipoDocumentoStorage tipoDocumento) { }

        byte[] ReporteComplementoBrasil(PolizaModelo Reporte, ComplementosPolizaBR complementosPoliza, Utilerias.Enums.Paises pais = Utilerias.Enums.Paises.Brasil) { return new byte[0]; }

        byte[] ConvertirHTMLEnPDF(PersonalizaReporte PersonalizaReporte) { return new byte[0]; }

        byte[] AgregarClausulasAEmision(byte[] ReporteOrdenEmision, PersonalizaReporte PersonalizaReporte) { return new byte[0]; }

        List<PremioBrasil> ConsultarPremioBrasil(PremioBrasil param) { return new List<PremioBrasil>(); }

        byte[] AgregarMarcaAgua(byte[] reporteFuente, string textoMarcaAgua) { return new byte[0]; }

        public int CalcularFactorVencimiento(DateTime fecha) { return 0; }

        CodigoBarraBoleto ConstruirCodigoBarra(CodigoBarraBoleto param, string P_OFICINA, string culture) { return new CodigoBarraBoleto(); }

        byte[] ObtenerBytesCodigoBarra(string strEntrada) { return new byte[0]; }
    }
}