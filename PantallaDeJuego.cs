using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Pang
{
    public class PantallaDeJuego
    {

        private Sprite fondo;
        private Sprite personaje;
        private Sprite balon;
        private int posParabolaActual;
        private Sprite disparo;
        private List<Sprite> posDisparo = new List<Sprite>();
        bool caida;
        int vidas;
        private int marco;

        private GestorDePantallas gestor;
        public bool Terminado { get; set; }
        private SpriteFont fuente;

        public PantallaDeJuego(GestorDePantallas gestor)
        {
            this.gestor = gestor;
            Terminado = false;
            posParabolaActual = 0;
            marco = 24;
            caida = true;
            vidas = 3;
        }

        public void CargarContenidos(ContentManager Content)
        {
            personaje = new Sprite(549, 538, "personaje", Content);
            fondo = new Sprite(0, 0, "fondoNivel1", Content);
            balon = new Sprite(349, 40, "balon", Content);
            balon.VelocX = 4;
            fuente = Content.Load<SpriteFont>("Arial");
            disparo = new Sprite(0, 0, "disparo", Content);
            disparo.Activo = false;
            disparo.VelocY = 5;
        }

        public void Actualizar(GameTime gameTime, ContentManager Content)
        {
            moverElementos(Content);
            MoverPersonaje(Content);
            ComprobarColisiones(Content);
        }

        private void moverElementos(ContentManager Content)
        {           
            if (caida && balon.Y + balon.Alto < fondo.Alto - marco)
                balon.Y += 10;
            else
                caida = false;

            /*
            int[] posParabola = {-10, -10, -10, -10, -8, -8, -8, -8, -8,
                -8, -6, -6, -6, -6, -6, -6, -4, -4, -4, -2, -2, -2, -1, -1, -1, -1,
                -1, -1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 2, 2, 2, 4, 4, 4, 6, 6,
                6, 6, 6, 6, 8, 8, 8, 8, 8, 8, 10, 10, 10, 10};
            */
            int[] posParabola = {-19,-19,-19,-19,-17,-17,-17,-17,-15,-15,-15,-15,
                -13,-13,-13,-13,-11,-11,-11,-11,-11,-9,-9,-9,-9,-9,-7,-7,-7,-7,-5,
                -5,-5,-5,-5,-3,-3,-3,-3,0,0,0,0,0,3,3,3,3,5,5,5,5,5,7,7,7,7,9,9,
                9,9,9,11,11,11,11,11,13,13,13,13,15,15,15,15,17,17,17, 17,19,19, 
                19,19};

            if (!caida)
            {
                balon.X += balon.VelocX;
                if (balon.X > fondo.Ancho - balon.Ancho - marco
                || balon.X < marco)
                {
                    balon.VelocX = -balon.VelocX;
                }
               
                balon.Y += posParabola[posParabolaActual];

                if (posParabolaActual < posParabola.Length - 1)
                    posParabolaActual++;
                else
                    posParabolaActual = 0;
            }


            if (disparo.Activo)
            {
                posDisparo.Add(new Sprite(0, (int)-disparo.VelocY, "disparo", Content));
                posDisparo[0] = new Sprite((int)posDisparo[0].X, 
                    (int)posDisparo[0].Y - (int)disparo.VelocY, 
                    "disparo", Content);


                for (int i = posDisparo.Count - 1; i > 0; i--)
                {
                    posDisparo[i] = posDisparo[i - 1];
                }
                
                if (posDisparo[0].Y < 0)
                {
                    disparo.Activo = false;
                    posDisparo.Clear();
                }
            }
        }

        private void MoverPersonaje(ContentManager Content)
        {
            var estadoTeclado = Keyboard.GetState();

            if (estadoTeclado.IsKeyDown(Keys.Left)
                && personaje.X > marco)
            {
                personaje.X -= 4;
            }

            if (estadoTeclado.IsKeyDown(Keys.Right)
                && personaje.X < fondo.Ancho - personaje.Ancho - marco)
            {
                personaje.X += 4;
            }

            if (!disparo.Activo && estadoTeclado.IsKeyDown(Keys.Space))
            {
                posDisparo.Add(new Sprite((int)personaje.X + 18, (int)personaje.Y - 18, "disparo", Content));
                disparo.Activo = true;
            }

            if (estadoTeclado.IsKeyDown(Keys.S))
            {
                gestor.modoActual = GestorDePantallas.MODO.CREDITOS;
            }
        }

        private void ComprobarColisiones(ContentManager Content)
        {

            int i = 0;
            while (i < posDisparo.Count && disparo.Activo)
            {
                Sprite rDisparo = new Sprite(
                    (int)posDisparo[i].X, (int)posDisparo[i].Y, 
                    "disparo", Content);

                if (balon.ColisionaCon(rDisparo))
                {
                    disparo.Activo = false;
                    posDisparo.Clear();
                }
                i++;
            }
            
            if (balon.ColisionaCon(personaje))
            {
                vidas--;
                ResetearPosicion();
            }

            if (vidas == 0)
            {
                vidas = 3;
                gestor.modoActual = GestorDePantallas.MODO.CREDITOS;
            }      
        }

        private void ResetearPosicion()
        {
            personaje.X = 549;
            personaje.Y = 538;
            balon.X = 349;
            balon.Y = 40;
            caida = true;
            posParabolaActual = 0;
            disparo.Activo = false;
            posDisparo.Clear();
        }

        public void Dibujar(GameTime gameTime, SpriteBatch spriteBatch)
        {
            fondo.Dibujar(spriteBatch);
            personaje.Dibujar(spriteBatch);
            balon.Dibujar(spriteBatch);

            foreach (Sprite pos in posDisparo)
            {
                pos.Dibujar(spriteBatch);
            }  


            spriteBatch.DrawString(fuente, "VIDAS: " + vidas,
                new Vector2(950, 26),
                Color.Green);
        }
    }
}
