using IPC2_Proy01_202602_202308221.controller;
using IPC2_Proy01_202602_202308221.Logica;
using IPC2_Proy01_202602_202308221.model;
using IPC2_Proy01_202602_202308221.Persistencia;
using IPC2_Proy01_202602_202308221.structures;
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

namespace IPC2_Proy01_202602_202308221
{
    public partial class Form1 : Form
    {

        private Controller controller;

        private ListaCelda objetivosActuales;
        private ListaRobots robotsActuales;

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
            cmbTipoMision.Items.Clear();
            cmbTipoMision.Items.Add("Rescate");
            cmbTipoMision.Items.Add("Extracción de recursos");
            cmbTipoMision.SelectedIndex = 0;
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

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    ResultadoCarga resultado = controller.CargarConfiguracion(filePath);

                    if (!resultado.Exito)
                    {
                        MessageBox.Show("No se pudo cargar el archivo:\n" + resultado.MensajeError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string mensaje = "Ciudades nuevas: " + resultado.CiudadesNuevas +
                        "\n Ciudades actualizadas: " + resultado.CiudadesActualizadas
                        + "\n Robots Nuevos: " + resultado.RobotsNuevos
                        + "\n Robots Actualizados: " + resultado.RobotsActualizados;

                    if (resultado.TieneAdvertencias())
                    {
                        mensaje = mensaje + "\n\nAdvertencias:\n "
                            + resultado.Advertencias;
                    }
                    MessageBox.Show(mensaje, "Carga Completada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtLogs.Text = controller.getCurrentLogData();

                    CargarComboCiudades();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (cmbCiudades.SelectedItem == null || cmbRobots.SelectedItem == null || cmbObjetivo.SelectedItem == null)
            {
                MessageBox.Show("Selecciona ciudad, robot y objetivo antes de iniciar la misión.", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nombreCiudad = cmbCiudades.SelectedItem.ToString();
            Robot robotSeleccionado = robotsActuales.ObtenerPorIndice(cmbRobots.SelectedIndex);
            Celda celdaObjetivo = objetivosActuales.ObtenerPorIndice(cmbObjetivo.SelectedIndex);
            bool esRescate = (cmbTipoMision.SelectedIndex == 0);

            string rutaImagen;
            ResultadoMision resultado;

            if (esRescate)
            {
                resultado = controller.EjecutarMisionRescate(nombreCiudad, celdaObjetivo, robotSeleccionado.Nombre, out rutaImagen);
            }
            else
            {
                resultado = controller.EjecutarMisionExtraccion(nombreCiudad, celdaObjetivo, robotSeleccionado.Nombre, out rutaImagen);
            }

            MostrarResultado(resultado, rutaImagen);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cmbCiudades.SelectedIndex=0;
            cmbRobots.SelectedIndex=0;
            cmbObjetivo.SelectedIndex=0;
            txtLogs.Text = "";

            if (picResultado.Image != null)
            {
                picResultado.Image.Dispose();
                picResultado.Image = null;
            }
        }

        private void cmbCiudades_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarComboObjetivo();
        }

        private void cmbTipoMision_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarComboObjetivo();
            ActualizarComboRobots();
        }

        private void CargarComboCiudades()
        {
            cmbCiudades.Items.Clear();
            ListaCiudad ciudades = controller.ObtenerCiudades();

            for (int i = 0; i < ciudades.Cantidad; i++)
            {
                Ciudad c = ciudades.ObtenerPorIndice(i);
                cmbCiudades.Items.Add(c.Nombre);
            }

            if (cmbCiudades.Items.Count > 0)
            {
                cmbCiudades.SelectedIndex = 0;
            }

            ActualizarComboRobots();
        }

        private void ActualizarComboObjetivo()
        {
            cmbObjetivo.Items.Clear();

            if (cmbCiudades.SelectedItem == null)
            {
                objetivosActuales = new ListaCelda();
                return;
            }

            string nombreCiudad = cmbCiudades.SelectedItem.ToString();
            bool esRescate = (cmbTipoMision.SelectedIndex == 0);

            objetivosActuales = esRescate
                ? controller.ObtenerCiviles(nombreCiudad)
                : controller.ObtenerRecursos(nombreCiudad);

            for (int i = 0; i < objetivosActuales.Cantidad; i++)
            {
                Celda celda = objetivosActuales.ObtenerPorIndice(i);
                cmbObjetivo.Items.Add(celda.Fila + "," + celda.Columna);
            }

            if (cmbObjetivo.Items.Count > 0)
            {
                cmbObjetivo.SelectedIndex = 0;
            }
        }

        private void ActualizarComboRobots()
        {
            cmbRobots.Items.Clear();
            robotsActuales = new ListaRobots();

            ListaRobots todos = controller.ObtenerRobots();
            bool esRescate = (cmbTipoMision.SelectedIndex == 0);

            for (int i = 0; i < todos.Cantidad; i++)
            {
                Robot r = todos.ObtenerPorIndice(i);

                if (esRescate && r is ChapinRescue)
                {
                    robotsActuales.Insertar(r);
                    cmbRobots.Items.Add(r.Nombre);
                }
                else if (!esRescate && r is ChapinFighter fighter)
                {
                    robotsActuales.Insertar(r);
                    cmbRobots.Items.Add(r.Nombre + "  (capacidad: " + fighter.Capacidad + ")");
                }
            }

            if (cmbRobots.Items.Count > 0)
            {
                cmbRobots.SelectedIndex = 0;
            }
        }

        private void MostrarResultado(ResultadoMision resultado, string rutaImagen)
        {
            if (!resultado.Exito)
            {
                txtLogs.Text = "Mision Imposible";
                if (picResultado.Image != null)
                {
                    picResultado.Image.Dispose();
                }
                picResultado.Image = null;
                return;
            }

            StringBuilder texto = new StringBuilder();
            texto.AppendLine("Tipo de misión: " + resultado.TipoMision);

            if (resultado.TipoMision == "rescate")
            {
                texto.AppendLine("Unidad civil rescatada: " + resultado.CeldaObjetivo.Fila + "," + resultado.CeldaObjetivo.Columna);
                texto.AppendLine("Robot utilizado: " + resultado.RobotUsado.Nombre + " (ChapinRescue)");
            }
            else
            {
                texto.AppendLine("Recurso extraído: " + resultado.CeldaObjetivo.Fila + "," + resultado.CeldaObjetivo.Columna);
                texto.AppendLine("Robot utilizado: " + resultado.RobotUsado.Nombre +
                    " (ChapinFighter - Capacidad de combate inicial " + resultado.CapacidadInicial +
                    ", Capacidad de combate final " + resultado.CapacidadFinal + ")");
            }

            txtLogs.Text = texto.ToString();

            if (!string.IsNullOrEmpty(rutaImagen) && File.Exists(rutaImagen))
            {
                if (picResultado.Image != null)
                {
                    picResultado.Image.Dispose();
                }

                using (var imagenTemporal = new Bitmap(rutaImagen))
                {
                    picResultado.Image = new Bitmap(imagenTemporal);
                }
            }
        }
    }
}