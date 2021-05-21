using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pang
{
    class PantallaDeBienvenida
    {
        private SpriteFont fuente;
        private GestorDePantallas gestor;

        public PantallaDeBienvenida(GestorDePantallas gestor)
        {
            this.gestor = gestor;
        }

        public void CargarContenidos(ContentManager Content)
        {
            fuente = Content.Load<SpriteFont>("Arial");
        }

        public void Actualizar(GameTime gameTime)
        {
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
            spriteBatch.DrawString(fuente, "INTRO para jugar",
                new Vector2(350, 400),
                Color.White);
            spriteBatch.DrawString(fuente, "ESCAPE para salir",
                new Vector2(350, 500),
                Color.White);
        }
    }
}
