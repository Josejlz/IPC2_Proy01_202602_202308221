using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPC2_Proy01_202602_202308221.structures
{
    public class ListaMallas
    {
        public MallaNodo Primero {  get; set; }
        public int Cantidad { get; private set; }
        public ListaMallas()
        {
            Primero = null;
            Cantidad = 0;
        }

        public bool isEmpty()
        {
            return (Primero == null);
        }

        public void Insertar(MallaTablero mallaTab)
        {
            MallaNodo nuevo = new MallaNodo(mallaTab);

            if (Primero == null)
            {
                Primero = nuevo;
            } else
            {
                MallaNodo actual = Primero;
                while (actual.Siguiente!=null)
                {
                    actual = actual.Siguiente;
                }
                actual.Siguiente = nuevo;
            }
            Cantidad = Cantidad + 1;
        }

        public MallaTablero BuscarPorIndice(int index)
        {
            MallaNodo actual = Primero;
            int contador = 0;
            while (actual != null)
            {
                if (contador == index)
                {
                    return actual.Dato;
                }
                actual = actual.Siguiente;
                contador += 1;
            }
            return null;
        }

        public MallaTablero BuscarPorNombre(string nombre)
        {
            MallaNodo actual = Primero;
            while (actual != null)
            {
                if (string.Equals(actual.Dato.nombre, nombre, StringComparison.OrdinalIgnoreCase))
                {
                    return actual.Dato;
                }
                actual = actual.Siguiente;
            }
            return null;
        }

        public void Vaciar()
        {
            Primero = null;
            Cantidad = 0;
        }

    }
}
