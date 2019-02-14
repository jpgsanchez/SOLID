using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace startup.examples.transverseobjects.common
{
    public class PolizaModelo : ReporteBusqueda
    {
        public List<CotizacionDescrip> DESCRIPCION { get; set; }
        public List<DatosFianza> DATOSFIANZA { get; set; }
        public List<CotizacionDetalle> DETALLE { get; set; }
        public List<FianzaSIS> FIANZASIS { get; set; }
        public List<EmisoresRep> EMISORES { get; set; }
        public List<Solicitud_Caja> SOLICITUD_CAJA { get; set; }
        public List<CotizacionProducto> PRODUCTOS { get; set; }
        public List<CotizacionCoaseguro> COASEGURADOS { get; set; }
        public List<Parcelas> PARCELAS { get; set; }
        public List<ComplementosPolizaBR> COMPLEMENTOS { get; set; }
        public string Pais { get; set; }
        public Boletos detalleBoleto { get; set; }
        public List<DatosEmisorPoliza> DATOSEMISORES { get; set; }
        public List<byte[]> DocumentosAdjuntos { get; set; }
        public List<AnexoPoliza> ANEXOS { get; set; }
        public List<Totales> TOTALES { get; set; }
        public List<EmisionNuevos> DATOSNUEVOS { get; set; }
        public List<ParcelasComision> PARCELASCOMISIONES { get; set; }
        public EmisionMotivo EMISIONMOTIVOS { get; set; }
        public Gerencia datosGerencia { get; set; }
        public string ClausulasEspeciales { get; set; }
        public List<CoberturasProducto> PRODUCTOSMOVIMIENTO { get; set; }
        public List<PremioBrasil> PREMIOBRASIL { get; set; }

        public List<Coaseguros> COASEGUROS_COMP { get; set; }
        public string CoaseguroTexto { get; set; }
        public string Cultura { get; set; }
        public List<DatosSucursal> DATOS_SUCURSAL { get; set; }
        public string NO_PAGE_HTML { get; set; }
        public string NO_POLIZA_DEV { get; set; }
        public bool InDocumentStorage { get; set; }
        public string NombreFirmante { get; set; }
        public string NumeroDocumentDIAN { get; set; }
        public decimal Fianza { get; set; }
        public decimal Inclusion { get; set; }
        public decimal Movimiento { get; set; }
    }
    public class Boletos
    {
        public string identificacionBanco { get; set; }
        public string moneda { get; set; }
        public string factorVencimiento { get; set; }
        public string valor { get; set; }
        public string codigoProducto { get; set; }
        public string iofr { get; set; }
        public string cuentaCosmo { get; set; }
    }

    public class Gerencia
    {
        public string nombreCompleto { get; set; }
        public string firmaDigital { get; set; }
        public string puesto { get; set; }
    }

    public class DatosSucursal { }
    public class Coaseguros
    {
        public string NO_MONEDA { get; set; }
        public string COAS_NOMBRE { get; set; }
        public string FIANZA_COASEGURO { get; set; }

    }
    public class PremioBrasil
    {
        public decimal P_NO_FIANZA { get; set; }
        public decimal P_NO_INCLUSION { get; set; }
        public decimal P_NO_MOVIMIENTO { get; set; }
        public decimal P_MONTO_PREMIO { get; set; }
    }
    public class CoberturasProducto { }
    public class EmisionMotivo { }
    public class CotizacionDescrip
    {
        public string DSP_ASEG_COL { get; set; }
        public string RFC_ASEG { get; set; }
        public string CIUDAD_TOMADOR_2 { get; set; }
        public string MFZA_NO_FIANZA { get; set; }
        public string MFZA_NO_INCLUSION { get; set; }
        public string MFZA_NO_MOVIMIENTO { get; set; }
        public string BENEFICIARIO_SIS { get; set; }
        public string NOMBRE_BENEFICIARIO { get; set; }
        public string NOMBRE_FIADO { get; set; }
        public string NOMBRE_ASEGURADO { get; set; }
    }
    public class DatosFianza
    {
        public string dir_aseg { get; set; }
        public DateTime vig_al { get; set; }
        public DateTime vig_del { get; set; }
        public string CVE_RAMO { get; set; }
        public string DSP_RAMO { get; set; }
        public decimal NO_MONEDA { get; set; }
        public string CVE_OFICINA { get; set; }
    }
    public class CotizacionDetalle
    {
        public string TXFZ_TEXTO { get; set; }
    }
    public class FianzaSIS
    {
        public string NO_FIANZA_SIS { get; set; }
        public string NO_ENDOSO_SIS { get; set; }
        public string ID_DOCUMENTACION { get; set; }
    }
    public class EmisoresRep
    {
        public int EXFL_NO_EMISOR { get; set; }
    }
    public class Solicitud_Caja { }
    public class CotizacionProducto { }
    public class CotizacionCoaseguro
    {
        public string COFI_NO_COASEGURADOR { get; set; }
        public string COAS_NOMBRE { get; set; }
        public string COFI_PCT_PARTICIPACION { get; set; }
        public string MVCO_PCT_COMISION { get; set; }
        public string COFI_VALOR_ASEGURADO { get; set; }
    }
    public class Parcelas
    {
        public int NO_PARCIALIDAD { get; set; }
        public DateTime FECHA_PAGO { get; set; }
        public int TOTAL { get; set; }
        public string DSP_TIPO_BANCO { get; set; }
        public decimal NO_MONEDA { get; set; }
        public string TIPO_BANCO { get; set; }
        public decimal NO_BOLETO { get; set; }
        public string NO_CARTERA { get; set; }
    }
    public class ComplementosPolizaBR
    {
        public ReportesComplementosBR Reporte { get; set; }

        public string Ruta { get; set; }

        public List<ParametrosReportes> Parametros { get; set; }
    }
    public class DatosEmisorPoliza { }
    public class AnexoPoliza
    {
        public decimal PRIMA_TOTAL { get; set; }
        public string TIPO_DOCUMENTO { get; set; }
    }
    public class Totales { }
    public class EmisionNuevos
    {
        public string P_NO_FIANZA { get; set; }
        public string P_NO_INCLUSION { get; set; }
        public string P_NO_MOVIMIENTO { get; set; }
        public int P_CVE_DETALLE { get; set; }
        public string RETURN_VALUE { get; set; }
    }
    public class ParcelasComision
    {
        public int AGENTE { get; set; }
    }

    public class CodigoBarraBoleto
    {
        public string MonoedaId_1 { get; set; }
        public string BancoId_3 { get; set; }
        public string FactorVencimiento_4 { get; set; }
        public string ValorTitulo_10 { get; set; }
        public string NossoNumero { get; set; }
        public string CodigoProducto_1 { get; set; }
        public string IOFR_2 { get; set; }
        public string CuentaCosmo_10 { get; set; }
        public string Cartera_3 { get; set; }
        public decimal MonedaPoliza { get; set; }

        public CodigoBarra Codigo { get; set; }
    }

    public class CodigoBarra
    {
        public string NumeroBarra { get; set; }
        public string NumeroBarraDigito { get; set; }
        public string RepresentacionNumerica { get; set; }
        public string NossoNumero { get; set; }
        public string NossoNumeroDigito { get; set; }
        public string BancoId { get; set; }
        public string BancoDigito { get; set; }
        public string CodigoCedente { get; set; }
        public string CodigoCedenteDigito { get; set; }
        public string Agencia { get; set; }
        public string AgenciaDigito { get; set; }
        public string Instrucciones { get; set; }
        public string Cartera { get; set; }
    }
}