using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPC2_Proy01_202602_202308221.model;

namespace IPC2_Proy01_202602_202308221.structures
{
    public class ListaCelda
    {
        public CeldaNodo Primero { get; set; }
        public int Cantidad { get; private set; }

        public ListaCelda()
        {
            Primero = null;
            Cantidad = 0;
        }

        public bool isEmpty()
        {
            return Primero == null;
        }

        public void Insertar(Celda celda)
        {
            CeldaNodo nuevo = new CeldaNodo(celda);
            if (Primero == null)
            {
                Primero = nuevo;
            }
            else
            {
                CeldaNodo actual = Primero;
                while (actual.Siguiente!=null)
                {
                    actual = actual.Siguiente;
                }
                actual.Siguiente = nuevo;
            }
            Cantidad = Cantidad + 1;
        }

        public Celda BuscarPorFilaColumna(int fila, int col)
        {

            CeldaNodo actual = Primero;

            while (actual!=null)
            {
                if ((actual.Dato.Fila == fila)&&(actual.Dato.Columna==col))
                {
                    return actual.Dato;
                }
                actual = actual.Siguiente;
            }

            return null;
        }

        public bool BuscarOActualizar(Celda celda)
        {
            Celda existente = BuscarPorFilaColumna(celda.Fila, celda.Columna);
            if (existente==null)
            {
                Insertar(celda);
                return true;
            } else
            {
                existente.Tipo = celda.Tipo;
                if (celda.TieneUnidadMilitar)
                {
                    existente.CapacidadMilitar = celda.CapacidadMilitar;
                }
            }
            return false;
        }

        public void EliminarUltimo()
        {
            if (Primero == null) return;

            if (Primero.Siguiente == null)
            {
                Primero = null;
                Cantidad = 0;
                return;
            }

            CeldaNodo actual = Primero;
            while (actual.Siguiente.Siguiente !=null)
            {
                actual = actual.Siguiente;
            }
            actual.Siguiente = null;
            Cantidad = Cantidad - 1;
            return;
        }

        public void ReiniciarVisitas()
        {
            CeldaNodo actual = Primero;
            while (actual != null)
            {
                actual.Dato.Visitada = false;
                actual = actual.Siguiente;
            }
        }

        public Celda ObtenerPorIndice(int indice)
        {
            if (indice < 0 || indice >= Cantidad)
            {
                return null;
            }

            CeldaNodo actual = Primero;
            int contador = 0;

            while (contador < indice)
            {
                actual = actual.Siguiente;
                contador = contador + 1;
            }

            return actual.Dato;
        }

    }



    
}






















