using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPC2_Proy01_202602_202308221.model;
using IPC2_Proy01_202602_202308221.view;

namespace IPC2_Proy01_202602_202308221.controller
{
    public class Controller
    {
        public  Model _model { get; set; }
        public View _view { get; set; }

        public Controller(Model model, View view)
        {
            this._model = model;
            this._view = view;
        }

        public Controller()
        {
            this._model = new Model();
            this._view = new View();
        }

        

    }
}
