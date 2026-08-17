using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPC2_Proy01_202602_202308221.model;

namespace IPC2_Proy01_202602_202308221.structures
{
    public class CeldaNodo
    {
        public Celda Dato { get; set; }
        public CeldaNodo Siguiente { get; set; }

        public CeldaNodo(Celda celda)
        {
            Dato = celda;
            Siguiente = null;
        }

    }
}
