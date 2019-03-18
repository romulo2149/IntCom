using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntCompiladores
{
    public class Token
    {
        private string lexema;
        private int linea;
        private string tipo;
        private int error;
        private int apuntador;

        public Token(string lexema, int linea, string tipo, int error)
        {
            this.Lexema = lexema;
            this.Linea = linea;
            this.Tipo = tipo;
            this.Error = error;
        }

        public string Lexema { get => lexema; set => lexema = value; }
        public int Linea { get => linea; set => linea = value; }
        public string Tipo { get => tipo; set => tipo = value; }
        public int Error { get => error; set => error = value; }
        public int Apuntador { get => apuntador; set => apuntador = value; }
    }
}
