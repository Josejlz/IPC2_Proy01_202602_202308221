using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPC2_Proy01_202602_202308221.structures
{
    public class ListaCabeceras
    {
        public NodoCabecera Primero { get; private set; }
        public int Cantidad { get; private set; }

        private NodoCabecera ultimo;

        public ListaCabeceras()
        {
            Primero = null;
            ultimo = null;
            Cantidad = 0;
        }

        public NodoCabecera Insertar(int indice)
        {
            NodoCabecera nueva = new NodoCabecera(indice);

            if (Primero == null)
            {
                Primero = nueva;
            } else
            {
                ultimo.Siguiente = nueva;
            }

            ultimo = nueva;
            Cantidad = Cantidad + 1;

            return nueva;
        }

        public NodoCabecera BuscarPorIndice(int indice)
        {
            NodoCabecera actual = Primero;
            while (actual != null)
            {
                if (actual.Indice == indice)
                {
                    return actual;
                }

                actual = actual.Siguiente;
            }
            return null;
        }
    }
}
