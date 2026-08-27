using IPC2_Proy01_202602_202308221.model;
using IPC2_Proy01_202602_202308221.structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPC2_Proy01_202602_202308221.Logica
{
    // Guarda el resultado de intentar ejecutar una mision de rescate o extraccion.
    public class ResultadoMision
    {
        public bool Exito { get; set; }
        public string TipoMision { get; set; }          
        public ListaCelda Camino { get; set; }           
        public Celda CeldaObjetivo { get; set; }          
        public Robot RobotUsado { get; set; }
        public int CapacidadInicial { get; set; }
        public int CapacidadFinal { get; set; }
        public string MensajeError { get; set; }

        public ResultadoMision()
        {
            Exito = false;
            Camino = new ListaCelda();
            MensajeError = "";
        }
    }
}
