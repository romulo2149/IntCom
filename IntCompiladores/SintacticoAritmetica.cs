using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntCompiladores
{
    class SintacticoAritmetica
    {
        private Lexico lex;
        private List<string> errores;
        public string preanalisis;
        Token toke;
        string lexema;
        Form1 form1;

        public List<string> Errores { get => errores; set => errores = value; }

        public SintacticoAritmetica(Lexico lex, Form1 f)
        {
            this.lex = lex;
            errores = new List<string>();
            toke = lex.AnalizaRecursivo().Token;
            preanalisis = toke.Tipo;
            lexema = toke.Lexema;
            form1 = f;
        }
        public void E()
        {
            System.Console.Out.WriteLine("dentro de e() el lexema es:" + lexema);
            System.Console.Out.WriteLine("dentro de e() el preanalisis es:" + preanalisis);
            if (preanalisis == "id" || preanalisis == "num" || preanalisis == "pA")
            {
                System.Console.Out.WriteLine("entro aca");
                T();
                Ep();
            }
            else
            {
                form1.Consola1.Text += "consola> Error en " + preanalisis + " en línea " + toke.Linea + ", Se esperaba un id, entero o ( \n";
            }
        }
   
        public void T()
        {
            System.Console.Out.WriteLine("dentro de t() el lexema es:" + lexema);
            System.Console.Out.WriteLine("dentro de t() el preanalisis es:" + preanalisis);
            if (preanalisis == "id" || preanalisis == "num" || preanalisis == "pA")
            {
                System.Console.Out.WriteLine("entro a t(if)");
                F();
                Tp();
            }
            else
            {
                form1.Consola1.Text += "consola> Error en " + preanalisis + " en línea " + toke.Linea + ", Se esperaba un id, entero o ( \n";
            }
        }

        public void Ep()
        {
            switch(preanalisis)
            {
                case "mas":
                    System.Console.Out.WriteLine("entro a Ep(mas)");
                    Emparejar("mas");
                    T();
                    Ep();
                    break;
                case "menos":
                    System.Console.Out.WriteLine("entro a Ep(menos)");
                    Emparejar("menos");
                    T();
                    Ep();
                    break;
                case "pC":
                    System.Console.Out.WriteLine("entro a Ep(pC)");
                    break;
                case "$":
                    System.Console.Out.WriteLine("entro a Ep($)");
                    break;
                default:
                    System.Console.Out.WriteLine("entro a Ep(default)");
                    form1.Consola1.Text += "consola> Error en " + preanalisis + " en línea " + toke.Linea + ", Se esperaba un +, -, ) o FINFICHERO \n";
                    break;
            }
        }

        public void Tp()
        {
            switch (preanalisis)
            {
                case "por":
                    System.Console.Out.WriteLine("entro a Tp(por)");
                    Emparejar("por");
                    F();
                    Tp();
                    break;
                case "entre":
                    System.Console.Out.WriteLine("entro a Tp(entre)");
                    Emparejar("entre");
                    F();
                    Tp();
                    break;
                case "mas":
                    System.Console.Out.WriteLine("entro a Tp(mas)");
                    Emparejar("mas");
                    F();
                    Tp();
                    break;
                case "menos":
                    System.Console.Out.WriteLine("entro a Tp(menos)");
                    Emparejar("menos");
                    F();
                    Tp();
                    break;
                case "pC":
                    System.Console.Out.WriteLine("entro a Tp(pC)");
                    break;
                case "$":
                    System.Console.Out.WriteLine("entro a Tp($)");
                    break;
                default:
                    System.Console.Out.WriteLine("entro a Tp(default)");
                    form1.Consola1.Text += "consola> Error en " + preanalisis + " en línea " + toke.Linea + ", Se esperaba un *, /, +, -, ) o FINFICHERO \n";
                    break;
            }
        }

        public void F()
        {
            System.Console.Out.WriteLine("entro a F()");
            switch (preanalisis)
            {
                case "id":
                    System.Console.Out.WriteLine("entro a F(id)");
                    Emparejar("id");
                    break;
                case "num":
                    System.Console.Out.WriteLine("entro a F(num)");
                    Emparejar("num");
                    break;
                case "pA":
                    System.Console.Out.WriteLine("entro a F(pA)");
                    Emparejar("pA");
                    E();
                    Emparejar("pC");
                    break;
                default:
                    System.Console.Out.WriteLine("entro a F(default)");
                    form1.Consola1.Text += "consola> Error en " + preanalisis + " en línea " + toke.Linea + ", Se esperaba un id, entero o ( \n";
                    break;
            }
        }

        public void Emparejar(String token)
        {
            if(token == preanalisis)
            {
                if (toke.Apuntador+1 > lex.Input.Length)
                {
                    preanalisis = "$";
                    lexema = "$" ;
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
                form1.Consola1.Text += "consola> Error en " + preanalisis + " en línea " + toke.Linea + ", Se esperaba un " +token + "\n";
            }
        }

    }
}
