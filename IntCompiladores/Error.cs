using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntCompiladores
{
    public class Error
    {
        private String error;
        public Form1 f1;
        public String DevuelveError(int erro)
        {
            switch(erro)
            {
                case 0:
                    this.error = "";
                    break;
                case 1:
                    this.error = "No existe transición válida para el caracter";
                    break;
                case 2:
                    this.error = "No existe el caracter en el alfabeto";
                    break;
            }

            return this.error;
        }

        public void NuevoError(string lexema, int linea, string esperado, Form1 form1)
        {
            form1.Consola1.Text += "consola> Error sintáctico encontrado en " + lexema +
                ", en la línea: " + linea + ", se esperaba " + esperado + ". \n";
        }
    }
}
