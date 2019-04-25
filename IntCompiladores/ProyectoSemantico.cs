using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntCompiladores
{
    class ProyectoSemantico
    {
        private Dictionary<string, Simbolo> tablaSimbolos;
        private Simbolo simbolo;
        private Form1 form;
        private AnalizaExpresion aex;

        
        public ProyectoSemantico(Form1 form, AnalizaExpresion aex)
        {
            TablaSimbolos = new Dictionary<string, Simbolo>();
            Form = form;
            Aex = aex;
        }

        public void nuevoSimbolo(string nombreSimbolo, string tipoSimbolo, string valorSimbolo, string alcanceSimbolo,
                                    int longitudSimbolo, int lineaSimbolo)
        {
            if (esCampo(alcanceSimbolo))
            {
                System.Console.Out.WriteLine("Se agrego la variable: " + nombreSimbolo);
                simbolo = new Simbolo(tipoSimbolo, nombreSimbolo, valorSimbolo, alcanceSimbolo, lineaSimbolo);
                TablaSimbolos.Add(nombreSimbolo + "_" + alcanceSimbolo, simbolo);
            }
            else
            {
                if (existeSimbolo(nombreSimbolo) == false)
                {
                    System.Console.Out.WriteLine("Se agrego la variable: " + nombreSimbolo);
                    simbolo = new Simbolo(tipoSimbolo, nombreSimbolo, valorSimbolo, alcanceSimbolo, lineaSimbolo);
                    TablaSimbolos.Add(nombreSimbolo, simbolo);
                }
                else
                {
                    form.Consola1.Text += "consola> Error semántico, la variable " + nombreSimbolo + " (línea " + lineaSimbolo + ") ya fue declarada anteriormente \n";
                }
            }
        }

        public bool esCampo(string alcance)
        {
            bool bandera = false;
            if (tablaSimbolos.ContainsKey(alcance))
            {
                var obj = tablaSimbolos[alcance];
                if (obj.Tipo == "ESTRUCTURA")
                {
                    bandera = true;
                }
            }
            return bandera;
        }

        public bool existeSimbolo(string nombreSimbolo)
        {
            if(TablaSimbolos.ContainsKey(nombreSimbolo))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool expresionValida(List<Token> t)
        {
            for(int i = 0; i < t.Count; i++)
            {
                System.Console.Out.WriteLine("Lexema " + t[i].Lexema);
            }
            analizaExpresion(t, tipoExpresion(t));
            if(tipoExpresion(t) > 9)
            {
                System.Console.Out.WriteLine("Expresión mal formada");
                return false;
            }
            else
            {
                System.Console.Out.WriteLine("Bien formada, es una expresion de tipo: " + tipoExpresion(t));
                return true;
            }
            
        }

        public void analizaExpresion(List<Token> t, int tipoExpresion)
        {
            switch (tipoExpresion)
            {
                case 0:
                    if (tablaSimbolos.ContainsKey(t[0].Lexema))
                    {
                        var obj = tablaSimbolos[t[0].Lexema];
                        if (obj.Alcance == "CONSTANTES")
                        {
                            form.Consola1.Text += "consola> Error semántico, la variable " + t[0].Lexema + " (línea " + t[0].Linea + ") es una constante, su valor no puede cambiar \n";
                        }
                        else if (tablaSimbolos.ContainsKey(obj.Tipo))
                        {
                            form.Consola1.Text += "consola> Error semántico, la variable " + t[0].Lexema + " (línea " + t[0].Linea + ") es una variable de tipo estructura, no se le puede asignar un valor directo \n";
                        }
                    }
                    else if (variableExiste(t[0].Lexema) == false)
                    {
                        if (esEntero(t[2].Lexema, t[2].Tipo))
                        {
                                simbolo = new Simbolo("ENTERO", t[0].Lexema, getValor(t[2]), "INSTRUCCION", t[0].Linea);
                                TablaSimbolos.Add(t[0].Lexema, simbolo);
                        }
                        else
                        {
                            form.Consola1.Text += "consola> Error semántico en expresión, " + t[2].Lexema + " (línea " + t[2].Linea + ") no es un número o una variable de tipo entero. \n";
                        }
                    }
                    break;
                case 1:
                    if(tablaSimbolos.ContainsKey(t[0].Lexema))
                    {
                        var obj = tablaSimbolos[t[0].Lexema];
                        if(obj.Alcance == "CONSTANTES")
                        {
                            form.Consola1.Text += "consola> Error semántico, la variable " + t[0].Lexema + " (línea " + t[0].Linea + ") es una constante, su valor no puede cambiar \n";
                        }
                        else if(tablaSimbolos.ContainsKey(obj.Tipo))
                        {
                            form.Consola1.Text += "consola> Error semántico, la variable " + t[0].Lexema + " (línea " + t[0].Linea + ") es una variable de tipo estructura, no se le puede asignar un valor directo \n";
                        }
                    }
                    else if (variableExiste(t[0].Lexema) == false)
                    {
                        if (esEntero(t[2].Lexema, t[2].Tipo))
                        {
                            if(esEntero(t[4].Lexema, t[4].Tipo))
                            {
                                simbolo = new Simbolo("ENTERO", t[0].Lexema, hacerOperacion(Convert.ToInt32(getValor(t[2])), t[3], Convert.ToInt32(getValor(t[4]))), "INSTRUCCION", t[0].Linea);
                                TablaSimbolos.Add(t[0].Lexema, simbolo);
                            }
                            else
                            {
                                form.Consola1.Text += "consola> Error semántico en expresión, " + t[4].Lexema + " (línea " + t[4].Linea + ") no es un número o una variable de tipo entero. \n";
                            }
                        }
                        else
                        {
                            form.Consola1.Text += "consola> Error semántico en expresión, " + t[2].Lexema + " (línea " + t[2].Linea + ") no es un número o una variable de tipo entero. \n";
                        }
                    }
                    break;
                case 3:
                    break;

            }
        }

        public bool variableExiste(string variable)
        {
            if(tablaSimbolos.ContainsKey(variable))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string getTipo(string variable)
        {
            var obj = tablaSimbolos[variable];
            if(obj.Tipo == "ENTERO")
            {
                return "ENTERO";
            }
            else
            {
                return "CHAR";
            }
        }

        public int tipoExpresion(List<Token> res)
        {
            int respuesta = 40;
            switch(res.Count - 1)
            {
                case 3:
                    if (res[0].Tipo == "ID" && res[1].Tipo == "OP_ASIGNACION" && esEnteroID(res[2].Tipo))
                    {
                        respuesta = 0;
                    }
                    else if (res[0].Tipo == "ID" && res[1].Tipo == "OP_ASIGNACION" && res[2].Tipo == "PR_NULL")
                    {
                        respuesta = 3;
                    }
                    else
                    {
                        respuesta = 100;
                    }
                    break;
                case 4:
                    if (res[0].Tipo == "ID" && res[1].Tipo == "OP_ASIGNACION" && res[2].Tipo == "OP_RESTA" && res[3].Tipo == "ENTERO")
                    {
                        respuesta = 9;
                    }
                    else
                    {
                        respuesta = 100;
                    }
                    break;
                case 5:
                    if (res[0].Tipo == "ID" && res[1].Tipo == "OP_ASIGNACION" && esEnteroID(res[2].Tipo) && esOperador(res[3].Tipo) == true && esEnteroID(res[4].Tipo))
                    {
                        respuesta = 1; // x = x + x
                    }
                    else if (res[0].Tipo == "ID" && res[1].Tipo == "OP_ASIGNACION" && res[2].Tipo == "ID" && res[3].Tipo == "S_PUNTO" && res[4].Tipo == "ID")
                    {
                        respuesta = 2;
                    }
                    else if (res[0].Tipo == "ID" && res[1].Tipo == "S_PUNTO" && res[2].Tipo == "ID" && res[3].Tipo == "OP_ASIGNACION" && esEnteroID(res[4].Tipo))
                    {
                        respuesta = 4;
                    }
                    else if (res[0].Tipo == "ID" && res[1].Tipo == "S_PUNTO" && res[2].Tipo == "ID" && res[3].Tipo == "OP_ASIGNACION" && res[4].Tipo == "PR_NULL")
                    {
                        respuesta = 7;
                    }
                    else if (res[0].Tipo == "ID" && res[1].Tipo == "OP_ASIGNACION" && res[2].Tipo == "S_COMILLA" && res[3].Tipo == "ID" && res[4].Tipo == "S_COMILLA")
                    {
                        respuesta = 8;
                    }
                    else
                    {
                        respuesta = 100;
                    }
                    break;
                case 7:
                    if (res[0].Tipo == "ID" && res[1].Tipo == "S_PUNTO" && res[2].Tipo == "ID" && res[3].Tipo == "OP_ASIGNACION" && esEnteroID(res[4].Tipo) && esOperador(res[5].Tipo) && esEnteroID(res[6].Tipo))
                    {
                        respuesta = 5;
                    }
                    else if (res[0].Tipo == "ID" && res[1].Tipo == "S_PUNTO" && res[2].Tipo == "ID" && res[3].Tipo == "OP_ASIGNACION" && res[4].Tipo == "ID" && res[5].Tipo == "S_PUNTO" && res[6].Tipo == "ID")
                    {
                        respuesta = 6;
                    }
                    else
                    {
                        respuesta = 100;
                    }
                    break;
                default:
                    respuesta = 100;
                    break;

            }
            
            return respuesta;
        }

        public bool esOperador(string operador)
        {
            bool bandera = false;
            switch(operador)
            {
                case "OP_SUMA":
                    bandera = true;
                    break;
                case "OP_RESTA":
                    bandera = true;
                    break;
                case "OP_MULTIPLICACION":
                    bandera = true;
                    break;
                case "OP_DIVISION":
                    bandera = true;
                    break;
                case "OP_MOD":
                    bandera = true;
                    break;
                default:
                    break;
            }
            return bandera;
        }

        public bool esEnteroID(string tipo)
        {
            if(tipo == "ENTERO")
            {
                return true;
            }
            else if(tipo == "ID")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool esEntero(string lexema, string tipo)
        {
            bool bandera = false;
            if (tipo == "ENTERO")
            {
                bandera = true;
            }
            else if (tipo == "ID")
            {
                if(tablaSimbolos.ContainsKey(lexema))
                {
                    var obj = tablaSimbolos[lexema];
                    if(obj.Tipo == "ENTERO")
                    {
                        bandera = true;
                    }
                }
            }
            else
            {
                bandera = false;
            }
            return bandera;
        }

        public string hacerOperacion(int izq, Token operador, int der)
        {
            int res = 0;
            switch(operador.Tipo)
            {
                case "OP_SUMA":
                    res = izq + der;
                    break;
                case "OP_RESTA":
                    res = izq - der;
                    break;
                case "OP_MULTIPLICACION":
                    res = izq * der;
                    break;
                case "OP_DIVISION":
                    double d = Math.Round(Convert.ToDouble(izq)/ Convert.ToDouble(der));
                    res = Convert.ToInt32(d);
                    break;
                case "OP_MOD":
                    double d2 = Math.Round(Convert.ToDouble(izq) % Convert.ToDouble(der));
                    res = Convert.ToInt32(d2);
                    break;
            }
            return Convert.ToString(res);
        }

        public string getValor(Token t)
        {
            string valor = "";
            if (t.Tipo == "ENTERO")
            {
                valor = t.Lexema;
            }
            else if (t.Tipo == "ID")
            {
                if (tablaSimbolos.ContainsKey(t.Lexema))
                {
                    var obj = tablaSimbolos[t.Lexema];
                    if (obj.Tipo == "ENTERO")
                    {
                        valor = obj.Valor;
                    }
                }
            }
            return valor;
        }

        public Form1 Form { get => form; set => form = value; }
        internal Dictionary<string, Simbolo> TablaSimbolos { get => tablaSimbolos; set => tablaSimbolos = value; }
        internal AnalizaExpresion Aex { get => aex; set => aex = value; }
    }
}
