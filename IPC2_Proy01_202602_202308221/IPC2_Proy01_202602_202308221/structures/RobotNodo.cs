using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPC2_Proy01_202602_202308221.model;

namespace IPC2_Proy01_202602_202308221.structures
{
    public class RobotNodo
    {
        public Robot Dato { get; set; }

        public RobotNodo Siguiente {  get; set; }

        public RobotNodo(Robot robos)
        {
            Dato = robos;
            Siguiente = null;
        }

    }
}
