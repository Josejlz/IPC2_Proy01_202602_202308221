using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace IPC2_Proy01_202602_202308221.structures
{
    public  class NodoCabecera
    {
        public int Indice { get; set; }
        public CeldaNodo Acceso { get; set; }
        public CeldaNodo Ultimo { get; set; }
        public NodoCabecera Siguiente { get; set; }
        public NodoCabecera(int indice)
        {
            Indice = indice;
            Acceso = null;
            Ultimo = null;
            Siguiente = null;
        }
    }
}
