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
    public class cdemostrativacomisio
    {
        public byte[] generademostrativacomisio(PolizaModelo Reporte, PersonalizaReporte PersonalizaReporte, ComplementosPolizaBR item, string documentoId)
        {
            Implementations clsImplementations = new Implementations();

            byte[] comisionBytes = null;
            byte[] bytesLocal = null;
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

                bytesLocal = clsImplementations.ReporteComplementoBrasil(repBroker, item, Utilerias.Enums.Paises.Brasil);
                if (comisionBytes == null)
                {
                    comisionBytes = bytesLocal;
                }
                else
                {
                    comisionBytes = clsImplementations.ConcatenarReportes(comisionBytes, bytesLocal);
                }
            }
            bytesLocal = comisionBytes;
            clsImplementations.BytesReportePolizaToDocumentStorage(item.Reporte.ToString(), documentoId, bytesLocal, PersonalizaReporte.MostrarMarcaAgua, Utilerias.Enums.TipoDocumentoStorage.POLAT);
            return bytesLocal;
        }
    }
}
