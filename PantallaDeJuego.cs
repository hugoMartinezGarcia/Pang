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
        private Personaje personaje;
        private Disparo disparo;
        private GestorDePantallas gestor;
        public bool Terminado { get; set; }

        private Song musicaDeFondo;
        private SoundEffect sonidoDeDisparo;

        public PantallaDeJuego(GestorDePantallas gestor)
        {
            this.gestor = gestor;
            Terminado = false;
        }

        public void CargarContenidos(ContentManager Content)
        {
            personaje = new Personaje(549, 538, Content);
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

            if (nivel.bola.Caida && nivel.bola.Y + nivel.bola.Alto < nivel.Fondo.Alto - nivel.Marco)
                nivel.bola.Y += 10;
            else
                nivel.bola.Caida = false;

            if (!nivel.bola.Caida)
            {
                nivel.bola.X += nivel.bola.VelocX;
                if (nivel.bola.X > nivel.Fondo.Ancho - nivel.bola.Ancho - nivel.Marco
                || nivel.bola.X < nivel.Marco)
                {
                    nivel.bola.VelocX = -nivel.bola.VelocX;
                }

                nivel.bola.MoverY();
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
                gestor.modoActual = GestorDePantallas.MODO.BIENVENIDA;
                Reiniciar(Content);
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

                if (nivel.bola.ColisionaCon(rDisparo))
                {
                    marcador.IncrementarPuntos(100);
                    disparo.Activo = false;
                    disparo.PosDisparo.Clear();
                    nivel.bola.MoverAPosicionInicial();
                }
                i++;
            }

            if (nivel.bola.ColisionaCon(personaje))
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
            nivel.bola.MoverAPosicionInicial();
            nivel.bola.PosParabolaActual = 0;
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
            nivel.bola.MoverAPosicionInicial();
            disparo.Activo = false;
            disparo.PosDisparo.Clear();
        }

        public void Dibujar(GameTime gameTime, SpriteBatch spriteBatch)
        {
            nivel.Dibujar(spriteBatch);
            disparo.Dibujar(spriteBatch);
            personaje.Dibujar(spriteBatch);
            marcador.Dibujar(spriteBatch);
        }
    }
}
