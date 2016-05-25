using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.Analisis_Lexico
{
    public class ComponenteLexico : ICloneable
    {
        public string lexema { get; set; }
        public string categoria { get; set; }
        public int numLinea { get; set; }
        public int posicionInicial { get; set; }
        public int posicionFinal { get; set; }
        public bool esReservada { get; set; }
        public ComponenteLexico(string lexema, string categoria, bool esReservada)
        {
            this.lexema = lexema;
            this.categoria = categoria;
            this.esReservada = esReservada;
        }
        public ComponenteLexico()
        {

        }
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
