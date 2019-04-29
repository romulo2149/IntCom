using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntCompiladores
{
    class Grafico
    {
        private List<VariableEstructura> lista1;
        private List<VariableEstructura> lista2;

        public Grafico()
        {
            lista1 = new List<VariableEstructura>();
            lista2 = new List<VariableEstructura>();
            
            }
        public void pintarEstructura(VariableEstructura variable, Form1 form1, int str)
        {
            Pen p = new Pen(Color.Black);
            SolidBrush s = new SolidBrush(Color.Red);
            Graphics g = form1.Panel1.CreateGraphics();
            switch (str)
            {
                case 1:
                    lista1.Add(variable);
                    int numero = (lista1.Count - 1) * 220;
                    g.DrawRectangle(p, new Rectangle(numero, 30, 180, 60));
                    g.DrawRectangle(p, new Rectangle(numero, 30, 60, 60));
                    g.DrawRectangle(p, new Rectangle(numero + 60, 30, 60, 60));
                    g.DrawRectangle(p, new Rectangle(numero + 120, 30, 60, 60));
                    
                    break;
                case 2:
                    lista2.Add(variable);
                    numero = (lista2.Count - 1) * 220;
                    s = new SolidBrush(Color.Blue);
                    g.DrawRectangle(p, new Rectangle(numero, 180, 180, 60));
                    g.DrawRectangle(p, new Rectangle(numero, 180, 60, 60));
                    g.DrawRectangle(p, new Rectangle(numero+60, 180, 60, 60));
                    g.DrawRectangle(p, new Rectangle(numero+120, 180, 60, 60));
                    break;
            }
        }

        internal List<VariableEstructura> Lista1 { get => lista1; set => lista1 = value; }
        internal List<VariableEstructura> Lista2 { get => lista2; set => lista2 = value; }
    }
}
