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
    public class cboleto
    {
        public byte[] generaboleto(PolizaModelo Reporte, PersonalizaReporte PersonalizaReporte, ComplementosPolizaBR item)
        {
            Implementations clsImplementations = new Implementations();

            string documentoId = "";
            ResultPoliza result = new ResultPoliza();
            bool ParcelasShenaRPPA = false;
            bool reporteChubb = false;
            bool reporteChubb17 = false;
            string extensionReporte = Path.GetExtension(Reporte.RutaReporte);
            byte[] bytesLocal = null;
            byte[] reporteConcatenado = null;

            bytesLocal = null;
            var temp = Reporte.Parametros;
            Reporte.Parametros.Clear();
            var SenhaRPPA = clsImplementations.ConsultarDatosAdicionalesFianza(new EmisionNuevos()
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
                    Reporte.RutaReporte = clsImplementations.ObtieneRutaComplementoBrasil(item.Ruta);
                }
                else if (reporteChubb17)
                {
                    Reporte.RutaReporte = clsImplementations.ObtieneRutaComplementoBrasil17(item.Ruta);
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
            }
            bytesLocal = boletosBytes;
            clsImplementations.BytesReportePolizaToDocumentStorage(item.Reporte.ToString(), documentoId, bytesLocal, PersonalizaReporte.MostrarMarcaAgua, Utilerias.Enums.TipoDocumentoStorage.POLAT);
            return (reporteConcatenado == null) ? bytesLocal : clsImplementations.ConcatenarReportes(reporteConcatenado, bytesLocal);
        }
    }
}