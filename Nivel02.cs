using Microsoft.Xna.Framework.Content;

namespace Pang
{
    class Nivel02 : Nivel
    {
        public Nivel02(ContentManager Content)
            : base(Content)
        {
            Fondo = new Sprite(0, 0, "fondoNivel2", Content);
            bolas.Add(new Bola(249, 40, Content));
            bolas.Add(new Bola(560, 40, Content));
        }

    }
}