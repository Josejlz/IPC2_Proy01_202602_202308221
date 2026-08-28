namespace IPC2_Proy01_202602_202308221
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.txtLogs = new System.Windows.Forms.RichTextBox();
            this.cmbTipoMision = new System.Windows.Forms.ComboBox();
            this.cmbCiudades = new System.Windows.Forms.ComboBox();
            this.cmbRobots = new System.Windows.Forms.ComboBox();
            this.cmbObjetivo = new System.Windows.Forms.ComboBox();
            this.picResultado = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picResultado)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MingLiU-ExtB", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(267, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(297, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "ChapinWarriors";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Aquamarine;
            this.button1.Font = new System.Drawing.Font("MingLiU-ExtB", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(90, 133);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(159, 33);
            this.button1.TabIndex = 1;
            this.button1.Text = "Cargar Archivo";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Aquamarine;
            this.button2.Font = new System.Drawing.Font("MingLiU-ExtB", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(319, 133);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(181, 33);
            this.button2.TabIndex = 2;
            this.button2.Text = "Comenzar Operación";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Aquamarine;
            this.button3.Font = new System.Drawing.Font("MingLiU-ExtB", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(574, 133);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(159, 33);
            this.button3.TabIndex = 3;
            this.button3.Text = "Limpiar";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // txtLogs
            // 
            this.txtLogs.Location = new System.Drawing.Point(62, 271);
            this.txtLogs.Name = "txtLogs";
            this.txtLogs.Size = new System.Drawing.Size(287, 164);
            this.txtLogs.TabIndex = 5;
            this.txtLogs.Text = "";
            // 
            // cmbTipoMision
            // 
            this.cmbTipoMision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoMision.FormattingEnabled = true;
            this.cmbTipoMision.Location = new System.Drawing.Point(51, 197);
            this.cmbTipoMision.Name = "cmbTipoMision";
            this.cmbTipoMision.Size = new System.Drawing.Size(228, 21);
            this.cmbTipoMision.TabIndex = 6;
            this.cmbTipoMision.SelectedIndexChanged += new System.EventHandler(this.cmbTipoMision_SelectedIndexChanged);
            
            // cmbCiudades
            this.cmbCiudades.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCiudades.FormattingEnabled = true;
            this.cmbCiudades.Location = new System.Drawing.Point(557, 197);
            this.cmbCiudades.Name = "cmbCiudades";
            this.cmbCiudades.Size = new System.Drawing.Size(238, 21);
            this.cmbCiudades.TabIndex = 7;
            this.cmbCiudades.SelectedIndexChanged += new System.EventHandler(this.cmbCiudades_SelectedIndexChanged);
            // 
            // cmbRobots
            // 
            this.cmbRobots.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRobots.FormattingEnabled = true;
            this.cmbRobots.Location = new System.Drawing.Point(297, 197);
            this.cmbRobots.Name = "cmbRobots";
            this.cmbRobots.Size = new System.Drawing.Size(241, 21);
            this.cmbRobots.TabIndex = 8;
            // 
            // cmbObjetivo
            // 
            this.cmbObjetivo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbObjetivo.FormattingEnabled = true;
            this.cmbObjetivo.Location = new System.Drawing.Point(297, 231);
            this.cmbObjetivo.Name = "cmbObjetivo";
            this.cmbObjetivo.Size = new System.Drawing.Size(241, 21);
            this.cmbObjetivo.TabIndex = 9;
            // 
            // picResultado
            // 
            this.picResultado.Location = new System.Drawing.Point(444, 273);
            this.picResultado.Name = "picResultado";
            this.picResultado.Size = new System.Drawing.Size(339, 162);
            this.picResultado.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picResultado.TabIndex = 10;
            this.picResultado.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Plum;
            this.ClientSize = new System.Drawing.Size(833, 465);
            this.Controls.Add(this.picResultado);
            this.Controls.Add(this.cmbObjetivo);
            this.Controls.Add(this.cmbRobots);
            this.Controls.Add(this.cmbCiudades);
            this.Controls.Add(this.cmbTipoMision);
            this.Controls.Add(this.txtLogs);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picResultado)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.RichTextBox txtLogs;
        private System.Windows.Forms.ComboBox cmbTipoMision;
        private System.Windows.Forms.ComboBox cmbCiudades;
        private System.Windows.Forms.ComboBox cmbRobots;
        private System.Windows.Forms.ComboBox cmbObjetivo;
        private System.Windows.Forms.PictureBox picResultado;
    }
}