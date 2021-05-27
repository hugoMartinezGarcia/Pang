using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Pang
{
    public class Marcador
    {
        public int PuntosAMostrar { get; set; }
        private int vidas;
        private SpriteFont fuente;
        public float segundosRestantes;

        public object Thread { get; internal set; }

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
            PuntosAMostrar = 0;
        }

        public void IncrementarPuntos(int cantidad)
        {
            PuntosAMostrar += cantidad;
        }

        public void Dibujar(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(fuente,
                "Puntos: " + PuntosAMostrar,
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
