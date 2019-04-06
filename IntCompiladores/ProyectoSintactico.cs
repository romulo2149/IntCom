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
                        Emparejar("PR_INICIO");
                        break;

                    case "PR_ESTRUCTURAS":
                        Emparejar("PR_ESTRUCTURAS");
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
         * CONSTANTE -> ε
         * CONSTANTE -> id OP_ASIGNACION VALCONS() CONSTANTE()
         */
        {
            if(preanalisis == "PR_INICIO" || preanalisis == "PR_ESTRUCTURAS")
            {
                switch(preanalisis)
                {
                    case "PR_INICIO":
                        Emparejar("PR_INICIO");
                        break;

                    case "PR_ESTRUCTURAS":
                        ESTRUCTURAS();
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
            }
        }

        public void CAMPOS()
        {

        }

        public void ESTRUCTURASII()
        {

        }

        public void INSTRUCCIONES()
        {
            Emparejar("PR_INSTRUCCIONES");
        }

        public void Emparejar(string token)
        {
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
                form1.Consola1.Text += "consola> Error sintactico EN "+ preanalisis +" \n";
            }
        }


    }
}
