using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pang
{
    class PantallaDeCreditos
    {
        private SpriteFont fuente;
        private GestorDePantallas gestor;


        public PantallaDeCreditos(GestorDePantallas gestor)
        {
            this.gestor = gestor;
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

            spriteBatch.DrawString(fuente, "PANG ORIGINAL:",
                new Vector2(450, 100),
                Color.White);

            spriteBatch.DrawString(fuente, "(C) 1989 Mitchell Corporation",
                new Vector2(400, 150), Color.White);

            spriteBatch.DrawString(fuente, "REMAKE: Por Hugo Martinez",
                new Vector2(400, 300),
                Color.White);

            spriteBatch.DrawString(fuente, "Pulsa S para volver",
                new Vector2(420
                , 450), Color.White);
        }
    }
}
