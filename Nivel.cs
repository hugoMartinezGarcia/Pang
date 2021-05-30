using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Pang
{
    abstract class Nivel
    {
        public List<Bola> Bolas { get; }
        public Sprite Fondo { get; set; }
        public int Marco { get; }
        public Sprite Item { get; set; }

        public float SegundosRestantes { get; set; }
        public bool TiempoTerminado { get; set; }

        public Nivel(ContentManager Content)
        {
            Bolas = new List<Bola>();
            Marco = 24;
        }

        public virtual void Reiniciar()
        {
            SegundosRestantes = 100;
            TiempoTerminado = false;
        }

        // Método abstracto para activar el ítem unos segundos determinados en cada nivel
        public abstract void ActivarItem(bool itemUsado);


        public void Animar(GameTime gameTime)
        {
            SegundosRestantes -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            TiempoTerminado = SegundosRestantes <= 0;
        }

        public void CrearNuevaBola(ContentManager Content)
        {
            Bola b = new Bola(0, 0, Content);
            b.MoverAPosicionInicial();
            Bolas.Add(b);
        }

        public virtual void Dibujar(SpriteBatch spriteBatch)
        {
            Fondo.Dibujar(spriteBatch);
            
            foreach (Bola b in Bolas)
            {
                b.Dibujar(spriteBatch);
            }

            if (Item.Visible)
            {
                Item.Dibujar(spriteBatch);
            }
        }
    }  
}
