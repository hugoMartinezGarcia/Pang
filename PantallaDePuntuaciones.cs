using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;

namespace Pang
{
    class PantallaDePuntuaciones
    {
        private SpriteFont fuente;
        private int incremento;
        private List<Puntuacion> puntuaciones;
        private GestorDePantallas gestor;
        private int puntosFinales;
        private string datosIntroducidos = "";
        private bool teclaBorrar;
        private Puntuacion puntuacionPartida;
        private string nombreJugador;
        private bool nombreIntroducido;
        private bool mostrarPuntuaciones;
        private int fotogramasRestantes;
        private ListaPuntuaciones listaFichero;

        public PantallaDePuntuaciones(GestorDePantallas gestor)
        {
            this.gestor = gestor;
            incremento = 0;
            listaFichero = new ListaPuntuaciones();
            puntuaciones = listaFichero.CargarPuntuaciones();
            nombreJugador = "";
            nombreIntroducido = false;
            mostrarPuntuaciones = false;
        }

        public void CargarContenidos(ContentManager Content)
        {
            fuente = Content.Load<SpriteFont>("Arial");
        }

        public void Actualizar(GameTime gameTime)
        {
            puntosFinales = gestor.Puntos;
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && !nombreIntroducido)
            {
                if (puntosFinales >= puntuaciones[puntuaciones.Count - 1].Puntos)
                {
                    puntuacionPartida = new Puntuacion(nombreJugador, puntosFinales);
                    puntuaciones.RemoveAt(puntuaciones.Count - 1);
                    puntuaciones.Add(puntuacionPartida);
                    puntuaciones.Sort();
                    listaFichero.GuardarPuntuaciones(puntuaciones);
                    nombreIntroducido = true;
                }
            }

            ComprobarPuntuacion();

            if ((nombreIntroducido || 
                puntosFinales < puntuaciones[puntuaciones.Count - 1].Puntos) && 
                !mostrarPuntuaciones)
            {
                mostrarPuntuaciones = true;
                fotogramasRestantes = 600;
            }

            if (mostrarPuntuaciones)
            {
                fotogramasRestantes--;
                if (fotogramasRestantes <= 0)
                {
                    mostrarPuntuaciones = false;
                    gestor.modoActual = GestorDePantallas.MODO.CREDITOS;
                }
            } 
        }

        public void Dibujar(GameTime gameTime, SpriteBatch spriteBatch)
        {

            if (!nombreIntroducido && 
                puntosFinales >= puntuaciones[puntuaciones.Count - 1].Puntos)
            {
                spriteBatch.DrawString(fuente, "INTRODUCE TUS INICIALES:",
                    new Vector2(450, 200),
                    Color.White);

                spriteBatch.DrawString(fuente, nombreJugador,
                    new Vector2(520, 300),
                    Color.White);
            }

            if (mostrarPuntuaciones)
            {
                spriteBatch.DrawString(fuente, "MEJORES PUNTUACIONES",
                    new Vector2(450, 100),
                    Color.White);

                if (puntuaciones.Count > 0)
                {
                    incremento = 0;
                    foreach (Puntuacion p in puntuaciones)
                    {
                        spriteBatch.DrawString(fuente, p.Nombre,
                        new Vector2(500, 150 + incremento),
                        Color.White);

                        spriteBatch.DrawString(fuente, p.Puntos.ToString(),
                        new Vector2(600, 150 + incremento),
                        Color.White);
                        incremento += 40;
                    }
                }

                fotogramasRestantes--;
            }
        }
        
        private void ComprobarPuntuacion()
        {
            KeyboardState estadoTeclado = Keyboard.GetState();
            Keys[] teclasPulsadas = estadoTeclado.GetPressedKeys();

            if (teclasPulsadas.Length > 0)
            {
                // Solo se guardan las letras de la A a la Z
                if ((int)teclasPulsadas[0] >= 65 && (int)teclasPulsadas[0] <= 90)
                    datosIntroducidos += teclasPulsadas[0].ToString();

                // 8 es la tecla de borrar
                if ((int)teclasPulsadas[0] == 8)
                    teclaBorrar = true;
            }

            // Si no hay ninguna tecla pulsada y se ha pulsado la tecla de borrar
            if (teclasPulsadas.Length == 0 && teclaBorrar)
            {
                if (nombreJugador.Length > 0)
                {
                    int longitudNombre = nombreJugador.Length;
                    nombreJugador = nombreJugador.Substring(
                        0, longitudNombre - 1);
                }

                teclaBorrar = false;
            }

            // Si no hay ninguna tecla pulsada, pero sí se ha pulsado anteriormente
            if (teclasPulsadas.Length == 0 && datosIntroducidos.Length > 0
                && nombreJugador.Length < 3)
            {
                nombreJugador += datosIntroducidos[0];
                datosIntroducidos = "";
            }    
        }
    }
}
