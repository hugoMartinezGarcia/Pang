using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pang
{
    class Disparo : Sprite
    {
        public List<Disparo> PosDisparo { get; set; }

        public Disparo(int x, int y, ContentManager Content)
            : base(x, y, "disparo", Content)
        {
            VelocY = 300;
            Activo = false;
            PosDisparo = new List<Disparo>();
        }

        public void Mover(GameTime gameTime, ContentManager Content)
        {
            if (Activo)
            {
                Y -= VelocY * (float)gameTime.ElapsedGameTime.TotalSeconds;

                PosDisparo.Add(new Disparo((int)X, (int)Y, Content));

                PosDisparo[0] = new Disparo((int)PosDisparo[0].X,
                    (int)PosDisparo[0].Y - (int)(VelocY * (float)gameTime.ElapsedGameTime.TotalSeconds), Content);


                for (int i = PosDisparo.Count - 1; i > 0; i--)
                {
                    PosDisparo[i] = PosDisparo[i - 1];
                }

                if (PosDisparo[0].Y < 24)
                {
                    Activo = false;
                    PosDisparo.Clear();
                }
            }

        }

        public override void Dibujar(SpriteBatch spriteBatch)
        {
            if (Activo)
            {
                foreach (Sprite pos in PosDisparo)
                {
                    spriteBatch.Draw(imagen, new Rectangle(
                        (int)pos.X,
                        (int)pos.Y,
                        imagen.Width, imagen.Height), Color.White);
                }
            }
        }
    }
}
