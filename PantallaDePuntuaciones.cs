using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Pang
{
    class PantallaDePuntuaciones
    {
        const string NOMBRE_ARCHIVO = "puntuaciones.txt";
        private SpriteFont fuente;
        private int incremento;
        List<string> puntuaciones;
        private GestorDePantallas gestor;

        public PantallaDePuntuaciones(GestorDePantallas gestor)
        {
            this.gestor = gestor;
            incremento = 0;
            puntuaciones = CargarPuntuaciones();
        }

        public void CargarContenidos(ContentManager Content)
        {
            fuente = Content.Load<SpriteFont>("Arial");
        }

        public void Actualizar(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                gestor.modoActual = GestorDePantallas.MODO.CREDITOS;
            }
        }

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
