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
    }
}
