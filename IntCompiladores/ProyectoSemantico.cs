using System;
using System.Collections.Generic;
using System.Drawing;
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
        private Grafico graf;

        
        public ProyectoSemantico(Form1 form, AnalizaExpresion aex)
        {
            TablaSimbolos = new Dictionary<string, Simbolo>();
            EstructuraSimbolos = new Dictionary<string, VariableEstructura>();
            Form = form;
            Aex = aex;
            estructura1 = new Estructura();
            estructura2 = new Estructura();
            graf = new Grafico();
            SolidBrush s = new SolidBrush(Color.White);
            Graphics g = form.Panel1.CreateGraphics();
            g.FillRectangle(s, new Rectangle(0, 0, 816, 384));
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
                                campo = new Campo(estructura1.ListaCampos[0].NombreCampo, estructura1.ListaCampos[0].ValorCampo, estructura1.ListaCampos[0].TipoCampo, nombreSimbolo);
                                variableEstructura.Campo1 = campo;
                            }
                            if (cuenta == 2)
                            {
                                campo = new Campo(estructura1.ListaCampos[0].NombreCampo, estructura1.ListaCampos[0].ValorCampo, estructura1.ListaCampos[0].TipoCampo, nombreSimbolo);
                                variableEstructura.Campo1 = campo;
                                campo = new Campo(estructura1.ListaCampos[1].NombreCampo, estructura1.ListaCampos[0].ValorCampo, estructura1.ListaCampos[0].TipoCampo, nombreSimbolo);
                                variableEstructura.Campo2 = campo;
                            }
                            if (cuenta == 3)
                            {
                                campo = new Campo(estructura1.ListaCampos[0].NombreCampo, estructura1.ListaCampos[0].ValorCampo, estructura1.ListaCampos[0].TipoCampo, nombreSimbolo);
                                variableEstructura.Campo1 = campo;
                                campo = new Campo(estructura1.ListaCampos[1].NombreCampo, estructura1.ListaCampos[1].ValorCampo, estructura1.ListaCampos[1].TipoCampo, nombreSimbolo);
                                variableEstructura.Campo2 = campo;
                                campo = new Campo(estructura1.ListaCampos[2].NombreCampo, estructura1.ListaCampos[2].ValorCampo, estructura1.ListaCampos[2].TipoCampo, nombreSimbolo);
                                variableEstructura.Campo3 = campo;
                            }
                        }
                        else if(estructura2.NombreEstructura == tipoSimbolo)
                        {
                            int cuenta = estructura1.ListaCampos.Count;
                            if (cuenta == 1)
                            {
                                campo = new Campo(estructura2.ListaCampos[0].NombreCampo, estructura2.ListaCampos[0].ValorCampo, estructura2.ListaCampos[0].TipoCampo, nombreSimbolo);
                                variableEstructura.Campo1 = campo;
                            }
                            if (cuenta == 2)
                            {
                                campo = new Campo(estructura2.ListaCampos[0].NombreCampo, estructura2.ListaCampos[0].ValorCampo, estructura2.ListaCampos[0].TipoCampo, nombreSimbolo);
                                variableEstructura.Campo1 = campo;
                                campo = new Campo(estructura2.ListaCampos[1].NombreCampo, estructura2.ListaCampos[0].ValorCampo, estructura2.ListaCampos[0].TipoCampo, nombreSimbolo);
                                variableEstructura.Campo2 = campo;
                            }
                            if (cuenta == 3)
                            {
                                campo = new Campo(estructura2.ListaCampos[0].NombreCampo, estructura2.ListaCampos[0].ValorCampo, estructura2.ListaCampos[0].TipoCampo, nombreSimbolo);
                                variableEstructura.Campo1 = campo;
                                campo = new Campo(estructura2.ListaCampos[1].NombreCampo, estructura2.ListaCampos[1].ValorCampo, estructura2.ListaCampos[1].TipoCampo, nombreSimbolo);
                                variableEstructura.Campo2 = campo;
                                campo = new Campo(estructura2.ListaCampos[2].NombreCampo, estructura2.ListaCampos[2].ValorCampo, estructura2.ListaCampos[2].TipoCampo, nombreSimbolo);
                                variableEstructura.Campo3 = campo;
                            }
                        }
                        EstructuraSimbolos.Add(nombreSimbolo, variableEstructura);
                        if (estructura1.NombreEstructura == tipoSimbolo)
                        {
                            graf.pintarEstructura(variableEstructura, form, 1);
                        }
                        else if (estructura2.NombreEstructura == tipoSimbolo)
                        {
                            graf.pintarEstructura(variableEstructura, form, 2);
                        }
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
                        else
                        {
                            if (esEntero(t[2].Lexema, t[2].Tipo))
                            {
                                if (esEntero(t[4].Lexema, t[4].Tipo))
                                {
                                    var objt = TablaSimbolos[t[0].Lexema];
                                    objt.Valor = hacerOperacion(Convert.ToInt32(getValor(t[2])), t[3], Convert.ToInt32(getValor(t[4])));
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
                    }
                    else if (variableExiste(t[0].Lexema) == false)
                    {
                        if (esEntero(t[2].Lexema, t[2].Tipo))
                        {
                            if (esEntero(t[4].Lexema, t[4].Tipo))
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
                case 4:
                    if(existeVar(t[0].Lexema))
                    {
                        if(esCampo(t[0],t[2].Lexema))
                        {
                            if(tablaSimbolos.ContainsKey(t[4].Lexema))
                            {
                                if (getTipoCampo(t[0], t[2].Lexema) == getTipo(t[4].Lexema))
                                {

                                    cambiarValorCampo(t[0], t[2].Lexema, t[4]);
                                }
                                else
                                {
                                    form.Consola1.Text += "consola> Error semántico en expresión, " + t[4].Lexema + " (línea " + t[4].Linea + "), el tipo no coincide con el tipo del campo. \n";
                                }
                            }
                            else if(esNumeroEntero(t[4]))
                            {
                                if (getTipoCampo(t[0], t[2].Lexema) == getTipoToken(t[4]))
                                {
                                    cambiarValorCampo(t[0], t[2].Lexema, t[4]);
                                }
                                else
                                {
                                    form.Consola1.Text += "consola> Error semántico en expresión, " + t[4].Lexema + " (línea " + t[4].Linea + "), el tipo no coincide con el tipo del campo. \n";
                                }
                            }
                            else
                            {
                                form.Consola1.Text += "consola> Error semántico en expresión, " + t[4].Lexema + " (línea " + t[4].Linea + "), no está declarada. \n";
                            }
                        }
                        else
                        {
                            form.Consola1.Text += "consola> Error semántico en expresión, " + t[2].Lexema + " (línea " + t[2].Linea + "), no es un campo de la variable "+ t[0].Lexema +". \n";
                        }
                    }
                    else
                    {
                        form.Consola1.Text += "consola> Error semántico en expresión, " + t[0].Lexema + " (línea " + t[0].Linea + "), no es una variable de tipo estructura. \n";
                    }
                    break;
                case 5:
                    //res[0].Tipo == "ID" && res[1].Tipo == "S_PUNTO" && res[2].Tipo == "ID" && res[3].Tipo == "OP_ASIGNACION" && esEnteroID(res[4].Tipo) && esOperador(res[5].Tipo) && esEnteroID(res[6].Tipo) && res[7].Tipo == "S_PUNTOCOMA"
                    if (existeVar(t[0].Lexema))
                    {
                        if (esCampo(t[0], t[2].Lexema))
                        {
                            if (esEntero(t[4].Lexema, t[4].Tipo))
                            {
                                if (esEntero(t[6].Lexema, t[6].Tipo))
                                {
                                    string valor = hacerOperacion(Convert.ToInt32(getValor(t[4])), t[5], Convert.ToInt32(getValor(t[6])));
                                    cambiarValorCampoOperacion(t[0], t[2].Lexema, valor);
                                }
                                else
                                {
                                    form.Consola1.Text += "consola> Error semántico en expresión, " + t[6].Lexema + " (línea " + t[6].Linea + ") no es un número o una variable de tipo entero. \n";
                                }
                            }
                            else
                            {
                                form.Consola1.Text += "consola> Error semántico en expresión, " + t[4].Lexema + " (línea " + t[4].Linea + ") no es un número o una variable de tipo entero. \n";
                            }
                        }
                        else
                        {
                            form.Consola1.Text += "consola> Error semántico en expresión, " + t[2].Lexema + " (línea " + t[2].Linea + "), no es un campo de la variable " + t[0].Lexema + ". \n";
                        }
                    }
                    else
                    {
                        form.Consola1.Text += "consola> Error semántico en expresión, " + t[0].Lexema + " (línea " + t[0].Linea + "), no es una variable de tipo estructura. \n";
                    }
                    break;

            }
        }

        public bool esNumeroEntero(Token t)
        {
            if(t.Tipo == "ENTERO")
            {
                return true;
            }
            else
            {
                return false;
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
            else if(obj.Tipo == "CHAR")
            {
                return "CHAR";
            }
            else
            {
                return "APUNTADOR";
            }
        }

        public string getTipoToken(Token t)
        {
            if (t.Tipo == "ENTERO")
            {
                return "ENTERO";
            }
            else if (t.Tipo == "CHAR")
            {
                return "CHAR";
            }
            else
            {
                return "APUNTADOR";
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
            && res[6].Tipo == "ID" && esOperador(res[7].Tipo) == true && esEnteroID(res[8].Tipo) && res[9].Tipo == "S_PUNTOCOMA")
            {  // x.x = x.x + x
                respuesta = 11;
            }
            else if (res[0].Tipo == "ID" && res[1].Tipo == "S_PUNTO" && res[2].Tipo == "ID" && res[3].Tipo == "OP_ASIGNACION" && esEnteroID(res[4].Tipo)
                && esOperador(res[5].Tipo) == true && res[6].Tipo == "ID" && res[7].Tipo == "S_PUNTO" && res[8].Tipo == "ID" && res[9].Tipo == "S_PUNTOCOMA")
            {  // x.x = x + x.x
                respuesta = 12;
            }

            return respuesta;
        }

        public string getTipoCampo(Token var, String campo)
        {

            string respuesta = "";
            if (existeVar(var.Lexema))
            {
                var obj = EstructuraSimbolos[var.Lexema];
               
                if (obj.Campo1.NombreCampo == campo)
                {
                    respuesta = obj.Campo1.TipoCampo;
                }
                else if (obj.Campo2.NombreCampo == campo)
                {
                    respuesta = obj.Campo2.TipoCampo;
                }
                else if (obj.Campo3.NombreCampo == campo)
                {
                    respuesta = obj.Campo3.TipoCampo;
                }
            }
            return respuesta;
        }

        public bool esCampo(Token var, String campo)
        {
            bool bandera = false;
            if(existeVar(var.Lexema))
            {
                var obj = EstructuraSimbolos[var.Lexema];
                if(obj.Campo1.NombreCampo == campo || obj.Campo2.NombreCampo == campo || obj.Campo3.NombreCampo == campo)
                {
                    bandera = true;
                }
            }
            return bandera;
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

        public void cambiarValorCampo(Token var, string campo, Token nuevo)
        {
            string nuevoValor = getValor(nuevo);
            if (existeVar(var.Lexema))
            {
                var obj = EstructuraSimbolos[var.Lexema];

                if (obj.Campo1.NombreCampo == campo)
                {
                    Campo editaCampo = obj.Campo1;
                    editaCampo.ValorCampo = nuevoValor;
                    EstructuraSimbolos[var.Lexema].Campo1 = editaCampo;
                }
                else if (obj.Campo2.NombreCampo == campo)
                {
                    Campo editaCampo = obj.Campo2;
                    editaCampo.ValorCampo = nuevoValor;
                    EstructuraSimbolos[var.Lexema].Campo2 = editaCampo;
                }
                else if (obj.Campo3.NombreCampo == campo)
                {
                    Campo editaCampo = obj.Campo3;
                    editaCampo.ValorCampo = nuevoValor;
                    EstructuraSimbolos[var.Lexema].Campo3 = editaCampo;
                }
            }
        }

        public void cambiarValorCampoOperacion(Token var, string campo, string nuevoValor)
        {
            if (existeVar(var.Lexema))
            {
                var obj = EstructuraSimbolos[var.Lexema];

                if (obj.Campo1.NombreCampo == campo)
                {
                    Campo editaCampo = obj.Campo1;
                    editaCampo.ValorCampo = nuevoValor;
                    EstructuraSimbolos[var.Lexema].Campo1 = editaCampo;
                }
                else if (obj.Campo2.NombreCampo == campo)
                {
                    Campo editaCampo = obj.Campo2;
                    editaCampo.ValorCampo = nuevoValor;
                    EstructuraSimbolos[var.Lexema].Campo2 = editaCampo;
                }
                else if (obj.Campo3.NombreCampo == campo)
                {
                    Campo editaCampo = obj.Campo3;
                    editaCampo.ValorCampo = nuevoValor;
                    EstructuraSimbolos[var.Lexema].Campo3 = editaCampo;
                }
            }
        }

        public Form1 Form { get => form; set => form = value; }
        internal Dictionary<string, Simbolo> TablaSimbolos { get => tablaSimbolos; set => tablaSimbolos = value; }
        internal AnalizaExpresion Aex { get => aex; set => aex = value; }
        internal Dictionary<string, VariableEstructura> EstructuraSimbolos { get => estructuraSimbolos; set => estructuraSimbolos = value; }
    }
}
