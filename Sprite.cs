using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Pang
{
    class Sprite
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float VelocX { get; set; }
        public float VelocY { get; set; }
        public bool Activo { get; set; }
        public int Ancho { get; set; }
        public int Alto { get; set; }
        protected Texture2D imagen;
        protected int cantidadFotogramas;
        protected Texture2D[][] secuencia;
        protected int fotogramaActual;
        protected int tiempoEnCadaFotograma;
        protected int tiempoHastaSiguienteFotograma;
        bool haySecuencia;

        protected enum direcciones
        {
            ESTATICO, DERECHA, IZQUIERDA
        };

        int direccionActual = (int)direcciones.ESTATICO;

        public Sprite(int x, int y, string nombreImagen, ContentManager Content)
        {
            X = x;
            Y = y;
            imagen = Content.Load<Texture2D>(nombreImagen);
            Activo = true;
            Ancho = imagen.Width;
            Alto = imagen.Height;
            haySecuencia = false;
        }

        public Sprite(int x, int y, string[] imagenes, ContentManager Content)
        {
            X = x;
            Y = y;
            Activo = true;
            secuencia = new Texture2D[sizeof(direcciones)][];
            CargarSecuencia(0, imagenes, Content);
            imagen = secuencia[0][0];
            fotogramaActual = 0;
            tiempoEnCadaFotograma = 100;
            tiempoHastaSiguienteFotograma = tiempoEnCadaFotograma;

            Ancho = imagen.Width;
            Alto = imagen.Height;
        }



        public void SetVelocidad(float vx, float vy)
        {
            VelocX = vx;
            VelocY = vy;
        }

        public virtual void Dibujar(SpriteBatch spriteBatch)
        {
            if (Activo)
            {
                spriteBatch.Draw(imagen, new Rectangle(
                (int)X, (int)Y,
                imagen.Width, imagen.Height), Color.White);
            }
            
        }

        public bool ColisionaCon(Sprite otro)
        {
            if (!Activo) return false;
            if (!otro.Activo) return false;

            Rectangle r1 = new Rectangle((int)X, (int)Y, imagen.Width, imagen.Height);
            Rectangle r2 = new Rectangle((int)otro.X, (int)otro.Y, otro.imagen.Width, otro.imagen.Height);

            return r1.Intersects(r2);
        }

        public virtual void Animar(GameTime gameTime)
        {
            if (haySecuencia)
            {
                tiempoHastaSiguienteFotograma -= gameTime.ElapsedGameTime.Milliseconds;
                if(tiempoHastaSiguienteFotograma <= 0)
                {
                    fotogramaActual++;
                    
                    if (fotogramaActual >= cantidadFotogramas)
                        fotogramaActual = 0;

                    tiempoHastaSiguienteFotograma = tiempoEnCadaFotograma;
                    imagen = secuencia[direccionActual][fotogramaActual];
                }
            }
        }

        public void CargarSecuencia(byte direcc, string[] imagenes, ContentManager Content)
        {
            byte tamanyoSecuencia = (byte)imagenes.Length;
            secuencia[direcc] = new Texture2D[tamanyoSecuencia];
            for (int i = 0; i < imagenes.Length; i++)
            {
                secuencia[direcc][i] = Content.Load<Texture2D>(imagenes[i]);
            }
            haySecuencia = true;
            cantidadFotogramas = imagenes.Length;
            direccionActual = direcc;

        }

        public void CambiarDireccion(byte nuevaDir)
        {
            if (!haySecuencia)
                return;

            if (direccionActual != nuevaDir)
            {
                direccionActual = nuevaDir;
                fotogramaActual = 0;
                cantidadFotogramas = (byte)secuencia[direccionActual].Length;
                imagen = secuencia[direccionActual][0];
            }
        }
    }
}
