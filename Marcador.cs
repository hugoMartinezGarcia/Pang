using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Pang
{
    class Marcador
    {
        public int puntosAMostrar;
        private int vidas;
        private SpriteFont fuente;
        public float segundosRestantes;

        public Marcador(ContentManager Content)
        {
            fuente = Content.Load<SpriteFont>("Arial");
        }

        public void SetVidas(int vidas)
        {
            this.vidas = vidas;
        }

        public void SetSegundosRestantes(float segundos)
        {
            this.segundosRestantes = segundos;
        }

        public void ReiniciarPuntos()
        {
            puntosAMostrar = 0;
        }

        public void IncrementarPuntos(int cantidad)
        {
            puntosAMostrar += cantidad;
        }

        public void Dibujar(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(fuente,
                "Puntos: " + puntosAMostrar,
                new Vector2(1000, 25), Color.White);
            spriteBatch.DrawString(fuente,
                "Vidas: " + vidas,
                new Vector2(1000, 60), Color.White);
            spriteBatch.DrawString(fuente,
                "Tiempo: " + (int)segundosRestantes,
                new Vector2(1000, 95), Color.White);
        }
    }
}
