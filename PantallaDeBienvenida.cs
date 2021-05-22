using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pang
{
    class PantallaDeBienvenida
    {
        private SpriteFont fuente;
        private SpriteFont fuenteOpciones;
        private GestorDePantallas gestor;
        private int p, a, n, g;

        public PantallaDeBienvenida(GestorDePantallas gestor)
        {
            this.gestor = gestor;
            p = 1500;
            a = 1800;
            n = 2100;
            g = 2400;
        }

        public void CargarContenidos(ContentManager Content)
        {
            fuente = Content.Load<SpriteFont>("GAME_glm");
            fuenteOpciones = Content.Load<SpriteFont>("Arial");
        }

        public void Actualizar(GameTime gameTime)
        {
            if (p > 350)
                p -= 5;
            if (a > 450)
                a -= 5;
            if (n > 550)
                n -= 5;
            if (g > 650)
                g -= 5;

            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                gestor.modoActual = GestorDePantallas.MODO.JUEGO;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                gestor.Terminar();
            }
        }

        public void Dibujar(GameTime gameTime, SpriteBatch spriteBatch)
        {
            
            spriteBatch.DrawString(fuente, "P",
            new Vector2(p, 150),
            Color.White);

           
            spriteBatch.DrawString(fuente, "A",
            new Vector2(a, 150),
            Color.White);

            spriteBatch.DrawString(fuente, "N",
            new Vector2(n, 150),
            Color.White);

            spriteBatch.DrawString(fuente, "G",
            new Vector2(g, 150),
            Color.White);


            spriteBatch.DrawString(fuenteOpciones, "INTRO para JUGAR",
                new Vector2(200, 500),
                Color.White);
            spriteBatch.DrawString(fuenteOpciones, "ESCAPE para SALIR",
                new Vector2(700, 500),
                Color.White);
        }
    }
}
