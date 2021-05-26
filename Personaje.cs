using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pang
{
    class Personaje : Sprite
    {
        public int Vidas { get; set; }

        public Personaje(int x, int y, ContentManager Content)
            : base(x, y, new string[] {"personajeD"}, Content)
        {
            VelocX = 240;
            Vidas = 3;
            CargarSecuencia((byte)direcciones.IZQUIERDA, 
                new string[] { "personajeI1", "personajeI2", "personajeI3", 
                    "personajeI4", "personajeI5", "personajeI4", "personajeI3", "personajeI2" }, 
                Content);

            CargarSecuencia((byte)direcciones.DERECHA,
                new string[] { "personajeD1", "personajeD2", "personajeD3", "personajeD4",
                "personajeD5", "personajeD4", "personajeD3", "personajeD2" },
                Content);

            CambiarDireccion((byte)direcciones.ESTATICO);
            tiempoEnCadaFotograma = 100;
        }

        public void MoverDerecha(GameTime gameTime)
        {
            CambiarDireccion((byte)direcciones.DERECHA);
            base.Animar(gameTime);
            float desplazamiento = VelocX * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (X + desplazamiento < 1152 - this.Ancho - 24)
                X += desplazamiento;
        }

        public void MoverIzquierda(GameTime gameTime)
        {
            CambiarDireccion((byte)direcciones.IZQUIERDA);
            base.Animar(gameTime);
            float desplazamiento = VelocX * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (X - desplazamiento > 24)
                X -= desplazamiento;
        }

        public void PermanecerEstatico(GameTime gameTime)
        {
            CambiarDireccion((byte)direcciones.ESTATICO);
            base.Animar(gameTime);
        }

        public void MoverAPosicionInicial()
        {
            X = 549;
            Y = 538;
        }
    }
}
