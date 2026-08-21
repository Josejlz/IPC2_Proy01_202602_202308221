using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPC2_Proy01_202602_202308221.model;
using IPC2_Proy01_202602_202308221.Persistencia;
using IPC2_Proy01_202602_202308221.structures;

namespace IPC2_Proy01_202602_202308221.controller
{
    public class Controller
    {
        public Model _model;
        private Lector _lector;

        public Controller(Model model)
        {
            _model = model;
            _lector = new Lector();
            
        }

        public Controller()
        {
            _model = new Model();
            _lector = new Lector();
        }

        public string getCurrentLogData()
        {
            return _model.innerData();
        }

        public ResultadoCarga CargarConfiguracion(string ruta)
        {
            return _lector.CargarConfiguracion(ruta, _model.listaCiudades, _model.listaRobots);
        }

        public Ciudad ObtenerCiudadPorNombre(string nombre)
        {
            return _model.listaCiudades.BuscarPorNombre(nombre);
        }

    }
}
