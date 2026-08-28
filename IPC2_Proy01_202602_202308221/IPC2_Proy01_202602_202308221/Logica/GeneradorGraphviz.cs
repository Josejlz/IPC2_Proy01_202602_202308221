using IPC2_Proy01_202602_202308221.model;
using IPC2_Proy01_202602_202308221.structures;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPC2_Proy01_202602_202308221.Logica
{
    internal class GeneradorGraphviz
    {
 
        private string rutaEjecutableDot = @"C:\Program Files\Graphviz\bin\dot.exe";

        public string GenerarImagenMision(Ciudad ciudad, MallaTablero malla, ListaCelda camino, string tituloMision)
        {
            string dot = ConstruirDot(ciudad, malla, camino, tituloMision);

            string carpetaSalida = Path.Combine(Path.GetTempPath(), "ChapinWarriors");
            Directory.CreateDirectory(carpetaSalida);

            string rutaDotFile = Path.Combine(carpetaSalida, "mision.dot");
            string rutaPngFile = Path.Combine(carpetaSalida, "mision.png");

            File.WriteAllText(rutaDotFile, dot);

            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = rutaEjecutableDot;
            psi.Arguments = "-Tpng \"" + rutaDotFile + "\" -o \"" + rutaPngFile + "\"";
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;

            using (Process proceso = Process.Start(psi))
            {
                proceso.WaitForExit();
            }

            return rutaPngFile;
        }

        private string ConstruirDot(Ciudad ciudad, MallaTablero malla, ListaCelda camino, string tituloMision)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("digraph mapa {");
            sb.AppendLine("  labelloc=\"t\";");
            sb.AppendLine("  label=\"" + tituloMision + "\";");
            sb.AppendLine("  node [shape=plaintext];");
            sb.AppendLine("  malla [label=<");
            sb.AppendLine("    <TABLE BORDER=\"0\" CELLBORDER=\"1\" CELLSPACING=\"0\" CELLPADDING=\"4\">");

            for (int fila = 1; fila <= ciudad.Filas; fila++)
            {
                sb.Append("      <TR>");
                for (int columna = 1; columna <= ciudad.Columnas; columna++)
                {
                    Celda celda = malla.obtenerCelda(fila, columna);
                    string color = ColorDeCelda(celda);
                    if (EstaEnCamino(camino, fila, columna))
                    {
       
                        if (celda.Tipo == TipoCelda.Camino)
                        {
                           
                            color = "burlywood2";
                        }
                    }

                    sb.Append("<TD WIDTH=\"18\" HEIGHT=\"18\" BGCOLOR=\"" + color + "\"></TD>");
                }
                sb.AppendLine("</TR>");
            }

            sb.AppendLine("    </TABLE>");
            sb.AppendLine("  >];");
            sb.AppendLine("}");

            return sb.ToString();
        }

        private bool EstaEnCamino(ListaCelda camino, int fila, int columna)
        {
            CeldaNodo actual = camino.Primero;
            while (actual != null)
            {
                if (actual.Dato.Fila == fila && actual.Dato.Columna == columna)
                {
                    return true;
                }
                actual = actual.Siguiente;
            }
            return false;
        }

        private string ColorDeCelda(Celda celda)
        {
            if (celda == null) return "black";

            switch (celda.Tipo)
            {
                case TipoCelda.Intransitable: return "black";
                case TipoCelda.PuntoEntrada: return "green3";
                case TipoCelda.Camino: return celda.TieneUnidadMilitar ? "red" : "white";
                case TipoCelda.UnidadCivil: return "blue";
                case TipoCelda.Recurso: return "gray";
                default: return "white";
            }
        }

    }
}
