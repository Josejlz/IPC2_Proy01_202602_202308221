using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPC2_Proy01_202602_202308221.controller;
using System.IO;
using IPC2_Proy01_202602_202308221.structures;

namespace IPC2_Proy01_202602_202308221.model
{
    public class Model
    {
        public ListaCiudad listaCiudades {  get; private set; }

        public MallaTablero mallaTablero { get; private set; };
        public ListaRobots listaRobots { get; private set; }

        public ListaMallas listaMallas { get; private set; }

        public Model()
        {
            listaCiudades = new ListaCiudad();
            listaRobots = new ListaRobots();
            listaMallas = new ListaMallas();
        }

        public string innerData()
        {
            string contents="";

            if (listaRobots.Primero!=null)
            {
                RobotNodo actual = listaRobots.Primero;
                int contador = 1;

                contents = "Lista de Robots: \n";

                if (actual==null)
                {
                    contents = "No hay Robots de Chapin Warriors\n";
                }

                while (actual!=null)
                {
                    
                    contents=contents+"Nombre "+contador+": "+actual.Dato.Nombre + "\n";
                    contador += 1;
                    actual = actual.Siguiente;

                }
            }

            if (listaCiudades.Primero != null)
            {
                CiudadNodo actual = listaCiudades.Primero;
                int contador = 1;

                if(actual == null)
                {
                    contents = contents+"No hay Ciudades";
                }

                while (actual != null)
                {
                    if (contador==1)
                    {
                        contents = contents + "\n\n\nLista de ciudades: \n";
                    }

                    contents = contents + "Nombre " + contador + ": " + actual.Dato.Nombre + "\n\n";

                    ListaCelda listaCeldaCiudad = actual.Dato.listaCeldas;
                    for (int i = 0; i<actual.Dato.Filas+1;i++)
                    {

                        for (int j=0; j<actual.Dato.Columnas+1;j++)
                        {
                            Celda celdaActual = listaCeldaCiudad.BuscarPorFilaColumna(i, j);
                            if (celdaActual!=null)
                            {
                                switch (celdaActual.Tipo)
                                {
                                    case TipoCelda.Intransitable:
                                        contents = contents + "*";
                                        break;
                                    case TipoCelda.Camino:
                                        contents = contents + " ";
                                        break;
                                    case TipoCelda.Recurso:
                                        contents = contents + "R";
                                        break;
                                    case TipoCelda.UnidadCivil:
                                        contents = contents + "C";
                                        break;
                                    case TipoCelda.PuntoEntrada:
                                        contents = contents + "E";
                                        break;
                                    default:
                                        contents = contents + "x";
                                        break;
                                }
                            }
                        }
                        contents = contents = contents + "\n";
                        
                    }

                    contador += 1;
                    actual = actual.Siguiente;

                }
            }

            return contents;
        }
    }
}
