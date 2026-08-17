using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPC2_Proy01_202602_202308221.structures;

namespace IPC2_Proy01_202602_202308221.model
{
    public class Ciudad
    {
        public string Nombre { get; set; }
        public int Filas { get; set; }
        public int Columnas { get; set; }
        public ListaCelda listaCeldas = new ListaCelda();



    }
}
