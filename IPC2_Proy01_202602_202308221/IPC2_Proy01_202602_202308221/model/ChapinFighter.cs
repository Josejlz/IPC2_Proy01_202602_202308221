using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPC2_Proy01_202602_202308221.model
{
    public class ChapinFighter : Robot
    {
        public int Capacidad { get; set; }
        public override string Tipo => "ChapinFighter";

        public ChapinFighter(string nombre, int capacidad) : base(nombre)
        {
            Capacidad = capacidad;
        }
    }
}
