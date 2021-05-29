using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Pang
{
    class Bola : Sprite
    {
        public int PosParabolaActual;
        public bool Caida { get; set; }
        public Vector2 Tamanyo;
        

        public int[] posParabola = {-19,-19,-19,-19,-17,-17,-17,-17,-15,-15,-15,-15,
                -13,-13,-13,-13,-11,-11,-11,-11,-11,-9,-9,-9,-9,-9,-7,-7,-7,-7,-5,
                -5,-5,-5,-5,-3,-3,-3,-3,0,0,0,0,0,3,3,3,3,5,5,5,5,5,7,7,7,7,9,9,
                9,9,9,11,11,11,11,11,13,13,13,13,15,15,15,15,17,17,17, 17,19,19,
                19,19};

        public Bola(int x, int y, ContentManager Content)
            : base(x, y, new string[] { "balon" }, Content)
        {
            PosParabolaActual = 0;
            Caida = true;
            VelocX = 4;
            Tamanyo = new Vector2();

            CargarSecuencia((byte) direcciones.DESAPARECIENDO,
                new string[] { "explosion1", "explosion2", "explosion3" },
                Content);
            CambiarDireccion(0);
        }

        public void Explotar()
        {
            Chocable = false;
            CambiarDireccion((byte)direcciones.DESAPARECIENDO);

        }

        public Bola(int x, int y, string nombreImagen, ContentManager Content)
            : base(x, y, nombreImagen, Content)
        { }

        public void MoverY()
        {
            Y += posParabola[PosParabolaActual];

            if (PosParabolaActual < posParabola.Length - 1)
                PosParabolaActual++;
            else
                PosParabolaActual = 0;
        }

        public void MoverAPosicionInicial()
        {
            Random posicionAzar = new Random();
            X = posicionAzar.Next(100, 900);
            Y = posicionAzar.Next(50, 200);
            Caida = true;
            PosParabolaActual = 0;
        }
    }
}
