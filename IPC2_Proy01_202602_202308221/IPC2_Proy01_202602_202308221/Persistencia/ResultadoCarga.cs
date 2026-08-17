using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPC2_Proy01_202602_202308221.Persistencia
{
    public class ResultadoCarga
    {
        public bool Exito { get; set; }
        public string MensajeError { get; set; }

        public int CiudadesNuevas { get; set; }

        public int CiudadesActualizadas { get; set; }

        public int RobotsNuevos { get; set; }

        public int RobotsActualizados { get; set; }

        public string Advertencias { get; set; }

        public ResultadoCarga()
        {
            Exito = true;
            MensajeError = "";
            Advertencias = "";
            RobotsNuevos = 0;
            RobotsActualizados = 0;
            CiudadesNuevas = 0;
            CiudadesActualizadas = 0;
        }

        public void AddWarning(string texto)
        {
            if (Advertencias.Length > 0)
            {
                Advertencias = Advertencias + "\n";
            }

            Advertencias = Advertencias + " - " + texto;

        }

        public bool TieneAdvertencias()
        {
            return Advertencias.Length > 0;
        }
    }
}
