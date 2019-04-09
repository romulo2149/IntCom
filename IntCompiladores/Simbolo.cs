using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntCompiladores
{
    class Simbolo
    {
        private string tipo;
        private string id;
        private Boolean esFuncion;
        private string valor;
        private string alcance;

        

        public string Tipo { get => tipo; set => tipo = value; }
        public string Id { get => id; set => id = value; }
        public bool EsFuncion { get => esFuncion; set => esFuncion = value; }
        public string Valor { get => valor; set => valor = value; }
        public string Alcance { get => alcance; set => alcance = value; }
    }
}
