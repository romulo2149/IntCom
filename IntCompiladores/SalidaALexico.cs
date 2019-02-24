using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntCompiladores
{
    class SalidaALexico
    {
        private string token;
        private string lexema;
        private int linea;
        private string error;

        public SalidaALexico()
        {

        }

        public string Token { get => token; set => token = value; }
        public string Lexema { get => lexema; set => lexema = value; }
        public int Linea { get => linea; set => linea = value; }
        public string Error { get => error; set => error = value; }
    }
}
