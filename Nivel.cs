using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pang
{
    class Nivel
    {
        public List<Bola> bolas;
        protected Sprite fondo;

        public float SegundosRestantes { get; set; }
        public bool TiempoTerminado;

        public Nivel(ContentManager Content)
        {
            fondo = new Sprite(0, 0, "fondoNivel1", Content);
            Reiniciar();
        }

        public virtual void Reiniciar()
        {
            bolas = new List<Bola>();
            SegundosRestantes = 10;
            TiempoTerminado = false;
        }

        public void Animar(GameTime gameTime)
        {
            SegundosRestantes -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            TiempoTerminado = SegundosRestantes <= 0;
        }
    }

    
}
