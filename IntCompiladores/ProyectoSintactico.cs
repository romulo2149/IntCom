using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntCompiladores
{
    class ProyectoSintactico
    {
        private Lexico lex;
        private AnalizaExpresion aex;
        public string preanalisis;
        Token toke;
        string lexema;
        Form1 form1;
        Error error = new Error();
        string valor, tipo, id, alcance, cadena = "";
        public List<Simbolo> sim = new List<Simbolo>();
        public List<Token> t;
        public Token to;
        public ProyectoSemantico ps;
        public string exp;
        public string estruct;


        public ProyectoSintactico(Lexico lex, AnalizaExpresion aex, Form1 f)
        {
            this.lex = lex;
            toke = lex.AnalizaRecursivo().Token;
            preanalisis = toke.Tipo;
            lexema = toke.Lexema;
            form1 = f;
            ps = new ProyectoSemantico(f, aex);
            Aex = aex;
        }

        internal AnalizaExpresion Aex { get => aex; set => aex = value; }
        public void PROGRAMA() 
        /*
         * PROGRAMA -> PR_PROGRAMA ID CONSTANTES() ESTRUCTURAS() PR_INICIO INSTRUCCIONES() PR_FIN { PR_PROGRAMA }
         */
        {
            if(preanalisis == "PR_PROGRAMA")
            {
                Emparejar("PR_PROGRAMA");
                Emparejar("ID");
                CONSTANTES();
                ESTRUCTURAS();
                Emparejar("PR_INICIO");
                INSTRUCCIONES();
                Emparejar("PR_FIN");
                form1.Consola1.Text += "consola> No se encontraron errores \n";
            }
            else
            {
                error.NuevoError(lexema, toke.Linea, "PROGRAMA", form1);
            }
            form1.Consola1.Text += "consola> Analisis terminado \n";
        }

        public void CONSTANTES()
        /*
         * CONSTANTES -> ε { PR_INICIO COMA PR_ESTRUCTURAS }
         * CONSTANTES -> PR_CONSTANTES ID OP_ASIGNACION VALCONS() CONSTANTE() { PR_CONSTANTES } 
         */
        {
           if(preanalisis == "PR_INICIO" || preanalisis == "PR_ESTRUCTURAS")
            {
                switch (preanalisis)
                {
                    case "PR_INICIO":
                        break;

                    case "PR_ESTRUCTURAS":
                        break;

                    default:
                        form1.Consola1.Text += "consola> Error Sintáctico \n";
                        break;
                }
            }
           else if(preanalisis == "PR_CONSTANTES")
            {
                Emparejar("PR_CONSTANTES");
                id = lexema;
                Emparejar("ID");
                Emparejar("OP_ASIGNACION");
                VALCONS(id);
                CONSTANTE();
            }
           else
            {
                error.NuevoError(lexema, toke.Linea, "CONSTANTES, ESTRUCTURAS ó INICIO", form1);
            }
        }

        public void CONSTANTE()
        /*
         * CONSTANTE -> ε { PR_INICIO, PR_ESTRUCTURAS }
         * CONSTANTE -> id OP_ASIGNACION VALCONS() CONSTANTE() { ID ]
         */
        {
            if(preanalisis == "PR_INICIO" || preanalisis == "PR_ESTRUCTURAS")
            {
                switch(preanalisis)
                {
                    case "PR_INICIO":
                        break;

                    case "PR_ESTRUCTURAS":
                        break;

                    default:
                        form1.Consola1.Text += "consola> Error Sintáctico \n";
                        break;
                }
            }
            else if(preanalisis == "ID")
            {
                string id2 = lexema;
                Emparejar("ID");
                Emparejar("OP_ASIGNACION");
                VALCONS(id2);
                CONSTANTE();
            }
            else
            {
                error.NuevoError(lexema, toke.Linea, "INICIO, ESTRUCTURAS, ID", form1);
            }
        }

        public void VALCONS(string id2)
        /*
         * VALCONS -> ENTERO { ENTERO }
         * VALCONS -> COMILLA ID COMILLA { S_COMILLA }
         */
        {
            switch (preanalisis)
            {
                case "ENTERO":
                    ps.nuevoSimbolo(id2, preanalisis, lexema, "CONSTANTES", id2.Length, toke.Linea);
                    Emparejar("ENTERO");
                    break;

                case "S_COMILLA":
                    Emparejar("S_COMILLA");
                    ps.nuevoSimbolo(id2, preanalisis, lexema, "CONSTANTES", id2.Length, toke.Linea);
                    Emparejar("ID");
                    Emparejar("S_COMILLA");
                    break;

                default:
                    error.NuevoError(lexema, toke.Linea, "ENTERO ó CARACTER", form1);
                    break;
            }
        }

        public void ESTRUCTURAS()
        /*
         * ESTRUCTURAS -> ε { PR_INICIO }
         * ESTRUCTURAS -> PR_ESTRUCTURAS ID OP_ASIGNACION LLAVE_IZQ CAMPOS() LLAVE_DER ESTRUCTURASII() { PR_ESTRUCTURAS }
         */
        {
            switch(preanalisis)
            {
                case "PR_ESTRUCTURAS":
                    Emparejar("PR_ESTRUCTURAS");
                    if(preanalisis == "ID")
                    {
                        estruct = lexema;
                        ps.nuevoSimbolo(lexema, "ESTRUCTURA", "", lexema, lexema.Length, toke.Linea);
                    }
                    Emparejar("ID");
                    Emparejar("OP_ASIGNACION");
                    Emparejar("LLAVE_IZQ");
                    CAMPOS();
                    Emparejar("LLAVE_DER");
                    ESTRUCTURASII();
                    break;

                case "PR_INICIO":
                    break;

                default:
                    error.NuevoError(lexema, toke.Linea, "ESTRUCTURAS ó INICIO", form1);
                    break;
            }
        }

        public void CAMPOS()
        /*
         * CAMPOS -> TIPO() ID SEP() { PR_ENTERO, PR_CHAR, PR_APUNTADOR }
         */
        {
            if(preanalisis == "PR_ENTERO" || preanalisis == "PR_CHAR" || preanalisis == "PR_APUNTADOR")
            {
                TIPO();
                if(preanalisis == "ID")
                {
                    ps.nuevoSimbolo(lexema, tipo, "", estruct, lexema.Length, toke.Linea);
                }
                Emparejar("ID");
                SEP(tipo);
            }
            else
            {
                error.NuevoError(lexema, toke.Linea, "palabra reservada ENTERO, CHAR o APUNTADOR", form1);
            }
        }

        public void TIPO()
        /*
         * TIPO -> PR_ENTERO { PR_ENTERO }
         * TIPO -> PR_CHAR { PR_CHAR }
         * TIPO -> PR_APUNTADOR { PR_APUNTADOR }
         */
        {
            switch (preanalisis)
            {
                case "PR_ENTERO":
                    tipo = lexema;
                    Emparejar("PR_ENTERO");
                    break;

                case "PR_CHAR":
                    tipo = lexema;
                    Emparejar("PR_CHAR");
                    break;

                case "PR_APUNTADOR":
                    tipo = lexema;
                    Emparejar("PR_APUNTADOR");
                    break;

                default:
                    error.NuevoError(lexema, toke.Linea, "palabra reservada ENTERO, CHAR o APUNTADOR", form1);
                    break;
            }
        }

        public void SEP(string tipoSEP)
        /*
         * SEP -> S_PUNTOCOMA CAMPO2() { S_PUNTOCOMA }
         * SEP -> S_COMA ID SEP2() { S_COMA } 
         */
        {
            switch(preanalisis)
            {
                case "S_PUNTOCOMA":
                    Emparejar("S_PUNTOCOMA");
                    CAMPO2();
                    break;

                case "S_COMA":
                    Emparejar("S_COMA");
                    if(preanalisis == "ID")
                    {
                        ps.nuevoSimbolo(lexema, tipoSEP, "", estruct, lexema.Length, toke.Linea);
                    }
                    Emparejar("ID");
                    SEP2(tipoSEP);
                    break;

                default:
                    error.NuevoError(lexema, toke.Linea, "COMA ó PUNTO y COMA", form1);
                    break;
            }
        }

        public void CAMPO2()
        /*
         * CAMPO2 -> ε { LLAVE_DER }
         * CAMPO2 -> TIPO() ID SEP3() { PR_ENTERO, PR_CHAR, PR_APUNTADOR }
         */
        {
            if (preanalisis == "PR_ENTERO" || preanalisis == "PR_CHAR" || preanalisis == "PR_APUNTADOR")
            {
                TIPO();
                if(preanalisis == "ID")
                {
                    ps.nuevoSimbolo(lexema, tipo, "", estruct, lexema.Length, toke.Linea);
                }
                Emparejar("ID");
                SEP3(tipo);
            }
            else if(preanalisis == "LLAVE_DER")
            {

            }
            else
            {
                error.NuevoError(lexema, toke.Linea, "palabra reservada ENTERO, CHAR, APUNTADOR o un símbolo }", form1);
            }
        }

        public void SEP2(string tipoSEP2)
        /*
         * SEP2 -> S_PUNTOCOMA CAMPO3() { S_PUNTOCOMA }
         * SEP2 -> S_COMA ID S_PUNTOCOMA { S_COMA }
         */
        {
            switch (preanalisis)
            {
                case "S_PUNTOCOMA":
                    Emparejar("S_PUNTOCOMA");
                    CAMPO3();
                    break;

                case "S_COMA":
                    Emparejar("S_COMA");
                    if (preanalisis == "ID")
                    {
                        ps.nuevoSimbolo(lexema, tipoSEP2, "", estruct, lexema.Length, toke.Linea);
                    }
                    Emparejar("ID");
                    Emparejar("S_PUNTOCOMA");
                    break;

                default:
                    error.NuevoError(lexema, toke.Linea, "COMA ó PUNTO y COMA", form1);
                    break;
            }
        }

        public void SEP3(string tipoSEP3)
        /*
         * SEP3 -> S_PUNTOCOMA CAMPO3() { S_PUNTOCOMA }
         * SEP3 -> S_COMA ID S_PUNTOCOMA { S_COMA }
         */
        {
            switch (preanalisis)
            {
                case "S_PUNTOCOMA":
                    Emparejar("S_PUNTOCOMA");
                    CAMPO3();
                    break;

                case "S_COMA":
                    Emparejar("S_COMA");
                    if (preanalisis == "ID")
                    {
                        ps.nuevoSimbolo(lexema, tipoSEP3, "", estruct, lexema.Length, toke.Linea);
                    }
                    Emparejar("ID");
                    Emparejar("S_PUNTOCOMA");
                    break;

                default:
                    error.NuevoError(lexema, toke.Linea, "COMA ó PUNTO y COMA", form1);
                    break;
            }
        }

        public void CAMPO3()
        /*
         * CAMPO3 -> ε { LLAVE_DER }
         * CAMPO3 -> TIPO() ID S_PUNTOCOMA { PR_ENTERO, PR_CHAR, PR_APUNTADOR }
         */
        {
            if (preanalisis == "PR_ENTERO" || preanalisis == "PR_CHAR" || preanalisis == "PR_APUNTADOR")
            {
                TIPO();
                if(preanalisis == "ID")
                {
                    ps.nuevoSimbolo(lexema, tipo, "", estruct, lexema.Length, toke.Linea);
                }
                Emparejar("ID");
                Emparejar("S_PUNTOCOMA");
            }
            else if (preanalisis == "LLAVE_DER")
            {

            }
            else
            {
                error.NuevoError(lexema, toke.Linea, "palabra reservada ENTERO, CHAR, APUNTADOR o el símbolo }", form1);
            }
        }

        public void ESTRUCTURASII()
        /*
         * ESTRUCTURASII -> ε { PR_INICIO }
         * ESTRUCTURASII -> ESTRUCTURA() { ID }
         */
        {
            switch(preanalisis)
            {
                case "PR_INICIO":
                    break;

                case "ID":
                    ESTRUCTURA();
                    break;

                default:
                    error.NuevoError(lexema, toke.Linea, "INICIO o un ID", form1);
                    break;
            }
        }

        public void ESTRUCTURA()
        /*
         * ESTRUCTURA -> ID VARSTR() { ID }
         */
        {
            if (preanalisis == "ID")
            {
                id = lexema;
                alcance = lexema;
                estruct = lexema;
                Emparejar("ID");
                VARSTR(id);
            }
            else
            {
                error.NuevoError(lexema, toke.Linea, "ID", form1);
            }
        }

        public void VARSTR(string str)
        /*
         * VARSTR -> ID VAR() { ID }
         * VARSTR -> OP_ASIGNACION LLAVE_IZQ CAMPOS() LLAVE_DER VARC() { OP_ASIGNACION }
         */
        {
            switch(preanalisis)
            {
                case "ID":
                    ps.nuevoSimbolo(lexema, str, "", "VAR_ESTRUCTURA", lexema.Length, toke.Linea);
                    Emparejar("ID");
                    VARF(str);
                    break;

                case "OP_ASIGNACION":
                    valor = "";
                    ps.nuevoSimbolo(id, "ESTRUCTURA", valor, "", id.Length, toke.Linea);
                    Emparejar("OP_ASIGNACION");
                    Emparejar("LLAVE_IZQ");
                    CAMPOS();
                    Emparejar("LLAVE_DER");
                    VARC();
                    break;

                default:
                    error.NuevoError(lexema, toke.Linea, "ID ó el operador =", form1);
                    break;
            }
        }

        public void VARC()
        /*
         * VARC -> ID ID VAR()
         */
        {
            if (preanalisis == "ID")
            {
                tipo = lexema;
                Emparejar("ID");
                ps.nuevoSimbolo(lexema, tipo, "", "VAR_ESTRUCTURA", lexema.Length, toke.Linea);
                Emparejar("ID");
                VARF(tipo);
            }
            else
            {
                error.NuevoError(lexema, toke.Linea, "ID", form1);
            }
        }

        public void VARF(string str)
        /*
         * VAR -> VAR2() S_PUNTOCOMA VAR3() { S_PUNTOCOMA, S_COMA }
         */
        {
            if (preanalisis == "S_PUNTOCOMA" || preanalisis == "S_COMA")
            {
                switch(preanalisis)
                {
                    case "S_PUNTOCOMA":
                        System.Console.Out.WriteLine("detecto el punto y coma" );
                        VAR2(str);
                        Emparejar("S_PUNTOCOMA");
                        VAR3(str);
                        break;
                    case "S_COMA":
                        VAR2(str);
                        Emparejar("S_PUNTOCOMA");
                        VAR3(str);
                        break;
                    default:
                        error.NuevoError(lexema, toke.Linea, "COMA ó PUNTO y COMA", form1);
                        break;
                }
            }
            else
            {
                error.NuevoError(lexema, toke.Linea, "COMA ó PUNTO y COMA", form1);
            }
        }

        public void VAR2(string str)
        /*
         * VAR2 -> S_COMA ID VAR2() { S_COMA }
         * VAR2 -> ε { S_PUNTOCOMA }
         */
        {
            switch (preanalisis)
            {
                case "S_COMA":
                    Emparejar("S_COMA");
                    if (preanalisis == "ID")
                    {
                        ps.nuevoSimbolo(lexema, str, "", "VAR_ESTRUCTURA", lexema.Length, toke.Linea);
                    }
                    Emparejar("ID");
                    VAR2(str);
                    break;

                case "S_PUNTOCOMA":
                    break;

                default:
                    error.NuevoError(lexema, toke.Linea, "COMA ó PUNTO y COMA", form1);
                    break;
            }
        }


        public void VAR3(string str)
        /*
         * VAR3 -> ID ID VAR2() S_PUNTOCOMA VAR3() { ID }
         * VAR3 -> ε { INICIO }
         */
        {
            switch(preanalisis)
            {
                case "ID":
                    tipo = lexema;
                    Emparejar("ID");
                    if (preanalisis == "ID")
                    {
                        ps.nuevoSimbolo(lexema, tipo, "", "VAR_ESTRUCTURA", lexema.Length, toke.Linea);
                    }
                    Emparejar("ID");
                    VAR2(tipo);
                    Emparejar("S_PUNTOCOMA"); //duda
                    VAR3(tipo);
                    break;

                case "PR_INICIO":
                    break;

                default:
                    error.NuevoError(lexema, toke.Linea, "palabra reservada INICIO o un ID", form1);
                    break;
            }
        }

        public void INSTRUCCIONES()
        /*
         * INSTRUCCIONES -> ε { PR_FIN, PR_SINO }
         * INSTRUCCIONES -> INSTRUCCION() INSTRUCCIONES() { ID, PR_SI, PR_MIENTRAS, PR_ESCRIBE, PR_LEE }
         */
        {
            if(preanalisis == "PR_SI" || preanalisis == "PR_MIENTRAS" || preanalisis == "PR_ESCRIBE" 
                || preanalisis == "PR_LEE" || preanalisis == "ID")
            {
                INSTRUCCION();
                INSTRUCCIONES();
            }
            else if(preanalisis == "PR_FIN" || preanalisis == "PR_SINO")
            {

            }
            else
            {
                error.NuevoError(lexema, toke.Linea, "una instrucción o una expresión, palabra reservada FIN o SINO", form1);
            }
        }

        public void INSTRUCCION()
        /*
         * INSTRUCCION -> SI() { PR_SI }
         * INSTRUCCION -> MIENTRAS() { PR_MIENTRAS }
         * INSTRUCCION -> ESCRIBE() { PR_ESCRIBE }
         * INSTRUCCION -> LEE() { PR_LEE }
         * INSTRUCCION -> EXPRESION() { ID }
         */
        {
            switch (preanalisis)
            {
                case "PR_SI":
                    SI();
                    break;

                case "PR_MIENTRAS":
                    MIENTRAS();
                    break;

                case "PR_ESCRIBE":
                    ESCRIBE();
                    break;

                case "PR_LEE":
                    LEE();
                    break;

                case "ID":
                    EXPRESION();
                    break;

                default:
                    error.NuevoError(lexema, toke.Linea, "una instrucción o una expresión", form1);
                    break;
            }
        }

        public void SI()
        /*
         *  SI -> SIP1 SIP2 PR_FIN { PR_SI }
         */
        {
            if(preanalisis == "PR_SI")
            {
                SIP1();
                SIP2();
                Emparejar("PR_FIN");
            }
            else
            {
                error.NuevoError(lexema, toke.Linea, "la condición SI", form1);
            }
        }

        public void SIP1()
        /*
         * SIP1 -> PR_SI PA_IZQ CONDICION() PA_DER PR_ENTONCES INSTRUCCIONES()
         */
        {
            if(preanalisis == "PR_SI")
            {
                Emparejar("PR_SI");
                Emparejar("PA_IZQ");
                CONDICION();
                Emparejar("PA_DER");
                Emparejar("PR_ENTONCES");
                INSTRUCCIONES();
            }
            else
            {
                error.NuevoError(lexema, toke.Linea, "palabra reservada SI", form1);
            }
        }

        public void SIP2()
        /*
         * SIP2 -> ε { PR_FIN }
         * SIP2 -> PR_SINO INSTRUCCIONES() { PR_SINO }
         */
        {
            if(preanalisis == "PR_SINO")
            {
                Emparejar("PR_SINO");
                INSTRUCCIONES();
            }
            else if(preanalisis == "PR_FIN")
            {

            }
        }

        public void CONDICION()
        /*
         * CONDICION -> OPERANDO OPERADOR_CONDICION OPERADOR { ID, ENTERO, S_COMILLA }
         */
        {
            if(preanalisis == "ID" || preanalisis == "ENTERO" || preanalisis == "S_COMILLA")
            {
                t = new List<Token>();
                OPERANDON();
                OPERADOR_CONDICION();
                OPERANDON();
                if (ps.checarCondicion(t) == false)
                {
                    error.NuevoError(exp, toke.Linea, "Error en la expresión, está mal formada", form1);
                }
                System.Console.Out.WriteLine("Expresión: " + exp);
            }
        }


        public void OPERADOR()
        /*
         * OPERADOR -> OP_SUMA { OP_SUMA }
         * OPERADOR -> OP-RESTA { OP_RESTA }
         * OPERADOR -> OP_MULTIPLICACION { OP_MULTIPLICACION }
         * OPERADOR -> OP_DIVISION { OP_DIVISION }
         * OPERADOR -> OP_MODULO { OP_MODULO }
         */
        {
            switch(preanalisis)
            {
                case "OP_SUMA":
                    exp = exp + lexema + " ";
                    to = new Token(toke.Lexema, toke.Linea, toke.Tipo, toke.Error);
                    t.Add(to);
                    Emparejar("OP_SUMA");
                    break;

                case "OP_RESTA":
                    exp = exp + lexema + " ";
                    to = new Token(toke.Lexema, toke.Linea, toke.Tipo, toke.Error);
                    t.Add(to);
                    Emparejar("OP_RESTA");
                    break;

                case "OP_MULTIPLICACION":
                    exp = exp + lexema + " ";
                    to = new Token(toke.Lexema, toke.Linea, toke.Tipo, toke.Error);
                    t.Add(to);
                    Emparejar("OP_MULTIPLICACION");
                    break;

                case "OP_DIVISION":
                    exp = exp + lexema + " ";
                    to = new Token(toke.Lexema, toke.Linea, toke.Tipo, toke.Error);
                    t.Add(to);
                    Emparejar("OP_DIVISION");
                    break;

                case "OP_MODULO":
                    exp = exp + lexema + " ";
                    to = new Token(toke.Lexema, toke.Linea, toke.Tipo, toke.Error);
                    t.Add(to);
                    Emparejar("OP_MODULO");
                    break;
            }
        }


        public void OPERADOR_CONDICION()
        /*
         * OPERADOR_CONDICION -> OP_MENOR { OP_MENOR }
         * OPERADOR_CONDICION -> OP_MAYOR { OP_MAYOR }
         * OPERADOR_CONDICION -> OP_MENORIGUAL { OP_MENORIGUAL }
         * OPERADOR_CONDICION -> OP_MAYORIGUAL { OP_MAYORIGUAL }
         * OPERADOR_CONDICION -> OP_IGUAL { OP_IGUAL }
         * OPERADOR_CONDICION -> OP_DIFERENTE { OP_DIFERENTE }
         */
        {
            switch(preanalisis)
            {
                case "OP_MENOR":
                    Emparejar("OP_MENOR");
                    break;

                case "OP_MAYOR":
                    Emparejar("OP_MAYOR");
                    break;

                case "OP_MENORIGUAL":
                    Emparejar("OP_MENORIGUAL");
                    break;

                case "OP_MAYORIGUAL":
                    Emparejar("OP_MAYORIGUAL");
                    break;

                case "OP_IGUAL":
                    Emparejar("OP_IGUAL");
                    break;

                case "OP_DIFERENTE":
                    Emparejar("OP_DIFERENTE");
                    break;
            }
        }


        public void OPERANDO()
        /*
         * OPERANDO -> ENTERO { ENTERO }
         * OPERANDO -> ID { ID }
         * OPERANDO -> S_COMILLA ID S_COMILLA { S_COMILLA }
         */
        {
            switch(preanalisis)
            {
                case "ENTERO":
                    exp = exp + lexema + " ";
                    to = new Token(toke.Lexema, toke.Linea, toke.Tipo, toke.Error);
                    t.Add(to);
                    Emparejar("ENTERO");
                    break;

                case "ID":
                    exp = exp + lexema + " ";
                    to = new Token(toke.Lexema, toke.Linea, toke.Tipo, toke.Error);
                    t.Add(to);
                    Emparejar("ID");
                    break;

                case "S_COMILLA":
                    exp = exp + lexema + " ";
                    to = new Token(toke.Lexema, toke.Linea, toke.Tipo, toke.Error);
                    t.Add(to);
                    Emparejar("S_COMILLA");
                    exp = exp + lexema + " ";
                    to = new Token(toke.Lexema, toke.Linea, toke.Tipo, toke.Error);
                    t.Add(to);
                    Emparejar("ID");
                    exp = exp + lexema + " ";
                    to = new Token(toke.Lexema, toke.Linea, toke.Tipo, toke.Error);
                    t.Add(to);
                    Emparejar("S_COMILLA");
                    break;

                case "PR_NULL":
                    exp = exp + lexema + " ";
                    to = new Token(toke.Lexema, toke.Linea, toke.Tipo, toke.Error);
                    t.Add(to);
                    Emparejar("PR_NULL");
                    break;
            }

        }


        public void MIENTRAS()
        /*
         * MIENTRAS -> PR_MIENTRAS PA_IZQ CONDICION() PA_DER PR_HACER INSTRUCCIONES() PR_FIN { PR_MIENTRAS }
         */
        {
            if(preanalisis == "PR_MIENTRAS")
            {
                Emparejar("PR_MIENTRAS");
                Emparejar("PA_IZQ");
                CONDICION();
                Emparejar("PA_DER");
                Emparejar("PR_HACER");
                INSTRUCCIONES();
                Emparejar("PR_FIN");
            }
        }

        public void LEE()
        /*
         * LEE -> PR_LEE PA_IZQ TEXTO() PA_DER S_PUNTOCOMA { PR_LEE }
         */
        {
            if (preanalisis == "PR_LEE")
            {
                Emparejar("PR_LEE");
                Emparejar("PA_IZQ");
                TEXTO();
                Emparejar("PA_DER");
                Emparejar("S_PUNTOCOMA");
            }
        }

        public void TEXTO()
        /*
         * TEXTO -> ID TEXTSTR() { ID }
         */
        {
            if(preanalisis == "ID")
            {
                exp = exp + lexema + " ";
                to = new Token(toke.Lexema, toke.Linea, toke.Tipo, toke.Error);
                t.Add(to);
                Emparejar("ID");
                TEXTSTR();
            }
        }

        public void TEXTSTR()
        /*
         * TEXTSTR -> ε { PA_DER}
         * TEXTSTR -> S_PUNTO ID { S_PUNTO }
         */
        {
            if(preanalisis == "S_PUNTO")
            {
                exp = exp + lexema + " ";
                to = new Token(toke.Lexema, toke.Linea, toke.Tipo, toke.Error);
                t.Add(to);
                Emparejar("S_PUNTO");
                exp = exp + lexema + " ";
                to = new Token(toke.Lexema, toke.Linea, toke.Tipo, toke.Error);
                t.Add(to);
                Emparejar("ID");
            }
            else if(preanalisis == "PA_DER")
            {

            }
        }

        public void ESCRIBE()
        /*
         *  ESCRIBE -> PR_ESCRIBE PA_IZQ CADENA() PA_DER S_PUNTOCOMA { PR_ESCRIBE }
         */
        {
            if(preanalisis == "PR_ESCRIBE")
            {
                Emparejar("PR_ESCRIBE");
                Emparejar("PA_IZQ");
                CADENA();
                Emparejar("PA_DER");
                if(preanalisis == "S_PUNTOCOMA")
                {
                    form1.Consola1.Text += "consola> " + cadena + "\n";
                }
                Emparejar("S_PUNTOCOMA");
                cadena = "";
            }
        }

        public void CADENA()
        /*
         * CADENA -> S_COMILLA CADENA S_COMILLA { S_COMILLA }
         * CADENA -> ID { ID }
         */
        {
            if (preanalisis == "S_COMILLA")
            {
                Emparejar("S_COMILLA");
                while(preanalisis != "S_COMILLA")
                {
                    System.Console.Out.WriteLine("Espacios: " + lex.cuentaEspacios);
                    cadena = cadena + lexema;
                    for (int i = 0; i < lex.cuentaEspacios; i++)
                    {
                        cadena = cadena + " ";
                    }
                    EmparejarCadena();
                    lex.cuentaEspacios = 0;
                }
                Emparejar("S_COMILLA");
            }
            else if (preanalisis == "ID")
            {
                Emparejar("ID");
            }
        }

        public void EXPRESION()
        /*
         * EXPRESION -> ID TEXTSTR2() { ID }
         */
        {
            exp = "";
            t = new List<Token>();
            if(preanalisis == "ID")
            {
                exp = exp + lexema + " ";
                to = new Token(toke.Lexema, toke.Linea, toke.Tipo, toke.Error);
                t.Add(to);
                Emparejar("ID");
                TEXTSTR2();
            }
        }


        public void O1()
        /*
         * O1 -> OPERANDON()
         */
        {
            if (preanalisis == "ENTERO" || preanalisis == "S_COMILLA" || preanalisis == "ID" || preanalisis == "PR_NULL")
            {
                OPERANDON();
            }
        }


        public void O2()
        /*
         * O2 -> OPERADOR() OPERANDO() { OP_SUMA, OP_RESTA, OP_MULTIPLICACION, OP_DIVISION, PR_MOD }
         * O2 -> ε { S_PUNTOCOMA }
         */
        {
            if (preanalisis == "OP_SUMA" || preanalisis == "OP_RESTA" || preanalisis == "OP_MULTIPLICACION"
                || preanalisis == "OP_DIVISION" || preanalisis == "OP_MODULO")
            {
                OPERADOR();
                OPERANDO();
            }
            else if (preanalisis == "S_PUNTOCOMA")
            {

            }
        }

        public void O3()
        /*
         * O2 -> OPERADOR() OPERANDO() { OP_SUMA, OP_RESTA, OP_MULTIPLICACION, OP_DIVISION, PR_MOD }
         * O2 -> ε { S_PUNTOCOMA }
         */
        {
            if (preanalisis == "OP_SUMA" || preanalisis == "OP_RESTA" || preanalisis == "OP_MULTIPLICACION"
                || preanalisis == "OP_DIVISION" || preanalisis == "OP_MODULO")
            {
                OPERADOR();
                OPERANDON();
            }
            else if (preanalisis == "S_PUNTOCOMA")
            {

            }
        }

        public void TEXTSTR2()
        /*
         * TEXTSTR2 -> S_PUNTO ID OP_ASIGNACION OPERANDON() O3() S_PUNTOCOMA { S_PUNTO }
         * TEXTSTR2 -> OP_ASIGNACION O1() O2() S_PUNTOCOMA { OP_ASIGNACION }
         */
        {
            switch (preanalisis)
            {
                case "S_PUNTO":
                    exp = exp + lexema + " ";
                    to = new Token(toke.Lexema, toke.Linea, toke.Tipo, toke.Error);
                    t.Add(to);
                    Emparejar("S_PUNTO");
                    exp = exp + lexema + " ";
                    to = new Token(toke.Lexema, toke.Linea, toke.Tipo, toke.Error);
                    t.Add(to);
                    Emparejar("ID");
                    exp = exp + lexema + " ";
                    to = new Token(toke.Lexema, toke.Linea, toke.Tipo, toke.Error);
                    t.Add(to);
                    Emparejar("OP_ASIGNACION");
                    OPERANDON();
                    O3();
                    exp = exp + lexema + " ";
                    to = new Token(toke.Lexema, toke.Linea, toke.Tipo, toke.Error);
                    t.Add(to);
                    Emparejar("S_PUNTOCOMA");
                    if(ps.expresionValida(t) == false)
                    {
                        error.NuevoError(exp, toke.Linea, "Error en la expresión, está mal formada", form1);
                    }
                    System.Console.Out.WriteLine("Expresión: " + exp);
                    break;

                case "OP_ASIGNACION":
                    exp = exp + lexema + " ";
                    to = new Token(toke.Lexema, toke.Linea, toke.Tipo, toke.Error);
                    t.Add(to);
                    Emparejar("OP_ASIGNACION");
                    OPERANDON();
                    O3();
                    exp = exp + lexema + " ";
                    to = new Token(toke.Lexema, toke.Linea, toke.Tipo, toke.Error);
                    t.Add(to);
                    Emparejar("S_PUNTOCOMA");
                    if (ps.expresionValida(t) == false)
                    {
                        error.NuevoError(exp, toke.Linea, "Error en la expresión, está mal formada", form1);
                    }
                    System.Console.Out.WriteLine("Expresión: " + exp);
                    break;
            }
        }
        public void OPERANDON()
        /*
         * OPERANDON -> ENTERO { ENTERO }
         * OPERANDON -> TEXTO() { ID }
         * OPERANDON -> S_COMILLA ID S_COMILLA { S_COMILLA }
         * OPERANDON -> PR_NULL { PR_NULL }
         */
        {
            switch (preanalisis)
            {
                case "ENTERO":
                    exp = exp + lexema + " ";
                    to = new Token(toke.Lexema, toke.Linea, toke.Tipo, toke.Error);
                    t.Add(to);
                    Emparejar("ENTERO");
                    break;

                case "ID":
                    TEXTO();
                    break;

                case "S_COMILLA":
                    exp = exp + lexema + " ";
                    to = new Token(toke.Lexema, toke.Linea, toke.Tipo, toke.Error);
                    t.Add(to);
                    Emparejar("S_COMILLA");
                    exp = exp + lexema + " ";
                    to = new Token(toke.Lexema, toke.Linea, toke.Tipo, toke.Error);
                    t.Add(to);
                    Emparejar("ID");
                    exp = exp + lexema + " ";
                    to = new Token(toke.Lexema, toke.Linea, toke.Tipo, toke.Error);
                    t.Add(to);
                    Emparejar("S_COMILLA");
                    break;

                case "PR_NULL":
                    exp = exp + lexema + " ";
                    to = new Token(toke.Lexema, toke.Linea, toke.Tipo, toke.Error);
                    t.Add(to);
                    Emparejar("PR_NULL");
                    break;
            }

        }

        public void DP()
        /*
         * DP -> ε { S_COMILLA }
         * DP -> S_DOSPUNTOS { S_DOSPUNTOS }
         */
        {
            if(preanalisis == "S_COMILLA")
            {

            }
            else if (preanalisis == "S_DOSPUNTOS")
            {
                Emparejar("S_DOSPUNTOS");
            }
        }

        public void Emparejar(string token)
        {
            form1.DataGridView1.Rows.Add(toke.Tipo, toke.Lexema, toke.Linea, toke.Error);
            System.Console.Out.WriteLine("dentro de emparejar el token esperado es:" + token);
            System.Console.Out.WriteLine("dentro de emparejar el preanalisis es:" + preanalisis);
            if (token == preanalisis)
            {
                lex.cuentaEspacios = 0;
                if (toke.Apuntador + 1 > lex.Input.Length)
                {
                    preanalisis = "$";
                    lexema = "$";
                    System.Console.Out.WriteLine("dentro de emparejar el nuevo lexema es:" + lexema);
                    System.Console.Out.WriteLine("dentro de emparejar el nuevo preanalisis es:" + preanalisis);
                    System.Console.Out.WriteLine("dentro de emparejar el ap es:" + toke.Apuntador);
                }
                else
                {
                    toke = lex.AnalizaRecursivo().Token;
                    preanalisis = toke.Tipo;
                    lexema = toke.Lexema;
                    System.Console.Out.WriteLine("dentro de emparejar el nuevo lexema es:" + lexema);
                    System.Console.Out.WriteLine("dentro de emparejar el nuevo preanalisis es:" + preanalisis);
                    System.Console.Out.WriteLine("dentro de emparejar el ap es:" + toke.Apuntador);
                }
            }
            else
            {
                System.Console.Out.WriteLine("hubo error en:" + lexema);
                form1.Consola1.Text += "consola> Error sintáctico en "+ lexema +" se esperaba " + token + "\n";
            }
        }

        public void EmparejarCadena()
        {
            toke = lex.AnalizaRecursivo().Token;
            preanalisis = toke.Tipo;
            lexema = toke.Lexema;
            System.Console.Out.WriteLine("dentro de emparejar el nuevo lexema es:" + lexema);
            System.Console.Out.WriteLine("dentro de emparejar el nuevo preanalisis es:" + preanalisis);
        }


    }
}
