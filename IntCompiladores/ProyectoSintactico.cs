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
        public string preanalisis;
        Token toke;
        string lexema;
        Form1 form1;

        public ProyectoSintactico(Lexico lex, Form1 f)
        {
            this.lex = lex;
            toke = lex.AnalizaRecursivo().Token;
            preanalisis = toke.Tipo;
            lexema = toke.Lexema;
            form1 = f;
        }

        public void PROGRAMA() 
        /*
         * PROGRAMA -> PR_PROGRAMA ID CONSTANTES() ESTRUCTURAS() PR_INICIO INSTRUCCIONES() PR_FIN { PR_PROGRAMA }
         */
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
                Emparejar("ID");
                Emparejar("OP_ASIGNACION");
                VALCONS();
                CONSTANTE();
            }
           else
            {
                form1.Consola1.Text += "consola> Error Sintáctico \n";
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
                Emparejar("ID");
                Emparejar("OP_ASIGNACION");
                VALCONS();
                CONSTANTE();
            }
            else
            {
                form1.Consola1.Text += "consola> Error Sintáctico \n";
            }
        }

        public void VALCONS()
        /*
         * VALCONS -> ENTERO { ENTERO }
         * VALCONS -> COMILLA ID COMILLA { S_COMILLA }
         */
        {
            switch (preanalisis)
            {
                case "ENTERO":
                    Emparejar("ENTERO");
                    break;

                case "S_COMILLA":
                    Emparejar("S_COMILLA");
                    Emparejar("ID");
                    Emparejar("S_COMILLA");
                    break;

                default:
                    form1.Consola1.Text += "consola> Error Sintáctico \n";
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
                    Emparejar("ID");
                    Emparejar("OP_ASIGNACION");
                    Emparejar("LLAVE_IZQ");
                    CAMPOS();
                    Emparejar("LLAVE_DER");
                    ESTRUCTURASII();
                    break;

                case "PR_INICIO":
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
                Emparejar("ID");
                SEP();
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
                    Emparejar("PR_ENTERO");
                    break;

                case "PR_CHAR":
                    Emparejar("PR_CHAR");
                    break;

                case "PR_APUNTADOR":
                    Emparejar("PR_APUNTADOR");
                    break;
            }
        }

        public void SEP()
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
                    Emparejar("ID");
                    SEP2();
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
                Emparejar("ID");
                SEP3();
            }
            else if(preanalisis == "LLAVE_DER")
            {

            }
        }

        public void SEP2()
        /*
         * SEP2 -> S_PUNTOCOMA CAMPO3() { S_PUNTOCOMA }
         * SEP2 -> S_COMA ID S_PUNTOCOMA { S_COMA }
         */
        {
            switch(preanalisis)
            {
                case "S_PUNTOCOMA":
                    Emparejar("S_PUNTOCOMA");
                    CAMPO3();
                    break;

                case "S_COMA":
                    Emparejar("S_COMA");
                    Emparejar("ID");
                    Emparejar("S_PUNTOCOMA");
                    break;
            }
        }

        public void SEP3()
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
                    Emparejar("ID");
                    Emparejar("S_PUNTOCOMA");
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
                Emparejar("ID");
                Emparejar("S_PUNTOCOMA");
            }
            else if (preanalisis == "LLAVE_DER")
            {

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
                    form1.Consola1.Text += "consola> Error Sintáctico \n";
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
                Emparejar("ID");
                VARSTR();
            }
        }

        public void VARSTR()
        /*
         * VARSTR -> ID VAR() { ID }
         * VARSTR -> OP_ASIGNACION LLAVE_IZQ CAMPOS() LLAVE_DER VARC() { OP_ASIGNACION }
         */
        {
            switch(preanalisis)
            {
                case "ID":
                    Emparejar("ID");
                    VARF();
                    break;

                case "OP_ASIGNACION":
                    Emparejar("OP_ASIGNACION");
                    Emparejar("LLAVE_IZQ");
                    CAMPOS();
                    Emparejar("LLAVE_DER");
                    VARC();
                    break;
            }
        }

        public void VARC()
        /*
         * VARC -> ID ID VAR()
         */
        {
            if(preanalisis == "ID")
            {
                Emparejar("ID");
                Emparejar("ID");
                VARF();
            }
        }

        public void VARF()
        /*
         * VAR -> VAR2() S_PUNTOCOMA VAR3() { S_PUNTOCOMA, S_COMA }
         */
        {
            if(preanalisis == "S_PUNTOCOMA" || preanalisis == "S_COMA")
            {
                switch(preanalisis)
                {
                    case "S_PUNTOCOMA":
                        System.Console.Out.WriteLine("detecto el punto y coma" );
                        VAR2();
                        Emparejar("S_PUNTOCOMA");
                        VAR3();
                        break;
                    case "S_COMA":
                        VAR2();
                        Emparejar("S_PUNTOCOMA");
                        VAR3();
                        break;
                    default:
                        break;
                }
            }
        }

        public void VAR2()
        /*
         * VAR2 -> S_COMA ID VAR2() { S_COMA }
         * VAR2 -> ε { S_PUNTOCOMA }
         */
        {
            switch(preanalisis)
            {
                case "S_COMA":
                    Emparejar("S_COMA");
                    Emparejar("ID");
                    VAR2();
                    break;

                case "S_PUNTOCOMA":
                    break;
            }
        }


        public void VAR3()
        /*
         * VAR3 -> ID ID VAR2() S_PUNTOCOMA VAR3() { ID }
         * VAR3 -> ε { INICIO }
         */
        {
            switch(preanalisis)
            {
                case "ID":
                    Emparejar("ID");
                    Emparejar("ID");
                    VAR2();
                    Emparejar("S_PUNTOCOMA"); //duda
                    VAR3();
                    break;

                case "PR_INICIO":
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
                OPERANDO();
                OPERADOR_CONDICION();
                OPERANDO();
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
                    Emparejar("OP_SUMA");
                    break;

                case "OP_RESTA":
                    Emparejar("OP_RESTA");
                    break;

                case "OP_MULTIPLICACION":
                    Emparejar("OP_MULTIPLICACION");
                    break;

                case "OP_DIVISION":
                    Emparejar("OP_DIVISION");
                    break;

                case "OP_MODULO":
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
                    Emparejar("ENTERO");
                    break;

                case "ID":
                    Emparejar("ID");
                    break;

                case "S_COMILLA":
                    Emparejar("S_COMILLA");
                    Emparejar("ID");
                    Emparejar("S_COMILLA");
                    break;

                case "PR_NULL":
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
                Emparejar("S_PUNTO");
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
                Emparejar("S_PUNTOCOMA");
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
                    EmparejarCadena();
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
            if(preanalisis == "ID")
            {
                Emparejar("ID");
                TEXTSTR2();
            }
        }


        public void O1()
        /*
         * O1 -> OPERANDO()
         */
        {
            if (preanalisis == "ENTERO" || preanalisis == "S_COMILLA" || preanalisis == "ID")
            {
                OPERANDO();
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

        public void TEXTSTR2()
        /*
         * TEXTSTR2 -> S_PUNTO ID OP_ASIGNACION OPERANDON() S_PUNTOCOMA { S_PUNTO }
         * TEXTSTR2 -> OP_ASIGNACION O1() O2() S_PUNTOCOMA { OP_ASIGNACION }
         */
        {
            switch (preanalisis)
            {
                case "S_PUNTO":
                    Emparejar("S_PUNTO");
                    Emparejar("ID");
                    Emparejar("OP_ASIGNACION");
                    OPERANDON();
                    Emparejar("S_PUNTOCOMA");
                    break;

                case "OP_ASIGNACION":
                    Emparejar("OP_ASIGNACION");
                    O1();
                    O2();
                    Emparejar("S_PUNTOCOMA");
                    break;
            }
        }
        public void OPERANDON()
        /*
         * OPERANDON -> ENTERO { ENTERO }
         * OPERANDON -> ID { ID }
         * OPERANDON -> S_COMILLA ID S_COMILLA { S_COMILLA }
         * OPERANDON -> PR_NULL { PR_NULL }
         */
        {
            switch (preanalisis)
            {
                case "ENTERO":
                    Emparejar("ENTERO");
                    break;

                case "ID":
                    Emparejar("ID");
                    break;

                case "S_COMILLA":
                    Emparejar("S_COMILLA");
                    Emparejar("ID");
                    Emparejar("S_COMILLA");
                    break;

                case "PR_NULL":
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
            System.Console.Out.WriteLine("dentro de emparejar el token esperado es:" + token);
            System.Console.Out.WriteLine("dentro de emparejar el preanalisis es:" + preanalisis);
            if (token == preanalisis)
            {
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
                form1.Consola1.Text += "consola> Error sintactico EN "+ lexema +" \n";
            }
        }

        public void EmparejarCadena()
        {
            toke = lex.AnalizaRecursivo().Token;
            preanalisis = toke.Tipo;
            lexema = toke.Lexema;
        }


    }
}
