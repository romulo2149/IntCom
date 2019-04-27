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
        private Dictionary<string, VariableEstructura> estructuraSimbolos;
        private Simbolo simbolo;
        private Estructura estructura1, estructura2;
        private VariableEstructura variableEstructura;
        private Campo campo;
        private Form1 form;
        private int contadorEstructuras = 0;
        private AnalizaExpresion aex;

        
        public ProyectoSemantico(Form1 form, AnalizaExpresion aex)
        {
            TablaSimbolos = new Dictionary<string, Simbolo>();
            EstructuraSimbolos = new Dictionary<string, VariableEstructura>();
            Form = form;
            Aex = aex;
            estructura1 = new Estructura();
            estructura2 = new Estructura();
        }

        public void nuevoSimbolo(string nombreSimbolo, string tipoSimbolo, string valorSimbolo, string alcanceSimbolo,
                                    int longitudSimbolo, int lineaSimbolo)
        {
            if (alcanceSimbolo == estructura1.NombreEstructura || alcanceSimbolo == estructura2.NombreEstructura)
            {
                System.Console.Out.WriteLine("Se agrego la variable: " + nombreSimbolo);
                campo = new Campo(nombreSimbolo, valorSimbolo, tipoSimbolo, alcanceSimbolo);
                if(alcanceSimbolo == estructura1.NombreEstructura)
                {
                    estructura1.ListaCampos.Add(campo);
                }
                else if(alcanceSimbolo == estructura2.NombreEstructura)
                {
                    estructura2.ListaCampos.Add(campo);
                }
            }
            else
            {
                if(esVar(tipoSimbolo))
                {
                    if(existeVar(nombreSimbolo) == false)
                    {
                        System.Console.Out.WriteLine("Se agrego la variable tipo estructura: " + nombreSimbolo);
                        variableEstructura = new VariableEstructura(tipoSimbolo, nombreSimbolo, valorSimbolo, lineaSimbolo, alcanceSimbolo);
                        if(estructura1.NombreEstructura == tipoSimbolo)
                        {
                            int cuenta = estructura1.ListaCampos.Count;
                            if(cuenta == 1)
                            {
                                variableEstructura.Campo1 = estructura1.ListaCampos[0];
                                variableEstructura.Campo1.EstructuraPadre = nombreSimbolo;
                            }
                            if (cuenta == 2)
                            {
                                variableEstructura.Campo1 = estructura1.ListaCampos[0];
                                variableEstructura.Campo2 = estructura1.ListaCampos[1];
                                variableEstructura.Campo1.EstructuraPadre = nombreSimbolo;
                                variableEstructura.Campo2.EstructuraPadre = nombreSimbolo;
                            }
                            if (cuenta == 3)
                            {
                                variableEstructura.Campo1 = estructura1.ListaCampos[0];
                                variableEstructura.Campo2 = estructura1.ListaCampos[1];
                                variableEstructura.Campo3 = estructura1.ListaCampos[2];
                                variableEstructura.Campo1.EstructuraPadre = nombreSimbolo;
                                variableEstructura.Campo2.EstructuraPadre = nombreSimbolo;
                                variableEstructura.Campo3.EstructuraPadre = nombreSimbolo;
                            }
                        }
                        else if(estructura2.NombreEstructura == tipoSimbolo)
                        {
                            int cuenta = estructura2.ListaCampos.Count;
                            if (cuenta == 1)
                            {
                                variableEstructura.Campo1 = estructura2.ListaCampos[0];
                                variableEstructura.Campo1.EstructuraPadre = nombreSimbolo;
                            }
                            if (cuenta == 2)
                            {
                                variableEstructura.Campo1 = estructura2.ListaCampos[0];
                                variableEstructura.Campo2 = estructura2.ListaCampos[1];
                                variableEstructura.Campo1.EstructuraPadre = nombreSimbolo;
                                variableEstructura.Campo2.EstructuraPadre = nombreSimbolo;
                            }
                            if (cuenta == 3)
                            {
                                variableEstructura.Campo1 = estructura2.ListaCampos[0];
                                variableEstructura.Campo2 = estructura2.ListaCampos[1];
                                variableEstructura.Campo3 = estructura2.ListaCampos[2];
                                variableEstructura.Campo1.EstructuraPadre = nombreSimbolo;
                                variableEstructura.Campo2.EstructuraPadre = nombreSimbolo;
                                variableEstructura.Campo3.EstructuraPadre = nombreSimbolo;
                            }
                        }
                        EstructuraSimbolos.Add(nombreSimbolo, variableEstructura);
                    }
                }
                else if(tipoValido(tipoSimbolo))
                {
                    if (existeSimbolo(nombreSimbolo) == false)
                    {
                        if (tipoSimbolo == "ESTRUCTURA")
                        {
                            if (contadorEstructuras == 0)
                            {
                                estructura1.NombreEstructura = nombreSimbolo;
                                contadorEstructuras++;
                            }
                            else if (contadorEstructuras == 1)
                            {
                                estructura2.NombreEstructura = nombreSimbolo;
                            }
                        }
                        System.Console.Out.WriteLine("Se agrego la variable: " + nombreSimbolo);
                        simbolo = new Simbolo(tipoSimbolo, nombreSimbolo, valorSimbolo, alcanceSimbolo, lineaSimbolo);
                        TablaSimbolos.Add(nombreSimbolo, simbolo);
                    }
                    else
                    {
                        form.Consola1.Text += "consola> Error semántico, la variable " + nombreSimbolo + " (línea " + lineaSimbolo + ") ya fue declarada anteriormente \n";
                    }
                }
                else
                {
                    form.Consola1.Text += "consola> Error semántico, el tipo de la variable " + nombreSimbolo + " (línea " + lineaSimbolo + ") no es una estructura \n";
                }
            }
        }

        public bool esVar(string tipo)
        {
            bool bandera = false;
            if (tipo == estructura1.NombreEstructura ||tipo == estructura2.NombreEstructura)
            {
                bandera = true;
            }
            return bandera;
        }

        public bool tipoValido(string tipo)
        {
            bool bandera = false;
            if(tipo == "ESTRUCTURA" || tipo == "ENTERO" || tipo == "APUNTADOR" || tipo == "CHAR" || tipo == "ID")
            {
                bandera = true;
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

        public bool existeVar(string nombreSimbolo)
        {
            if (EstructuraSimbolos.ContainsKey(nombreSimbolo))
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
            if(tipoExpresion(t) > 12)
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
            
            if (res[0].Tipo == "ID" && res[1].Tipo == "OP_ASIGNACION" && esEnteroID(res[2].Tipo) && res[3].Tipo == "S_PUNTOCOMA")
            {
                respuesta = 0;
            }
            else if (res[0].Tipo == "ID" && res[1].Tipo == "OP_ASIGNACION" && res[2].Tipo == "PR_NULL" && res[3].Tipo == "S_PUNTOCOMA")
            {
                respuesta = 3;
            }
            else if (res[0].Tipo == "ID" && res[1].Tipo == "OP_ASIGNACION" && res[2].Tipo == "OP_RESTA" && res[3].Tipo == "ENTERO" && res[4].Tipo == "S_PUNTOCOMA")
            {
                respuesta = 9;
            } 
            else if (res[0].Tipo == "ID" && res[1].Tipo == "OP_ASIGNACION" && esEnteroID(res[2].Tipo) && esOperador(res[3].Tipo) == true && esEnteroID(res[4].Tipo) && res[5].Tipo == "S_PUNTOCOMA")
            {
                respuesta = 1; // x = x + x
            }
            else if (res[0].Tipo == "ID" && res[1].Tipo == "OP_ASIGNACION" && res[2].Tipo == "ID" && res[3].Tipo == "S_PUNTO" && res[4].Tipo == "ID" && res[5].Tipo == "S_PUNTOCOMA")
            {
                respuesta = 2;
            }
            else if (res[0].Tipo == "ID" && res[1].Tipo == "S_PUNTO" && res[2].Tipo == "ID" && res[3].Tipo == "OP_ASIGNACION" && esEnteroID(res[4].Tipo) && res[5].Tipo == "S_PUNTOCOMA")
            {
                respuesta = 4;
            }
            else if (res[0].Tipo == "ID" && res[1].Tipo == "S_PUNTO" && res[2].Tipo == "ID" && res[3].Tipo == "OP_ASIGNACION" && res[4].Tipo == "PR_NULL" && res[5].Tipo == "S_PUNTOCOMA")
            {
                respuesta = 7;
            }
            else if (res[0].Tipo == "ID" && res[1].Tipo == "OP_ASIGNACION" && res[2].Tipo == "S_COMILLA" && res[3].Tipo == "ID" && res[4].Tipo == "S_COMILLA" && res[5].Tipo == "S_PUNTOCOMA")
            {
                respuesta = 8;
            }
            else if (res[0].Tipo == "ID" && res[1].Tipo == "S_PUNTO" && res[2].Tipo == "ID" && res[3].Tipo == "OP_ASIGNACION" && esEnteroID(res[4].Tipo) && esOperador(res[5].Tipo) && esEnteroID(res[6].Tipo) && res[7].Tipo == "S_PUNTOCOMA")
            {
                respuesta = 5;
            }
            else if (res[0].Tipo == "ID" && res[1].Tipo == "S_PUNTO" && res[2].Tipo == "ID" && res[3].Tipo == "OP_ASIGNACION" && res[4].Tipo == "ID" && res[5].Tipo == "S_PUNTO" && res[6].Tipo == "ID" && res[7].Tipo == "S_PUNTOCOMA")
            {
                respuesta = 6;
            }
            else if (res[0].Tipo == "ID" && res[1].Tipo == "S_PUNTO" && res[2].Tipo == "ID" && res[3].Tipo == "OP_ASIGNACION" && res[4].Tipo == "ID" && res[5].Tipo == "S_PUNTO" 
            && res[6].Tipo == "ID" && esOperador(res[7].Tipo) == true && res[8].Tipo == "ID" && res[9].Tipo == "S_PUNTO" && res[10].Tipo == "ID" && res[11].Tipo == "S_PUNTOCOMA")
            {  // x.x = x.x + x.x
                respuesta = 10;
            }
            else if (res[0].Tipo == "ID" && res[1].Tipo == "S_PUNTO" && res[2].Tipo == "ID" && res[3].Tipo == "OP_ASIGNACION" && res[4].Tipo == "ID" && res[5].Tipo == "S_PUNTO"
            && res[6].Tipo == "ID" && esOperador(res[7].Tipo) == true && res[8].Tipo == "ID" && res[9].Tipo == "S_PUNTOCOMA")
            {  // x.x = x.x + x
                respuesta = 11;
            }
            else if (res[0].Tipo == "ID" && res[1].Tipo == "S_PUNTO" && res[2].Tipo == "ID" && res[3].Tipo == "OP_ASIGNACION" && res[4].Tipo == "ID"
                && esOperador(res[5].Tipo) == true && res[6].Tipo == "ID" && res[7].Tipo == "S_PUNTO" && res[8].Tipo == "ID" && res[9].Tipo == "S_PUNTOCOMA")
            {  // x.x = x + x.x
                respuesta = 12;
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
        internal Dictionary<string, VariableEstructura> EstructuraSimbolos { get => estructuraSimbolos; set => estructuraSimbolos = value; }
    }
}
