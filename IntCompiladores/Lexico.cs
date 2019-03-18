using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IntCompiladores
{
    class Lexico
    {
        private List<string> conjuntoEstados;
        private List<char> alfabeto;
        private List<Transicion> transiciones;
        private string estadoInicial;
        private List<string> estadosFinales;
        private List<string> nombresTokens;
        private List<string> palabrasReservadas;
        private string input;
        Token token;
        RespuestaLexico res;
        public int linea = 1;
        public int apuntador = 0;

        public Lexico(List<string> conjuntoEstados, List<char> alfabeto, List<Transicion> transiciones, string estadoInicial, List<string> estadosFinales, List<string> nombresTokens, List<string> palabrasReservadas, string input)
        {
            this.ConjuntoEstados = conjuntoEstados;
            this.Alfabeto = alfabeto;
            this.Transiciones = transiciones;
            this.EstadoInicial = estadoInicial;
            this.EstadosFinales = estadosFinales;
            this.NombresTokens = nombresTokens;
            this.PalabrasReservadas = palabrasReservadas;
            this.Input = input;
        }

        public List<string> ConjuntoEstados { get => conjuntoEstados; set => conjuntoEstados = value; }
        public List<char> Alfabeto { get => alfabeto; set => alfabeto = value; }
        public string EstadoInicial { get => estadoInicial; set => estadoInicial = value; }
        public List<string> EstadosFinales { get => estadosFinales; set => estadosFinales = value; }
        public List<string> NombresTokens { get => nombresTokens; set => nombresTokens = value; }
        public List<string> PalabrasReservadas { get => palabrasReservadas; set => palabrasReservadas = value; }
        public string Input { get => input; set => input = value; }
        internal List<Transicion> Transiciones { get => transiciones; set => transiciones = value; }

        public RespuestaLexico Analiza()
        {
            string estadoActual = estadoInicial;
            char caracter;
            Regex esLetra = new Regex("[a-zA-Z]");
            Regex esNumero = new Regex("[0-9]");
            string lexema = "";
            string tipo = "no entro a los if";
            int error = 0;
            for(int i = apuntador; i < this.input.Length; i++)
            {
                caracter = this.input[i];
                if (esLetra.IsMatch(caracter.ToString()))  //checa si es letra 
                {
                    caracter = 'l';
                }
                if (esNumero.IsMatch(caracter.ToString()))  // checa si es numero
                {
                    caracter = 'd';
                }
                if (caracter.ToString() == ",") // checa si es coma
                {
                    caracter = 'c';
                }
                Transicion normal = Transiciones.Find(t => t.EstadoInicial == estadoActual
                                            && t.Simbolo == caracter);  // busca si existe una transicion con el estado actual y el simbolo
                Transicion retroceso = Transiciones.Find(t => t.EstadoInicial == estadoActual
                                            && t.Simbolo == 'o');  // busca si existe un caracter de retroceso

                if (normal != null) //si existe una transicion
                {
                    lexema = lexema + this.input[i];   //acumula los caracteres hasta encontrar un estado final o retroceso
                    if (EstadosFinales.Contains(normal.EstadoFinal))
                    {
                        if(PalabrasReservadas.Contains(lexema))
                        {
                            tipo = "Palabra Reservada";
                        }
                        else
                        {
                            tipo = TipoToken(Convert.ToInt32(normal.EstadoFinal));
                        }
                        error = 0;
                        apuntador = i + 1;
                        break;
                    }
                    else if ((i + 1) >= input.Length)
                    {
                        Transicion retroceso2 = Transiciones.Find(t => t.EstadoInicial == normal.EstadoFinal
                                            && t.Simbolo == 'o');
                        if (PalabrasReservadas.Contains(lexema))
                        {
                            tipo = "Palabra Reservada";

                        }
                        else
                        {
                            tipo = TipoToken(Convert.ToInt32(retroceso2.EstadoFinal));
                        }
                        error = 0;
                        apuntador = i + 1;
                        break;
                    }
                    else
                    {
                        estadoActual = normal.EstadoFinal;
                    }
                }
                else if (retroceso != null) // si existe caracater de retroceso
                {
                    if (PalabrasReservadas.Contains(lexema))
                    {
                        tipo = "Palabra Reservada";

                    }
                    else
                    {
                        tipo = TipoToken(Convert.ToInt32(retroceso.EstadoFinal));
                    }
                    error = 0;
                    apuntador = i;
                    break;
                }
                else // no pertenece al alfabeto o no hay transicion
                {
                    if (this.input[i] != '\n' && this.input[i] != '\t' && this.input[i] != ' ') //ignora espacios y tabulaciones como errores
                    {
                        if (Alfabeto.Contains(caracter))
                        {
                            tipo = "ERROR";
                            error = 1;
                            apuntador = i + 1;
                        }
                        else
                        {
                            tipo = "ERROR";
                            error = 2;
                            apuntador = i + 1;
                        }
                        break;
                    }
                }
                if (this.input[i] == '\n') //contador de lineas
                {
                    linea++;
                }
            }
            token = new Token(lexema, linea, tipo, error);
            res = new RespuestaLexico(linea, apuntador, token);
            return res;
        }

        public String devuelveLetra(int index)
        {
            return this.input[index].ToString();
        }

        public string TipoToken(int valor)
        {
            for (int i = 0; i < nombresTokens.Count; i++)
            {
                int index = nombresTokens[i].IndexOf(':');
                string izq = nombresTokens[i].Substring(0, index);
                string der = nombresTokens[i].Substring(index + 1, (nombresTokens[i].Length) - (index + 1));

                if (Convert.ToInt32(izq) == valor)
                {
                    return der;
                }
            }
            return "";
        }
    }
}

