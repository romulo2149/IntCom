﻿using System.Windows.Forms;

namespace IntCompiladores
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TabControl tabControl1;
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.Consola = new System.Windows.Forms.RichTextBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.Errores = new System.Windows.Forms.RichTextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nuevoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.códigoToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.lenguajeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lenguajeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.códigoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lenguajeToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.autómataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.guardarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarComoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ejecuciónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ejecutarAnalizadorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ejecutarConAutómataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configuraciónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.formatoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ayudaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Editor = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            tabControl1 = new System.Windows.Forms.TabControl();
            tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(this.tabPage2);
            tabControl1.Controls.Add(this.tabPage1);
            tabControl1.Controls.Add(this.tabPage4);
            tabControl1.Controls.Add(this.tabPage3);
            tabControl1.Location = new System.Drawing.Point(12, 469);
            tabControl1.Multiline = true;
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new System.Drawing.Size(1345, 243);
            tabControl1.TabIndex = 2;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.Consola);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1337, 217);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Consola";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // Consola
            // 
            this.Consola.BackColor = System.Drawing.SystemColors.MenuText;
            this.Consola.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Consola.ForeColor = System.Drawing.SystemColors.Info;
            this.Consola.Location = new System.Drawing.Point(0, 0);
            this.Consola.Name = "Consola";
            this.Consola.ReadOnly = true;
            this.Consola.Size = new System.Drawing.Size(1341, 217);
            this.Consola.TabIndex = 0;
            this.Consola.Text = "";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1337, 217);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Salida";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(1, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1333, 217);
            this.dataGridView1.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.dataGridView2);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(1337, 217);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Tabla Símbolos";
            this.tabPage4.UseVisualStyleBackColor = true;
            this.tabPage4.Click += new System.EventHandler(this.tabPage4_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AllowUserToResizeColumns = false;
            this.dataGridView2.AllowUserToResizeRows = false;
            this.dataGridView2.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(0, 0);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(1337, 217);
            this.dataGridView2.TabIndex = 1;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.Errores);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1337, 217);
            this.tabPage3.TabIndex = 4;
            this.tabPage3.Text = "Errores";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // Errores
            // 
            this.Errores.BackColor = System.Drawing.SystemColors.MenuText;
            this.Errores.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Errores.ForeColor = System.Drawing.SystemColors.Info;
            this.Errores.Location = new System.Drawing.Point(-4, 0);
            this.Errores.Name = "Errores";
            this.Errores.ReadOnly = true;
            this.Errores.Size = new System.Drawing.Size(1341, 225);
            this.Errores.TabIndex = 1;
            this.Errores.Text = "";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem,
            this.ejecuciónToolStripMenuItem,
            this.configuraciónToolStripMenuItem,
            this.formatoToolStripMenuItem,
            this.ayudaToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1370, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevoToolStripMenuItem,
            this.abrirToolStripMenuItem,
            this.toolStripSeparator1,
            this.guardarToolStripMenuItem,
            this.guardarComoToolStripMenuItem,
            this.toolStripSeparator2,
            this.salirToolStripMenuItem});
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.archivoToolStripMenuItem.Text = "Archivo";
            // 
            // nuevoToolStripMenuItem
            // 
            this.nuevoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.códigoToolStripMenuItem1,
            this.lenguajeToolStripMenuItem,
            this.lenguajeToolStripMenuItem1});
            this.nuevoToolStripMenuItem.Name = "nuevoToolStripMenuItem";
            this.nuevoToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.nuevoToolStripMenuItem.Text = "Nuevo";
            // 
            // códigoToolStripMenuItem1
            // 
            this.códigoToolStripMenuItem1.Name = "códigoToolStripMenuItem1";
            this.códigoToolStripMenuItem1.Size = new System.Drawing.Size(192, 22);
            this.códigoToolStripMenuItem1.Text = "Documento en Blanco";
            // 
            // lenguajeToolStripMenuItem
            // 
            this.lenguajeToolStripMenuItem.Name = "lenguajeToolStripMenuItem";
            this.lenguajeToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.lenguajeToolStripMenuItem.Text = "Autómata vacío";
            this.lenguajeToolStripMenuItem.Click += new System.EventHandler(this.lenguajeToolStripMenuItem_Click);
            // 
            // lenguajeToolStripMenuItem1
            // 
            this.lenguajeToolStripMenuItem1.Name = "lenguajeToolStripMenuItem1";
            this.lenguajeToolStripMenuItem1.Size = new System.Drawing.Size(192, 22);
            this.lenguajeToolStripMenuItem1.Text = "Lenguaje vacío";
            // 
            // abrirToolStripMenuItem
            // 
            this.abrirToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.códigoToolStripMenuItem,
            this.lenguajeToolStripMenuItem2,
            this.autómataToolStripMenuItem});
            this.abrirToolStripMenuItem.Name = "abrirToolStripMenuItem";
            this.abrirToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.abrirToolStripMenuItem.Text = "Abrir";
            // 
            // códigoToolStripMenuItem
            // 
            this.códigoToolStripMenuItem.Name = "códigoToolStripMenuItem";
            this.códigoToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.códigoToolStripMenuItem.Text = "Código";
            this.códigoToolStripMenuItem.Click += new System.EventHandler(this.códigoToolStripMenuItem_Click);
            // 
            // lenguajeToolStripMenuItem2
            // 
            this.lenguajeToolStripMenuItem2.Name = "lenguajeToolStripMenuItem2";
            this.lenguajeToolStripMenuItem2.Size = new System.Drawing.Size(127, 22);
            this.lenguajeToolStripMenuItem2.Text = "Lenguaje";
            this.lenguajeToolStripMenuItem2.Click += new System.EventHandler(this.lenguajeToolStripMenuItem2_Click);
            // 
            // autómataToolStripMenuItem
            // 
            this.autómataToolStripMenuItem.Name = "autómataToolStripMenuItem";
            this.autómataToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.autómataToolStripMenuItem.Text = "Autómata";
            this.autómataToolStripMenuItem.Click += new System.EventHandler(this.autómataToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(156, 6);
            // 
            // guardarToolStripMenuItem
            // 
            this.guardarToolStripMenuItem.Name = "guardarToolStripMenuItem";
            this.guardarToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.guardarToolStripMenuItem.Text = "Guardar";
            // 
            // guardarComoToolStripMenuItem
            // 
            this.guardarComoToolStripMenuItem.Name = "guardarComoToolStripMenuItem";
            this.guardarComoToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.guardarComoToolStripMenuItem.Text = "Guardar como...";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(156, 6);
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.salirToolStripMenuItem.Text = "Salir";
            // 
            // ejecuciónToolStripMenuItem
            // 
            this.ejecuciónToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ejecutarAnalizadorToolStripMenuItem,
            this.ejecutarConAutómataToolStripMenuItem});
            this.ejecuciónToolStripMenuItem.Name = "ejecuciónToolStripMenuItem";
            this.ejecuciónToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.ejecuciónToolStripMenuItem.Text = "Ejecución";
            // 
            // ejecutarAnalizadorToolStripMenuItem
            // 
            this.ejecutarAnalizadorToolStripMenuItem.Name = "ejecutarAnalizadorToolStripMenuItem";
            this.ejecutarAnalizadorToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.ejecutarAnalizadorToolStripMenuItem.Text = "Analizar Código";
            this.ejecutarAnalizadorToolStripMenuItem.Click += new System.EventHandler(this.ejecutarAnalizadorToolStripMenuItem_Click);
            // 
            // ejecutarConAutómataToolStripMenuItem
            // 
            this.ejecutarConAutómataToolStripMenuItem.Name = "ejecutarConAutómataToolStripMenuItem";
            this.ejecutarConAutómataToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.ejecutarConAutómataToolStripMenuItem.Text = "Analizar Cadena";
            this.ejecutarConAutómataToolStripMenuItem.Click += new System.EventHandler(this.ejecutarConAutómataToolStripMenuItem_Click);
            // 
            // configuraciónToolStripMenuItem
            // 
            this.configuraciónToolStripMenuItem.Name = "configuraciónToolStripMenuItem";
            this.configuraciónToolStripMenuItem.Size = new System.Drawing.Size(95, 20);
            this.configuraciónToolStripMenuItem.Text = "Configuración";
            // 
            // formatoToolStripMenuItem
            // 
            this.formatoToolStripMenuItem.Name = "formatoToolStripMenuItem";
            this.formatoToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.formatoToolStripMenuItem.Text = "Formato";
            // 
            // ayudaToolStripMenuItem
            // 
            this.ayudaToolStripMenuItem.Name = "ayudaToolStripMenuItem";
            this.ayudaToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.ayudaToolStripMenuItem.Text = "Ayuda";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Editor);
            this.groupBox1.Location = new System.Drawing.Point(12, 48);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(511, 415);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Editor";
            // 
            // Editor
            // 
            this.Editor.AcceptsTab = true;
            this.Editor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Editor.Location = new System.Drawing.Point(6, 19);
            this.Editor.Name = "Editor";
            this.Editor.Size = new System.Drawing.Size(500, 393);
            this.Editor.TabIndex = 0;
            this.Editor.Text = "";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel1);
            this.groupBox2.Location = new System.Drawing.Point(529, 48);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(829, 415);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Pantalla";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Location = new System.Drawing.Point(7, 19);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(816, 384);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 749);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(tabControl1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nuevoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem códigoToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem lenguajeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem códigoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configuraciónToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem formatoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ayudaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lenguajeToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem lenguajeToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem autómataToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem guardarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem guardarComoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ejecuciónToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ejecutarAnalizadorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ejecutarConAutómataToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox Editor;
        private System.Windows.Forms.GroupBox groupBox2;
        private TabPage tabPage4;
        private TabPage tabPage1;
        private DataGridView dataGridView1;
        private TabPage tabPage2;
        private RichTextBox Consola;
        private DataGridView dataGridView2;
        private TabPage tabPage3;
        private RichTextBox Errores;
        private Panel panel1;

        public RichTextBox Consola1 { get => Consola; set => Consola = value; }
        public DataGridView DataGridView1 { get => dataGridView1; set => dataGridView1 = value; }
        public Panel Panel1 { get => panel1; set => panel1 = value; }
    }
}

