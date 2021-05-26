using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Pang
{
    class Nivel
    {
        public Bola bola { get; }
        public Sprite Fondo { get; }
        public int Marco { get; }

        public float SegundosRestantes { get; set; }
        public bool TiempoTerminado { get; set; }

        public Nivel(ContentManager Content)
        {
            Fondo = new Sprite(0, 0, "fondoNivel1", Content);
            bola = new Bola(349, 40, Content);
            Marco = 24;
        }

        public virtual void Reiniciar()
        {
            SegundosRestantes = 10;
            TiempoTerminado = false;
        }

        public void Animar(GameTime gameTime)
        {
            SegundosRestantes -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            TiempoTerminado = SegundosRestantes <= 0;
        }

        public void Dibujar(SpriteBatch spriteBatch)
        {
            Fondo.Dibujar(spriteBatch);
            bola.Dibujar(spriteBatch);
        }
    }

    
}
