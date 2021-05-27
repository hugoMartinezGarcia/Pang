using System;


namespace Pang
{
    class Puntuacion : IComparable<Puntuacion>
    {
        public string Nombre { get; set; }
        public int Puntos { get; set; }

        public Puntuacion(string nombre, int puntuacion)
        {
            this.Nombre = nombre;
            this.Puntos = puntuacion;
        }

        public void CrearDesdeFichero(string linea)
        {
            string[] fragmentos = linea.Split(';');

            Nombre = fragmentos[0];
            Puntos = Convert.ToInt32(fragmentos[1]);
        }

        public string PrepararParaFichero()
        {
            return Nombre + ";" + Puntos;
        }

        public int CompareTo(Puntuacion otro)
        {
            return otro.Puntos.CompareTo(Puntos);
        }

    }
}
