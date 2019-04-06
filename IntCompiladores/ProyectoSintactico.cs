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
        {
            Emparejar("PR_INSTRUCCIONES");
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


    }
}
