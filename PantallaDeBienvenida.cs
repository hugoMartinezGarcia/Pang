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
        private int posXLetraP, posXLetraA, posXLetraN, porXLetraG;

        public PantallaDeBienvenida(GestorDePantallas gestor)
        {
            this.gestor = gestor;
            posXLetraP = 1500;
            posXLetraA = 1800;
            posXLetraN = 2100;
            porXLetraG = 2400;
        }

        public void CargarContenidos(ContentManager Content)
        {
            fuente = Content.Load<SpriteFont>("GAME_glm");
            fuenteOpciones = Content.Load<SpriteFont>("Arial");
        }

        public void Actualizar(GameTime gameTime)
        {
            gestor.MoverTexto(ref posXLetraP, 350, 5);
            gestor.MoverTexto(ref posXLetraA, 450, 5);
            gestor.MoverTexto(ref posXLetraN, 550, 5);
            gestor.MoverTexto(ref porXLetraG, 650, 5);

            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                gestor.modoActual = GestorDePantallas.MODO.JUEGO;

                posXLetraP = 1500;
                posXLetraA = 1800;
                posXLetraN = 2100;
                porXLetraG = 2400;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                gestor.Terminar();
            }
        }

        public void Dibujar(GameTime gameTime, SpriteBatch spriteBatch)
        {
            
            spriteBatch.DrawString(fuente, "P",
            new Vector2(posXLetraP, 150),
            Color.White);

           
            spriteBatch.DrawString(fuente, "A",
            new Vector2(posXLetraA, 150),
            Color.White);

            spriteBatch.DrawString(fuente, "N",
            new Vector2(posXLetraN, 150),
            Color.White);

            spriteBatch.DrawString(fuente, "G",
            new Vector2(porXLetraG, 150),
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
