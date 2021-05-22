using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pang
{
    class BolaMediana : Bola
    {
        public BolaMediana(int x, int y, ContentManager Content)
            : base(x, y, "balon2", Content)
        {
            Caida = false;
        }

        public override void Dibujar(SpriteBatch spriteBatch)
        {
            if (Activo)
            {
                spriteBatch.Draw(imagen, new Rectangle(
                (int)X, (int)Y,
                imagen.Width, imagen.Height), Color.White);
            }

        }
    }
}
