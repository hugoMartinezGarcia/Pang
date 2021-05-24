using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Pang
{
    public class PantallaDeJuego
    {
        private Marcador marcador;
        private Nivel nivel;
        private Sprite fondo;
        private Personaje personaje;
        private Bola bola;
        private Disparo disparo;
        private int marco;
        private GestorDePantallas gestor;
        public bool Terminado { get; set; }
        private SpriteFont fuente;

        private Song musicaDeFondo;
        private SoundEffect sonidoDeDisparo;

        public PantallaDeJuego(GestorDePantallas gestor)
        {
            this.gestor = gestor;
            Terminado = false;
            marco = 24;
        }

        public void CargarContenidos(ContentManager Content)
        {
            personaje = new Personaje(549, 538, Content);
            fondo = new Sprite(0, 0, "fondoNivel1", Content);
            bola = new Bola(349, 40, Content);
            fuente = Content.Load<SpriteFont>("Arial");
            disparo = new Disparo(0, 0, Content);
            marcador = new Marcador(Content);
            nivel = new Nivel(Content);
            sonidoDeDisparo = Content.Load<SoundEffect>("sonidoDisparo");
            musicaDeFondo = Content.Load<Song>("musicaJuego");
            MediaPlayer.Play(musicaDeFondo);
            MediaPlayer.IsRepeating = true;

            Reiniciar(Content);
        }

        public void Actualizar(GameTime gameTime, ContentManager Content)
        {
            MoverElementos(gameTime, Content);
            ComprobarEntrada(Content, gameTime);
            ComprobarColisiones(Content);
        }

        private void MoverElementos(GameTime gameTime, ContentManager Content)
        {        

            if (bola.Caida && bola.Y + bola.Alto < fondo.Alto - marco)
                bola.Y += 10;
            else
                bola.Caida = false;

            if (!bola.Caida)
            {
                bola.X += bola.VelocX;
                if (bola.X > fondo.Ancho - bola.Ancho - marco
                || bola.X < marco)
                {
                    bola.VelocX = -bola.VelocX;
                }

                bola.MoverY();
            }


            disparo.Mover(gameTime, Content);
            
            nivel.Animar(gameTime);
            marcador.SetSegundosRestantes(nivel.SegundosRestantes);
            if (nivel.TiempoTerminado)
            {
                PerderVida();
            }
                
        }

        public void ComprobarEntrada(ContentManager Content, GameTime gameTime)
        {
            var estadoTeclado = Keyboard.GetState();
            if (estadoTeclado.IsKeyDown(Keys.S))
            {
                Terminado = true;
                gestor.modoActual = GestorDePantallas.MODO.CREDITOS;
            }

            if (!estadoTeclado.IsKeyDown(Keys.Left) 
                    && !estadoTeclado.IsKeyDown(Keys.Right))
                personaje.PermanecerEstatico(gameTime);

            if (estadoTeclado.IsKeyDown(Keys.Left))
                personaje.MoverIzquierda(gameTime);

            

            if (estadoTeclado.IsKeyDown(Keys.Right))
                personaje.MoverDerecha(gameTime);


            if (!disparo.Activo && estadoTeclado.IsKeyDown(Keys.Space))
            {
                sonidoDeDisparo.Play();
                disparo.X = personaje.X + 18;
                disparo.Y = 585;
                disparo.PosDisparo.Add(new Disparo((int)disparo.X, (int)disparo.Y, Content));
                disparo.Activo = true;
            }
        }

        private void ComprobarColisiones(ContentManager Content)
        {
            int i = 0;
            while (i < disparo.PosDisparo.Count && disparo.Activo)
            {
                Sprite rDisparo = new Sprite(
                    (int)disparo.PosDisparo[i].X, (int)disparo.PosDisparo[i].Y,
                    "disparo", Content);

                if (bola.ColisionaCon(rDisparo))
                {
                    marcador.IncrementarPuntos(100);
                    disparo.Activo = false;
                    disparo.PosDisparo.Clear();
                    bola.MoverAPosicionInicial();
                }
                i++;
            }

            if (bola.ColisionaCon(personaje))
            {
                PerderVida();
            }           
        }

        private void PerderVida()
        {
            personaje.Vidas--;
            marcador.SetVidas(personaje.Vidas);
            nivel.Reiniciar();
            personaje.MoverAPosicionInicial();
            bola.MoverAPosicionInicial();
            bola.PosParabolaActual = 0;
            disparo.Activo = false;
            disparo.PosDisparo.Clear();

            if (personaje.Vidas <= 0)
            {
                Terminado = true;
            }
        }

        private void Reiniciar(ContentManager Content)
        {
            Terminado = false;
            nivel.Reiniciar();
            personaje.Vidas = 3;
            marcador.SetVidas(personaje.Vidas);
            marcador.ReiniciarPuntos();
            personaje.MoverAPosicionInicial();
            bola.MoverAPosicionInicial();
            disparo.Activo = false;
            disparo.PosDisparo.Clear();
        }

        public void Dibujar(GameTime gameTime, SpriteBatch spriteBatch)
        {
            
            fondo.Dibujar(spriteBatch);
            disparo.Dibujar(spriteBatch);
            personaje.Dibujar(spriteBatch);
 
            bola.Dibujar(spriteBatch);

            marcador.Dibujar(spriteBatch);
        }
    }
}
