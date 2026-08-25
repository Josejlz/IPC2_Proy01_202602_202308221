using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using IPC2_Proy01_202602_202308221.model;

namespace IPC2_Proy01_202602_202308221.structures
{
    public class MallaTablero
    {

        public string nombre { get; set; }
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

            // para las cabeceras
            int i = 1;
            while (i <= TotalFilas)
            {
                cabecerasFila.Insertar(i);
                i++;
            }

            // creacion de cabeceras de columna
            int j = 1;
            while (j <= TotalColumnas)
            {
                cabecerasColumna.Insertar(j);
                j++;
            }

        }

        //para insertar celdas

        public bool Insertar(Celda dato, int fila, int column)
        {

            NodoCabecera cabeceraFila = cabecerasFila.BuscarPorIndice(fila);
            NodoCabecera cabeceraColumna = cabecerasColumna.BuscarPorIndice(column);

            if (cabeceraFila==null||cabeceraColumna==null)
            {
                return false;
            }

            NodoCeldaMalla nuevo = new NodoCeldaMalla(dato, fila, column);


            if (cabeceraFila.Ultimo == null)
            {
                cabeceraFila.Acceso = nuevo;
            } else {
                nuevo.Arriba = cabeceraColumna.Ultimo;
                cabeceraColumna.Ultimo.Abajo = nuevo;
            }

            cabeceraColumna.Ultimo = nuevo;


            return true;
        }

        //para buscar la wea

        public NodoCeldaMalla ObtenerNodoMalla(int fila, int column)
        {
            NodoCabecera cabecera = cabecerasFila.BuscarPorIndice(fila);

            if (cabecera==null)
            {
                return null;
            }

            NodoCeldaMalla actual = cabecera.Acceso;

            while (actual!=null)
            {
                if (actual.Columna == column)
                {
                    return actual;
                }

                actual = actual.Derecha;
            }

            return null;
        }

        public Celda obtenerCelda(int fila, int column)
        {
            NodoCeldaMalla nodo = ObtenerNodoMalla(fila, column);

            if (nodo==null)
            {
                return null;
            }

            return nodo.Dato;
        }

        public bool ReemplazarCelda(int fila, int columna, Celda nuevo)
        {
            NodoCeldaMalla nodo = ObtenerNodoMalla(fila, columna);
            if (nodo==null)
            {
                return false;
            }
            nodo.Dato = nuevo;
            return true;
        }

        public NodoCeldaMalla ObtenerPrimeroDeFila(int fila)
        {
            NodoCabecera cabecera = cabecerasFila.BuscarPorIndice(fila);

            if (cabecera==null)
            {
                return null;
            }

            return cabecera.Acceso;

        }

        

        

    }

        
}
