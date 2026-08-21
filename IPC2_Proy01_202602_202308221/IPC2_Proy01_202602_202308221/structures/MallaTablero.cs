using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPC2_Proy01_202602_202308221.model;

namespace IPC2_Proy01_202602_202308221.structures
{
    public class MallaTablero
    {
        private ListaCabeceras cabecerasFila;
        private ListaCabeceras cabecerasColumna;
        public int TotalFilas { get; private set; }
        public int TotalColumnas { get; private set; }
        public MallaTablero(int totalFilas, int totalColumnas)
        {
            TotalFilas = totalFilas;
            TotalColumnas = totalColumnas;

            cabecerasFila = new ListaCabeceras();
            cabecerasColumna = new ListaCabeceras();

            int i = 1;
            while (i<=TotalFilas)
            {
                cabecerasFila.Insertar(i);
                i = i + 1;
            }
            i = 1;
            while (i<=TotalColumnas)
            {
                cabecerasFila.Insertar(i);
                i = i + 1;
            }
        }

        
    }
}
