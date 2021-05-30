using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Pang
{
    class Nivel01 : Nivel
    {

        public Nivel01(ContentManager Content) 
            : base(Content)
        {
            Fondo = new Sprite(0, 0, "fondoNivel1", Content);
            Bolas.Add(new Bola(349, 40, Content));
            Item = new Sprite(200, 565, "reloj", Content);

        }

        public override void ActivarItem(bool itemUsado)
        {
            if (SegundosRestantes <= 85 && SegundosRestantes >= 70 && !itemUsado)
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
