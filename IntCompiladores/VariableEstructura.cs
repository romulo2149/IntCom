using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntCompiladores
{
    class VariableEstructura
    {
        private string tipoVarEstructura;
        private string nombreVarEstructura;
        private string valorVarEstructura;
        private int lineaVarEstructura;
        private string alcanceVarEstructura;
        private Campo campo1;
        private Campo campo2;
        private Campo campo3;

        public VariableEstructura(string tipoVarEstructura, string nombreVarEstructura, string valorVarEstructura, int lineaVarEstructura, string alcanceVarEstructura)
        {
            this.tipoVarEstructura = tipoVarEstructura;
            this.nombreVarEstructura = nombreVarEstructura;
            this.valorVarEstructura = valorVarEstructura;
            this.lineaVarEstructura = lineaVarEstructura;
            this.alcanceVarEstructura = alcanceVarEstructura;
        }

        public string NombreVarEstructura { get => nombreVarEstructura; set => nombreVarEstructura = value; }
        public string ValorVarEstructura { get => valorVarEstructura; set => valorVarEstructura = value; }
        public int LineaVarEstructura { get => lineaVarEstructura; set => lineaVarEstructura = value; }
        public string AlcanceVarEstructura { get => alcanceVarEstructura; set => alcanceVarEstructura = value; }
        public string TipoVarEstructura { get => tipoVarEstructura; set => tipoVarEstructura = value; }
        internal Campo Campo1 { get => campo1; set => campo1 = value; }
        internal Campo Campo2 { get => campo2; set => campo2 = value; }
        internal Campo Campo3 { get => campo3; set => campo3 = value; }
    }
}
