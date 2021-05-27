using Microsoft.Xna.Framework.Content;

namespace Pang
{
    class Nivel01 : Nivel
    {
        public Nivel01(ContentManager Content) 
            : base(Content)
        {
            bolas.Add(new Bola(349, 40, Content));
        }

    }
}
