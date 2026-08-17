using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPC2_Proy01_202602_202308221.model
{
    public enum TipoCelda { Intransitable, Camino, PuntoEntrada, UnidadCivil, Recurso }

    public class Celda
    {
        

            public int Fila { get; set; }
            public int Columna { get; set; }
            public TipoCelda Tipo { get; set; }
            public int? CapacidadMilitar { get; set; }

            public bool TieneUnidadMilitar => CapacidadMilitar.HasValue;
        
    }
}
