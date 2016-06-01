using Compilador.Analisis_Lexico;
using Compilador.Manejador_de_errores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compilador.Analizador_Sintactico
{
    public class AnalizadorSintactico
    {
        private AnalizadorLexico analizadorLexico;
        private ComponenteLexico preAnalisis;
        public void Analizar()
        {
            try
            {
                analizadorLexico = new AnalizadorLexico();
                preAnalisis = analizadorLexico.analizar();//El primer componente
                ciclo();

                if ("FIN DE ARCHIVO".Equals(preAnalisis.categoria))
                {
                    if (ManejadorErrores.obtenerInstancia().existenErrores())
                    {
                        MessageBox.Show("El programa tiene errores");
                    }
                    else
                    {
                        MessageBox.Show("El programa está bien escrito");
                    }

                }
                else
                {
                    MessageBox.Show("El programa tiene errores");
                }
            }
            catch (Exception excepcion)
            {
                MessageBox.Show(excepcion.Message);
            }
        }

        private void ciclo()
        {
            if ("PARA".Equals(preAnalisis.categoria))
            {
                Avanzar(preAnalisis.categoria);
                Avanzar("PARENTESIS ABRE");
                identificador();
                Avanzar("DESDE");
                limiteinicial();
                Avanzar("HASTA");
                limitefinal();
                A();
                Avanzar("PARENTESIS CIERRA");
           
                Avanzar("HACER");
                sentencias();
                Avanzar("FIN");
                Avanzar("PARA");
            }
            else
            {
                Error error = new Error();
                error.valorRecibido = preAnalisis.lexema;
                error.posicionInicial = preAnalisis.posicionInicial;
                error.posicionFinal = preAnalisis.posicionFinal;
                error.numLinea = preAnalisis.numLinea;
                error.valorEsperado = "PARA";
                error.descripcionError = "COMPONETE RECIBIDO NO VÁLIDO, YA QUE RECIBÍ " + preAnalisis.categoria + "-->" + preAnalisis.lexema;
                error.tipoError = "SINTACTICO";
                ManejadorErrores.obtenerInstancia().adicionarError(error);
                throw new Exception("ERROR FATAL, SINTACTICO " + error.descripcionError);

            }
        }
        private void A()
        {
            if ("EN".Equals(preAnalisis.categoria))
            {
                Avanzar("EN");
                Avanzar("INCREMENTOS");
                Avanzar("DE");
                valor();
                Avanzar("EN");
                Avanzar("CADA");
                Avanzar("PASO");
            }
            //else epsilon no hace nada

        }
        private void identificador()
        {
            if ("IDENTIFICADOR".Equals(preAnalisis.categoria))
            {
                Avanzar(preAnalisis.categoria);
            }
            else
            {
                Error error = new Error();
                error.valorRecibido = preAnalisis.lexema;
                error.posicionInicial = preAnalisis.posicionInicial;
                error.posicionFinal = preAnalisis.posicionFinal;
                error.numLinea = preAnalisis.numLinea;
                error.valorEsperado = "IDENTIFICADOR";
                error.descripcionError = "COMPONETE RECIBIDO NO VÁLIDO, YA QUE RECIBÍ " + preAnalisis.categoria + "-->" + preAnalisis.lexema;
                error.tipoError = "SINTACTICO";
                ManejadorErrores.obtenerInstancia().adicionarError(error);
                throw new Exception("ERROR FATAL, SINTACTICO " + error.descripcionError);
                //preAnalisis.categoria = "IDENTIFICADOR DUMMY";
                //Avanzar(preAnalisis.categoria);
            }
        }
        private void limiteinicial()
        {
            if("ENTERO".Equals(preAnalisis.categoria) || "DECIMAL".Equals(preAnalisis.categoria))
            {
                Avanzar(preAnalisis.categoria);
            }
            else
            {
                Error error = new Error();
                error.valorRecibido = preAnalisis.lexema;
                error.posicionInicial = preAnalisis.posicionInicial;
                error.posicionFinal = preAnalisis.posicionFinal;
                error.numLinea = preAnalisis.numLinea;
                error.valorEsperado = "ENTERO | DECIMAL";
                error.descripcionError = "COMPONETE RECIBIDO NO VÁLIDO, YA QUE RECIBÍ " + preAnalisis.categoria + "-->" + preAnalisis.lexema;
                error.tipoError = "SINTACTICO";
                ManejadorErrores.obtenerInstancia().adicionarError(error);
                throw new Exception("ERROR FATAL, SINTACTICO " + error.descripcionError);
                //preAnalisis.categoria = "ENTERO DUMMY";
                //Avanzar(preAnalisis.categoria);
            }
        }
        private void limitefinal()
        {
            if ("ENTERO".Equals(preAnalisis.categoria) || "DECIMAL".Equals(preAnalisis.categoria))
            {
                Avanzar(preAnalisis.categoria);
            }
            else
            {
                Error error = new Error();
                error.valorRecibido = preAnalisis.lexema;
                error.posicionInicial = preAnalisis.posicionInicial;
                error.posicionFinal = preAnalisis.posicionFinal;
                error.numLinea = preAnalisis.numLinea;
                error.valorEsperado = "ENTERO | DECIMAL";
                error.descripcionError = "COMPONETE RECIBIDO NO VÁLIDO, YA QUE RECIBÍ " + preAnalisis.categoria + "-->" + preAnalisis.lexema;
                error.tipoError = "SINTACTICO";
                ManejadorErrores.obtenerInstancia().adicionarError(error);
                throw new Exception("ERROR FATAL, SINTACTICO " + error.descripcionError);


                //preAnalisis.categoria = "ENTERO DUMMY";
               // Avanzar(preAnalisis.categoria);
            }
        }
        private void valor()
        {
            if ("ENTERO".Equals(preAnalisis.categoria) || "DECIMAL".Equals(preAnalisis.categoria) || "IDENTIFICADOR".Equals(preAnalisis.categoria))
            {
                Avanzar(preAnalisis.categoria);
            }
            else
            {
                Error error = new Error();
                error.valorRecibido = preAnalisis.lexema;
                error.posicionInicial = preAnalisis.posicionInicial;
                error.posicionFinal = preAnalisis.posicionFinal;
                error.numLinea = preAnalisis.numLinea;
                error.valorEsperado = "ENTERO | DECIMAL | IDENTIFICADOR";
                error.descripcionError = "COMPONETE RECIBIDO NO VÁLIDO, YA QUE RECIBÍ " + preAnalisis.categoria + "-->" + preAnalisis.lexema;
                error.tipoError = "SINTACTICO";
                ManejadorErrores.obtenerInstancia().adicionarError(error);
                throw new Exception("ERROR FATAL, SINTACTICO " + error.descripcionError);

//                preAnalisis.categoria = "ENTERO DUMMY";
//                Avanzar(preAnalisis.categoria);
            }
        }
        private void sentencias()
        {
            if ("IDENTIFICADOR".Equals(preAnalisis.categoria) || "ESCRIBIR".Equals(preAnalisis.categoria) || "LEER".Equals(preAnalisis.categoria))
            {
                sentencia();
                sentenciasPrima();
            }
            else
            {
                Error error = new Error();
                error.valorRecibido = preAnalisis.lexema;
                error.posicionInicial = preAnalisis.posicionInicial;
                error.posicionFinal = preAnalisis.posicionFinal;
                error.numLinea = preAnalisis.numLinea;
                error.valorEsperado = "IDENTIFICADOR | ESCRIBIR | LEER";
                error.descripcionError = "COMPONETE RECIBIDO NO VÁLIDO, ESPERABA "+error.valorEsperado+" NO ES POSIBLE RECUPERARSE DE ESTE ERROR";
                error.tipoError = "SEMANTICO";
                ManejadorErrores.obtenerInstancia().adicionarError(error);
                throw new Exception("ERROR FATAL, SINTACTICO " + error.descripcionError);
            }
 
        }
        private void sentenciasPrima()
        {
            if ("IDENTIFICADOR".Equals(preAnalisis.categoria) || "ESCRIBIR".Equals(preAnalisis.categoria) || "LEER".Equals(preAnalisis.categoria))
            {
                sentencias();
                sentenciasPrima();
            }
            //else epsilon no hace nada            
        }
        private void sentencia()
        {
            if ("IDENTIFICADOR".Equals(preAnalisis.categoria))
            {
                Avanzar(preAnalisis.categoria);
                Avanzar("ASIGNACION");
                expresion();
                Avanzar(";");
            }
            else if ("ESCRIBIR".Equals(preAnalisis.categoria))
            {
                Avanzar(preAnalisis.categoria);
                Avanzar("PARENTESIS ABRE");
                expresion();
                Avanzar("PARENTESIS CIERRA");
                Avanzar(";");
            }
            else if ("LEER".Equals(preAnalisis.categoria))
            {
                Avanzar(preAnalisis.categoria);
                Avanzar("PARENTESIS ABRE");
                identificador();
                Avanzar("PARENTESIS CIERRA");
                Avanzar(";");
            }
            else
            {
                Error error = new Error();
                error.valorRecibido = preAnalisis.lexema;
                error.posicionInicial = preAnalisis.posicionInicial;
                error.posicionFinal = preAnalisis.posicionFinal;
                error.numLinea = preAnalisis.numLinea;
                error.valorEsperado = "IDENTIFICADOR | ESCRIBIR | LEER";
                error.descripcionError = "COMPONETE RECIBIDO NO VÁLIDO, YA QUE RECIBÍ " + preAnalisis.categoria + "-->" + preAnalisis.lexema;
                error.tipoError = "SINTACTICO";
                ManejadorErrores.obtenerInstancia().adicionarError(error);
                throw new Exception("ERROR FATAL, SINTACTICO " + error.descripcionError);

               // preAnalisis.categoria = "IDENTIFICADOR DUMMY";
               // Avanzar(preAnalisis.categoria);
               // Avanzar("ASIGNACION");
               // expresion();
               // Avanzar(";");
            }
        }
        private void expresion()
        {
            valor();
            operador();
            valor();
        }
        private void operador()
        {
            if("SUMA".Equals(preAnalisis.categoria) || "RESTA".Equals(preAnalisis.categoria) || "MULTIPLICACION".Equals(preAnalisis.categoria) || "DIVISION".Equals(preAnalisis.categoria) || "MODULO".Equals(preAnalisis.categoria) || "MENOR".Equals(preAnalisis.categoria) || "MENOR IGUAL".Equals(preAnalisis.categoria) || "MAYOR".Equals(preAnalisis.categoria) || "MAYOR IGUAL".Equals(preAnalisis.categoria) || "DIFERENTE".Equals(preAnalisis.categoria) || "IGUAL".Equals(preAnalisis.categoria) || "AND".Equals(preAnalisis.categoria) || "OR".Equals(preAnalisis.categoria))
            {
                Avanzar(preAnalisis.categoria);
            }
            else
            {
                Error error = new Error();
                error.valorRecibido = preAnalisis.lexema;
                error.posicionInicial = preAnalisis.posicionInicial;
                error.posicionFinal = preAnalisis.posicionFinal;
                error.numLinea = preAnalisis.numLinea;
                error.valorEsperado = "SUMA | RESTA | MULTIPLICACION | DIVISION | MODULO | MENOR | MENOR IGUAL | MAYOR | MAYOR IGUAL | DIFERENTE | IGUAL | AND | OR";
                error.descripcionError = "COMPONETE RECIBIDO NO VÁLIDO, YA QUE RECIBÍ " + preAnalisis.categoria + "-->" + preAnalisis.lexema;
                error.tipoError = "SINTACTICO";
                ManejadorErrores.obtenerInstancia().adicionarError(error);
                throw new Exception("ERROR FATAL, SINTACTICO " + error.descripcionError);

               // preAnalisis.categoria = "SUMA DUMMY";
               // Avanzar(preAnalisis.categoria);
            }
        }
        private void Avanzar(String categoria)
        {
            if (categoria.Equals(preAnalisis.categoria))
            {
                preAnalisis = analizadorLexico.analizar();
            }
            else
            {
                Error error = new Error();
                error.valorRecibido = preAnalisis.lexema;
                error.posicionInicial = preAnalisis.posicionInicial;
                error.posicionFinal = preAnalisis.posicionFinal;
                error.numLinea = preAnalisis.numLinea;
                error.valorEsperado = preAnalisis.categoria;
                error.descripcionError = "ESPERABA " + categoria + " PERO RECIBI " + preAnalisis.categoria;
                error.tipoError = "SINTACTICO";
                ManejadorErrores.obtenerInstancia().adicionarError(error);
                preAnalisis = analizadorLexico.analizar();
                throw new Exception("ERROR FATAL, SINTACTICO "+error.descripcionError);
            }
        }

    }
}
