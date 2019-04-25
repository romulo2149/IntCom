using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IntCompiladores
{
    class AnalizaExpresion
    {
        private List<string> conjuntoEstados;
        private List<char> alfabeto;
        private List<Transicion> transiciones;
        private string estadoInicial;
        private List<string> estadosFinales;
        private List<string> nombresTokens;
        private List<string> palabrasReservadas;
        public string input;
        Token token;
        RespuestaLexico res;
        public int linea = 1;
        public int apuntador = 0;
        public string lexema = "";
        public char caracter;
        Regex esLetra = new Regex("[a-zA-Z]");
        Regex esNumero = new Regex("[0-9]");
        public string tipo = "f";
        int error = 0;
        public string estadoActual;
        public string lexema2;
        public int apuntador2;
        public int cuentaEspacios = 0;

        public AnalizaExpresion(List<string> conjuntoEstados, List<char> alfabeto, List<Transicion> transiciones, string estadoInicial, List<string> estadosFinales, List<string> nombresTokens, List<string> palabrasReservadas)
        {
            this.ConjuntoEstados = conjuntoEstados;
            this.Alfabeto = alfabeto;
            this.Transiciones = transiciones;
            this.EstadoInicial = estadoInicial;
            this.EstadosFinales = estadosFinales;
            this.NombresTokens = nombresTokens;
            this.PalabrasReservadas = palabrasReservadas;
            estadoActual = estadoInicial;
        }

        public List<string> ConjuntoEstados { get => conjuntoEstados; set => conjuntoEstados = value; }
        public List<char> Alfabeto { get => alfabeto; set => alfabeto = value; }
        public string EstadoInicial { get => estadoInicial; set => estadoInicial = value; }
        public List<string> EstadosFinales { get => estadosFinales; set => estadosFinales = value; }
        public List<string> NombresTokens { get => nombresTokens; set => nombresTokens = value; }
        public List<string> PalabrasReservadas { get => palabrasReservadas; set => palabrasReservadas = value; }
        public string Input { get => input; set => input = value; }
        internal List<Transicion> Transiciones { get => transiciones; set => transiciones = value; }

        
        public RespuestaLexico AnalizaRecursivo()
        {
            if (apuntador < input.Length)
            {
                if (input[apuntador] == ' ')
                {
                    cuentaEspacios++;
                }
                Transicion normal = Transiciones.Find(t => t.EstadoInicial == estadoActual
                                                && t.Simbolo == getCaracter(input[apuntador]));  // busca si existe una transicion con el estado actual y el simbolo
                Transicion retroceso = Transiciones.Find(t => t.EstadoInicial == estadoActual
                                            && t.Simbolo == 'o');  // busca si existe un caracter de retroceso
                //System.Console.Out.WriteLine(getCaracter(input[apuntador]));

                if (normal != null) //si existe una transicion
                {
                    lexema = lexema + input[apuntador];   //acumula los caracteres hasta encontrar un estado final o retroceso
                    lexema2 = lexema;
                    // System.Console.Out.WriteLine(lexema);
                    if (EstadosFinales.Contains(normal.EstadoFinal))
                    {
                        if (PalabrasReservadas.Contains(lexema))
                        {
                            if (lexema == "MOD")
                            {
                                tipo = "OP_MODULO";
                            }
                            else
                            {
                                tipo = "PR_" + lexema;
                            }
                        }
                        else
                        {
                            tipo = TipoToken(Convert.ToInt32(normal.EstadoFinal));
                        }
                        error = 0;
                        apuntador = apuntador + 1;
                        apuntador2 = apuntador;
                    }
                    else if ((apuntador + 1) >= input.Length)
                    {
                        Transicion retroceso2 = Transiciones.Find(t => t.EstadoInicial == normal.EstadoFinal
                                            && t.Simbolo == 'o');
                        if (PalabrasReservadas.Contains(lexema))
                        {
                            if (lexema == "MOD")
                            {
                                tipo = "OP_MODULO";
                            }
                            else
                            {
                                tipo = "PR_" + lexema;
                            }
                        }
                        else
                        {
                            tipo = TipoToken(Convert.ToInt32(retroceso2.EstadoFinal));
                        }
                        error = 0;
                        apuntador = apuntador + 1;
                        apuntador2 = apuntador;
                    }
                    else
                    {
                        estadoActual = normal.EstadoFinal;
                        apuntador = apuntador + 1;
                        apuntador2 = apuntador;
                        AnalizaRecursivo();
                    }
                }
                else if (retroceso != null) // si existe caracater de retroceso
                {
                    if (PalabrasReservadas.Contains(lexema))
                    {
                        if (lexema == "MOD")
                        {
                            tipo = "OP_MODULO";
                        }
                        else
                        {
                            tipo = "PR_" + lexema;
                        }
                    }
                    else
                    {
                        tipo = TipoToken(Convert.ToInt32(retroceso.EstadoFinal));
                    }
                    error = 0;
                }
                else // no pertenece al alfabeto o no hay transicion
                {
                    if (this.input[apuntador] != '\n' && this.input[apuntador] != '\t' && this.input[apuntador] != ' ') //ignora espacios y tabulaciones como errores
                    {
                        if (Alfabeto.Contains(caracter))
                        {
                            tipo = "ERROR";
                            error = 1;
                            apuntador = apuntador + 1;
                            apuntador2 = apuntador;
                        }
                        else
                        {
                            tipo = "ERROR";
                            error = 1;
                            apuntador = apuntador + 1;
                            apuntador2 = apuntador;
                        }
                    }
                    else
                    {
                        if (input[apuntador] == '\n')
                        {
                            linea++;
                        }
                        apuntador = apuntador + 1;
                        apuntador2 = apuntador;
                        lexema = "";
                        estadoActual = estadoInicial;
                        AnalizaRecursivo();

                    }

                }
            }
            estadoActual = estadoInicial;
            token = new Token(lexema2, linea, tipo, error);
            token.Apuntador = apuntador2;
            res = new RespuestaLexico(linea, apuntador2, token);
            lexema = "";
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

        public char getCaracter(char c)
        {
            if (esLetra.IsMatch(c.ToString()))  //checa si es letra 
            {
                c = 'l';
            }
            if (esNumero.IsMatch(c.ToString()))  // checa si es numero
            {
                c = 'd';
            }
            if (c.ToString() == ",") // checa si es coma
            {
                c = 'c';
            }
            return c;
        }
    }
}
