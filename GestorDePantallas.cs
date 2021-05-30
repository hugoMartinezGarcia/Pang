using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pang
{
    public class GestorDePantallas : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private PantallaDeBienvenida bienvenida;
        private PantallaDeJuego juego;
        private PantallaDeCreditos creditos;
        private PantallaDePuntuaciones puntuaciones;
        public int Puntos { get; set; }

        public enum MODO {BIENVENIDA, JUEGO, PUNTUACIONES, CREDITOS};
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
            puntuaciones = new PantallaDePuntuaciones(this);
            creditos = new PantallaDeCreditos(this);
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            bienvenida.CargarContenidos(Content);
            juego.CargarContenidos(Content);
            puntuaciones.CargarContenidos(Content);
            creditos.CargarContenidos(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            switch (modoActual)
            {
                case MODO.BIENVENIDA: bienvenida.Actualizar(gameTime); break;
                case MODO.JUEGO: juego.Actualizar(gameTime, Content); break;
                case MODO.PUNTUACIONES: puntuaciones.Actualizar(gameTime); break;
                case MODO.CREDITOS: creditos.Actualizar(gameTime); break;
            }

            if (juego.Terminado == true)
            {
                Puntos = juego.marcador.PuntosAMostrar;
                modoActual = MODO.PUNTUACIONES;
                juego.Terminado = false;
                juego.CargarContenidos(Content);
            }          

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.RoyalBlue);

            spriteBatch.Begin();
            switch (modoActual)
            {
                case MODO.JUEGO: juego.Dibujar(gameTime, spriteBatch); break;
                case MODO.BIENVENIDA: bienvenida.Dibujar(gameTime, spriteBatch); break;
                case MODO.PUNTUACIONES: puntuaciones.Dibujar(gameTime, spriteBatch); break;
                case MODO.CREDITOS: creditos.Dibujar(gameTime, spriteBatch); break;
            }
            spriteBatch.End();           

            base.Draw(gameTime);
        }

        public int MoverTexto(ref int posInicial, int posFinal, int velocidad)
        {
            if (posInicial > posFinal)
                posInicial -= velocidad;

            return posInicial;
        }

        public void Terminar()
        {
            Exit();
        }
    }
}
