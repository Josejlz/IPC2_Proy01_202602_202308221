using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using IPC2_Proy01_202602_202308221.model;
using IPC2_Proy01_202602_202308221.controller;
using IPC2_Proy01_202602_202308221.view;


namespace IPC2_Proy01_202602_202308221
{
    internal static class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var model = new Model();
            var view = new IPC2_Proy01_202602_202308221.view.View();
            var controller = new Controller(model, view);
            var apli=new Form1();
            apli.setController(controller);
            Application.Run(apli);
        }
    }

}
