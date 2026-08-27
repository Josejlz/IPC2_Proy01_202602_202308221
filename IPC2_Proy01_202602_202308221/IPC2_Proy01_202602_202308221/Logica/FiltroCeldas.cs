using IPC2_Proy01_202602_202308221.model;
using IPC2_Proy01_202602_202308221.structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPC2_Proy01_202602_202308221.Logica
{
    public static class FiltroCeldas
    {
        public static ListaCelda ObtenerPorTipo(ListaCelda listaCeldasCiudad, TipoCelda tipo)
        {
            ListaCelda resultado = new ListaCelda();
            CeldaNodo actual = listaCeldasCiudad.Primero;

            while (actual != null)
            {
                if (actual.Dato.Tipo == tipo)
                {
                    resultado.Insertar(actual.Dato);
                }
                actual = actual.Siguiente;
            }

            return resultado;
        }
    }
}
