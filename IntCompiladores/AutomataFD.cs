using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntCompiladores
{
    class AutomataFD
    {
        private readonly List<string> ConjuntoEstados = new List<string>();
        private readonly List<char> Alfabeto = new List<char>();
        private readonly List<Transicion> Transiciones = new List<Transicion>();
        private string Q0;
        private readonly List<string> F = new List<string>();
        Respuesta respuesta = new Respuesta();

        public AutomataFD(List<string> conjuntoEstados, List<char> alfabeto, List<Transicion> transiciones, string q0, List<string> f)
        {
            ConjuntoEstados = conjuntoEstados;
            Alfabeto = alfabeto;
            Transiciones = transiciones;
            Q0 = q0;
            F = f;
        }

        public Respuesta AnalizarCadena(string input)
        {
            string estadoActual = Q0;
            for (int i = 0; i < input.Length; i++)
            {
                Transicion tran = Transiciones.Find(t => t.EstadoInicial == estadoActual
                                            && t.Simbolo == input[i]);  // busca si existe una transicion con el estado actual y el simbolo
                if (tran == null)
                {
                    respuesta.Estado = false;
                    respuesta.Mensaje = "Cadena '" + input + "' no aceptada";
                    return respuesta;
                }

                estadoActual = tran.EstadoFinal;
            }
            if(F.Contains(estadoActual))
            {
                respuesta.Estado = true;
                respuesta.Mensaje = "Cadena '"+ input +"' aceptada";
                return respuesta;
            }
            respuesta.Estado = false;
            respuesta.Mensaje = "Cadena NO aceptada \n Detenido en el estado: " + estadoActual + " el cual no es un estado final";
            return respuesta;
        }
    }
}
