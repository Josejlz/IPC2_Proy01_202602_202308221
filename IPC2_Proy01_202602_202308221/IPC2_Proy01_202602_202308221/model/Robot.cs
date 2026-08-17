using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPC2_Proy01_202602_202308221.model
{
    public abstract class Robot
    {
        public string Nombre { get; set; }
        public abstract string Tipo { get; }

        protected Robot(string nombre)
        {
            Nombre = nombre;
        }
    }
}
