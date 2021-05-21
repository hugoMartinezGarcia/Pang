using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pang
{
    class Personaje : Sprite
    {
        public Personaje(int x, int y, string nombreImagen, ContentManager Content)
            : base(x, y, nombreImagen, Content)
        {
        }
    }
}
