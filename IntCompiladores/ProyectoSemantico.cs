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
                if (tipoSimbolo == "ENTERO")
                {
                    valorSimbolo = "0";
                }
                if (tipoSimbolo == "CHAR")
                {
                    valorSimbolo = "' '";
                }
                if (tipoSimbolo == "APUNTADOR")
                {
                    valorSimbolo = "NULL";
                }
                campo = new Campo(nombreSimbolo, valorSimbolo, tipoSimbolo, alcanceSimbolo);
                if (alcanceSimbolo == estructura1.NombreEstructura)
                {
                    estructura1.ListaCampos.Add(campo);
                }
                else if (alcanceSimbolo == estructura2.NombreEstructura)
                {
                    estructura2.ListaCampos.Add(campo);
                }
            }
            else
            {
                if (esVar(tipoSimbolo))
                {
                    if (existeVar(nombreSimbolo) == false)
                    {
                        System.Console.Out.WriteLine("Se agrego la variable tipo estructura: " + nombreSimbolo);
                        variableEstructura = new VariableEstructura(tipoSimbolo, nombreSimbolo, valorSimbolo, lineaSimbolo, alcanceSimbolo);
                        if (estructura1.NombreEstructura == tipoSimbolo)
                        {
                            int cuenta = estructura1.ListaCampos.Count;
                            if (cuenta == 1)
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
                        else if (estructura2.NombreEstructura == tipoSimbolo)
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
                else if (tipoValido(tipoSimbolo))
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
                        if (tipoSimbolo == "ID")
                        {
                            if (valorSimbolo.Length == 1)
                            {
                                valorSimbolo = "'" + valorSimbolo + "'";
                                System.Console.Out.WriteLine("Se agrego la variable: " + nombreSimbolo);
                                simbolo = new Simbolo("CHAR", nombreSimbolo, valorSimbolo, alcanceSimbolo, lineaSimbolo);
                                TablaSimbolos.Add(nombreSimbolo, simbolo);
                            }
                            else
                            {
                                form.Consola1.Text += "consola> Error semántico, la variable " + nombreSimbolo + " (línea " + lineaSimbolo + ") espera un solo caracter \n";
                            }
                        }
                        else
                        {
                            System.Console.Out.WriteLine("Se agrego la variable: " + nombreSimbolo);
                            simbolo = new Simbolo(tipoSimbolo, nombreSimbolo, valorSimbolo, alcanceSimbolo, lineaSimbolo);
                            TablaSimbolos.Add(nombreSimbolo, simbolo);
                        }
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
            if (tipo == estructura1.NombreEstructura || tipo == estructura2.NombreEstructura)
            {
                bandera = true;
            }
            return bandera;
        }

        public bool tipoValido(string tipo)
        {
            bool bandera = false;
            if (tipo == "ESTRUCTURA" || tipo == "ENTERO" || tipo == "APUNTADOR" || tipo == "CHAR" || tipo == "ID")
            {
                bandera = true;
            }

            return bandera;
        }

        public bool existeSimbolo(string nombreSimbolo)
        {
            if (TablaSimbolos.ContainsKey(nombreSimbolo))
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
            for (int i = 0; i < t.Count; i++)
            {
                System.Console.Out.WriteLine("Lexema " + t[i].Lexema);
            }
            analizaExpresion(t, tipoExpresion(t));
            if (tipoExpresion(t) > 16)
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
                        else
                        {
                            if (esEntero(t[2].Lexema, t[2].Tipo))
                            {
                                var objt = TablaSimbolos[t[0].Lexema];
                                objt.Valor = getValor(t[2]);

                            }
                            else
                            {
                                form.Consola1.Text += "consola> Error semántico en expresión, " + t[2].Lexema + " (línea " + t[2].Linea + ") no es un número o una variable de tipo entero. \n";
                            }
                        }
                    }
                    else if (variableExiste(t[0].Lexema) == false && existeVar(t[0].Lexema) == false)

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
                    else
                    {
                        form.Consola1.Text += "consola> Error semántico en expresión, " + t[0].Lexema + " (línea " + t[0].Linea + ") la variable ya existe. \n";
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
                    if (existeVar(t[0].Lexema))
                    {
                        if (esCampo(t[0], t[2].Lexema))
                        {
                            if (tablaSimbolos.ContainsKey(t[4].Lexema))
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
                            else if (esNumeroEntero(t[4]))
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
                            form.Consola1.Text += "consola> Error semántico en expresión, " + t[2].Lexema + " (línea " + t[2].Linea + "), no es un campo de la variable " + t[0].Lexema + ". \n";
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
                case 6:
                    //res[0].Tipo == "ID" && res[1].Tipo == "S_PUNTO" && res[2].Tipo == "ID" && res[3].Tipo == "OP_ASIGNACION" && res[4].Tipo == "ID" && res[5].Tipo == "S_PUNTO" && res[6].Tipo == "ID" && res[7].Tipo == "S_PUNTOCOMA"
                    //"ID" "S_PUNTO" "ID" "OP_ASIGNACION" "ID" "S_PUNTO" "ID" "S_PUNTOCOMA"
                    //hola.f = d.d;
                    if (existeVar(t[0].Lexema))
                    {
                        if (esCampo(t[0], t[2].Lexema))
                        {
                            if (existeVar(t[4].Lexema))
                            {
                                if (esCampo(t[4], t[6].Lexema))
                                {

                                    if (getTipoCampo(t[0], t[2].Lexema) == "ENTERO" && getTipoCampo(t[4], t[6].Lexema) == "ENTERO")
                                    {
                                        cambiarValorCampoOperacion(t[0], t[2].Lexema, getValorCampo(t[4], t[6].Lexema));
                                    }
                                    else if (getTipoCampo(t[0], t[2].Lexema) == "APUNTADOR")
                                    {
                                        if (getTipoCampo(t[4], t[6].Lexema) == "APUNTADOR")
                                        {
                                            cambiarValorCampoOperacion(t[0], t[2].Lexema, getValorCampo(t[4], t[6].Lexema));
                                        }
                                        else
                                        {
                                            cambiarValorCampoOperacion(t[0], t[2].Lexema, t[4].Lexema + t[5].Lexema + t[6].Lexema);
                                        }
                                    }
                                    else
                                    {
                                        form.Consola1.Text += "consola> Error semántico en expresión, " + t[6].Lexema + " (línea " + t[6].Linea + "), los tipos de dato no coinciden" + t[0].Lexema + ". \n";
                                    }
                                }
                                else
                                {
                                    form.Consola1.Text += "consola> Error semántico en expresión, " + t[6].Lexema + " (línea " + t[6].Linea + "), no es un campo de la variable " + t[4].Lexema + ". \n";
                                }
                            }
                            else
                            {
                                form.Consola1.Text += "consola> Error semántico en expresión, " + t[4].Lexema + " (línea " + t[4].Linea + "), no es una variable de tipo estructura. \n";
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
                case 7:
                    //res[0].Tipo == "ID" && res[1].Tipo == "S_PUNTO" && res[2].Tipo == "ID" && res[3].Tipo == "OP_ASIGNACION" && res[4].Tipo == "PR_NULL" && res[5].Tipo == "S_PUNTOCOMA"
                    if (existeVar(t[0].Lexema))
                    {
                        if (esCampo(t[0], t[2].Lexema))
                        {
                            if (getTipoToken(t[2]) == "APUNTADOR")
                            {
                                if (t[4].Tipo == "PR_NULL")
                                {
                                    cambiarValorCampoOperacion(t[0], t[2].Lexema, "NULL");
                                }
                                else
                                {
                                    form.Consola1.Text += "consola> Error semántico en expresión, " + t[4].Lexema + " (línea " + t[4].Linea + "), se esperaba la palabra reservada NULL. \n";
                                }
                            }
                            else
                            {
                                form.Consola1.Text += "consola> Error semántico en expresión, " + t[4].Lexema + " (línea " + t[4].Linea + ") no es un apuntador. \n";
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
                case 8:
                    //res[0].Tipo == "ID" && res[1].Tipo == "OP_ASIGNACION" && res[2].Tipo == "S_COMILLA" && res[3].Tipo == "ID" && res[4].Tipo == "S_COMILLA" && res[5].Tipo == "S_PUNTOCOMA"
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
                            if (t[2].Tipo == "S_COMILLA" && t[4].Tipo == "S_COMILLA")
                            {
                                if (t[3].Lexema.Length == 1)
                                {
                                    obj = TablaSimbolos[t[0].Lexema];
                                    obj.Valor = t[2].Lexema + t[3].Lexema + t[4].Lexema;
                                }
                                else
                                {
                                    form.Consola1.Text += "consola> Error semántico, " + t[3].Lexema + " (línea " + t[3].Linea + ") tiene más de un caracter \n";
                                }
                            }
                            else
                            {

                            }
                        }
                    }
                    else if (variableExiste(t[0].Lexema) == false)
                    {
                        if (t[2].Tipo == "S_COMILLA" && t[4].Tipo == "S_COMILLA")
                        {
                            if (t[3].Lexema.Length == 1)
                            {

                                simbolo = new Simbolo("CHAR", t[0].Lexema, t[2].Lexema + t[3].Lexema + t[4].Lexema, "INSTRUCCION", t[0].Linea);
                                TablaSimbolos.Add(t[0].Lexema, simbolo);
                            }
                            else
                            {
                                form.Consola1.Text += "consola> Error semántico, " + t[3].Lexema + " (línea " + t[3].Linea + ") tiene más de un caracter \n";
                            }
                        }
                        else
                        {

                        }
                    }
                    break;
                case 10:
                    /*
                     * (res[0].Tipo == "ID" && res[1].Tipo == "S_PUNTO" && res[2].Tipo == "ID" && res[3].Tipo == "OP_ASIGNACION" && res[4].Tipo == "ID" && res[5].Tipo == "S_PUNTO" 
                         && res[6].Tipo == "ID" && esOperador(res[7].Tipo) == true && res[8].Tipo == "ID" && res[9].Tipo == "S_PUNTO" && res[10].Tipo == "ID" && res[11].Tipo == "S_PUNTOCOMA")
                     */
                    if (existeVar(t[0].Lexema))
                    {
                        if (esCampo(t[0], t[2].Lexema))
                        {
                            if (getTipoCampo(t[0], t[2].Lexema) == "ENTERO")
                            {
                                if (existeVar(t[4].Lexema))
                                {
                                    if (esCampo(t[4], t[6].Lexema))
                                    {
                                        if (existeVar(t[8].Lexema))
                                        {
                                            if (esCampo(t[8], t[10].Lexema))
                                            {
                                                if (getTipoCampo(t[4], t[6].Lexema) == "ENTERO" && getTipoCampo(t[8], t[10].Lexema) == "ENTERO")
                                                {
                                                    string izq = getValorCampo(t[4], t[6].Lexema);
                                                    string der = getValorCampo(t[8], t[10].Lexema);
                                                    string res = hacerOperacion(Convert.ToInt32(izq), t[7], Convert.ToInt32(der));
                                                    cambiarValorCampoOperacion(t[0], t[2].Lexema, res);
                                                }
                                            }
                                            else
                                            {
                                                form.Consola1.Text += "consola> Error semántico en expresión, " + t[10].Lexema + " (línea " + t[10].Linea + "), no es un campo de la variable " + t[8].Lexema + ". \n";
                                            }
                                        }
                                        else
                                        {
                                            form.Consola1.Text += "consola> Error semántico en expresión, " + t[8].Lexema + " (línea " + t[8].Linea + "), no es una variable de tipo estructura. \n";
                                        }
                                    }
                                    else
                                    {
                                        form.Consola1.Text += "consola> Error semántico en expresión, " + t[6].Lexema + " (línea " + t[6].Linea + "), no es un campo de la variable " + t[4].Lexema + ". \n";
                                    }
                                }
                                else
                                {
                                    form.Consola1.Text += "consola> Error semántico en expresión, " + t[4].Lexema + " (línea " + t[4].Linea + "), no es una variable de tipo estructura. \n";
                                }
                            }
                            else
                            {
                                form.Consola1.Text += "consola> Error semántico en expresión, " + t[2].Lexema + " (línea " + t[2].Linea + "), el campo no es de tipo entero " + t[0].Lexema + ". \n";
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
                case 11:
                    // res[0].Tipo == "ID" && res[1].Tipo == "S_PUNTO" && res[2].Tipo == "ID" && res[3].Tipo == "OP_ASIGNACION" && res[4].Tipo == "ID" && res[5].Tipo == "S_PUNTO" && res[6].Tipo == "ID" && esOperador(res[7].Tipo) == true && esEnteroID(res[8].Tipo) && res[9].Tipo == "S_PUNTOCOMA")
                    // "ID" "S_PUNTO" "ID" "OP_ASIGNACION" "ID" "S_PUNTO" "ID" esOperador esEnteroID "S_PUNTOCOMA"
                    // x.x = x.x + x
                    if (existeVar(t[0].Lexema))
                    {
                        if (esCampo(t[0], t[2].Lexema))
                        {
                            if (getTipoCampo(t[0], t[2].Lexema) == "ENTERO")
                            {
                                if (existeVar(t[4].Lexema))
                                {
                                    if (esCampo(t[4], t[6].Lexema))
                                    {
                                        if (getTipoCampo(t[4], t[6].Lexema) == "ENTERO")
                                        {


                                            if (tablaSimbolos.ContainsKey(t[8].Lexema))
                                            {

                                                if (getTipo(t[8].Lexema) == "ENTERO")
                                                {
                                                    // x.x = x.x + x
                                                    string izq = getValorCampo(t[4], t[6].Lexema);
                                                    string der = getValor(t[8]);
                                                    string respuesta = hacerOperacion(Convert.ToInt32(izq), t[7], Convert.ToInt32(der));

                                                    cambiarValorCampoOperacion(t[0], t[2].Lexema, respuesta);
                                                }
                                                else
                                                {
                                                    form.Consola1.Text += "consola> Error semántico en expresión, " + t[6].Lexema + " (línea " + t[6].Linea + "), el campo no es de tipo entero " + t[4].Lexema + ". \n";
                                                }

                                            }
                                            else
                                            {
                                                form.Consola1.Text += "consola> Error semántico en expresión, " + t[8].Lexema + " (línea " + t[8].Linea + "), la variable no existe. \n";
                                            }

                                        }
                                        else
                                        {
                                            form.Consola1.Text += "consola> Error semántico en expresión, " + t[6].Lexema + " (línea " + t[6].Linea + "), el campo no es de tipo entero " + t[4].Lexema + ". \n";
                                        }
                                    }
                                    else
                                    {
                                        form.Consola1.Text += "consola> Error semántico en expresión, " + t[6].Lexema + " (línea " + t[6].Linea + "), no es un campo de la variable " + t[4].Lexema + ". \n";
                                    }
                                }
                                else
                                {
                                    form.Consola1.Text += "consola> Error semántico en expresión, " + t[4].Lexema + " (línea " + t[4].Linea + "), no es una variable de tipo estructura. \n";
                                }
                            }
                            else
                            {
                                form.Consola1.Text += "consola> Error semántico en expresión, " + t[2].Lexema + " (línea " + t[2].Linea + "), el campo no es de tipo entero " + t[0].Lexema + ". \n";
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
                case 12:
                    // x.x = x + x.x
                    if (existeVar(t[0].Lexema))
                    {
                        if (esCampo(t[0], t[2].Lexema))
                        {
                            if (getTipoCampo(t[0], t[2].Lexema) == "ENTERO")
                            {
                                if (tablaSimbolos.ContainsKey(t[4].Lexema))
                                {
                                    if (getTipo(t[4].Lexema) == "ENTERO")
                                    {


                                        if (existeVar(t[6].Lexema))
                                        {
                                            if (esCampo(t[6], t[8].Lexema))
                                            {
                                                if (getTipoCampo(t[6], t[8].Lexema) == "ENTERO")
                                                {
                                                    string izq = getValorCampo(t[6], t[8].Lexema);
                                                    string der = getValor(t[4]);
                                                    string respuesta = hacerOperacion(Convert.ToInt32(izq), t[5], Convert.ToInt32(der));

                                                    cambiarValorCampoOperacion(t[0], t[2].Lexema, respuesta);
                                                }
                                                else
                                                {
                                                    form.Consola1.Text += "consola> Error semántico en expresión, " + t[8].Lexema + " (línea " + t[2].Linea + "), el campo no es de tipo entero " + t[8].Lexema + ". \n";
                                                }
                                            }
                                            else
                                            {
                                                form.Consola1.Text += "consola> Error semántico en expresión, " + t[2].Lexema + " (línea " + t[6].Linea + "), no es un campo de la variable " + t[8].Lexema + ". \n";
                                            }
                                        }
                                        else
                                        {
                                            form.Consola1.Text += "consola> Error semántico en expresión, " + t[6].Lexema + " (línea " + t[6].Linea + "), no es una variable de tipo estructura. \n";
                                        }




                                    }
                                    else
                                    {
                                        form.Consola1.Text += "consola> Error semántico en expresión, " + t[6].Lexema + " (línea " + t[6].Linea + "), el campo no es de tipo entero " + t[4].Lexema + ". \n";
                                    }

                                }
                                else
                                {
                                    form.Consola1.Text += "consola> Error semántico en expresión, " + t[4].Lexema + " (línea " + t[4].Linea + "), la variable no existe. \n";
                                }

                            }
                            else
                            {
                                form.Consola1.Text += "consola> Error semántico en expresión, " + t[2].Lexema + " (línea " + t[2].Linea + "), el campo no es de tipo entero " + t[0].Lexema + ". \n";
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
                case 13:
                    if (TablaSimbolos.ContainsKey(t[0].Lexema))
                    {
                        if (esCampo(t[2], t[4].Lexema))
                        {
                            if (existeVar(t[6].Lexema))
                            {
                                if (esCampo(t[6], t[8].Lexema))
                                {
                                    if (getTipoCampo(t[2], t[4].Lexema) == "ENTERO" && getTipoCampo(t[6], t[8].Lexema) == "ENTERO")
                                    {
                                        string izq = getValorCampo(t[2], t[4].Lexema);
                                        string der = getValorCampo(t[6], t[8].Lexema);
                                        System.Console.Out.WriteLine("Se va a hacer " + izq + t[7].Lexema + der);
                                        string res = hacerOperacion(Convert.ToInt32(izq), t[7], Convert.ToInt32(der));
                                        var objt = TablaSimbolos[t[0].Lexema];
                                        objt.Valor = res;
                                    }
                                    else
                                    {
                                        form.Consola1.Text += "consola> Error semántico en expresión, " + t[0].Lexema + " (línea " + t[0].Linea + "), una de los campos no es entero. \n";
                                    }
                                }
                                else
                                {
                                    form.Consola1.Text += "consola> Error semántico en expresión, " + t[8].Lexema + " (línea " + t[8].Linea + "), no es un campo de una estructura. \n";
                                }
                            }
                            else
                            {
                                form.Consola1.Text += "consola> Error semántico en expresión, " + t[6].Lexema + " (línea " + t[6].Linea + "), no es una variable de tipo estructura. \n";
                            }
                        }
                        else
                        {
                            form.Consola1.Text += "consola> Error semántico en expresión, " + t[4].Lexema + " (línea " + t[2].Linea + "), no es un campo de la estructura. \n";
                        }
                    }
                    else if (existeVar(t[0].Lexema))
                    {
                        form.Consola1.Text += "consola> Error semántico en expresión, " + t[0].Lexema + " (línea " + t[0].Linea + "), es una variable de tipo estructura, no se puede accesar de ese modo. \n";
                    }
                    else
                    {
                        if (existeVar(t[2].Lexema))
                        {
                            if (esCampo(t[2], t[4].Lexema))
                            {
                                if (existeVar(t[6].Lexema))
                                {
                                    if (esCampo(t[6], t[8].Lexema))
                                    {
                                        if (getTipoCampo(t[2], t[4].Lexema) == "ENTERO" && getTipoCampo(t[6], t[8].Lexema) == "ENTERO")
                                        {
                                            simbolo = new Simbolo("ENTERO", t[0].Lexema, hacerOperacion(Convert.ToInt32(getValorCampo(t[2], t[4].Lexema)), t[5], Convert.ToInt32(getValorCampo(t[6], t[8].Lexema))), "INSTRUCCION", t[0].Linea);
                                            TablaSimbolos.Add(t[0].Lexema, simbolo);
                                        }
                                        else
                                        {
                                            form.Consola1.Text += "consola> Error semántico en expresión, " + t[0].Lexema + " (línea " + t[0].Linea + "), una de los campos no es entero. \n";
                                        }
                                    }
                                    else
                                    {
                                        form.Consola1.Text += "consola> Error semántico en expresión, " + t[8].Lexema + " (línea " + t[8].Linea + "), no es un campo de una estructura. \n";
                                    }
                                }
                                else
                                {
                                    form.Consola1.Text += "consola> Error semántico en expresión, " + t[6].Lexema + " (línea " + t[6].Linea + "), no es una variable de tipo estructura. \n";
                                }
                            }
                            else
                            {
                                form.Consola1.Text += "consola> Error semántico en expresión, " + t[4].Lexema + " (línea " + t[2].Linea + "), no es un campo de la estructura. \n";
                            }
                        }
                        else
                        {
                            form.Consola1.Text += "consola> Error semántico en expresión, " + t[2].Lexema + " (línea " + t[0].Linea + "), no es una variable de tipo estructura. \n";
                        }
                    }
                    break;
                case 14:
                    //else if (res[0].Tipo == "ID" && res[1].Tipo == "OP_ASIGNACION" && res[2].Tipo == "ID" && res[3].Tipo == "S_PUNTO" && res[4].Tipo == "ID" &&
                    //esOperador(res[5].Tipo) == true && res[6].Tipo == "ID" && res[7].Tipo == "S_PUNTOCOMA")
                    if (TablaSimbolos.ContainsKey(t[0].Lexema))
                    {
                        if (esCampo(t[2], t[4].Lexema))
                        {
                            if (TablaSimbolos.ContainsKey(t[6].Lexema))
                            {
                                if (getTipoCampo(t[2], t[4].Lexema) == "ENTERO" && getTipo(t[6].Lexema) == "ENTERO")
                                {
                                    string izq = getValorCampo(t[2], t[4].Lexema);
                                    string der = getValor(t[6]);
                                    System.Console.Out.WriteLine("Se va a hacer " + izq + t[5].Lexema + der);
                                    string res = hacerOperacion(Convert.ToInt32(izq), t[5], Convert.ToInt32(der));
                                    var objt = TablaSimbolos[t[0].Lexema];
                                    objt.Valor = res;
                                }
                                else
                                {
                                    form.Consola1.Text += "consola> Error semántico en expresión, " + t[6].Lexema + " (línea " + t[6].Linea + "), la variable no está declarada. \n";
                                }
                            }
                            else
                            {
                                form.Consola1.Text += "consola> Error semántico en expresión, " + t[8].Lexema + " (línea " + t[8].Linea + "), no es un campo de una estructura. \n";
                            }
                        }
                        else
                        {
                            form.Consola1.Text += "consola> Error semántico en expresión, " + t[4].Lexema + " (línea " + t[2].Linea + "), no es un campo de la estructura. \n";
                        }
                    }
                    else if (existeVar(t[0].Lexema))
                    {
                        form.Consola1.Text += "consola> Error semántico en expresión, " + t[0].Lexema + " (línea " + t[0].Linea + "), es una variable de tipo estructura, no se puede accesar de ese modo. \n";
                    }
                    else
                    {
                        if (existeVar(t[2].Lexema))
                        {
                            if (esCampo(t[2], t[4].Lexema))
                            {
                                if (TablaSimbolos.ContainsKey(t[6].Lexema))
                                {
                                    if (getTipoCampo(t[2], t[4].Lexema) == "ENTERO" && getTipo(t[6].Lexema) == "ENTERO")
                                    {
                                        string izq = getValorCampo(t[2], t[4].Lexema);
                                        string der = getValor(t[6]);
                                        System.Console.Out.WriteLine("Se va a hacer " + izq + t[5].Lexema + der);
                                        string res = hacerOperacion(Convert.ToInt32(izq), t[5], Convert.ToInt32(der));
                                        simbolo = new Simbolo("ENTERO", t[0].Lexema, res, "INSTRUCCION", t[0].Linea);
                                        TablaSimbolos.Add(t[0].Lexema, simbolo);
                                    }
                                    else
                                    {
                                        form.Consola1.Text += "consola> Error semántico en expresión, " + t[6].Lexema + " (línea " + t[6].Linea + "), la variable no está declarada. \n";
                                    }
                                }
                                else
                                {
                                    form.Consola1.Text += "consola> Error semántico en expresión, " + t[8].Lexema + " (línea " + t[8].Linea + "), no es un campo de una estructura. \n";
                                }
                            }
                            else
                            {
                                form.Consola1.Text += "consola> Error semántico en expresión, " + t[4].Lexema + " (línea " + t[2].Linea + "), no es un campo de la estructura. \n";
                            }
                        }
                        else
                        {
                            form.Consola1.Text += "consola> Error semántico en expresión, " + t[2].Lexema + " (línea " + t[0].Linea + "), no es una variable de tipo estructura. \n";
                        }
                    }
                    break;
                case 15:
                    if (TablaSimbolos.ContainsKey(t[0].Lexema))
                    {
                        if (TablaSimbolos.ContainsKey(t[2].Lexema))
                        {
                            if (existeVar(t[4].Lexema))
                            {
                                if (esCampo(t[4], t[6].Lexema))
                                {
                                    string der = getValorCampo(t[6], t[8].Lexema);
                                    string izq = getValor(t[4]);
                                    System.Console.Out.WriteLine("Se va a hacer " + izq + t[3].Lexema + der);
                                    string res = hacerOperacion(Convert.ToInt32(izq), t[3], Convert.ToInt32(der));
                                    var objt = TablaSimbolos[t[0].Lexema];
                                    objt.Valor = res;
                                }
                                else
                                {
                                    form.Consola1.Text += "consola> Error semántico en expresión, " + t[8].Lexema + " (línea " + t[0].Linea + "), no es un campo de la estructura. \n";
                                }
                            }
                            else
                            {
                                form.Consola1.Text += "consola> Error semántico en expresión, " + t[6].Lexema + " (línea " + t[0].Linea + "), no es una variable de tipo estructura. \n";
                            }
                        }
                        else
                        {
                            form.Consola1.Text += "consola> Error semántico en expresión, " + t[4].Lexema + " (línea " + t[0].Linea + "), no está declarada. \n";
                        }
                    }
                    else if (existeVar(t[0].Lexema))
                    {
                        form.Consola1.Text += "consola> Error semántico en expresión, " + t[0].Lexema + " (línea " + t[0].Linea + "), es una variable de tipo estructura, no se puede accesar de ese modo. \n";
                    }
                    else
                    {
                        if (TablaSimbolos.ContainsKey(t[2].Lexema))
                        {
                            if (existeVar(t[4].Lexema))
                            {
                                if (esCampo(t[4], t[6].Lexema))
                                {
                                    string der = getValorCampo(t[4], t[6].Lexema);
                                    string izq = getValor(t[2]);
                                    System.Console.Out.WriteLine("Se va a hacer " + izq + t[3].Lexema + der);
                                    string res = hacerOperacion(Convert.ToInt32(izq), t[3], Convert.ToInt32(der));
                                    simbolo = new Simbolo("ENTERO", t[0].Lexema, res, "INSTRUCCION", t[0].Linea);
                                    TablaSimbolos.Add(t[0].Lexema, simbolo);
                                }
                                else
                                {
                                    form.Consola1.Text += "consola> Error semántico en expresión, " + t[8].Lexema + " (línea " + t[0].Linea + "), no es un campo de la estructura. \n";
                                }
                            }
                            else
                            {
                                form.Consola1.Text += "consola> Error semántico en expresión, " + t[6].Lexema + " (línea " + t[0].Linea + "), no es una variable de tipo estructura. \n";
                            }
                        }
                        else
                        {
                            form.Consola1.Text += "consola> Error semántico en expresión, " + t[4].Lexema + " (línea " + t[0].Linea + "), no está declarada. \n";
                        }
                    }
                    break;
                case 16:
                    if (existeVar(t[0].Lexema))
                    {
                        if (esCampo(t[0], t[2].Lexema))
                        {
                            if (getTipoCampo(t[0], t[2].Lexema) == "CHAR")
                            {
                                
                                if(t[5].Lexema.Length == 1)
                                {
                                    cambiarValorCampoOperacion(t[0], t[2].Lexema, "'"+t[5].Lexema+"'");
                                }
                                else
                                {
                                    form.Consola1.Text += "consola> Error semántico en expresión, " + t[5].Lexema + " (línea " + t[5].Linea + ") tiene más de un caracter. \n";
                                }
                            }
                            else
                            {
                                form.Consola1.Text += "consola> Error semántico en expresión, " + t[2].Lexema + " (línea " + t[2].Linea + "), el campo no es de tipo caracter " + t[0].Lexema + ". \n";
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
            if (t.Tipo == "ENTERO")
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
            if (tablaSimbolos.ContainsKey(variable))
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
            if (obj.Tipo == "ENTERO")
            {
                return "ENTERO";
            }
            else if (obj.Tipo == "CHAR")
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
            else if (res[0].Tipo == "ID" && res[1].Tipo == "OP_ASIGNACION" && res[2].Tipo == "ID" && res[3].Tipo == "S_PUNTO" && res[4].Tipo == "ID" &&
                    esOperador(res[5].Tipo) == true && res[6].Tipo == "ID" && res[7].Tipo == "S_PUNTO" && res[8].Tipo == "ID" && res[9].Tipo == "S_PUNTOCOMA")
            { // x = x.x + x.x
                respuesta = 13;
            }
            else if (res[0].Tipo == "ID" && res[1].Tipo == "OP_ASIGNACION" && res[2].Tipo == "ID" && res[3].Tipo == "S_PUNTO" && res[4].Tipo == "ID" &&
                    esOperador(res[5].Tipo) == true && res[6].Tipo == "ID" && res[7].Tipo == "S_PUNTOCOMA")
            { // x = x.x + x
                respuesta = 14;
            }
            else if (res[0].Tipo == "ID" && res[1].Tipo == "OP_ASIGNACION" && res[2].Tipo == "ID" && esOperador(res[3].Tipo) == true && res[4].Tipo == "ID" && res[5].Tipo == "S_PUNTO" && res[6].Tipo == "ID" && res[7].Tipo == "S_PUNTOCOMA")
            {// x = x + x.x
                respuesta = 15;
            }
            else if (res[0].Tipo == "ID" && res[1].Tipo == "S_PUNTO" && res[2].Tipo == "ID" && res[3].Tipo == "OP_ASIGNACION" && res[4].Tipo == "S_COMILLA" && res[5].Tipo == "ID" && res[6].Tipo == "S_COMILLA" && res[7].Tipo == "S_PUNTOCOMA")
            {  // x.x = x.x + x
                respuesta = 16;
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

        public string getValorCampo(Token var, String campo)
        {

            string respuesta = "";
            if (existeVar(var.Lexema))
            {
                var obj = EstructuraSimbolos[var.Lexema];

                if (obj.Campo1.NombreCampo == campo)
                {
                    respuesta = obj.Campo1.ValorCampo;
                }
                else if (obj.Campo2.NombreCampo == campo)
                {
                    respuesta = obj.Campo2.ValorCampo;
                }
                else if (obj.Campo3.NombreCampo == campo)
                {
                    respuesta = obj.Campo3.ValorCampo;
                }
            }
            return respuesta;
        }

        public bool esCampo(Token var, String campo)
        {
            bool bandera = false;
            if (existeVar(var.Lexema))
            {
                var obj = EstructuraSimbolos[var.Lexema];
                if (obj.Campo1.NombreCampo == campo || obj.Campo2.NombreCampo == campo || obj.Campo3.NombreCampo == campo)
                {
                    bandera = true;
                }
            }
            return bandera;
        }

        public bool esOperador(string operador)
        {
            bool bandera = false;
            switch (operador)
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

        public bool esOperadorCondicion(string operador)
        { //11:OP_IGUAL,13:OP_MENOR,14:OP_MENORIGUAL,16:OP_MAYOR,17:OP_MAYORIGUAL,19:OP_DIFERENTE
            bool bandera = false;
            switch (operador)
            {
                case "OP_IGUAL":
                    bandera = true;
                    break;
                case "OP_MENOR":
                    bandera = true;
                    break;
                case "OP_MENORIGUAL":
                    bandera = true;
                    break;
                case "OP_MAYOR":
                    bandera = true;
                    break;
                case "OP_MAYORIGUAL":
                    bandera = true;
                    break;
                case "OP_DIFERENTE":
                    bandera = true;
                    break;
                default:
                    break;
            }
            return bandera;
        }

        public bool esEnteroID(string tipo)
        {
            if (tipo == "ENTERO")
            {
                return true;
            }
            else if (tipo == "ID")
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
                if (tablaSimbolos.ContainsKey(lexema))
                {
                    var obj = tablaSimbolos[lexema];
                    if (obj.Tipo == "ENTERO")
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
            switch (operador.Tipo)
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
                    double d = Math.Round(Convert.ToDouble(izq) / Convert.ToDouble(der));
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
                    else if(obj.Tipo == "CHAR")
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

        public bool checarCondicion(List<Token> t)
        {
            for (int i = 0; i < t.Count; i++)
            {
                System.Console.Out.WriteLine("Lexema " + t[i].Lexema);
            }
            if (tipoCondicion(t) > 16)
            {
                System.Console.Out.WriteLine("Condicion mal formada");
                return false;
            }
            else
            {
                System.Console.Out.WriteLine("Bien formada, es una condicion de tipo: " + tipoCondicion(t));
                return true;
            }
        }

        public int tipoCondicion(List<Token> t)
        {
            int respuesta = 30;
            switch (t.Count)
            {
                case 3:
                    if (esEnteroID(t[0].Tipo) && esOperadorCondicion(t[1].Tipo) && esEnteroID(t[2].Tipo))
                    {
                        respuesta = 0;
                    }
                    break;
                case 5:
                    if(t[0].Tipo == "ID" && t[1].Tipo == "S_PUNTO" && t[2].Tipo == "ID" && esOperadorCondicion(t[3].Tipo) && esEnteroID(t[4].Tipo))
                    {
                        respuesta = 2;
                    }
                    else if (esEnteroID(t[0].Tipo) && esOperadorCondicion(t[1].Tipo) && t[2].Tipo == "ID" && t[3].Tipo == "S_PUNTO" && t[4].Tipo == "ID")
                    {
                        respuesta = 4;
                    }
                    else if (t[0].Tipo == "ID" && esOperadorCondicion(t[1].Tipo) && t[2].Tipo == "S_COMILLA" && t[3].Tipo == "ID" && t[4].Tipo == "S_COMILLA")
                    {
                        respuesta = 5;
                    }
                    break;
                case 7:
                    if (t[0].Tipo == "ID" && t[1].Tipo == "S_PUNTO" && t[2].Tipo == "ID" && esOperadorCondicion(t[3].Tipo) && t[4].Tipo == "ID" && t[5].Tipo == "S_PUNTO" && t[6].Tipo == "ID")
                    {
                        respuesta = 1;
                    }
                    else if (t[0].Tipo == "ID" && t[1].Tipo == "S_PUNTO" && t[2].Tipo == "ID" && esOperadorCondicion(t[3].Tipo) && t[4].Tipo == "S_COMILLA" && t[5].Tipo == "ID" && t[6].Tipo == "S_COMILLA")
                    {
                        respuesta = 3;
                    }
                    break;
            }
            return respuesta;
        }

        public bool operacionCondicion(string izq, Token t, string der)
        {
            bool bandera = false;
            switch(t.Tipo)
            {
                case "OP_IGUAL":
                    if(izq == der)
                    {
                        bandera = true;
                        System.Console.Out.WriteLine("--------------------------------------ENTRO A IGUAL");
                    }
                    break;
                case "OP_MENOR":
                    if (Convert.ToInt32(izq) < Convert.ToInt32(der))
                    {
                        bandera = true;
                    }
                    break;
                case "OP_MENORIGUAL":
                    if (Convert.ToInt32(izq) <= Convert.ToInt32(der))
                    {
                        bandera = true;
                    }
                    break;
                case "OP_MAYOR":
                    if (Convert.ToInt32(izq) > Convert.ToInt32(der))
                    {
                        bandera = true;
                    }
                    break;
                case "OP_MAYORIGUAL":
                    if (Convert.ToInt32(izq) >= Convert.ToInt32(der))
                    {
                        bandera = true;
                    }
                    break;
                case "OP_DIFERENTE":
                    if (izq != der)
                    {
                        bandera = true;
                    }
                    break;
                default:
                    break;
            }
            return bandera;
        }

        public bool analizaCondicion(List<Token> t,int tipoCondicion)
        {
            bool bandera = false;
            switch (tipoCondicion)
            {
                case 0:
                    // esEnteroID esOperadorCondicion esEnteroID
                    if (t[0].Tipo == "ID")
                    {
                        if (existeSimbolo(t[0].Lexema))
                        {
                            if (t[2].Tipo == "ID")
                            {
                                if (existeSimbolo(t[2].Lexema))
                                {
                                    if (getTipo(t[0].Lexema) == "ENTERO" && getTipo(t[2].Lexema) == "ENTERO")
                                    {
                                        bandera = operacionCondicion(getValor(t[0]), t[1], getValor(t[2]));
                                        System.Console.Out.WriteLine("--------------------------------------CHECO CONDICION OPERACION");
                                    }
                                    else
                                    {
                                        form.Consola1.Text += "consola> Error semántico, la condición en la línea " + t[0].Linea +" tiene un error, los tipos no coinciden \n";
                                    }
                                }
                                else
                                {
                                    form.Consola1.Text += "consola> Error semántico, la variable " + t[2].Lexema + " (línea " + t[2].Linea + ") no está declarada \n";
                                }
                            }
                            else if (t[2].Tipo == "ENTERO")
                            {
                                bandera = operacionCondicion(getValor(t[0]), t[1], t[2].Lexema);
                            }
                        }
                        else
                        {
                            form.Consola1.Text += "consola> Error semántico, la variable " + t[0].Lexema + " (línea " + t[0].Linea + ") no está declarada \n";
                        }
                    }
                    else if (t[0].Tipo == "ENTERO")
                    {
                        if (t[2].Tipo == "ID")
                        {
                            if (existeSimbolo(t[2].Lexema))
                            {
                                if (t[0].Tipo == "ENTERO" && getTipo(t[2].Lexema) == "ENTERO")
                                {
                                    bandera = operacionCondicion(t[0].Lexema, t[1], getValor(t[2]));
                                }
                                else
                                {
                                    form.Consola1.Text += "consola> Error semántico, la condición en la línea " + t[0].Linea + " tiene un error, los tipos no coinciden \n";
                                }
                            }
                            else
                            {
                                form.Consola1.Text += "consola> Error semántico, la variable " + t[2].Lexema + " (línea " + t[2].Linea + ") no está declarada \n";
                            }
                        }
                        else if (t[2].Tipo == "ENTERO")
                        {
                            bandera = operacionCondicion(t[0].Lexema, t[1], t[2].Lexema);
                        }
                    }
                    break;
                case 1:
                    //"ID" "S_PUNTO" "ID" esOperadorCondicion "ID" "S_PUNTO" "ID"
                    if (existeVar(t[0].Lexema))
                    {
                        if (esCampo(t[0], t[2].Lexema))
                        {
                            if (existeVar(t[4].Lexema))
                            {
                                if (esCampo(t[4], t[6].Lexema))
                                {
                                    if (getTipoCampo(t[0], t[2].Lexema) == getTipoCampo(t[4], t[6].Lexema))
                                    {
                                        bandera = operacionCondicion(getValorCampo(t[0], t[2].Lexema), t[3], getValorCampo(t[4], t[6].Lexema));
                                    }
                                    else
                                    {
                                        form.Consola1.Text += "consola> Error semántico, la condición en la línea " + t[0].Linea + " tiene un error, los tipos no coinciden \n";
                                    }
                                }
                                else
                                {
                                    form.Consola1.Text += "consola> Error semántico, el campo " + t[6].Lexema + " (línea " + t[6].Linea + ") no existe \n";
                                }
                            }
                            else
                            {
                                form.Consola1.Text += "consola> Error semántico, la variable " + t[4].Lexema + " (línea " + t[4].Linea + ") no es una variable de tipo estructura \n";
                            }
                        }
                        else
                        {
                            form.Consola1.Text += "consola> Error semántico, el campo " + t[2].Lexema + " (línea " + t[2].Linea + ") no existe \n";
                        }
                    }
                    else
                    {
                        form.Consola1.Text += "consola> Error semántico, la variable " + t[0].Lexema + " (línea " + t[0].Linea + ") no es una variable de tipo estructura \n";
                    }
                    break;
                case 2:
                    //"ID" "S_PUNTO" "ID" esOperadorCondicion esEnteroID
                    if (existeVar(t[0].Lexema))
                    {
                        if (esCampo(t[0], t[2].Lexema))
                        {
                            if (existeSimbolo(t[4].Lexema))
                            {
                                if (getTipo(t[4].Lexema) == "ENTERO")
                                {
                                    bandera = operacionCondicion(getValorCampo(t[0], t[2].Lexema), t[3], getValor(t[4]));
                                }
                                else
                                {
                                    form.Consola1.Text += "consola> Error semántico, la variable " + t[4].Lexema + " (línea " + t[4].Linea + ") no es de tipo entera \n";
                                }
                            }
                            else if (t[4].Tipo == "ENTERO")
                            {
                                bandera = operacionCondicion(getValorCampo(t[0], t[2].Lexema), t[3], t[4].Lexema);
                            }
                        }
                        else
                        {
                            form.Consola1.Text += "consola> Error semántico, el campo " + t[2].Lexema + " (línea " + t[2].Linea + ") no existe \n";
                        }
                    }
                    else
                    {
                        form.Consola1.Text += "consola> Error semántico, la variable " + t[4].Lexema + " (línea " + t[4].Linea + ") no es una variable de tipo estructura \n";
                    }

                    break;
                case 3:
                    //t[0].Tipo == "ID" && t[1].Tipo == "S_PUNTO" && t[2].Tipo == "ID" && esOperadorCondicion(t[3].Tipo) && t[4].Tipo == "S_COMILLA" && t[5].Tipo == "ID" && t[6].Tipo == "S_COMILLA" 
                    //"ID" "S_PUNTO" "ID" esOperadorCondicion "S_COMILLA" "ID" "S_COMILLA" 
                    if (existeVar(t[0].Lexema))
                    {
                        if (esCampo(t[0], t[2].Lexema))
                        {
                            if (getTipoCampo(t[0], t[2].Lexema) == "CHAR")
                            {
                                if (t[5].Lexema.Length == 1)
                                {
                                    bandera = operacionCondicion(getValorCampo(t[0], t[2].Lexema), t[3], "'"+t[5].Lexema+"'");
                                }
                                else
                                {
                                    form.Consola1.Text += "consola> Error semántico, en " + t[5].Lexema + " (línea " + t[5].Linea + ") la longitud del caracter excede a uno \n";
                                }
                            }
                            else
                            {
                                form.Consola1.Text += "consola> Error semántico, el campo " + t[2].Lexema + " (línea " + t[5].Linea + ") no es de tipo CHAR \n";
                            }
                        }
                        else
                        {
                            form.Consola1.Text += "consola> Error semántico, el campo " + t[2].Lexema + " (línea " + t[2].Linea + ") no existe \n";
                        }
                    }
                    else
                    {
                        form.Consola1.Text += "consola> Error semántico, la variable " + t[0].Lexema + " (línea " + t[0].Linea + ") no es una variable de tipo estructura \n";
                    }
                    break;

                case 4:

                    //esEnteroID(t[0].Tipo) && esOperadorCondicion(t[1].Tipo) && t[2].Tipo == "ID" && t[3].Tipo == "S_PUNTO" && t[4].Tipo == "ID"
                    //esEnteroID esOperadorCondicion "ID" "S_PUNTO" "ID"

                    if (t[0].Tipo == "ID")
                    {
                        if (existeSimbolo(t[0].Lexema))
                        {
                            if (existeVar(t[2].Lexema))
                            {
                                if (esCampo(t[2], t[4].Lexema))
                                {
                                    //Exito
                                }
                                else
                                {
                                    //el campo no existe
                                }

                            }
                            else
                            {
                                //La estructura no existe
                            }

                        }
                        else
                        {
                            form.Consola1.Text += "consola> Error semántico, la variable " + t[0].Lexema + " (línea " + t[0].Linea + ") no es una variable declarada \n";
                        }
                    }
                    else if (t[0].Tipo == "ENTERO")
                    {
                        if (existeVar(t[2].Lexema))
                        {
                            if (esCampo(t[2], t[4].Lexema))
                            {
                                //Exito
                            }
                            else
                            {
                                //el campo no existe
                            }

                        }
                        else
                        {
                            //La estructura no existe
                        }
                    }
                    break;
                case 5:
                    //t[0].Tipo == "ID" && esOperadorCondicion(t[1].Tipo) && t[2].Tipo == "S_COMILLA" && t[3].Tipo == "ID" && t[4].Tipo == "S_COMILLA"
                    //"ID" esOperadorCondicion "S_COMILLA" "ID" "S_COMILLA"
                    if (existeSimbolo(t[0].Lexema))
                    {
                        if(getTipo(t[0].Lexema) == "CHAR")
                            
                        {
                            System.Console.Out.WriteLine(getValor(t[0]));
                            if (t[3].Lexema.Length == 1)
                            {
                                bandera = operacionCondicion(getValor(t[0]), t[1], "'" + t[3].Lexema + "'");
                            }
                            else
                            {
                                form.Consola1.Text += "consola> Error semántico, " + t[3].Lexema + " (línea " + t[3].Linea + ") excede un caracter \n";
                            }
                        }
                        else
                        {
                            form.Consola1.Text += "consola> Error semántico, la variable " + t[0].Lexema + " (línea " + t[0].Linea + ") no es de tipo CHAR \n";
                        }
                    }
                    else
                    {
                        form.Consola1.Text += "consola> Error semántico, la variable " + t[0].Lexema + " (línea " + t[0].Linea + ") no es una variable \n";
                    }
                    break;
            }
            return bandera;
        }

        public Form1 Form { get => form; set => form = value; }
        internal Dictionary<string, Simbolo> TablaSimbolos { get => tablaSimbolos; set => tablaSimbolos = value; }
        internal AnalizaExpresion Aex { get => aex; set => aex = value; }
        internal Dictionary<string, VariableEstructura> EstructuraSimbolos { get => estructuraSimbolos; set => estructuraSimbolos = value; }
    }
}
