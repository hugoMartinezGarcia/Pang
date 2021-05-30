using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Pang
{
    class Nivel03 : Nivel
    {
        public Nivel03(ContentManager Content)
            : base(Content)
        {
            Fondo = new Sprite(0, 0, "fondoNivel3", Content);
            Bolas.Add(new Bola(149, 40, Content));
            Bolas.Add(new Bola(460, 40, Content));
            Bolas.Add(new Bola(760, 40, Content));
            Item = new Sprite(600, 565, "reloj", Content);
        }

        public override void ActivarItem(bool itemUsado)
        {
            if (SegundosRestantes <= 30 && SegundosRestantes >= 20 && !itemUsado)
            {
                Item.Chocable = true;
                Item.Visible = true;
            }
            else
            {
                Item.Chocable = false;
                Item.Visible = false;
            }
        }
    }
}
