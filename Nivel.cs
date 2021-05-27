using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Pang
{
    class Nivel
    {
        public List<Bola> bolas { get; }
        public Sprite Fondo { get; }
        public int Marco { get; }

        public float SegundosRestantes { get; set; }
        public bool TiempoTerminado { get; set; }

        public Nivel(ContentManager Content)
        {
            Fondo = new Sprite(0, 0, "fondoNivel1", Content);
            bolas = new List<Bola>();
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
            
            foreach (Bola b in bolas)
            {
                b.Dibujar(spriteBatch);
            }
        }
    }

    
}
