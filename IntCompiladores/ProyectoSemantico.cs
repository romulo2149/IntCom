using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntCompiladores
{
    class ProyectoSemantico
    {
        private Dictionary<string, Simbolo> tablaSimbolos;
        private Simbolo simbolo;
        private Form1 form;

        
        public ProyectoSemantico(Form1 form)
        {
            TablaSimbolos = new Dictionary<string, Simbolo>();
            Form = form;
        }

        public void nuevoSimbolo(string nombreSimbolo, string tipoSimbolo, string valorSimbolo, string alcanceSimbolo,
                                    int longitudSimbolo, int lineaSimbolo)
        {
            if(existeSimbolo(nombreSimbolo) == false)
            {
                System.Console.Out.WriteLine("Se agrego la variable: " + nombreSimbolo);
                simbolo = new Simbolo(tipoSimbolo, nombreSimbolo, valorSimbolo, alcanceSimbolo, lineaSimbolo);
                TablaSimbolos.Add(nombreSimbolo, simbolo);
            }
            else
            {
                form.Consola1.Text += "consola> Error semántico, la variable " + nombreSimbolo + " (línea " + lineaSimbolo + ") ya fue declarada anteriormente \n";
            }
        }

        public bool existeSimbolo(string nombreSimbolo)
        {
            if(TablaSimbolos.ContainsKey(nombreSimbolo))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Form1 Form { get => form; set => form = value; }
        internal Dictionary<string, Simbolo> TablaSimbolos { get => tablaSimbolos; set => tablaSimbolos = value; }
    }
}
