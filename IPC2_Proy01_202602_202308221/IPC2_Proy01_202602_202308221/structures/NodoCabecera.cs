using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using IPC2_Proy01_202602_202308221.model;

namespace IPC2_Proy01_202602_202308221.structures
{
    public  class NodoCabecera
    {
        public int Indice { get; set; }
        public NodoCeldaMalla Acceso { get; set; }
        public NodoCeldaMalla Ultimo { get; set; }
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
