using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Pang
{
    class PantallaDeCreditos
    {
        private SpriteFont fuente;
        private GestorDePantallas gestor;
        private Puntuacion puntuacion;
        private int incremento;
        List<string> puntuaciones;

        public PantallaDeCreditos(GestorDePantallas gestor)
        {
            this.gestor = gestor;
            incremento = 0;
            puntuacion = new Puntuacion();
            puntuaciones = puntuacion.CargarPuntuaciones();
        }

        public void CargarContenidos(ContentManager Content)
        {
            fuente = Content.Load<SpriteFont>("Arial");
        }

        public void Actualizar(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                gestor.modoActual = GestorDePantallas.MODO.BIENVENIDA;
            }
        }

        public void Dibujar(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(fuente, "MEJORES PUNTUACIONES",
                new Vector2(450, 100),
                Color.White);

            if (puntuaciones.Count > 0)
            {
                incremento = 0;
                foreach (String linea in puntuaciones)
                {
                    spriteBatch.DrawString(fuente, linea,
                    new Vector2(500, 150 + incremento),
                    Color.White);
                    incremento += 50;
                }
            }
            

        }
    }
}
