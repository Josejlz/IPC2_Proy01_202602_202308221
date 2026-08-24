using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPC2_Proy01_202602_202308221.model;

namespace IPC2_Proy01_202602_202308221.structures
{
    public class NodoCeldaMalla
    {
        public Celda Dato { get; set; }

        public int Fila { get; set; }

        public int Columna { get; set; }

        public NodoCeldaMalla Arriba { get; set; }

        public NodoCeldaMalla Abajo { get; set; }

        public NodoCeldaMalla Izquierda { get; set; }

        public NodoCeldaMalla Derecha { get; set; }


        public NodoCeldaMalla(Celda dato, int fila, int columna)
        {
            Dato = dato;
            Fila = fila;
            Columna = columna;
            Arriba = null;
            Abajo = null;
            Izquierda = null;
            Derecha = null;
        }
    }
}
