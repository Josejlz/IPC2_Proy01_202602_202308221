using IPC2_Proy01_202602_202308221.model;
using IPC2_Proy01_202602_202308221.structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPC2_Proy01_202602_202308221.Logica
{
    public class BuscadorRuta
    {
        // Mision de rescate
        public ResultadoMision BuscarRutaRescate(MallaTablero malla, ListaCelda listaCeldasCiudad, Celda entrada, Celda civilObjetivo, ChapinRescue robot)
        {
            ResultadoMision resultado = new ResultadoMision();
            resultado.TipoMision = "rescate";
            resultado.RobotUsado = robot;
            resultado.CeldaObjetivo = civilObjetivo;

            listaCeldasCiudad.ReiniciarVisitas();

            ListaCelda camino = new ListaCelda();
            bool encontrado = PasoRescate(malla, entrada.Fila, entrada.Columna,
                civilObjetivo.Fila, civilObjetivo.Columna, camino);

            resultado.Exito = encontrado;
            resultado.Camino = camino;
            if (!encontrado)
            {
                resultado.MensajeError = "Mision Imposible";
            }

            return resultado;
        }

        private bool PasoRescate(MallaTablero malla, int fila, int columna,
            int filaObjetivo, int columnaObjetivo, ListaCelda camino)
        {
            NodoCeldaMalla nodo = malla.ObtenerNodoMalla(fila, columna);
            if (nodo == null) return false;

            Celda celda = nodo.Dato;
            if (celda == null) return false;
            if (celda.Visitada) return false;

            if (celda.Tipo == TipoCelda.Intransitable) return false;
            if (celda.Tipo == TipoCelda.Recurso) return false;
            if (celda.TieneUnidadMilitar) return false; // ChapinRescue no puede enfrentarlas

            celda.Visitada = true;
            camino.Insertar(celda);

            bool esObjetivo = (fila == filaObjetivo && columna == columnaObjetivo);
            if (esObjetivo)
            {
                return true;
            }

            if (PasoRescate(malla, fila - 1, columna, filaObjetivo, columnaObjetivo, camino)) return true;
            if (PasoRescate(malla, fila + 1, columna, filaObjetivo, columnaObjetivo, camino)) return true;
            if (PasoRescate(malla, fila, columna - 1, filaObjetivo, columnaObjetivo, camino)) return true;
            if (PasoRescate(malla, fila, columna + 1, filaObjetivo, columnaObjetivo, camino)) return true;

            // Retroceso 
            camino.EliminarUltimo();
            celda.Visitada = false;
            return false;
        }

        // miaion sw wextraccion
        public ResultadoMision BuscarRutaExtraccion(MallaTablero malla, ListaCelda listaCeldasCiudad, Celda entrada, Celda recursoObjetivo, ChapinFighter robot)
        {
            ResultadoMision resultado = new ResultadoMision();
            resultado.TipoMision = "extraccion";
            resultado.RobotUsado = robot;
            resultado.CeldaObjetivo = recursoObjetivo;
            resultado.CapacidadInicial = robot.Capacidad;

            listaCeldasCiudad.ReiniciarVisitas();

            ListaCelda camino = new ListaCelda();
            int capacidadActual = robot.Capacidad;

            bool encontrado = PasoExtraccion(malla, entrada.Fila, entrada.Columna, recursoObjetivo.Fila, recursoObjetivo.Columna, camino, ref capacidadActual);

            resultado.Exito = encontrado;
            resultado.Camino = camino;
            resultado.CapacidadFinal = encontrado ? capacidadActual : robot.Capacidad;
            if (!encontrado)
            {
                resultado.MensajeError = "Mision Imposible";
            }

            return resultado;
        }

        private bool PasoExtraccion(MallaTablero malla, int fila, int columna, int filaObjetivo, int columnaObjetivo, ListaCelda camino, ref int capacidadActual)
        {
            NodoCeldaMalla nodo = malla.ObtenerNodoMalla(fila, columna);
            if (nodo == null) return false;

            Celda celda = nodo.Dato;
            if (celda == null) return false;
            if (celda.Visitada) return false;

            bool esObjetivo = (fila == filaObjetivo && columna == columnaObjetivo);

            if (celda.Tipo == TipoCelda.Intransitable) return false;
            if (celda.Tipo == TipoCelda.Recurso && !esObjetivo) return false;

            int capacidadUsadaAqui = 0;
            if (celda.TieneUnidadMilitar)
            {
                if (capacidadActual <= celda.CapacidadMilitar.Value)
                {
                    return false; // no logra superar a la unidad militar
                }
                capacidadUsadaAqui = celda.CapacidadMilitar.Value;
                capacidadActual -= capacidadUsadaAqui;
            }

            celda.Visitada = true;
            camino.Insertar(celda);

            if (esObjetivo)
            {
                return true;
            }

            if (PasoExtraccion(malla, fila - 1, columna, filaObjetivo, columnaObjetivo, camino, ref capacidadActual)) return true;
            if (PasoExtraccion(malla, fila + 1, columna, filaObjetivo, columnaObjetivo, camino, ref capacidadActual)) return true;
            if (PasoExtraccion(malla, fila, columna - 1, filaObjetivo, columnaObjetivo, camino, ref capacidadActual)) return true;
            if (PasoExtraccion(malla, fila, columna + 1, filaObjetivo, columnaObjetivo, camino, ref capacidadActual)) return true;

            // Retroceso
            camino.EliminarUltimo();
            celda.Visitada = false;
            if (capacidadUsadaAqui > 0)
            {
                capacidadActual += capacidadUsadaAqui;
            }
            return false;
        }
    }
}
