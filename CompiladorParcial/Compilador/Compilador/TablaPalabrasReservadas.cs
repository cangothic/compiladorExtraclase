using Compilador.Analisis_Lexico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.Tabla_de_simbolos
{
    public class TablaPalabrasReservadas
    {
        private static TablaPalabrasReservadas INSTANCIA = new TablaPalabrasReservadas();
        private Dictionary<string, ComponenteLexico> mapaPalabrasReservadas = new Dictionary<string, ComponenteLexico>();
        //public TablaPalabrasReservadas() { }
        public static TablaPalabrasReservadas obtenerInstancia()
        {
            return INSTANCIA;
        }


        public ComponenteLexico obtenerPalabraReservada(string clave)
        {
            ComponenteLexico retorno = new ComponenteLexico();

            if (clave != null && clave.Trim() != "" && mapaPalabrasReservadas.ContainsKey(clave))
            {
                retorno = (ComponenteLexico)mapaPalabrasReservadas[clave].Clone();
            }
            return retorno;
        }

        private TablaPalabrasReservadas()
        {
            mapaPalabrasReservadas.Add("PARA", new ComponenteLexico("PARA", "PARA", true));
            mapaPalabrasReservadas.Add("DESDE", new ComponenteLexico("DESDE", "DESDE", true));
            mapaPalabrasReservadas.Add("HASTA", new ComponenteLexico("HASTA", "HASTA", true));
            mapaPalabrasReservadas.Add("EN", new ComponenteLexico("EN", "EN", true));
            mapaPalabrasReservadas.Add("INCREMENTOS", new ComponenteLexico("INCREMENTOS", "INCREMENTOS", true));
            mapaPalabrasReservadas.Add("DE", new ComponenteLexico("DE", "DE", true));
            mapaPalabrasReservadas.Add("CADA", new ComponenteLexico("CADA", "CADA", true));
            mapaPalabrasReservadas.Add("PASO", new ComponenteLexico("PASO", "PASO", true));
            mapaPalabrasReservadas.Add("HACER", new ComponenteLexico("HACER", "HACER", true));
            mapaPalabrasReservadas.Add("FIN", new ComponenteLexico("FIN", "FIN", true));
            mapaPalabrasReservadas.Add("ESCRIBIR", new ComponenteLexico("ESCRIBIR", "ESCRIBIR", true));
            mapaPalabrasReservadas.Add("LEER", new ComponenteLexico("LEER", "LEER", true));
        }
    }
}
