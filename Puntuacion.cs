using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Pang
{
    class Puntuacion
    {
        const string NOMBRE_ARCHIVO = "puntuaciones.txt";
        public List<string> CargarPuntuaciones()
        {
            List<string> puntuaciones = new List<string>();

            if (File.Exists(NOMBRE_ARCHIVO))
                puntuaciones = new List<string>(File.ReadAllLines(NOMBRE_ARCHIVO));
            return puntuaciones;
        }
        

        public void GuardarPuntuaciones()
        {
            List<string> prueba = new List<string>();
            File.WriteAllLines(NOMBRE_ARCHIVO, prueba);
        }

    }
}
