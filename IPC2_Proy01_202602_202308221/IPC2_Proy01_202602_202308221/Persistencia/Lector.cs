using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPC2_Proy01_202602_202308221.model;
using IPC2_Proy01_202602_202308221.structures;
using System.IO;
using System.Xml;
using IPC2_Proy01_202602_202308221.model;

namespace IPC2_Proy01_202602_202308221.Persistencia
{
    public class Lector
    {
        public ResultadoCarga CargarConfiguracion(string ruta, ListaCiudad listaCiudad, ListaRobots listaRobots, ListaMallas listaMallas)
        {
            ResultadoCarga resultado = new ResultadoCarga();
            if (!System.IO.File.Exists(ruta))
            {
                resultado.Exito = false;
                resultado.MensajeError = "No se encontro el archivo: " + ruta;
                return resultado;
            }

            try
            {
                XmlDocument documento = new XmlDocument();
                documento.PreserveWhitespace = true;
                documento.Load(ruta);

                CargarCiudades(documento, listaCiudad, resultado);
                CargarRobots(documento, listaRobots, resultado);
                CargarMallas(listaCiudad, listaRobots, listaMallas);
            }
            catch (XmlException e)
            {
                resultado.Exito = false;
                resultado.MensajeError = "El .xml esta mal formado: " + e.Message;
            }
            catch(Exception e)
            {
                resultado.Exito = false;
                resultado.MensajeError = "Error inesperado al leer: " + e.Message;
            }

            return resultado;

        }

        public void CargarRobots(XmlDocument documento, ListaRobots listaRobots, ResultadoCarga resultado)
        {
            XmlNodeList nodosRobots = documento.SelectNodes("configuracion/robots/robot");
            if (nodosRobots==null)
            {
                return;
            }

            foreach (XmlNode nodoRobot in nodosRobots)
            {
                XmlNode nodoNombre = nodoRobot.SelectSingleNode("nombre");

                if (nodoNombre==null)
                {
                    resultado.AddWarning("Hay un robot sin etiqueta nombre. Se omitió");
                    continue;
                }

                string nombre = nodoNombre.InnerText.Trim();
                string tipo = LeerAtributoTexto(nodoNombre, "tipo", "");

                if (nombre.Length==0)
                {
                    resultado.AddWarning("Se encontró un robot sin nombre, se omitió.");
                    continue;
                }

                Robot robot = null;

                if (string.Equals(tipo, "ChapinFighter",StringComparison.OrdinalIgnoreCase))
                {
                    int capacidad = LeerEntero(nodoNombre, "capacidad", -1);

                    if (capacidad < 0)
                    {
                        resultado.AddWarning("La unidad '"+nombre+"' es ChapinFighter pero no tiene capacidad de batalla valida. Se omitió.");
                        continue;
                    }

                    robot = new ChapinFighter(nombre, capacidad);

                }
                else if (string.Equals(tipo, "ChapinRescue", StringComparison.OrdinalIgnoreCase))
                {
                    robot = new ChapinRescue(nombre);
                } else
                {
                    resultado.AddWarning("El robot '"+nombre+"' tiene un tipo desconocido ('"+tipo+"'). Se omitió.");
                    continue;
                }

                bool isNew = listaRobots.InsertarOActualizar(robot);

                if (isNew)
                {
                    resultado.RobotsNuevos = resultado.RobotsNuevos + 1;
                }

            }
        }

        public void CargarCiudades(XmlDocument documento, ListaCiudad listaCiudad, ResultadoCarga resultado)
        {
            XmlNodeList nodosCiudad = documento.SelectNodes("configuracion/listaCiudades/ciudad");
            if (nodosCiudad == null)
            {
                return;
            }

            foreach (XmlNode nodoCiudad in nodosCiudad)
            {
                XmlNode nodoNombre = nodoCiudad.SelectSingleNode("nombre");

                if (nodoNombre == null)
                {
                    resultado.AddWarning("Hay una ciudad sin etiqueta nombre. Se omitió.");
                    continue;
                }

                string nombre = nodoNombre.InnerText.Trim();
                int filas = LeerEntero(nodoNombre, "filas", -1);
                int columnas = LeerEntero(nodoNombre, "columnas", -1);

                if (nombre.Length == 0)
                {
                    resultado.AddWarning("Se encontró una ciudad sin nombre. Se omitió.");
                    continue;
                }

                if (filas <= 0 || columnas <= 0)
                {
                    resultado.AddWarning("La ciudad '" + nombre + "' no tiene atributos filas/columnas válidos. Se omitió.");
                    continue;
                }

                Ciudad ciudad = new Ciudad();
                ciudad.Nombre = nombre;
                ciudad.Filas = filas;
                ciudad.Columnas = columnas;

                CargarCeldas(nodoCiudad, ciudad, resultado);
                CargarUnidadesMilitares(nodoCiudad, ciudad, resultado);

                bool esNueva = listaCiudad.BuscarOActualizar(ciudad);

                if (esNueva)
                {
                    resultado.CiudadesNuevas = resultado.CiudadesNuevas + 1;
                }
                else
                {
                    resultado.CiudadesActualizadas = resultado.CiudadesActualizadas + 1;
                }
            }
        }

        public void CargarCeldas(XmlNode nodoCiudad, Ciudad ciudad, ResultadoCarga resultado)
        {
            XmlNodeList nodosFila = nodoCiudad.SelectNodes("fila");

            if (nodosFila == null)
            {
                resultado.AddWarning("La ciudad '" + ciudad.Nombre + "' no tiene filas definidas.");
                return;
            }

            ListaCelda nuevaListaCeldas = new ListaCelda();

            foreach (XmlNode nodoFila in nodosFila)
            {
                int numeroFila = LeerEntero(nodoFila, "numero", -1);

                if (numeroFila < 1 || numeroFila > ciudad.Filas)
                {
                    resultado.AddWarning("La ciudad '" + ciudad.Nombre + "' tiene una fila con número inválido (" + numeroFila + "). Se omitió esa fila.");
                    continue;
                }

                string contenido = QuitarComillas(nodoFila.InnerText.Trim());

                if (contenido.Length != ciudad.Columnas)
                {
                    resultado.AddWarning("La fila " + numeroFila + " de la ciudad '" + ciudad.Nombre + "' no tiene " + ciudad.Columnas + " caracteres (tiene " + contenido.Length + "). Se omitió esa fila.");
                    continue;
                }

                for (int j = 0; j < contenido.Length; j++)
                {
                    char caracter = contenido[j];
                    int numeroColumna = j + 1;

                    TipoCelda? tipo = MapearCaracterATipo(caracter);

                    if (tipo == null)
                    {
                        resultado.AddWarning("Carácter inválido '" + caracter + "' en fila " + numeroFila + ", columna " + numeroColumna + " de la ciudad '" + ciudad.Nombre + "'. Se trató como intransitable.");
                        tipo = TipoCelda.Intransitable;
                    }

                    Celda celda = new Celda();
                    celda.Fila = numeroFila;
                    celda.Columna = numeroColumna;
                    celda.Tipo = tipo.Value;
                    celda.CapacidadMilitar = null;

                    nuevaListaCeldas.Insertar(celda);
                }
            }

            ciudad.listaCeldas = nuevaListaCeldas;
        }

        public void CargarUnidadesMilitares(XmlNode nodoCiudad, Ciudad ciudad, ResultadoCarga resultado)
        {
            XmlNodeList nodosUnidad = nodoCiudad.SelectNodes("unidadMilitar");

            if (nodosUnidad == null)
            {
                return;
            }

            foreach (XmlNode nodoUnidad in nodosUnidad)
            {
                int fila = LeerEntero(nodoUnidad, "fila", -1);
                int columna = LeerEntero(nodoUnidad, "columna", -1);

                int capacidad;
                bool capacidadValida = int.TryParse(nodoUnidad.InnerText.Trim(), out capacidad);

                if (fila < 1 || columna < 1 || !capacidadValida || capacidad < 0)
                {
                    resultado.AddWarning("Hay una unidad militar inválida en la ciudad '" + ciudad.Nombre + "'. Se omitió.");
                    continue;
                }

                Celda celda = ciudad.listaCeldas.BuscarPorFilaColumna(fila, columna);

                if (celda == null)
                {
                    resultado.AddWarning("La unidad militar en (" + fila + "," + columna + ") de la ciudad '" + ciudad.Nombre + "' no coincide con ninguna celda de la malla. Se omitió.");
                    continue;
                }

                if (celda.Tipo != TipoCelda.Camino)
                {
                    resultado.AddWarning("La unidad militar en (" + fila + "," + columna + ") de la ciudad '" + ciudad.Nombre + "' está sobre una celda que no es de tipo Camino (" + celda.Tipo + "). Se omitió.");
                    continue;
                }

                celda.CapacidadMilitar = capacidad;
            }
        }

        private string QuitarComillas(string texto)
        {
            if (texto.Length >= 2 && texto.StartsWith("\"") && texto.EndsWith("\""))
            {
                return texto.Substring(1, texto.Length - 2);
            }
            return texto;
        }

        private TipoCelda? MapearCaracterATipo(char caracter)
        {
            switch (caracter)
            {
                case '*': return TipoCelda.Intransitable;
                case ' ': return TipoCelda.Camino;
                case 'E': return TipoCelda.PuntoEntrada;
                case 'C': return TipoCelda.UnidadCivil;
                case 'R': return TipoCelda.Recurso;
                default: return null;
            }
        }

        

        private int LeerEntero(XmlNode nodo, string nombreAtributo, int valorPorDefecto)
        {
            string texto = LeerAtributoTexto(nodo, nombreAtributo, "");
            int valor;
            if (int.TryParse(texto, out valor))
            {
                return valor;
            }
            return valorPorDefecto;
        }

        private string LeerAtributoTexto(XmlNode nodo, string nombreAtributo, string valorPorDefecto)
        {
            if (nodo.Attributes == null)
            {
                return valorPorDefecto;
            }

            XmlAttribute atributo = nodo.Attributes[nombreAtributo];

            if (atributo==null)
            {
                return valorPorDefecto;
            }

            return atributo.Value.Trim();
        }

        public void CargarMallas(ListaCiudad listaCiudad, ListaRobots listaRobots, ListaMallas listaMallas)
        {
            if (listaCiudad==null)
            {
                return;
            }

           CiudadNodo ciudadActual = listaCiudad.Primero;

            while (ciudadActual!=null)
            {
                CeldaNodo celdaActual = ciudadActual.Dato.listaCeldas.Primero;
                while (celdaActual!=null)
                {
                    MallaTablero mallaNueva = new MallaTablero(ciudadActual.Dato.Filas, ciudadActual.Dato.Columnas);
                    mallaNueva.Insertar(celdaActual.Dato, celdaActual.Dato.Fila, celdaActual.Dato.Columna);
                    celdaActual = celdaActual.Siguiente;
                }

                ciudadActual = ciudadActual.Siguiente;
                
            }

        }

    }
}
