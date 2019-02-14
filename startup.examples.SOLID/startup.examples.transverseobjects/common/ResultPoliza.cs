using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace startup.examples.transverseobjects.common
{
    public class ResultPoliza
    {
        public byte[] Datos { get; set; }
    }

    public class PersonalizaReporte
    {
        public string Pais { get; set; }
        public string RutaImagenHeader { get; set; }
        public string TextoFooter { get; set; }
        public bool MostrarMarcaAgua { get; set; }
        public string Html { get; set; }
        public string TextoMarcaAgua { get; set; }
        public string RutaTipoLetra { get; set; }
        public string TextoHeader { get; set; }
        public string TextoHeaderTam { get; set; }
        public string MostrarTextoFooter { get; set; }
        public string MostrarTextoHeader { get; set; }
        public Area AreaRectangle { get; set; }
    }

    public class Area
    {
        public Area()
        {
            X = 0.5f;
            Y = 1.5f;
            Width = 7;
            Height = 10;
        }
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
    }
}