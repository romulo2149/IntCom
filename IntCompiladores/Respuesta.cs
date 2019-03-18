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

        public Respuesta()
        {

        }

        public bool Estado { get => estado; set => estado = value; }
        public string Mensaje { get => mensaje; set => mensaje = value; }
    }
}
