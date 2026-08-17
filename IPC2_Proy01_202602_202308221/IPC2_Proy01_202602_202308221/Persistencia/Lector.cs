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
        public ResultadoCarga CargarConfiguracion(string ruta, ListaCiudad listaCiudad, ListaCelda listaCelda, ListaRobots listaRobots)
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

                CargarRobots(documento, listaRobots, resultado);
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
                string tipo = LeerAtributoTexto(nodoRobot, "tipo", "");

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
                        resultado.AddWarning("La unidad ´"+nombre+"´ es ChapinFighter pero no tiene capacidad de batalla valida. Se omitió.");
                        continue;
                    }

                    robot = new ChapinFighter(nombre, capacidad);

                }
                else if (string.Equals(tipo, "ChapinRescue", StringComparison.OrdinalIgnoreCase))
                {
                    robot = new ChapinRescue(nombre);
                } else
                {
                    resultado.AddWarning("El robot ´"+nombre+"´ tiene un tipo desconocido (´"+tipo+"´). Se omitió.");
                    continue;
                }

                bool isNew = listaRobots.InsertarOActualizar(robot);

                if (isNew)
                {
                    resultado.RobotsNuevos = resultado.RobotsNuevos + 1;
                }

            }
        }

        public void CargarCiudades()
        {

        }

        public void CargarCeldas()
        {

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

    }
}
