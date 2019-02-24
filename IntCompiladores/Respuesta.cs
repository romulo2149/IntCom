using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntCompiladores
{
    class Respuesta
    {
        private Boolean estado;
        private string mensaje;
        private List<SalidaALexico> listaSalida;

        public Respuesta()
        {

        }

        public bool Estado { get => estado; set => estado = value; }
        public string Mensaje { get => mensaje; set => mensaje = value; }
        internal List<SalidaALexico> ListaSalida { get => listaSalida; set => listaSalida = value; }
    }
}
