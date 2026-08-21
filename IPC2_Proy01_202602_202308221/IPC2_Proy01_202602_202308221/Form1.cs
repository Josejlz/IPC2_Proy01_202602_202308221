using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IPC2_Proy01_202602_202308221.controller;
using IPC2_Proy01_202602_202308221.Persistencia;

namespace IPC2_Proy01_202602_202308221
{
    public partial class Form1 : Form
    {

        private Controller controller;

        
        internal void setController(Controller controller)
        {
            this.controller = controller;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = @"C:\";
                openFileDialog.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if(openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    ResultadoCarga resultado = controller.CargarConfiguracion(filePath);
                    MessageBox.Show($"Selected file: {filePath}");


                    if (!resultado.Exito)
                    {
                        MessageBox.Show("No se pudeo cargar el archivo:\n" + resultado.MensajeError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    string mensaje = "Ciudades nuevas: "+ resultado.CiudadesNuevas +
                        "\n Ciudades actualizadas: " + resultado.CiudadesActualizadas 
                        + "\n Robots Nuevos: " + resultado.RobotsNuevos
                        + "\n Robots Actualizados: " + resultado.RobotsActualizados;

                    if (resultado.TieneAdvertencias())
                    {
                        mensaje = mensaje + "\n\nAdvertencias:\n "
                            +resultado.Advertencias;
                    }
                    MessageBox.Show(mensaje, "Carga Completada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtLogs.Text = controller.getCurrentLogData();

                }

            }
        }
    }
}
