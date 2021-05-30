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
        private int posYTitulo, posYSubtitulo, posYAutor;


        public PantallaDeCreditos(GestorDePantallas gestor)
        {
            this.gestor = gestor;
            posYTitulo = 650;
            posYSubtitulo = 700;
            posYAutor = 850;
        }

        public void CargarContenidos(ContentManager Content)
        {
            fuente = Content.Load<SpriteFont>("Arial");
        }

        public void Actualizar(GameTime gameTime)
        {
            gestor.MoverTexto(ref posYTitulo, -100, 2);
            gestor.MoverTexto(ref posYSubtitulo, -100, 2);
            gestor.MoverTexto(ref posYAutor, -100, 2);

            if (posYAutor == -10 || Keyboard.GetState().IsKeyDown(Keys.S))
            {
                gestor.modoActual = GestorDePantallas.MODO.BIENVENIDA;

                posYTitulo = 650;
                posYSubtitulo = 700;
                posYAutor = 850;
            }
        }

        public void Dibujar(GameTime gameTime, SpriteBatch spriteBatch)
        {

            spriteBatch.DrawString(fuente, "PANG ORIGINAL:",
                new Vector2(450, posYTitulo),
                Color.White);

            spriteBatch.DrawString(fuente, "(C) 1989 Mitchell Corporation",
                new Vector2(400, posYSubtitulo), Color.White);

            spriteBatch.DrawString(fuente, "REMAKE: Por Hugo Martinez",
                new Vector2(400, posYAutor),
                Color.White);
        }
    }
}
