using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pang
{
    public class GestorDePantallas : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private PantallaDeBienvenida bienvenida;
        private PantallaDeJuego juego;
        private PantallaDeCreditos creditos;

        public enum MODO {BIENVENIDA, JUEGO, CREDITOS};
        public MODO modoActual { get; set; }

        public GestorDePantallas()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1152;
            graphics.PreferredBackBufferHeight = 621;
            graphics.ApplyChanges();

            bienvenida = new PantallaDeBienvenida(this);
            juego = new PantallaDeJuego(this);
            creditos = new PantallaDeCreditos(this);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            bienvenida.CargarContenidos(Content);
            juego.CargarContenidos(Content);
            creditos.CargarContenidos(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            switch (modoActual)
            {
                case MODO.JUEGO: juego.Actualizar(gameTime, Content); break;
                case MODO.BIENVENIDA: bienvenida.Actualizar(gameTime); break;
                case MODO.CREDITOS: creditos.Actualizar(gameTime); break;
            }

            if (juego.Terminado == true)
            {
                modoActual = MODO.CREDITOS;
                juego.Terminado = false;
                juego.CargarContenidos(Content);
            }
                

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            switch (modoActual)
            {
                case MODO.JUEGO: juego.Dibujar(gameTime, spriteBatch); break;
                case MODO.BIENVENIDA: bienvenida.Dibujar(gameTime, spriteBatch); break;
                case MODO.CREDITOS: creditos.Dibujar(gameTime, spriteBatch); break;
            }
            spriteBatch.End();           

            base.Draw(gameTime);
        }

        public void Terminar()
        {
            Exit();
        }
    }
}
