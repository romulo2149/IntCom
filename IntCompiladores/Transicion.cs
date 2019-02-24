using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntCompiladores
{
    class Transicion
    {
        private string estadoInicial;
        private char simbolo;
        private string estadoFinal;

        public Transicion(string estadoInicial, char simbolo, string estadoFinal)
        {
            this.EstadoInicial = estadoInicial;
            this.Simbolo = simbolo;
            this.EstadoFinal = estadoFinal;
        }

        public string EstadoInicial { get => estadoInicial; set => estadoInicial = value; }
        public char Simbolo { get => simbolo; set => simbolo = value; }
        public string EstadoFinal { get => estadoFinal; set => estadoFinal = value; }

        public override string ToString()
        {
            return string.Format("∂({0}, {1}) -> {2}", EstadoInicial, Simbolo, EstadoFinal);
        }

    }
}
