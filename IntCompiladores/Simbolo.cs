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
        private string nombre;
        private string valor;
        private string alcance;
        private int linea;

        public Simbolo(string nombre)
        {
            this.nombre = nombre;
        }

        public Simbolo(string tipo, string nombre, string valor, string alcance, int linea)
        {
            this.Tipo = tipo;
            this.Nombre = nombre;
            this.Valor = valor;
            this.Alcance = alcance;
            this.Linea = linea;
        }

        public string Tipo { get => tipo; set => tipo = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Valor { get => valor; set => valor = value; }
        public string Alcance { get => alcance; set => alcance = value; }
        public int Linea { get => linea; set => linea = value; }
    }
}
