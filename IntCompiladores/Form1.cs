using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IntCompiladores
{
    public partial class Form1 : Form
    {
        String rutaArchivo;
        String nombre;
        String nombreInterprete = "Automata Mamalon";

        List<string> Q;
        List<char> Alfabeto;
        List<char> EstadoInicial;
        List<string> EstadoFinal;
        List<string> Nombres;
        List<Transicion> Transiciones;
        List<string> DATA;
        List<string> PalabrasR;
        public Form1()
        {
            
            InitializeComponent();
        }

        private void lenguajeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void ejecutarAnalizadorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Consola.Text = "";
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            int contadorError = 0;

            if (Editor.Text == "")
            {
                Consola.Text += "consola> No hay código que analizar \n";

            }
            else if (DATA == null)
            {
                Consola.Text += "consola> No hay un lenguaje cargado \n";

            }
            else
            {
                for (int i = 0; i < Transiciones.Count; i++)
                {
                    string inicial = Transiciones[i].EstadoInicial;
                    char simbolo = Transiciones[i].Simbolo;
                    Console.Out.WriteLine(inicial + " " + simbolo);
                    for (int j = i + 1; j < Transiciones.Count; j++)
                    {
                        if (inicial == Transiciones[j].EstadoInicial && simbolo == Transiciones[j].Simbolo)
                        {
                            contadorError++;
                        }
                    }

                    Console.Out.WriteLine(contadorError);
                }
                if (contadorError < 1)
                {
                    Console.Out.WriteLine("Conjunto estados");
                    for (int i = 0; i < Q.Count; i++)
                    {
                        Console.WriteLine(Q[i]);
                    }
                    Console.Out.WriteLine("Alfabeto");
                    for (int i = 0; i < Alfabeto.Count; i++)
                    {
                        Console.WriteLine(Alfabeto[i]);
                    }
                    Console.Out.WriteLine("Estado Inicial");
                    for (int i = 0; i < EstadoInicial.Count; i++)
                    {
                        Console.WriteLine(EstadoInicial[i]);
                    }
                    Console.Out.WriteLine("Estado Final");
                    for (int i = 0; i < EstadoFinal.Count; i++)
                    {
                        Console.WriteLine(EstadoFinal[i]);
                    }

                    for (int i = 0; i < Transiciones.Count; i++)
                    {
                        Console.WriteLine(Transiciones[i]);
                    }
                    String aux = EstadoInicial[0] + "";
                    Console.Out.WriteLine(aux);
                    dataGridView1.ColumnCount = 4;
                    dataGridView1.Columns[0].Name = "Token";
                    dataGridView1.Columns[1].Name = "Lexema";
                    dataGridView1.Columns[2].Name = "Linea";
                    dataGridView1.Columns[3].Name = "Error";
                    dataGridView1.Columns[0].Width = 110;
                    dataGridView1.Columns[1].Width = 150;
                    dataGridView1.Columns[2].Width = 50;
                    dataGridView1.Columns[3].Width = 550;

                    dataGridView2.ColumnCount = 5;
                    dataGridView2.Columns[0].Name = "Nombre";
                    dataGridView2.Columns[1].Name = "Tipo";
                    dataGridView2.Columns[2].Name = "Valor";
                    dataGridView2.Columns[3].Name = "Linea";
                    dataGridView2.Columns[4].Name = "Alcance";
                    dataGridView2.Columns[0].Width = 150;
                    dataGridView2.Columns[1].Width = 150;
                    dataGridView2.Columns[2].Width = 150;
                    dataGridView2.Columns[3].Width = 50;
                    dataGridView2.Columns[4].Width = 150;
                    var MEFD = new Lexico(Q, Alfabeto, Transiciones, aux, EstadoFinal, Nombres, PalabrasR, Editor.Text.TrimEnd());
                    var ExpLex = new AnalizaExpresion(Q, Alfabeto, Transiciones, aux, EstadoFinal, Nombres, PalabrasR);
                    ProyectoSintactico ps = new ProyectoSintactico(MEFD, ExpLex, this);
                    ps.PROGRAMA();

                    Consola.Text += "consola> Tabla de Símbolos: \n";

                    foreach (KeyValuePair <string, Simbolo> fila in ps.ps.TablaSimbolos)
                    {
                        /*Consola.Text += "consola> Nombre: " + fila.Value.Nombre + " | Tipo: " + fila.Value.Tipo +
                            " | Valor: " + fila.Value.Valor + " | Alcance: " + fila.Value.Alcance +
                            " | Línea: " + fila.Value.Linea + " \n";*/
                        Consola.Text += "consola>"+ fila.Key + " \n";
                        dataGridView2.Rows.Add(fila.Value.Nombre, fila.Value.Tipo, fila.Value.Valor, fila.Value.Linea, fila.Value.Alcance);
                    }
                    foreach (KeyValuePair<string, VariableEstructura> fila in ps.ps.EstructuraSimbolos)
                    {
                        /*Consola.Text += "consola> Nombre: " + fila.Value.Nombre + " | Tipo: " + fila.Value.Tipo +
                            " | Valor: " + fila.Value.Valor + " | Alcance: " + fila.Value.Alcance +
                            " | Línea: " + fila.Value.Linea + " \n";*/
                        Consola.Text += "consola>" + fila.Key + " \n";
                        dataGridView2.Rows.Add(fila.Value.NombreVarEstructura, fila.Value.TipoVarEstructura, fila.Value.ValorVarEstructura, fila.Value.LineaVarEstructura, fila.Value.AlcanceVarEstructura);
                        if(fila.Value.Campo1 != null)
                        {
                            dataGridView2.Rows.Add(fila.Value.Campo1.NombreCampo, fila.Value.Campo1.TipoCampo, fila.Value.Campo1.ValorCampo, fila.Value.LineaVarEstructura, fila.Value.Campo1.EstructuraPadre);
                        }
                        if(fila.Value.Campo2 != null)
                        {
                            dataGridView2.Rows.Add(fila.Value.Campo2.NombreCampo, fila.Value.Campo2.TipoCampo, fila.Value.Campo2.ValorCampo, fila.Value.LineaVarEstructura, fila.Value.Campo2.EstructuraPadre);
                        }
                        if(fila.Value.Campo3 != null)
                        {
                            dataGridView2.Rows.Add(fila.Value.Campo3.NombreCampo, fila.Value.Campo3.TipoCampo, fila.Value.Campo3.ValorCampo, fila.Value.LineaVarEstructura, fila.Value.Campo3.EstructuraPadre);
                        }
                        
                        
                    }


                }
                else
                {
                    Consola.Text += "consola> El autómata no es determinista \n";
                }
            }
        }


        /*
         * 
         * presentacion
         * integrantes
         * indice
         * introduccion que entregamos
         * analisis que usamos, arquitectura del sistema, como funciona, que lenguaje
         * desarrollo, diseño, diagrama de clases, entradas salidas, pruebas
         * conclusiones, alcances, limitaciones (codigo, gui)
         * referencias
         * 
         * */

        private void lenguajeToolStripMenuItem2_Click(object sender, EventArgs e)
        {

            rutaArchivo = "";
            nombre = "";
            Stream Flujo;
            OpenFileDialog DialogoAbrirArchivo = new OpenFileDialog();
            DialogoAbrirArchivo.InitialDirectory = @"C:\Users\romulo\Desktop\Proyecto";
            DialogoAbrirArchivo.Filter = "(*.lt2)|*.lt2";
            if (DialogoAbrirArchivo.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if ((Flujo = DialogoAbrirArchivo.OpenFile()) != null)
                {
                    rutaArchivo = DialogoAbrirArchivo.FileName;
                    nombre = DialogoAbrirArchivo.SafeFileName;

                    int counter = 0;
                    string line;
                    Q = new List<string>();
                    Alfabeto = new List<char>();
                    Alfabeto.Add('\n');
                    Alfabeto.Add('\t');
                    Alfabeto.Add(' ');
                    Nombres = new List<string>();
                    EstadoInicial = new List<char>();
                    EstadoFinal = new List<string>();
                    Transiciones = new List<Transicion>();
                    PalabrasR = new List<string>();
                    DATA = new List<string>();
                    StreamReader file = new System.IO.StreamReader(rutaArchivo);
                    while ((line = file.ReadLine()) != null)
                    {
                        DATA.Add(line);
                        counter++;
                    }
                    file.Close();

                    for (int i = 0; i < DATA.Count - 1; i++)
                    {
                        switch (i)
                        {
                            case 1:
                                foreach (var s in DATA[i].Split(','))
                                {
                                    Q.Add(s);
                                }
                                break;
                            case 3:
                                foreach (var s in DATA[i].Split(','))
                                {
                                    Alfabeto.Add(s[0]);
                                }
                                break;
                            case 5:
                                foreach (var s in DATA[i].Split(','))
                                {
                                    EstadoInicial.Add(s[0]);
                                }
                                break;
                            case 7:
                                foreach (var s in DATA[i].Split(','))
                                {
                                    EstadoFinal.Add(s);
                                }
                                break;
                            case 9:
                                foreach (var s in DATA[i].Split(','))
                                {
                                    PalabrasR.Add(s);
                                }
                                break;
                            case 11:
                                foreach (var s in DATA[i].Split(','))
                                {
                                    Nombres.Add(s);
                                    int index = s.IndexOf(':');
                                    string izq = s.Substring(0, index);
                                    string der = s.Substring(index + 1, (s.Length) - (index + 1));
                                    Console.WriteLine(izq + " " + der);
                                }
                                break;
                            default:
                                if (i > 12)
                                {
                                    String[] transicion = new String[3];
                                    int count = 0;
                                    foreach (string s in DATA[i].Split(','))
                                    {
                                        transicion[count] = s;
                                        count++;
                                    }
                                    Transiciones.Add(new Transicion(transicion[0], (transicion[1])[0], transicion[2]));

                                }

                                break;
                        }
                    }
                    String Qinfo = "Q = { ";
                    for (int i = 0; i < Q.Count; i++)
                    {
                        if (i == Q.Count - 1)
                        {
                            Qinfo = Qinfo + Q[i] + " }";
                        }
                        else
                        {
                            Qinfo = Qinfo + Q[i] + ",";
                        }
                    }

                    String AlfabetoInfo = "∑ = { ";
                    for (int i = 0; i < Alfabeto.Count; i++)
                    {
                        if (i == Alfabeto.Count - 1)
                        {
                            AlfabetoInfo = AlfabetoInfo + Alfabeto[i] + " }";
                        }
                        else
                        {
                            AlfabetoInfo = AlfabetoInfo + Alfabeto[i] + ",";
                        }
                    }

                    String q0info = "q0 = { " + EstadoInicial[0] + " }";
                    String Finfo = "F = { ";
                    for (int i = 0; i < EstadoFinal.Count; i++)
                    {
                        if (i == EstadoFinal.Count - 1)
                        {
                            Finfo = Finfo + EstadoFinal[i] + " }";
                        }
                        else
                        {
                            Finfo = Finfo + EstadoFinal[i] + ",";
                        }
                    }
                    String transicionesinfo = "Transiciones: \n";
                    for (int i = 0; i < Transiciones.Count; i++)
                    {
                        transicionesinfo = transicionesinfo + Transiciones[i] + "\n";
                    }
                    String showData = Qinfo + "\n" + AlfabetoInfo + "\n" + q0info + "\n" + Finfo + "\n" + transicionesinfo;
                    
                    Form1.ActiveForm.Text = nombre + " - " + nombreInterprete;
                    Consola.Text += "consola> Lenguaje cargado \n";
                }
                else
                {
                    Consola.Text += "consola> Error al abrir el archivo \n";

                }
            }
            else
            {
                Consola.Text += "consola> No se ha seleccionado archivo \n";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Consola.Text = "consola> ... \n";
        }

        private void autómataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rutaArchivo = "";
            nombre = "";
            Stream Flujo;
            OpenFileDialog DialogoAbrirArchivo = new OpenFileDialog();
            DialogoAbrirArchivo.Filter = "(*.lt2)|*.lt2";
            if (DialogoAbrirArchivo.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if ((Flujo = DialogoAbrirArchivo.OpenFile()) != null)
                {
                    rutaArchivo = DialogoAbrirArchivo.FileName;
                    nombre = DialogoAbrirArchivo.SafeFileName;

                    int counter = 0;
                    string line;
                    Q = new List<string>();
                    Alfabeto = new List<char>();
                    EstadoInicial = new List<char>();
                    EstadoFinal = new List<string>();
                    Transiciones = new List<Transicion>();
                    DATA = new List<string>();
                    StreamReader file = new System.IO.StreamReader(rutaArchivo);
                    while ((line = file.ReadLine()) != null)
                    {
                        DATA.Add(line);
                        counter++;
                    }
                    file.Close();

                    for (int i = 0; i < DATA.Count - 1; i++)
                    {
                        switch (i)
                        {
                            case 1:
                                foreach (var s in DATA[i].Split(','))
                                {
                                    Q.Add(s);
                                }
                                break;
                            case 3:
                                foreach (var s in DATA[i].Split(','))
                                {
                                    Alfabeto.Add(s[0]);
                                }
                                break;
                            case 5:
                                foreach (var s in DATA[i].Split(','))
                                {
                                    EstadoInicial.Add(s[0]);
                                }
                                break;
                            case 7:
                                foreach (var s in DATA[i].Split(','))
                                {
                                    EstadoFinal.Add(s);
                                }
                                break;
                            default:
                                if (i > 8)
                                {
                                    String[] transicion = new String[3];
                                    int count = 0;
                                    foreach (string s in DATA[i].Split(','))
                                    {
                                        transicion[count] = s;
                                        count++;
                                    }
                                    Transiciones.Add(new Transicion(transicion[0], (transicion[1])[0], transicion[2]));

                                }

                                break;
                        }

                    }
                    Consola.Text += "consola> Autómata cargado... \n";

                    String Qinfo = "Q = { ";
                    for (int i = 0; i < Q.Count; i++)
                    {
                        if (i == Q.Count - 1)
                        {
                            Qinfo = Qinfo + Q[i] + " }";
                        }
                        else
                        {
                            Qinfo = Qinfo + Q[i] + ",";
                        }
                    }

                    String AlfabetoInfo = "∑ = { ";
                    for (int i = 0; i < Alfabeto.Count; i++)
                    {
                        if (i == Alfabeto.Count - 1)
                        {
                            AlfabetoInfo = AlfabetoInfo + Alfabeto[i] + " }";
                        }
                        else
                        {
                            AlfabetoInfo = AlfabetoInfo + Alfabeto[i] + ",";
                        }
                    }

                    String q0info = "q0 = { " + EstadoInicial[0] + " }";
                    String Finfo = "F = { ";
                    for (int i = 0; i < EstadoFinal.Count; i++)
                    {
                        if (i == EstadoFinal.Count - 1)
                        {
                            Finfo = Finfo + EstadoFinal[i] + " }";
                        }
                        else
                        {
                            Finfo = Finfo + EstadoFinal[i] + ",";
                        }
                    }
                    String transicionesinfo = "Transiciones: \n";
                    for (int i = 0; i < Transiciones.Count; i++)
                    {
                        transicionesinfo = transicionesinfo + Transiciones[i] + "\n";
                    }
                    String showData = Qinfo + "\n" + AlfabetoInfo + "\n" + q0info + "\n" + Finfo + "\n" + transicionesinfo;
                    //Datos.Text = showData;
                    
                    Form1.ActiveForm.Text = nombre + " - " + nombreInterprete;
                }
                else
                {
                    Consola.Text += "consola> Error abrir Archivo \n";

                }
            }
            else
            {
                Consola.Text += "consola> No se ha seleccionado un archivo \n";
            }
        }

        private void ejecutarConAutómataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int contadorError = 0;

            if (DATA == null)
            {
                Consola.Text += "consola> No se ha cargado un autómata  \n";

            }
            else
            {
                for (int i = 0; i < Transiciones.Count; i++)
                {
                    string inicial = Transiciones[i].EstadoInicial;
                    char simbolo = Transiciones[i].Simbolo;
                    Console.Out.WriteLine(inicial + " " + simbolo);
                    for (int j = i + 1; j < Transiciones.Count; j++)
                    {
                        if (inicial == Transiciones[j].EstadoInicial && simbolo == Transiciones[j].Simbolo)
                        {
                            contadorError++;
                        }
                    }

                    Console.Out.WriteLine(contadorError);
                }
                if (contadorError < 1)
                {
                    Console.Out.WriteLine("Conjunto estados");
                    for (int i = 0; i < Q.Count; i++)
                    {
                        Console.WriteLine(Q[i]);
                    }
                    Console.Out.WriteLine("Alfabeto");
                    for (int i = 0; i < Alfabeto.Count; i++)
                    {
                        Console.WriteLine(Alfabeto[i]);
                    }
                    Console.Out.WriteLine("Estado Inicial");
                    for (int i = 0; i < EstadoInicial.Count; i++)
                    {
                        Console.WriteLine(EstadoInicial[i]);
                    }
                    Console.Out.WriteLine("Estado Final");
                    for (int i = 0; i < EstadoFinal.Count; i++)
                    {
                        Console.WriteLine(EstadoFinal[i]);
                    }

                    for (int i = 0; i < Transiciones.Count; i++)
                    {
                        Console.WriteLine(Transiciones[i]);
                    }
                    String aux = EstadoInicial[0] + "";
                    Console.Out.WriteLine(aux);
                    var MEFD = new AutomataFD(Q, Alfabeto, Transiciones, aux, EstadoFinal);
                    Respuesta res = new Respuesta();
                    res = MEFD.AnalizarCadena(Editor.Text);

                    Consola.Text += "consola> "+res.Mensaje+"\n";
                }
                else
                {
                    Consola.Text += "consola> El automata no es determinista \n";
                }
            }
        }

        private void códigoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream Flujo;
            OpenFileDialog DialogoAbrirArchivo = new OpenFileDialog();
            DialogoAbrirArchivo.Filter = "(*.txt)|*.txt";
            DialogoAbrirArchivo.InitialDirectory = @"C:\Users\romulo\Desktop\Proyecto";
            if (DialogoAbrirArchivo.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if ((Flujo = DialogoAbrirArchivo.OpenFile()) != null)
                {
                    rutaArchivo = DialogoAbrirArchivo.FileName;
                    nombre = DialogoAbrirArchivo.SafeFileName;
                    String textoArchivo = File.ReadAllText(rutaArchivo);
                    Editor.Text = textoArchivo;
                    Form1.ActiveForm.Text = nombre + " - " + nombreInterprete;
                    Flujo.Close();
                }
                else
                {
                    Consola.Text += "consola> Error al abrir archivo \n";
                }
            }
            else
            {
                Consola.Text += "consola> Error al abrir archivo \n"; 
            }
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        
    }
}
