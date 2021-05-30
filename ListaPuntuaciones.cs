using System;
using System.Collections.Generic;
using System.IO;

namespace Pang
{
    class ListaPuntuaciones
    {
        private const string NOMBRE_ARCHIVO = "puntuaciones.txt";
        
        public List<Puntuacion> CargarPuntuaciones()
        {
            List<Puntuacion> puntuaciones = new List<Puntuacion>();

            if (File.Exists(NOMBRE_ARCHIVO))
            {
                try
                {
                    StreamReader fichero = new StreamReader(NOMBRE_ARCHIVO);
                    string linea;

                    do
                    {
                        linea = fichero.ReadLine();

                        if (linea != null)
                        {
                            Puntuacion p = new Puntuacion("", 0);
                            p.CrearDesdeFichero(linea);

                            puntuaciones.Add(p);
                        }
                    }
                    while (linea != null);

                    fichero.Close();
                    puntuaciones.Sort();
                }
                catch (IOException)
                {
                    Console.WriteLine("Error de lectura");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                }
            }
            return puntuaciones;
        }

        public void GuardarPuntuaciones(List<Puntuacion> puntuaciones)
        {
            try
            {
                StreamWriter fichero = new StreamWriter(NOMBRE_ARCHIVO);

                foreach (Puntuacion p in puntuaciones)
                {
                    fichero.WriteLine(p.PrepararParaFichero());
                }

                fichero.Close();
            }
            catch (IOException)
            {
                Console.WriteLine("Error de escritura");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }
    }
}
