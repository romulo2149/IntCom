using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntCompiladores
{
    class RespuestaLexico
    {
        private int numeroLinea;
        private int apuntador;
        private Token token;

        public RespuestaLexico(int numeroLinea, int apuntador, Token token)
        {
            this.NumeroLinea = numeroLinea;
            this.Apuntador = apuntador;
            this.Token = token;
        }

        public int NumeroLinea { get => numeroLinea; set => numeroLinea = value; }
        public int Apuntador { get => apuntador; set => apuntador = value; }
        public Token Token { get => token; set => token = value; }
    }
}
