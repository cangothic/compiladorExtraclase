using Compilador.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compilador.Tabla_de_simbolos;
using Compilador.Manejador_de_errores;

namespace Compilador.Analisis_Lexico
{
    public class AnalizadorLexico
    {

        private Boolean esPrimerLlamado = true;
        private int numLineaActual = 0;
        private string contenidoLineaActual;
        private int puntero;
        private string caracterActual;
        private string lexema;
        private int estadoActual;
        private ComponenteLexico componente;

        private void cargarNuevaLinea()
        {

            numLineaActual += 1;
            if (ProgramaFuente.obtenerInstancia().getLineas().Count >= numLineaActual)
            {
                ProgramaFuente inst = ProgramaFuente.obtenerInstancia();

                contenidoLineaActual = inst.getLinea(numLineaActual).getContenido();
            }
            else {
                contenidoLineaActual = "@EOF@";
            }
            puntero = 1;

        }

        private void leerSiguienteCaracter()
        {

            if (contenidoLineaActual.Equals("@EOF@"))
            {
                caracterActual = "@EOF@";

            }
            else if (contenidoLineaActual.Length >= puntero)
            {

                caracterActual = contenidoLineaActual.Substring(puntero - 1, 1);
            }
            else {
                caracterActual = "@FL@";
            }
            puntero += 1;
        }

        private void devolverPuntero()
        {
            puntero -= 1;
        }

        private void reiniciarVariables()
        {

            caracterActual = "";
            lexema = "";
            estadoActual = 0;
        }

        public ComponenteLexico analizar()
        {
            if (esPrimerLlamado)
            {
                cargarNuevaLinea();
                esPrimerLlamado = false;
            }

            reiniciarVariables();

            Boolean continuarAnalisis = true;
            while (continuarAnalisis)
            {

                switch (estadoActual)
                {
                    case 0:
                        leerSiguienteCaracter();
                        while (caracterActual.Equals(" "))
                        {
                            leerSiguienteCaracter();
                        }
                        if (Char.IsDigit(caracterActual.ToCharArray()[0]))
                        {
                            lexema += caracterActual;
                            estadoActual = 8;
                        }
                        else if (caracterActual.Equals("Y"))
                        {
                            lexema += caracterActual;
                            estadoActual = 4;
                        }
                        else if (caracterActual.Equals("O"))
                        {
                            lexema += caracterActual;
                            estadoActual = 6;
                        }
                        else if (Char.IsLetter(caracterActual.ToCharArray()[0]))
                        {
                            lexema += caracterActual;
                            estadoActual = 1;
                        }
                        else if (caracterActual.Equals("="))
                        {
                            lexema += caracterActual;
                            estadoActual = 29;
                        }
                        else if (caracterActual.Equals("+"))
                        {
                            lexema += caracterActual;
                            estadoActual = 14;
                        }
                        else if (caracterActual.Equals("-"))
                        {
                            lexema += caracterActual;
                            estadoActual = 15;
                        }
                        else if (caracterActual.Equals("*"))
                        {
                            lexema += caracterActual;
                            estadoActual = 16;
                        }
                        else if (caracterActual.Equals("/"))
                        {
                            lexema += caracterActual;
                            estadoActual = 17;
                        }
                        else if (caracterActual.Equals("%"))
                        {
                            lexema += caracterActual;
                            estadoActual = 18;
                        }
                        else if (caracterActual.Equals(":"))
                        {
                            lexema += caracterActual;
                            estadoActual = 19;
                        }
                        else if (caracterActual.Equals(">"))
                        {
                            lexema += caracterActual;
                            estadoActual = 22;
                        }
                        else if (caracterActual.Equals("<"))
                        {
                            lexema += caracterActual;
                            estadoActual = 25;
                        }
                        else if (caracterActual.Equals("!"))
                        {
                            lexema += caracterActual;
                            estadoActual = 30;
                        }
                        else if (caracterActual.Equals("("))
                        {
                            lexema += caracterActual;
                            estadoActual = 33;
                        }
                        else if (caracterActual.Equals(")"))
                        {
                            lexema += caracterActual;
                            estadoActual = 34;
                        }
                        else if (caracterActual.Equals("@EOF@"))
                        {
                            lexema += caracterActual;
                            estadoActual = 37;
                        }
                        else if (caracterActual.Equals("@FL@"))
                        {
                            estadoActual = 36;
                        }
                        else
                        {
                            estadoActual = 2;
                        }
                        break;
                    case 1:
                        leerSiguienteCaracter();
                        if (Char.IsLetter(caracterActual.ToCharArray()[0]))
                        {
                            lexema += caracterActual;
                            estadoActual = 1;
                        }
                        else {
                            estadoActual = 3;
                        }
                        break;
                    case 3:
                        devolverPuntero();
                        componente = new ComponenteLexico();
                        componente.lexema = lexema;
                        componente.categoria = "IDENTIFICADOR";
                        componente.numLinea = numLineaActual;
                        componente.posicionInicial = puntero - lexema.Length;
                        componente.posicionFinal = puntero - 1;


                        continuarAnalisis = false;

                        break;
                    case 4:
                        leerSiguienteCaracter();
                        if (Char.IsLetter(caracterActual.ToCharArray()[0]))
                        {
                            lexema += lexema;
                            estadoActual = 1;
                        }
                        else {
                            estadoActual = 5;
                        }
                        break;
                    case 5:
                        devolverPuntero();
                        componente = new ComponenteLexico();
                        componente.lexema = lexema;
                        componente.categoria = "AND";
                        componente.numLinea = numLineaActual;
                        componente.posicionInicial = puntero - lexema.Length;
                        componente.posicionFinal = puntero - 1;

                        continuarAnalisis = false;

                        break;
                    case 6:
                        leerSiguienteCaracter();
                        if (Char.IsLetter(caracterActual.ToCharArray()[0]))
                        {
                            lexema += lexema;
                            estadoActual = 1;
                        }
                        else {
                            estadoActual = 7;
                        }
                        break;
                    case 7:
                        devolverPuntero();
                        componente = new ComponenteLexico();
                        componente.lexema = lexema;
                        componente.categoria = "OR";
                        componente.numLinea = numLineaActual;
                        componente.posicionInicial = puntero - lexema.Length;
                        componente.posicionFinal = puntero - 1;

                        continuarAnalisis = false;

                        break;
                    case 8:
                        leerSiguienteCaracter();
                        if (Char.IsDigit(caracterActual.ToCharArray()[0]))
                        {
                            lexema += caracterActual;
                            estadoActual = 8;
                        }
                        else if (caracterActual.Equals(","))
                        {
                            lexema += caracterActual;
                            estadoActual = 10;
                        }
                        else
                        {
                            estadoActual = 9;
                        }
                        break;
                    case 9:
                        devolverPuntero();
                        componente = new ComponenteLexico();
                        componente.lexema = lexema;
                        componente.categoria = "ENTERO";
                        componente.numLinea = numLineaActual;
                        componente.posicionInicial = puntero - lexema.Length;
                        componente.posicionFinal = puntero - 1;

                        continuarAnalisis = false;

                        break;
                    case 10:
                        leerSiguienteCaracter();
                        if (Char.IsDigit(caracterActual.ToCharArray()[0]))
                        {
                            lexema += caracterActual;
                            estadoActual = 11;
                        }
                        else
                        {
                            estadoActual = 13;
                        }

                        break;
                    case 11:
                        leerSiguienteCaracter();
                        if (Char.IsDigit(caracterActual.ToCharArray()[0]))
                        {
                            lexema += caracterActual;
                            estadoActual = 11;
                        }
                        else
                        {
                            estadoActual = 12;
                        }
                        break;
                    case 12:
                        devolverPuntero();
                        componente = new ComponenteLexico();
                        componente.lexema = lexema;
                        componente.categoria = "DECIMAL";
                        componente.numLinea = numLineaActual;
                        componente.posicionInicial = puntero - lexema.Length;
                        componente.posicionFinal = puntero - 1;

                        continuarAnalisis = false;

                        break;
                    case 13:
                        string numeroDecimalDummie = "999.99";
                        devolverPuntero();
                        componente = new ComponenteLexico();
                        componente.lexema = numeroDecimalDummie;
                        componente.categoria = "decimal dummy";
                        componente.numLinea = numLineaActual;
                        componente.posicionInicial = puntero - lexema.Length;
                        componente.posicionFinal = puntero - 1;
                        Error errorNumeroDecimal = new Error();
                        errorNumeroDecimal.numLinea = componente.numLinea;
                        errorNumeroDecimal.posicionFinal = componente.posicionFinal;
                        errorNumeroDecimal.posicionInicial = componente.posicionInicial;
                        errorNumeroDecimal.tipoError = "LEXICO";
                        errorNumeroDecimal.valorEsperado = "6";
                        errorNumeroDecimal.valorRecibido = caracterActual;
                        errorNumeroDecimal.descripcionError = "se esperaba un digito";
                        ManejadorErrores.obtenerInstancia().adicionarError(errorNumeroDecimal);
                        continuarAnalisis = false;
                        break;
                    case 14:
                        componente = new ComponenteLexico();
                        componente.lexema = lexema;
                        componente.categoria = "SUMA";
                        componente.numLinea = numLineaActual;
                        componente.posicionInicial = puntero - lexema.Length;
                        componente.posicionFinal = puntero - 1;

                        continuarAnalisis = false;
                        break;
                    case 15:
                        componente = new ComponenteLexico();
                        componente.lexema = lexema;
                        componente.categoria = "RESTA";
                        componente.numLinea = numLineaActual;
                        componente.posicionInicial = puntero - lexema.Length;
                        componente.posicionFinal = puntero - 1;

                        continuarAnalisis = false;
                        break;
                    case 16:
                        componente = new ComponenteLexico();
                        componente.lexema = lexema;
                        componente.categoria = "MULTIPLICACION";
                        componente.numLinea = numLineaActual;
                        componente.posicionInicial = puntero - lexema.Length;
                        componente.posicionFinal = puntero - 1;

                        continuarAnalisis = false;
                        break;
                    case 17:
                        componente = new ComponenteLexico();
                        componente.lexema = lexema;
                        componente.categoria = "DIVISION";
                        componente.numLinea = numLineaActual;
                        componente.posicionInicial = puntero - lexema.Length;
                        componente.posicionFinal = puntero - 1;

                        continuarAnalisis = false;
                        /*string numeroDEcimalDummie = "999.99";
                        devolverPuntero();
                        componente = new ComponenteLexico();
                        componente.lexema = lexema;
                        componente.categoria = numeroDEcimalDummie;
                        componente.numLinea = numLineaActual;
                        componente.posicionInicial = puntero - lexema.Length;
                        componente.posicionFinal = puntero - 1;
                        continuarAnalisis = false;
                        */
                        break;
                    case 18:
                        componente = new ComponenteLexico();
                        componente.lexema = lexema;
                        componente.categoria = "MODULO";
                        componente.numLinea = numLineaActual;
                        componente.posicionInicial = puntero - lexema.Length;
                        componente.posicionFinal = puntero - 1;

                        continuarAnalisis = false;
                        /*continuarAnalisis = false;
                        throw new Exception("ERROR FATAL");*/
                        break;
                    case 19:
                        leerSiguienteCaracter();
                        if (caracterActual.Equals("="))
                        {
                            lexema += caracterActual;
                            estadoActual = 20;
                        }
                        else
                        {
                            estadoActual = 21;
                        }
                        break;
                    case 20:
                        componente = new ComponenteLexico();
                        componente.lexema = lexema;
                        componente.categoria = "ASIGNACION";
                        componente.numLinea = numLineaActual;
                        componente.posicionInicial = puntero - lexema.Length;
                        componente.posicionFinal = puntero - 1;

                        continuarAnalisis = false;
                        break;
                    case 21:
                        string asignacionDummy = ":=";
                        devolverPuntero();
                        componente = new ComponenteLexico();
                        componente.lexema = asignacionDummy;
                        componente.categoria = "asignacion dummy";
                        componente.numLinea = numLineaActual;
                        componente.posicionInicial = puntero - lexema.Length;
                        componente.posicionFinal = puntero - 1;
                        Error errorAsignacion = new Error();
                        errorAsignacion.numLinea = componente.numLinea;
                        errorAsignacion.posicionFinal = componente.posicionFinal;
                        errorAsignacion.posicionInicial = componente.posicionInicial;
                        errorAsignacion.tipoError = "LEXICO";
                        errorAsignacion.valorEsperado = "=";
                        errorAsignacion.valorRecibido = caracterActual;
                        errorAsignacion.descripcionError = "se esperaba =";
                        ManejadorErrores.obtenerInstancia().adicionarError(errorAsignacion);
                        continuarAnalisis = false;
                        break;
                    case 22:
                        leerSiguienteCaracter();
                        if (caracterActual.Equals("="))
                        {
                            lexema += caracterActual;
                            estadoActual = 23;
                        }
                        else
                        {
                            estadoActual = 24;
                        }
                        break;
                    case 23:
                        componente = new ComponenteLexico();
                        componente.lexema = lexema;
                        componente.categoria = "MAYOR O IGUAL";
                        componente.numLinea = numLineaActual;
                        componente.posicionInicial = puntero - lexema.Length;
                        continuarAnalisis = false;
                        break;
                    case 24:
                        devolverPuntero();
                        componente = new ComponenteLexico();
                        componente.lexema = lexema;
                        componente.categoria = "MAYOR";
                        componente.numLinea = numLineaActual;
                        componente.posicionInicial = puntero - lexema.Length;
                        componente.posicionFinal = puntero - 1;
                        continuarAnalisis = false;
                        break;
                    case 25:
                        leerSiguienteCaracter();
                        if (caracterActual.Equals("="))
                        {
                            lexema += caracterActual;
                            estadoActual = 26;
                        }
                        else if (caracterActual.Equals(">"))
                        {
                            lexema += caracterActual;
                            estadoActual = 27;
                        }
                        else
                        {
                            estadoActual = 28;
                        }
                        break;
                    case 26:
                        componente = new ComponenteLexico();
                        componente.lexema = lexema;
                        componente.categoria = "MENOR O IGUAL";
                        componente.numLinea = numLineaActual;
                        componente.posicionInicial = puntero - lexema.Length;
                        componente.posicionFinal = puntero - 1;

                        continuarAnalisis = false;
                        break;
                    case 27:
                        componente = new ComponenteLexico();
                        componente.lexema = lexema;
                        componente.categoria = "DIFERENTE";
                        componente.numLinea = numLineaActual;
                        componente.posicionInicial = puntero - lexema.Length;
                        componente.posicionFinal = puntero - 1;

                        continuarAnalisis = false;
                        break;
                    case 28:
                        devolverPuntero();
                        componente = new ComponenteLexico();
                        componente.lexema = lexema;
                        componente.categoria = "MENOR";
                        componente.numLinea = numLineaActual;
                        componente.posicionInicial = puntero - lexema.Length;
                        componente.posicionFinal = puntero - 1;


                        continuarAnalisis = false;
                        break;
                    case 29:
                        componente = new ComponenteLexico();
                        componente.lexema = lexema;
                        componente.categoria = "IGUAL";
                        componente.numLinea = numLineaActual;
                        componente.posicionInicial = puntero - lexema.Length;
                        componente.posicionFinal = puntero - 1;
                        continuarAnalisis = false;
                        break;
                    case 30:
                        leerSiguienteCaracter();
                        if (caracterActual.Equals("="))
                        {
                            lexema += lexema;
                            estadoActual = 27;
                        }
                        else {
                            estadoActual = 32;
                        }
                        break;
                    case 32:
                        string diferenteDummy = "!=";
                        devolverPuntero();
                        componente = new ComponenteLexico();
                        componente.lexema = diferenteDummy;
                        componente.categoria = "diferente dummy";
                        componente.numLinea = numLineaActual;
                        componente.posicionInicial = puntero - lexema.Length;
                        componente.posicionFinal = puntero - 1;
                        Error errorDiferente = new Error();
                        errorDiferente.numLinea = componente.numLinea;
                        errorDiferente.posicionFinal = componente.posicionFinal;
                        errorDiferente.posicionInicial = componente.posicionInicial;
                        errorDiferente.tipoError = "LEXICO";
                        errorDiferente.valorEsperado = "=";
                        errorDiferente.valorRecibido = caracterActual;
                        errorDiferente.descripcionError = "se esperaba =";
                        ManejadorErrores.obtenerInstancia().adicionarError(errorDiferente);
                        continuarAnalisis = false;
                        break;
                    case 33:
                        componente = new ComponenteLexico();
                        componente.lexema = lexema;
                        componente.categoria = "PARENTESIS ABRE";
                        componente.numLinea = numLineaActual;
                        componente.posicionInicial = puntero - lexema.Length;
                        componente.posicionFinal = puntero - 1;
                        continuarAnalisis = false;
                        break;
                    case 34:
                        componente = new ComponenteLexico();
                        componente.lexema = lexema;
                        componente.categoria = "PARENTESIS CIERRE";
                        componente.numLinea = numLineaActual;
                        componente.posicionInicial = puntero - lexema.Length;
                        componente.posicionFinal = puntero - 1;
                        continuarAnalisis = false;
                        break;
                    case 36:
                        cargarNuevaLinea();
                        estadoActual = 0;
                        break;
                    case 37:
                        componente = new ComponenteLexico();
                        componente.lexema = lexema;
                        componente.categoria = "FIN DE ARCHIVO";
                        componente.numLinea = numLineaActual;
                        componente.posicionInicial = puntero - lexema.Length;
                        componente.posicionFinal = puntero - 1;
                        continuarAnalisis = false;
                        break;
                    case 2:
                        Error errorFatal = new Error();
                        errorFatal.numLinea = numLineaActual;
                        errorFatal.posicionFinal =puntero-1 ;
                        errorFatal.posicionInicial = puntero - lexema.Length;
                        errorFatal.tipoError = "LEXICO";
                        errorFatal.valorEsperado = "=";
                        errorFatal.valorRecibido = caracterActual;
                        errorFatal.descripcionError = "se esperaba cualquier valido";
                        ManejadorErrores.obtenerInstancia().adicionarError(errorFatal);
                        continuarAnalisis = false;
                        break;
                }
            }
            if (componente != null)
            {
                TablaDeSimbolos.obtenerInstancia().adicionarSimbolo(componente);
            }
            return componente;
        }
    }
}