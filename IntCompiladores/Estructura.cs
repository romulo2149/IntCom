using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntCompiladores
{
    class Estructura
    {
        private string nombreEstructura;
        private List<Campo> listaCampos;

        public Estructura()
        {
            ListaCampos = new List<Campo>();
        }

        public string NombreEstructura { get => nombreEstructura; set => nombreEstructura = value; }
        internal List<Campo> ListaCampos { get => listaCampos; set => listaCampos = value; }
    }
}
