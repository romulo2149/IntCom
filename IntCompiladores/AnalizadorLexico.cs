using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IntCompiladores
{
    class AnalizadorLexico
    {
        private List<string> ConjuntoEstados = new List<string>();
        private List<char> Alfabeto = new List<char>();
        private List<Transicion> Transiciones = new List<Transicion>();
        private string Q0;
        private List<string> F = new List<string>();
        private string acumula = "";
        private List<String> acumulador = new List<string>();
        private List<string> Nombres = new List<string>();
        private List<string> Reservadas = new List<string>();
        Respuesta respuesta = new Respuesta();
        SalidaALexico sal;

        public AnalizadorLexico(List<string> conjuntoEstados, List<char> alfabeto, List<Transicion> transiciones, string q0, List<string> f, List<string> nombres, List<string> reservadas)
        {
            ConjuntoEstados = conjuntoEstados;
            Alfabeto = alfabeto;
            Transiciones = transiciones;
            Q0 = q0;
            F = f;
            Nombres = nombres;
            Reservadas = reservadas;
        }

        public Respuesta Analizar(string input)
        {
            string estadoActual = Q0;
            int contadorLinea = 1;
            string toke = "";
            string carError = "";
            char caracter;
            List<SalidaALexico> listaSalida = new List<SalidaALexico>();
            for (int i = 0; i < input.Length; i++)
            {
                caracter = input[i];
                Regex rexstr = new Regex("[a-zA-Z]");
                Regex rexint = new Regex("[0-9]");

                if (rexstr.IsMatch(caracter.ToString()))  //checa si es letra 
                {
                    caracter = 'l';
                }
                if (rexint.IsMatch(caracter.ToString()))  // checa si es numero
                {
                    caracter = 'd';
                }

                Transicion normal = Transiciones.Find(t => t.EstadoInicial == estadoActual
                                            && t.Simbolo == caracter);  // busca si existe una transicion con el estado actual y el simbolo
                Transicion retroceso = Transiciones.Find(t => t.EstadoInicial == estadoActual
                                            && t.Simbolo == 'o');  // busca si existe un caracter de retroceso

                if(normal != null)
                {
                    acumula = acumula + input[i];   //acumula los caracteres hasta encontrar un estado final o retroceso
                    if (F.Contains(normal.EstadoFinal))
                    {
                        if (Reservadas.Contains(acumula))  //busca si el acumulado coincide con una palabra reservada
                        {
                            toke = "Palabra reservada";
                        }
                        else
                        {
                            toke = EstadoNombre(Convert.ToInt32(normal.EstadoFinal));
                        }
                        sal = new SalidaALexico();
                        sal.Token = toke;
                        sal.Lexema = acumula;
                        sal.Linea = contadorLinea;
                        listaSalida.Add(sal);
                        estadoActual = Q0;
                        acumula = "";
                    }
                    else if ((i + 1) >= input.Length)
                    {
                        Transicion retroceso2 = Transiciones.Find(t => t.EstadoInicial == normal.EstadoFinal
                                            && t.Simbolo == 'o');
                        if (Reservadas.Contains(acumula))
                        {
                            toke = "Palabra reservada";
                        }
                        else
                        {
                            toke = EstadoNombre(Convert.ToInt32(retroceso2.EstadoFinal));
                        }
                        sal = new SalidaALexico();
                        sal.Token = toke;
                        sal.Lexema = acumula;
                        sal.Linea = contadorLinea;
                        listaSalida.Add(sal);
                        estadoActual = Q0;
                        acumula = "";
                    }
                    else
                    {
                        estadoActual = normal.EstadoFinal;
                    }
                }
                else if(retroceso != null)
                {
                    if (Reservadas.Contains(acumula))
                    {
                        toke = "Palabra reservada";
                    }
                    else
                    {
                        toke = EstadoNombre(Convert.ToInt32(retroceso.EstadoFinal));
                    }
                    sal = new SalidaALexico();
                    sal.Token = toke;
                    sal.Lexema = acumula;
                    sal.Linea = contadorLinea;
                    listaSalida.Add(sal);
                    acumula = "";
                    estadoActual = Q0;
                    i = i - 1;
                }
                else  //token no encontrado en el alfabeto
                {
                    if (input[i] != '\n' && input[i] != '\t' && input[i] != ' ') //ignora espacios y tabulaciones como errores
                    {
                        if (Alfabeto.Contains(caracter))
                        {
                            sal = new SalidaALexico();
                            sal.Token = "Error";
                            sal.Lexema = input[i-1].ToString();
                            sal.Linea = contadorLinea;
                            sal.Error = "Se encontró un error en '" + input[i-1].ToString() + "', no existe una transición válida para el siguiente caracter";
                            listaSalida.Add(sal);
                            estadoActual = Q0;
                            i = i - 1;
                            acumula = "";
                        }
                        else
                        {
                            sal = new SalidaALexico();
                            sal.Token = "Error";
                            sal.Lexema = input[i].ToString();
                            sal.Linea = contadorLinea;
                            sal.Error = "El caracter '" + input[i].ToString() + "' no pertenece al alfabeto";
                            listaSalida.Add(sal);
                            estadoActual = Q0;
                            acumula = "";
                        }
                    }
                }
                if (input[i] == '\n') //contador de lineas
                {
                    contadorLinea++;
                }
            } //fin for

            respuesta.ListaSalida = listaSalida;
            return respuesta;
        }

        public string EstadoNombre(int valor)
        {
            for (int i = 0; i < Nombres.Count; i++)
            {
                int index = Nombres[i].IndexOf(':');
                string izq = Nombres[i].Substring(0, index);
                string der = Nombres[i].Substring(index + 1, (Nombres[i].Length) - (index + 1));

                if (Convert.ToInt32(izq) == valor)
                {
                    return der;
                }
            }
            return "";
        }

    }
}
