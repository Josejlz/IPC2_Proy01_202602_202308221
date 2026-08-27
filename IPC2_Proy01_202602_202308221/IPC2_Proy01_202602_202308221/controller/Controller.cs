using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPC2_Proy01_202602_202308221.Logica;
using IPC2_Proy01_202602_202308221.model;
using IPC2_Proy01_202602_202308221.Persistencia;
using IPC2_Proy01_202602_202308221.structures;

namespace IPC2_Proy01_202602_202308221.controller
{
    public class Controller
    {
        public Model _model;
        private Lector _lector;
        private BuscadorRuta _buscadorRuta;
        private GeneradorGraphviz _generadorGraphviz;

        public Controller(Model model)
        {
            _model = model;
            _lector = new Lector();
            _buscadorRuta = new BuscadorRuta();
            _generadorGraphviz = new GeneradorGraphviz();
        }

        public Controller()
        {
            _model = new Model();
            _lector = new Lector();
            _buscadorRuta = new BuscadorRuta();
            _generadorGraphviz = new GeneradorGraphviz();
        }

        public string getCurrentLogData()
        {
            return _model.innerData(true);
        }

        public ResultadoCarga CargarConfiguracion(string ruta)
        {
            return _lector.CargarConfiguracion(ruta, _model.listaCiudades, _model.listaRobots, _model.listaMallas);
        }

        public Ciudad ObtenerCiudadPorNombre(string nombre)
        {
            return _model.listaCiudades.BuscarPorNombre(nombre);
        }

        public ListaCiudad ObtenerCiudades()
        {
            return _model.listaCiudades;
        }

        public ListaRobots ObtenerRobots()
        {
            return _model.listaRobots;
        }

        public ListaCelda ObtenerPuntosEntrada(string nombreCiudad)
        {
            Ciudad ciudad = ObtenerCiudadPorNombre(nombreCiudad);
            if (ciudad == null) return new ListaCelda();
            return FiltroCeldas.ObtenerPorTipo(ciudad.listaCeldas, TipoCelda.PuntoEntrada);
        }

        public ListaCelda ObtenerCiviles(string nombreCiudad)
        {
            Ciudad ciudad = ObtenerCiudadPorNombre(nombreCiudad);
            if (ciudad == null) return new ListaCelda();
            return FiltroCeldas.ObtenerPorTipo(ciudad.listaCeldas, TipoCelda.UnidadCivil);
        }

        public ListaCelda ObtenerRecursos(string nombreCiudad)
        {
            Ciudad ciudad = ObtenerCiudadPorNombre(nombreCiudad);
            if (ciudad == null) return new ListaCelda();
            return FiltroCeldas.ObtenerPorTipo(ciudad.listaCeldas, TipoCelda.Recurso);
        }

        // para misiones

        public ResultadoMision EjecutarMisionRescate(string nombreCiudad, Celda civilObjetivo, string nombreRobot, out string rutaImagen)
        {
            rutaImagen = null;

            Ciudad ciudad = ObtenerCiudadPorNombre(nombreCiudad);
            MallaTablero malla = _model.listaMallas.BuscarPorNombre(nombreCiudad);
            Robot robot = _model.listaRobots.BuscarPorNombre(nombreRobot);
            ListaCelda entradas = ObtenerPuntosEntrada(nombreCiudad);

            if (ciudad == null || malla == null || !(robot is ChapinRescue) || entradas.isEmpty())
            {
                ResultadoMision fallo = new ResultadoMision();
                fallo.TipoMision = "rescate";
                fallo.MensajeError = "Mision Imposible";
                return fallo;
            }

            ResultadoMision resultado = _buscadorRuta.BuscarRutaRescate(malla, ciudad.listaCeldas, entradas.Primero.Dato, civilObjetivo, (ChapinRescue)robot);

            if (resultado.Exito)
            {
                rutaImagen = _generadorGraphviz.GenerarImagenMision(ciudad, malla, resultado.Camino, "Ruta de rescate:");
            }

            return resultado;
        }

        public ResultadoMision EjecutarMisionExtraccion(string nombreCiudad, Celda recursoObjetivo, string nombreRobot, out string rutaImagen)
        {
            rutaImagen = null;

            Ciudad ciudad = ObtenerCiudadPorNombre(nombreCiudad);
            MallaTablero malla = _model.listaMallas.BuscarPorNombre(nombreCiudad);
            Robot robot = _model.listaRobots.BuscarPorNombre(nombreRobot);
            ListaCelda entradas = ObtenerPuntosEntrada(nombreCiudad);

            if (ciudad == null || malla == null || !(robot is ChapinFighter) || entradas.isEmpty())
            {
                ResultadoMision fallo = new ResultadoMision();
                fallo.TipoMision = "extraccion";
                fallo.MensajeError = "Mision Imposible";
                return fallo;
            }

            ResultadoMision resultado = _buscadorRuta.BuscarRutaExtraccion(malla, ciudad.listaCeldas, entradas.Primero.Dato, recursoObjetivo, (ChapinFighter)robot);

            if (resultado.Exito)
            {
                rutaImagen = _generadorGraphviz.GenerarImagenMision(ciudad, malla, resultado.Camino, "Ruta de extracción de recurso:");
            }

            return resultado;
        }

    }
}
