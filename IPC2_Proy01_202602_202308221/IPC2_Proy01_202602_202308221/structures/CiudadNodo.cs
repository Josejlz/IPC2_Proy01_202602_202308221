using IPC2_Proy01_202602_202308221.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPC2_Proy01_202602_202308221.model;

namespace IPC2_Proy01_202602_202308221.structures
{
    public class CiudadNodo
    {
        public Ciudad Dato { get; set; }
        public CiudadNodo Siguiente { get; set; }
        public CiudadNodo(Ciudad ciudad)
        {
            Dato = ciudad;
            Siguiente = null;
        }
    }
}
