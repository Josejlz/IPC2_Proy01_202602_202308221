using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPC2_Proy01_202602_202308221.model;

namespace IPC2_Proy01_202602_202308221.structures
{
    public class ListaRobots
    {
        public RobotNodo Primero { get; private set; }
        public int Cantidad { get; private set; }

        public ListaRobots()
        {
            Primero = null;
            Cantidad = 0;
        }

        public bool isEmpty()
        {
            return Primero == null;
        }

        /// Insercion de Robot al final de la lista. 
        
        public void Insertar(Robot robot)
        {
            RobotNodo nuevo = new RobotNodo(robot);
            if (Primero==null)
            {
                Primero = nuevo;
            }
            else
            {
                RobotNodo actual = Primero;

                while (actual.Siguiente !=null)
                {
                    actual = actual.Siguiente;
                }
                actual.Siguiente = nuevo;
            }
            Cantidad = Cantidad + 1;
        }



        public Robot BuscarPorNombre(string nombre)
        {

            RobotNodo actual = Primero;

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

        public bool InsertarOActualizar(Robot robot)
        {
            Robot existente = BuscarPorNombre(robot.Nombre);

            if (existente == null)
            {
                Insertar(robot);
                return true;
            }

            if (existente is ChapinFighter fighterExistente && robot is ChapinFighter fighterNuevo)
            {
                fighterExistente.Capacidad = fighterNuevo.Capacidad;
            } else if (existente is ChapinRescue rescueExistente && robot is ChapinRescue rescueNuevo)
            {
                //por si llego a necesitarlo luego, pero no tiene nada extra :V
            }

            return false;
        }

        public bool EliminarPorNombre(string nombre)
        {
            if(Primero == null)
            {
                return false;
            }

            RobotNodo actual = Primero;
            RobotNodo anterior = null;

            while (actual!=null)
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
                anterior = actual;
                actual = actual.Siguiente;
            }

            return false;
        }

        public Robot ObtenerPorIndice(int indice)
        {
            if (indice < 0 || indice >= Cantidad)
            {
                return null;
            }

            RobotNodo actual = Primero;
            int counter = 0;

            while (counter<indice)
            {
                actual = actual.Siguiente;
                counter = counter + 1;
            }

            return actual.Dato;

        }


    }
}
