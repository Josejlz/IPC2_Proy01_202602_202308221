using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPC2_Proy01_202602_202308221.model;

namespace IPC2_Proy01_202602_202308221.structures
{
    public class ListaCiudad
    {
        public CiudadNodo Primero { get; set; }
        public int Cantidad { get; private set; }
        public ListaCiudad()
        {
            Primero = null;
            Cantidad = 0;
        }
        public bool isEmpty()
        {
            return Primero == null;
        }

        public void Insertar(Ciudad ciudad)
        {
            CiudadNodo nuevo = new CiudadNodo(ciudad);
            if (Primero == null)
            {
                Primero = nuevo;
            }
            else
            {
                CiudadNodo actual = Primero;
                while (actual.Siguiente!=null)
                {
                    actual = actual.Siguiente; 
                }
                actual.Siguiente = nuevo;
            }
            Cantidad += 1;
        }

        public Ciudad BuscarPorNombre(string nombre)
        {

            CiudadNodo actual = Primero;

            while (actual!=null)
            {
                if (string.Equals(actual.Dato.Nombre, nombre, System.StringComparison.OrdinalIgnoreCase))
                {
                    return actual.Dato;
                }
                    actual = actual.Siguiente;

            }

            return null;
        }

        public bool BuscarOActualizar(Ciudad ciudad)
        {
            Ciudad existente = BuscarPorNombre(ciudad.Nombre);
            if (existente==null)
            {
                Insertar(ciudad);
                return true;
            } else
            {
                existente.Nombre = ciudad.Nombre;
                existente.Filas = ciudad.Filas;
                existente.Columnas = ciudad.Columnas;
                existente.listaCeldas = ciudad.listaCeldas;
            }
            return false;
        }

        public bool EliminarPorNombre(string nombre)
        {
            if(Primero == null)
            { return false; }

            CiudadNodo actual = Primero;
            CiudadNodo anterior = null;

            while (actual != null)
            {

                if (string.Equals(actual.Dato.Nombre, nombre, System.StringComparison.OrdinalIgnoreCase))
                {
                    if (anterior==null)
                    {
                        Primero = actual.Siguiente;
                    } else
                    {
                        anterior.Siguiente = actual.Siguiente;
                    }
                    Cantidad = Cantidad - 1;
                    return true;
                }
                anterior= actual;
                actual = actual.Siguiente;

            }

            return false;
        }

        public Ciudad ObtenerPorIndice(int indice)
        {
            if (indice<0||indice>=Cantidad)
            {
                return null;
            }
            CiudadNodo actual = Primero;
            int contador = 0;

            while (contador<indice)
            {
                actual = actual.Siguiente;
                contador = contador + 1;
            }
            return actual.Dato;
        }

    }
}
