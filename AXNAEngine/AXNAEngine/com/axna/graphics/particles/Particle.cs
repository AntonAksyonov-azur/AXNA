using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AXNAEngine.com.axna.graphics.particles
{
    public class Particle
    {
        /// <summary>
        /// Положение частицы
        /// </summary>
        public Vector2 Position { get; set; }
        
        /// <summary>
        /// Направление движения
        /// </summary>
        public Vector2 Direction { get; set; }
        
        /// <summary>
        /// Длительность существования частицы (в ms)
        /// </summary>
        public int LifeTime { get; set; }
 
        /// <summary>
        /// Графическое представление частицы
        /// </summary>
        public Texture2D Texture { get; set; }

        /// <summary>
        /// Цветовое перекрытие частицы
        /// </summary>
        public Color Color { get; set; }

        private readonly Random _random;

        public Particle(Texture2D texture, Vector2 position, Vector2 direction, int lifeTime, Color color)
        {
            Texture = texture;
            Position = position;
            Direction = direction;
            LifeTime = lifeTime;
            Color = color;
            _random = new Random();
        }

        public void UpdateParticle(GameTime gameTime)
        {
            LifeTime -= gameTime.ElapsedGameTime.Milliseconds;
            Position += Direction;
            //Color = new Color(1f, (float)_random.NextDouble(), 0f);
        }

        public void DrawParticle(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}
